using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using DataLayer;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Globalization;


namespace Billing.Accountsbootstrap
{
    public partial class CustomerWiseSummaryProductionStatus_Jobworker : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();

            if (!IsPostBack)
            {
                DataSet dsset = objBs.Bind_Customer();
                if (dsset.Tables[0].Rows.Count > 0)
                {
                    ddlBuyerCode.DataSource = dsset.Tables[0];
                    ddlBuyerCode.DataTextField = "LedgerName";
                    ddlBuyerCode.DataValueField = "LedgerID";
                    ddlBuyerCode.DataBind();
                    ddlBuyerCode.Items.Insert(0, "Select");

                }
            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            BindData();
        }


        DataSet dsss;
        DataRow drNew1;
        double iVAMark = 0;
        double iVAMarkTot = 0;
        private void BindData()
        {
            DataSet ds = objBs.gridProcess();
            DataSet dsss1;
            DataTable dt1;
            DataColumn dc1;

            dsss1 = new DataSet();
            dt1 = new DataTable();

            dc1 = new DataColumn("CustomerName");
            dt1.Columns.Add(dc1);

            dc1 = new DataColumn("Work Order No");
            dt1.Columns.Add(dc1);

            dc1 = new DataColumn("Work Order Date");
            dt1.Columns.Add(dc1);

            dc1 = new DataColumn("Expected Delivery Date");
            dt1.Columns.Add(dc1);

            dc1 = new DataColumn("Style");
            dt1.Columns.Add(dc1);

            dc1 = new DataColumn("Color");
            dt1.Columns.Add(dc1);

            dc1 = new DataColumn("Work Order Qty");
            dt1.Columns.Add(dc1);

            //dc1 = new DataColumn("Fabric");
            //dt1.Columns.Add(dc1);

            //dc1 = new DataColumn("Trims");
            //dt1.Columns.Add(dc1);

            dc1 = new DataColumn("Cutting Completed");
            dt1.Columns.Add(dc1);

            DataSet dsBuyerOrder11 = objBs.Bind_EXENo(Convert.ToInt32(ddlBuyerCode.SelectedValue));
            if (dsBuyerOrder11.Tables[0].Rows.Count > 0)
            {
                for (int gv1 = 0; gv1 < dsBuyerOrder11.Tables[0].Rows.Count; gv1++)
                {
                    DataSet dsCuttingJobWorker = objBs.Bind_Cutting_Jobworker(Convert.ToInt32(dsBuyerOrder11.Tables[0].Rows[gv1]["BuyerOrderId"]));
                    if (dsCuttingJobWorker.Tables[0].Rows.Count > 0)
                    {
                        for (int gv = 0; gv < dsCuttingJobWorker.Tables[0].Rows.Count; gv++)
                        {
                            DataColumnCollection columns = dt1.Columns;
                            string columnName = dsCuttingJobWorker.Tables[0].Rows[gv]["LedgerName"].ToString() + " - Cutting Completed";
                            if (columns.Contains(columnName))
                            {

                            }
                            else
                            {
                                dc1 = new DataColumn(dsCuttingJobWorker.Tables[0].Rows[gv]["LedgerName"].ToString() + " - Cutting Completed");
                                dt1.Columns.Add(dc1);
                            }
                        }
                    }
                }
            }

            for (int g = 0; g < ds.Tables[0].Rows.Count; g++)
            {
                dc1 = new DataColumn(ds.Tables[0].Rows[g]["Process"].ToString());
                dt1.Columns.Add(dc1);

                DataSet dsProcessJobWorker = objBs.Bind_Process_Jobworker(Convert.ToInt32(ds.Tables[0].Rows[g]["ProcessID"]));
                if (dsProcessJobWorker.Tables[0].Rows.Count > 0)
                {
                    for (int gv = 0; gv < dsProcessJobWorker.Tables[0].Rows.Count; gv++)
                    {
                        dc1 = new DataColumn(dsProcessJobWorker.Tables[0].Rows[gv]["LedgerName"].ToString() + " - " + ds.Tables[0].Rows[g]["Process"].ToString());
                        dt1.Columns.Add(dc1);
                    }
                }
            }

            dsss1.Tables.Add(dt1);

            //DateTime From = DateTime.ParseExact(DateTime.Now.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataSet dsBuyerOrder1 = objBs.Bind_EXENo(Convert.ToInt32(ddlBuyerCode.SelectedValue));
            if (dsBuyerOrder1.Tables[0].Rows.Count > 0)
            {
                for (int gv = 0; gv < dsBuyerOrder1.Tables[0].Rows.Count; gv++)
                {

                    DataSet dsBuyerOrder = objBs.GetBuyerOrderData_OrderDate_BO(Convert.ToInt32(dsBuyerOrder1.Tables[0].Rows[gv]["BuyerOrderId"].ToString()));

                    for (int g = 0; g < dsBuyerOrder.Tables[0].Rows.Count; g++)
                    {
                        drNew1 = dt1.NewRow();

                        drNew1["CustomerName"] = dsBuyerOrder.Tables[0].Rows[g]["LedgerName"].ToString();
                        drNew1["Work Order No"] = dsBuyerOrder.Tables[0].Rows[g]["ExcNo"].ToString();
                        drNew1["Work Order Date"] = Convert.ToDateTime(dsBuyerOrder.Tables[0].Rows[g]["OrderDate"]).ToString("dd/MM/yyyy");
                        drNew1["Expected Delivery Date"] = Convert.ToDateTime(dsBuyerOrder.Tables[0].Rows[g]["DeliveryDate"]).ToString("dd/MM/yyyy");
                        DataSet dsBuyerOrderStyleColor = objBs.GetBuyerOrderData_Style_Color(Convert.ToInt32(dsBuyerOrder.Tables[0].Rows[g]["BuyerOrderId"]));
                        if (dsBuyerOrderStyleColor.Tables[0].Rows.Count > 0)
                        {
                            drNew1["Style"] = dsBuyerOrderStyleColor.Tables[0].Rows[0]["styleno"].ToString();
                            drNew1["Color"] = dsBuyerOrderStyleColor.Tables[0].Rows[0]["color"].ToString();
                            drNew1["Work Order Qty"] = dsBuyerOrderStyleColor.Tables[0].Rows[0]["Qty"].ToString();
                        }
                        else
                        {
                            drNew1["Style"] = "";
                            drNew1["Color"] = "";
                            drNew1["Work Order Qty"] = "";
                        }

                        //DataSet dsBuyerOrderFabric = objBs.GetBuyerOrderData_Fabric_MaterialIssue(dsBuyerOrder.Tables[0].Rows[g]["ExcNo"].ToString());
                        //if (dsBuyerOrderFabric.Tables[0].Rows.Count > 0)
                        //{
                        //    drNew1["Fabric"] = "Yes";
                        //}
                        //else
                        //{
                        //    drNew1["Fabric"] = "No";
                        //}
                        //drNew1["Trims"] = "Yes";

                        DataSet dsBuyerOrder111 = objBs.Bind_EXENo(Convert.ToInt32(ddlBuyerCode.SelectedValue));
                        if (dsBuyerOrder111.Tables[0].Rows.Count > 0)
                        {
                            for (int gv11 = 0; gv11 < dsBuyerOrder111.Tables[0].Rows.Count; gv11++)
                            {
                                DataSet dsCuttingJobWorker1 = objBs.Bind_Cutting_Jobworker(Convert.ToInt32(dsBuyerOrder111.Tables[0].Rows[gv11]["BuyerOrderId"]));
                                if (dsCuttingJobWorker1.Tables[0].Rows.Count > 0)
                                {
                                    for (int gv1 = 0; gv1 < dsCuttingJobWorker1.Tables[0].Rows.Count; gv1++)
                                    {
                                        DataSet dsMasterQty = objBs.GetBuyerOrderCuttingData_MasterCutting_Jobworker(Convert.ToInt32(dsBuyerOrder.Tables[0].Rows[g]["BuyerOrderId"]), Convert.ToInt32(dsCuttingJobWorker1.Tables[0].Rows[gv1]["CuttingLedger"]));
                                        if (dsMasterQty.Tables[0].Rows.Count > 0)
                                        {
                                            #region

                                            DataSet dsBOCQty = objBs.GetBuyerOrderCuttingQty_MasterCutting(Convert.ToInt32(dsBuyerOrder.Tables[0].Rows[g]["BuyerOrderId"]));

                                            foreach (DataRow dr in dsMasterQty.Tables[0].Rows)
                                            {
                                                DataRow[] RowsBOQty = dsBOCQty.Tables[0].Select("BuyerOrderId='" + dr["BuyerOrderId"] + "' ");
                                                if ((Convert.ToInt32(RowsBOQty[0]["CQty"]) - (Convert.ToInt32(dr["RecQty"]) + Convert.ToInt32(dr["DmgQty"]))) > 0)
                                                {
                                                   // drNew1["Cutting Completed"] = RowsBOQty[0]["CQty"];
                                                    drNew1[dsCuttingJobWorker1.Tables[0].Rows[gv1]["LedgerName"].ToString() + " - Cutting Completed"] = RowsBOQty[0]["CQty"];
                                                    dc1 = new DataColumn();
                                                    //drNew1["Pending Cutting"] = (Convert.ToInt32(RowsBOQty[0]["CQty"]) - (Convert.ToInt32(dr["RecQty"]) + Convert.ToInt32(dr["DmgQty"])));
                                                    //drNew1["Ready For"] = dr["RecQty"];
                                                }
                                                else
                                                {
                                                  //  drNew1["Cutting Completed"] = "0";
                                                    drNew1[dsCuttingJobWorker1.Tables[0].Rows[gv1]["LedgerName"].ToString() + " - Cutting Completed"] = "0";
                                                    //drNew1["Pending Cutting"] = "0";
                                                    //drNew1["Ready For"] = "0";
                                                }
                                            }
                                            #endregion
                                        }
                                    }
                                }
                            }
                        }

                        foreach (DataRow drt in ds.Tables[0].Rows)
                        {
                            string item1 = Convert.ToString(drt["ProcessID"]);

                            DataSet dsProcessJobWorker = objBs.Bind_Process_Jobworker(Convert.ToInt32(item1));
                            if (dsProcessJobWorker.Tables[0].Rows.Count > 0)
                            {
                                for (int gv2 = 0; gv2 < dsProcessJobWorker.Tables[0].Rows.Count; gv2++)
                                {
                                    DataSet dsCPQty = objBs.GetCuttingProcessQty_BuyerOrderId_Jobworker(Convert.ToInt32(dsBuyerOrder.Tables[0].Rows[g]["BuyerOrderId"]), Convert.ToInt32(item1), Convert.ToInt32(dsProcessJobWorker.Tables[0].Rows[gv2]["ProcessLedger"]));
                                    if (dsCPQty.Tables[0].Rows.Count > 0)
                                    {
                                        //drNew1[Convert.ToString(drt["Process"])] = dsCPQty.Tables[0].Rows[0]["TotalReceived"].ToString();
                                        drNew1[dsProcessJobWorker.Tables[0].Rows[gv2]["LedgerName"].ToString() + " - " + drt["Process"]] = dsCPQty.Tables[0].Rows[0]["TotalReceived"].ToString();
                                    }
                                    else
                                    {
                                        //drNew1[Convert.ToString(drt["Process"])] = "0";
                                        drNew1[dsProcessJobWorker.Tables[0].Rows[gv2]["LedgerName"].ToString() + " - " + drt["Process"]] = "0";
                                    }
                                }
                            }
                        }

                        //drNew1["Despatch Qty"] = "0";
                        //drNew1["DC/Inv No"] = "0";
                        dsss1.Tables[0].Rows.Add(drNew1);

                    }
                }
            }

            DataTable firstTable = dsss1.Tables[0];
            DataTable firstTable1 = FlipDataTable(firstTable);

            gridview.DataSource = firstTable1;
            gridview.DataBind();
        }


        public static DataTable FlipDataTable(DataTable dt)
        {

            DataTable table = new DataTable();

            //Get all the rows and change into columns

            for (int i = 0; i <= dt.Rows.Count; i++)
            {

                table.Columns.Add(Convert.ToString(i));

            }

            DataRow dr;

            //get all the columns and make it as rows

            for (int j = 0; j < dt.Columns.Count; j++)
            {

                dr = table.NewRow();

                dr[0] = dt.Columns[j].ToString();

                for (int k = 1; k <= dt.Rows.Count; k++)

                    dr[k] = dt.Rows[k - 1][j];

                table.Rows.Add(dr);

            }



            return table;

        }
    }
}


