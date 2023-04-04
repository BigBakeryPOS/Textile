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
    public partial class ContractorWiseProductionSummary : System.Web.UI.Page
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
                DataSet dsset = objBs.GetApprovedProcessLedger();
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

            dc1 = new DataColumn("DCNo");
            dt1.Columns.Add(dc1);

            dc1 = new DataColumn("DCDate");
            dt1.Columns.Add(dc1);

            dc1 = new DataColumn("Style");
            dt1.Columns.Add(dc1);
                      

            dc1 = new DataColumn("Qty");
            dt1.Columns.Add(dc1);

            dsss1.Tables.Add(dt1);

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
                }
                else
                {
                    drNew1["Style"] = "";
                }

                DataSet dsCPQty = objBs.GetCuttingProcessQty_Full(Convert.ToInt32(dsBuyerOrder.Tables[0].Rows[g]["BuyerOrderId"]));
                if (dsCPQty.Tables[0].Rows.Count > 0)
                {
                    drNew1["Qty"] = dsCPQty.Tables[0].Rows[0]["TotalReceived"].ToString();
                }
                else
                {
                    drNew1["Qty"] = "0";
                }

                DataSet dsBuyerOrderStyleColor1 = objBs.GetBuyerOrderData_Style_Color(Convert.ToInt32(dsBuyerOrder.Tables[0].Rows[g]["BuyerOrderId"]));
                if (dsBuyerOrderStyleColor1.Tables[0].Rows.Count > 0)
                {
                    DataSet dsBuyerOrderStyleColor12 = objBs.GetBuyerOrderData_Style_Invoice(Convert.ToInt32(dsBuyerOrder.Tables[0].Rows[g]["BuyerOrderId"]), dsBuyerOrderStyleColor1.Tables[0].Rows[0]["stylenoid"].ToString());
                    if (dsBuyerOrderStyleColor12.Tables[0].Rows.Count > 0)
                    {
                        string invno = "";
                        string invdate = "";
                        for (int g1 = 0; g1 < dsBuyerOrderStyleColor12.Tables[0].Rows.Count; g1++)
                        {
                            invno = invno + "," + dsBuyerOrderStyleColor12.Tables[0].Rows[g1]["FullInvoiceno"].ToString();
                            invdate = invdate + "," + Convert.ToDateTime(dsBuyerOrderStyleColor12.Tables[0].Rows[g1]["InvoiceDate"]).ToString("dd/MM/yyyy");
                        }
                        drNew1["DCNo"] = invno;
                        drNew1["DCDate"] = invdate;
                    }
                    else
                    {
                        drNew1["DCNo"] = "0";
                        drNew1["DCDate"] = "";
                    }
                }
                else
                {
                    drNew1["DCNo"] = "0";
                    drNew1["DCDate"] = "";
                }


                dsss1.Tables[0].Rows.Add(drNew1);
            }            

            gridview.DataSource = dsss1;
            gridview.DataBind();
        }
    }
}


