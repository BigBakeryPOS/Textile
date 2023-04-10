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
    public partial class ProcessWiseContractorWiseBreakup : System.Web.UI.Page
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
                DataSet dsProcessLedger = objBs.GetApprovedProcessLedger();
                if (dsProcessLedger.Tables[0].Rows.Count > 0)
                {
                    ddlBuyerCode.DataSource = dsProcessLedger.Tables[0];
                    ddlBuyerCode.DataTextField = "LedgerName";
                    ddlBuyerCode.DataValueField = "LedgerID";
                    ddlBuyerCode.DataBind();
                    ddlBuyerCode.Items.Insert(0, "Select Jobwork Ledger");
                }
                else
                {
                    ddlBuyerCode.Items.Insert(0, "Select Jobwork Ledger");
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

            dc1 = new DataColumn("Fabric");
            dt1.Columns.Add(dc1);

            dc1 = new DataColumn("Trims");
            dt1.Columns.Add(dc1);

            dc1 = new DataColumn("Cutting Completed");
            dt1.Columns.Add(dc1);


            for (int g = 0; g < ds.Tables[0].Rows.Count; g++)
            {
                dc1 = new DataColumn(ds.Tables[0].Rows[g]["Process"].ToString());
                dt1.Columns.Add(dc1);
            }

            dsss1.Tables.Add(dt1);

            DateTime From = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataSet dsBuyerOrder = objBs.GetBuyerOrderData_ProcessLedger(Convert.ToInt32(ddlBuyerCode.SelectedValue));

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

                DataSet dsBuyerOrderFabric = objBs.GetBuyerOrderData_Fabric_MaterialIssue(dsBuyerOrder.Tables[0].Rows[g]["ExcNo"].ToString());
                if (dsBuyerOrderFabric.Tables[0].Rows.Count > 0)
                {
                    drNew1["Fabric"] = "Yes";
                }
                else
                {
                    drNew1["Fabric"] = "No";
                }
                drNew1["Trims"] = "Yes";

                DataSet dsMasterQty = objBs.GetBuyerOrderCuttingData_MasterCutting(Convert.ToInt32(dsBuyerOrder.Tables[0].Rows[g]["BuyerOrderId"]));
                if (dsMasterQty.Tables[0].Rows.Count > 0)
                {
                    #region

                    DataSet dsBOCQty = objBs.GetBuyerOrderCuttingQty_MasterCutting(Convert.ToInt32(dsBuyerOrder.Tables[0].Rows[g]["BuyerOrderId"]));

                    foreach (DataRow dr in dsMasterQty.Tables[0].Rows)
                    {
                        DataRow[] RowsBOQty = dsBOCQty.Tables[0].Select("BuyerOrderId='" + dr["BuyerOrderId"] + "' ");
                        if ((Convert.ToInt32(RowsBOQty[0]["CQty"]) - (Convert.ToInt32(dr["RecQty"]) + Convert.ToInt32(dr["DmgQty"]))) > 0)
                        {
                            drNew1["Cutting Completed"] = RowsBOQty[0]["CQty"];                          
                        }
                        else
                        {
                            drNew1["Cutting Completed"] = "0";                           
                        }
                    }
                    #endregion
                }

             
                foreach (DataRow drt in ds.Tables[0].Rows)
                {
                    string item1 = Convert.ToString(drt["ProcessID"]);
                    DataSet dsCPQty = objBs.GetCuttingProcessQty_BuyerOrderId_ProcessLedger(Convert.ToInt32(dsBuyerOrder.Tables[0].Rows[g]["BuyerOrderId"]), Convert.ToInt32(item1), Convert.ToInt32(ddlBuyerCode.SelectedValue));
                     if (dsCPQty.Tables[0].Rows.Count > 0)
                     {
                         drNew1[Convert.ToString(drt["Process"])] = dsCPQty.Tables[0].Rows[0]["TotalReceived"].ToString();
                     }
                     else
                     {
                         drNew1[Convert.ToString(drt["Process"])] = "0";
                     }                   
                }

                dsss1.Tables[0].Rows.Add(drNew1);
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


