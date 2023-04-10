using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.IO;

namespace Billing.Accountsbootstrap
{
    public partial class PurchaseGRN : System.Web.UI.Page
    {

        BSClass objBs = new BSClass();
        string sTableName = "";
        string YearCode = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();

            //YearCode = Session["YearCode"].ToString();
            YearCode = Request.Cookies["userInfo"]["YearCode"].ToString();
            if (!IsPostBack)
            {
                txtRecDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                txtOrderDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDeliveryFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDeliveryTo.Text = DateTime.Now.ToString("dd/MM/yyyy");

                DataSet dsPONo = objBs.GetPurchaseRecPONo(YearCode);
                string PONo = dsPONo.Tables[0].Rows[0]["RecPONo"].ToString().PadLeft(4, '0');
                txtRecNo.Text = PONo + " / " + YearCode;

                DataSet dsCompany = objBs.GetCompanyDetails();
                if (dsCompany.Tables[0].Rows.Count > 0)
                {
                    ddlCompany.DataSource = dsCompany.Tables[0];
                    ddlCompany.DataTextField = "CompanyName";
                    ddlCompany.DataValueField = "ComapanyID";
                    ddlCompany.DataBind();
                    ddlCompany.Items.Insert(0, "CompanyName");
                }

             

                DataSet dsProcessOn = objBs.GetCategory_as_Process("ShowPOrder");
                if (dsProcessOn.Tables[0].Rows.Count > 0)
                {
                    ddlProcessOn.DataSource = dsProcessOn.Tables[0];
                    ddlProcessOn.DataTextField = "category";
                    ddlProcessOn.DataValueField = "categoryid";
                    ddlProcessOn.DataBind();
                    ddlProcessOn.Items.Insert(0, "ProcessOn");
                }


                DataSet dsset = objBs.getLedger_New(lblContactTypeId.Text);
                if (dsset.Tables[0].Rows.Count > 0)
                {
                    ddlPartyCode.DataSource = dsset.Tables[0];
                    ddlPartyCode.DataTextField = "CompanyCode";
                    ddlPartyCode.DataValueField = "LedgerID";
                    ddlPartyCode.DataBind();
                    ddlPartyCode.Items.Insert(0, "PartyCode");

                    ddlPartyName.DataSource = dsset.Tables[0];
                    ddlPartyName.DataTextField = "LedgerName";
                    ddlPartyName.DataValueField = "LedgerID";
                    ddlPartyName.DataBind();
                    ddlPartyName.Items.Insert(0, "PartyName");
                }


                string POGRNId = Request.QueryString.Get("POGRNId");
                if (POGRNId != "" && POGRNId != null)
                {
                    DataSet dsRecPO = objBs.getPurchaseGRN(Convert.ToInt32(POGRNId));
                    if (dsRecPO.Tables[0].Rows.Count > 0)
                    {
                        #region

                       

                        ddlPoNo.SelectedValue = dsRecPO.Tables[0].Rows[0]["POId"].ToString();
                        ddlPoNo.Enabled = false;
                        ddlProcessOn.SelectedValue = dsRecPO.Tables[0].Rows[0]["ProcessOn"].ToString();

                        txtRecDate.Text = Convert.ToDateTime(dsRecPO.Tables[0].Rows[0]["Date"]).ToString("dd/MM/yyyy");

                        DataSet dsPO = objBs.getPurchaseOrder(Convert.ToInt32(dsRecPO.Tables[0].Rows[0]["POId"].ToString()));
                        if (dsPO.Tables[0].Rows.Count > 0)
                        {
                            txtOrderDate.Text = Convert.ToDateTime(dsPO.Tables[0].Rows[0]["OrderDate"]).ToString("dd/MM/yyyy");
                            txtDeliveryFrom.Text = Convert.ToDateTime(dsPO.Tables[0].Rows[0]["FromDate"]).ToString("dd/MM/yyyy");
                            txtDeliveryTo.Text = Convert.ToDateTime(dsPO.Tables[0].Rows[0]["ToDate"]).ToString("dd/MM/yyyy");
                        }

                        txtDeliveryPlace.Text = dsRecPO.Tables[0].Rows[0]["DeliveryPlace"].ToString();
                        txtRecNo.Text = dsRecPO.Tables[0].Rows[0]["FullRecPONo"].ToString();
                        ddlCompany.SelectedValue = dsRecPO.Tables[0].Rows[0]["companyid"].ToString();

                        ddlPartyCode.SelectedValue = dsRecPO.Tables[0].Rows[0]["PartyCode"].ToString();
                        ddlPartyName.SelectedValue = dsRecPO.Tables[0].Rows[0]["PartyCode"].ToString();
                        ddlPartyCode_OnSelectedIndexChanged(sender, e);

                        DataSet dsItemProcessPo = objBs.GetPo(ddlPartyName.SelectedValue, "U");
                        if (dsItemProcessPo.Tables[0].Rows.Count > 0)
                        {
                            ddlPoNo.DataSource = dsItemProcessPo.Tables[0];
                            ddlPoNo.DataTextField = "FullPONo";
                            ddlPoNo.DataValueField = "PoId";
                            ddlPoNo.DataBind();
                            ddlPoNo.Items.Insert(0, "PONo");
                        }
                        else
                        {
                            ddlPoNo.Items.Insert(0, "PONo");
                        }

                        txtChallanNo.Text = dsRecPO.Tables[0].Rows[0]["ChallanNo"].ToString();

                        txtTotalAmount.Text = Convert.ToDouble(dsRecPO.Tables[0].Rows[0]["TotalAmount"]).ToString("f2");

                        btnSave.Text = "Update";
                        btnSave.Enabled = true;

                        #endregion
                    }

                    DataSet ds2 = objBs.getTransPurchaseGRN(Convert.ToInt32(POGRNId));
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        #region

                        DataSet dstd = new DataSet();
                        DataTable dtddd = new DataTable();
                        DataRow drNew;
                        DataColumn dct;
                        DataTable dttt = new DataTable();

                        dct = new DataColumn("TransId");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("PurchaseFor");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("PurchaseForId");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("PurchaseForType");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("PurchaseForTypeId");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("Item");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("ItemId");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("Color");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("ColorId");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("Qty");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Shrink");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("TotalQty");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("RQty");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("BalQty");
                        dttt.Columns.Add(dct);


                        dct = new DataColumn("Rate");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Amount");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("RecQty");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Remarks");
                        dttt.Columns.Add(dct);


                        dstd.Tables.Add(dttt);

                        foreach (DataRow Dr in ds2.Tables[0].Rows)
                        {
                            drNew = dttt.NewRow();

                            drNew["TransId"] = Dr["POTransId"];

                            drNew["PurchaseFor"] = Dr["PurchaseFor"];
                            drNew["PurchaseForID"] = Dr["PurchaseforId"];
                            drNew["PurchaseForType"] = Dr["PurchaseforType"];
                            drNew["PurchaseForTypeId"] = Dr["PurchaseforTypeId"];

                            drNew["Item"] = Dr["Item"];
                            drNew["ItemId"] = Dr["ItemId"];

                            drNew["Color"] = Dr["Color"];
                            drNew["ColorId"] = Dr["ColorId"];

                            drNew["Qty"] = Dr["Qty"];
                            drNew["Shrink"] = Dr["Shrink"];
                            drNew["TotalQty"] = Dr["TotalQty"];

                            drNew["RQty"] = Dr["RecQty"];

                            drNew["BalQty"] = (Convert.ToDouble(Dr["TotalQty"]) - Convert.ToDouble(Dr["RecQty"])).ToString();

                            drNew["Rate"] = Dr["Rate"];
                            drNew["Amount"] = Dr["Amount"];

                            drNew["RecQty"] = Dr["RecQty"];

                            drNew["Remarks"] = Dr["Remarks"];


                            dstd.Tables[0].Rows.Add(drNew);
                            dtddd = dstd.Tables[0];
                        }

                        #endregion

                        ViewState["CurrentTable1"] = dtddd;
                        GVItem.DataSource = dtddd;
                        GVItem.DataBind();


                    }
                }
            }

        }

        protected void Party_Click_chnaged(object sender, EventArgs e)
        {
            ddlPoNo.ClearSelection();
            ddlPoNo.Items.Clear();

            if (ddlPartyName.SelectedValue == "PartyName")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Valid Party Name.')", true);
                return;
            }
            else
            {
                DataSet dsItemProcessPo = objBs.GetPo(ddlPartyName.SelectedValue,"S");
                if (dsItemProcessPo.Tables[0].Rows.Count > 0)
                {
                    ddlPoNo.DataSource = dsItemProcessPo.Tables[0];
                    ddlPoNo.DataTextField = "FullPONo";
                    ddlPoNo.DataValueField = "PoId";
                    ddlPoNo.DataBind();
                    ddlPoNo.Items.Insert(0, "PONo");
                }
                else
                {
                    ddlPoNo.Items.Insert(0, "PONo");
                }
            }

          
        }

        protected void ddlPoNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPoNo.SelectedValue != "" && ddlPoNo.SelectedValue != "" && ddlPoNo.SelectedValue != "PONo")
            {
                DataSet dsPO = objBs.getPurchaseOrder(Convert.ToInt32(ddlPoNo.SelectedValue));
                if (dsPO.Tables[0].Rows.Count > 0)
                {
                    #region

                    ddlProcessOn.SelectedValue = dsPO.Tables[0].Rows[0]["ProcessOn"].ToString();

                    txtOrderDate.Text = Convert.ToDateTime(dsPO.Tables[0].Rows[0]["OrderDate"]).ToString("dd/MM/yyyy");
                    txtDeliveryFrom.Text = Convert.ToDateTime(dsPO.Tables[0].Rows[0]["FromDate"]).ToString("dd/MM/yyyy");
                    txtDeliveryTo.Text = Convert.ToDateTime(dsPO.Tables[0].Rows[0]["ToDate"]).ToString("dd/MM/yyyy");

                    ddlPartyCode.SelectedValue = dsPO.Tables[0].Rows[0]["PartyCode"].ToString();
                    ddlPartyName.SelectedValue = dsPO.Tables[0].Rows[0]["PartyCode"].ToString();
                    ddlPartyCode_OnSelectedIndexChanged(sender, e);

                    #endregion
                }

                DataSet ds2 = objBs.getTransPurchaseGRNPO(Convert.ToInt32(ddlPoNo.SelectedValue));
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    #region

                    DataSet dstd = new DataSet();
                    DataTable dtddd = new DataTable();
                    DataRow drNew;
                    DataColumn dct;
                    DataTable dttt = new DataTable();

                    dct = new DataColumn("TransId");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("PurchaseFor");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("PurchaseForId");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("PurchaseForType");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("PurchaseForTypeId");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("Item");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("ItemId");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("Color");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("ColorId");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("Qty");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("Shrink");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("TotalQty");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("RQty");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("BalQty");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("Rate");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("Amount");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("RecQty");
                    dttt.Columns.Add(dct);

                    dct = new DataColumn("Remarks");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("IsRequest");
                    dttt.Columns.Add(dct);
                    dct = new DataColumn("IsReceive");
                    dttt.Columns.Add(dct);

                    dstd.Tables.Add(dttt);

                    foreach (DataRow Dr in ds2.Tables[0].Rows)
                    {
                        drNew = dttt.NewRow();

                        drNew["TransId"] = Dr["TransId"];

                        drNew["PurchaseFor"] = Dr["PurchaseFor"];
                        drNew["PurchaseForID"] = Dr["PurchaseforId"];
                        drNew["PurchaseForType"] = Dr["PurchaseforType"];
                        drNew["PurchaseForTypeId"] = Dr["PurchaseforTypeId"];

                        drNew["Item"] = Dr["Item"];
                        drNew["ItemId"] = Dr["ItemId"];

                        drNew["Color"] = Dr["Color"];
                        drNew["ColorId"] = Dr["ColorId"];

                        drNew["Qty"] = Dr["Qty"];
                        drNew["Shrink"] = Dr["Shrink"];
                        drNew["TotalQty"] = Dr["TotalQty"];

                        drNew["RQty"] = Dr["RecQty"];

                        drNew["BalQty"] = (Convert.ToDouble(Dr["TotalQty"]) - Convert.ToDouble(Dr["RecQty"])).ToString();


                        drNew["Rate"] = Dr["Rate"];
                        drNew["Amount"] = Dr["Amount"];

                        drNew["Remarks"] = Dr["Remarks"];
                        drNew["IsRequest"] = Dr["Request"];
                        drNew["IsReceive"] = Dr["Receive"];

                        drNew["RecQty"] = 0;// Convert.ToDouble(Dr["Qty"]) - Convert.ToDouble(Dr["RecQty"]);

                        dstd.Tables[0].Rows.Add(drNew);
                        dtddd = dstd.Tables[0];
                    }

                    #endregion

                    ViewState["CurrentTable1"] = dtddd;
                    GVItem.DataSource = dtddd;
                    GVItem.DataBind();
                }
                else
                {
                    ViewState["CurrentTable1"] = null;
                    GVItem.DataSource = null;
                    GVItem.DataBind();
                }

            }
            else
            {
                ViewState["CurrentTable1"] = null;
                GVItem.DataSource = null;
                GVItem.DataBind();
            }
        }

        protected void ddlPartyCode_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPartyCode.SelectedValue != "" && ddlPartyCode.SelectedValue != "0" && ddlPartyCode.SelectedValue != "PartyCode")
            {
                ddlPartyName.SelectedValue = ddlPartyCode.SelectedValue;

                DataSet dsParty = objBs.GetLedgerDetails(Convert.ToInt32(ddlPartyCode.SelectedValue));
                if (dsParty.Tables[0].Rows.Count > 0)
                {
                    txtContPerson.Text = dsParty.Tables[0].Rows[0]["ContacrPerson"].ToString();
                    txtAddress.Text = dsParty.Tables[0].Rows[0]["Address"].ToString();
                    txtPhone.Text = dsParty.Tables[0].Rows[0]["PhoneNo"].ToString();
                    txtCity.Text = dsParty.Tables[0].Rows[0]["City"].ToString();
                }

            }
            else
            {
                ddlPartyName.ClearSelection();

                txtContPerson.Text = "";
                txtAddress.Text = "";
                txtPhone.Text = "";
                txtCity.Text = "";
            }



        }

        private void FirstGridViewRow1()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("Item", typeof(string)));
            dt.Columns.Add(new DataColumn("ReceiveItem", typeof(string)));
            dt.Columns.Add(new DataColumn("Process", typeof(string)));

            dt.Columns.Add(new DataColumn("Color", typeof(string)));

            dt.Columns.Add(new DataColumn("Quantity", typeof(string)));
            dt.Columns.Add(new DataColumn("Extra", typeof(string)));
            dt.Columns.Add(new DataColumn("TotalQuantity", typeof(string)));

            dt.Columns.Add(new DataColumn("Rate", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));

            dr = dt.NewRow();
            dr["Item"] = string.Empty;
            dr["ReceiveItem"] = string.Empty;
            dr["Process"] = string.Empty;

            dr["Color"] = string.Empty;

            dr["Quantity"] = string.Empty;
            dr["Extra"] = string.Empty;
            dr["TotalQuantity"] = string.Empty;

            dr["Rate"] = string.Empty;
            dr["Amount"] = string.Empty;

            dt.Rows.Add(dr);

            ViewState["CurrentTable1"] = dt;

            GVItem.DataSource = dt;
            GVItem.DataBind();

            DataTable dtt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dtt = new DataTable();

            dct = new DataColumn("Item");
            dtt.Columns.Add(dct);
            dct = new DataColumn("ReceiveItem");
            dtt.Columns.Add(dct);
            dct = new DataColumn("Process");
            dtt.Columns.Add(dct);

            dct = new DataColumn("Color");
            dtt.Columns.Add(dct);


            dct = new DataColumn("Quantity");
            dtt.Columns.Add(dct);
            dct = new DataColumn("Extra");
            dtt.Columns.Add(dct);
            dct = new DataColumn("TotalQuantity");
            dtt.Columns.Add(dct);

            dct = new DataColumn("Rate");
            dtt.Columns.Add(dct);
            dct = new DataColumn("Amount");
            dtt.Columns.Add(dct);

            dstd.Tables.Add(dtt);

            drNew = dtt.NewRow();
            drNew["Item"] = 0;
            drNew["ReceiveItem"] = 0;
            drNew["Process"] = 0;

            drNew["Color"] = "";

            drNew["Quantity"] = "";
            drNew["Extra"] = "";
            drNew["TotalQuantity"] = "";

            drNew["Rate"] = "";
            drNew["Amount"] = "";

            dstd.Tables[0].Rows.Add(drNew);

            GVItem.DataSource = dstd;
            GVItem.DataBind();

        }
        private void AddNewRow1()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        DropDownList ddlItemCode = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlItemCode");
                        DropDownList ddlReceiveItemCode = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlReceiveItemCode");
                        DropDownList ddlProcess = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlProcess");

                        DropDownList ddlReceiveColor = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlReceiveColor");

                        TextBox txtQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtQuantity");
                        TextBox txtExtra = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtExtra");
                        TextBox txtTotalQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtTotalQuantity");

                        TextBox txtRate = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtRate");
                        TextBox txtAmount = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtAmount");

                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["Item"] = ddlItemCode.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["ReceiveItem"] = ddlReceiveItemCode.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Process"] = ddlProcess.SelectedValue;

                        dtCurrentTable.Rows[i - 1]["Color"] = ddlReceiveColor.SelectedValue;

                        dtCurrentTable.Rows[i - 1]["Quantity"] = txtQuantity.Text;
                        dtCurrentTable.Rows[i - 1]["Extra"] = txtExtra.Text;
                        dtCurrentTable.Rows[i - 1]["TotalQuantity"] = txtTotalQuantity.Text;

                        dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
                        dtCurrentTable.Rows[i - 1]["Amount"] = txtAmount.Text;

                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable1"] = dtCurrentTable;

                    GVItem.DataSource = dtCurrentTable;
                    GVItem.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            SetPreviousData1();

        }
        private void SetRowData1()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        DropDownList ddlItemCode = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlItemCode");
                        DropDownList ddlReceiveItemCode = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlReceiveItemCode");
                        DropDownList ddlProcess = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlProcess");

                        DropDownList ddlReceiveColor = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlReceiveColor");

                        TextBox txtQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[3].FindControl("txtQuantity");
                        TextBox txtExtra = (TextBox)GVItem.Rows[rowIndex].Cells[3].FindControl("txtExtra");
                        TextBox txtTotalQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[3].FindControl("txtTotalQuantity");

                        TextBox txtRate = (TextBox)GVItem.Rows[rowIndex].Cells[3].FindControl("txtRate");
                        TextBox txtAmount = (TextBox)GVItem.Rows[rowIndex].Cells[3].FindControl("txtAmount");

                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["Item"] = ddlItemCode.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["ReceiveItem"] = ddlReceiveItemCode.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Process"] = ddlProcess.SelectedValue;

                        dtCurrentTable.Rows[i - 1]["Color"] = ddlReceiveColor.SelectedValue;

                        dtCurrentTable.Rows[i - 1]["Quantity"] = txtQuantity.Text;
                        dtCurrentTable.Rows[i - 1]["Extra"] = txtExtra.Text;
                        dtCurrentTable.Rows[i - 1]["TotalQuantity"] = txtTotalQuantity.Text;

                        dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
                        dtCurrentTable.Rows[i - 1]["Amount"] = txtAmount.Text;

                        rowIndex++;

                    }

                    ViewState["CurrentTable1"] = dtCurrentTable;
                    GVItem.DataSource = dtCurrentTable;
                    GVItem.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData1();
        }
        private void SetPreviousData1()
        {
            double ItemCost = 0;
            int rowIndex = 0;
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable1"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddlItemCode = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlItemCode");
                        DropDownList ddlReceiveItemCode = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlReceiveItemCode");
                        DropDownList ddlProcess = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlProcess");

                        DropDownList ddlReceiveColor = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlReceiveColor");

                        TextBox txtQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtQuantity");
                        TextBox txtExtra = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtExtra");
                        TextBox txtTotalQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtTotalQuantity");

                        TextBox txtRate = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtRate");
                        TextBox txtAmount = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtAmount");

                        ddlItemCode.SelectedValue = dt.Rows[i]["Item"].ToString();
                        ddlReceiveItemCode.SelectedValue = dt.Rows[i]["ReceiveItem"].ToString();
                        ddlProcess.SelectedValue = dt.Rows[i]["Process"].ToString();

                        ddlReceiveColor.SelectedValue = dt.Rows[i]["Color"].ToString();

                        txtQuantity.Text = dt.Rows[i]["Quantity"].ToString();
                        txtExtra.Text = dt.Rows[i]["Extra"].ToString();
                        txtTotalQuantity.Text = dt.Rows[i]["TotalQuantity"].ToString();

                        txtRate.Text = dt.Rows[i]["Rate"].ToString();
                        txtAmount.Text = dt.Rows[i]["Amount"].ToString();

                        if (txtAmount.Text == "")
                            txtAmount.Text = "0";

                        ItemCost += Convert.ToDouble(txtAmount.Text);

                        rowIndex++;

                    }
                }
            }


        }


        protected void GVItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // SetRowData1();
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable1"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable1"] = dt;
                    GVItem.DataSource = dt;
                    GVItem.DataBind();

                    //  SetPreviousData1();

                }
                else if (dt.Rows.Count == 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable1"] = dt;
                    GVItem.DataSource = dt;
                    GVItem.DataBind();

                    //  SetPreviousData1();
                    //  FirstGridViewRow1();
                }
            }

            Calculations();
        }

        protected void txtQty_OnTextChanged(object sender, EventArgs e)
        {
            TextBox ddl = (TextBox)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            TextBox txtQty = (TextBox)row.FindControl("txtQty");

            Calculations();
            txtQty.Focus();
        }


        protected void txtRate_OnTextChanged(object sender, EventArgs e)
        {
            Calculations();
        }

        public void Calculations()
        {
            double TotalAmount = 0;

            for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
            {
                HiddenField hdAmount = (HiddenField)GVItem.Rows[vLoop].FindControl("hdAmount");
                TextBox txtRate = (TextBox)GVItem.Rows[vLoop].FindControl("txtRate");
                TextBox txtQty = (TextBox)GVItem.Rows[vLoop].FindControl("txtQty");

                if (txtRate.Text == "")
                    txtRate.Text = "0";

                if (txtQty.Text == "")
                    txtQty.Text = "0";

                TotalAmount += Convert.ToDouble(txtRate.Text) * Convert.ToDouble(txtQty.Text);
                hdAmount.Value = (Convert.ToDouble(txtRate.Text) * Convert.ToDouble(txtQty.Text)).ToString();
            }

            txtTotalAmount.Text = TotalAmount.ToString();
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {

            if (GVItem.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check The Data.')", true);
                return;
            }

            if (txtTotalAmount.Text == "")
                txtTotalAmount.Text = "0";
            if (Convert.ToDouble(txtTotalAmount.Text) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check The Data.')", true);
                return;
            }

            if (txtChallanNo.Text == "" || txtChallanNo.Text == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check ChallanNo.')", true);
                return;
            }

            DateTime RecDate = DateTime.ParseExact(txtRecDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (btnSave.Text == "Save")
            {

                // CHECK CHALLAN NUMBER

                DataSet dcheckchallan = objBs.check_challan(txtChallanNo.Text, ddlPartyCode.SelectedValue,"0");
                if (dcheckchallan.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Challan No Already Exists.For This Particular Party Code / Name.Thank You!!!.')", true);
                    txtChallanNo.Focus();
                    return;
                }



                DataSet dsPONo = objBs.GetPurchaseRecPONo(YearCode);
                string PONo = dsPONo.Tables[0].Rows[0]["RecPONo"].ToString().PadLeft(4, '0');
                txtRecNo.Text = PONo + " / " + YearCode;

                DataSet dsPoNo = objBs.CheckPurchaseGRNPONo(txtRecNo.Text, 0);
                if (dsPoNo.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Pro.Rec. No. was already Exists.')", true);
                    txtRecNo.Focus();
                    return;
                }
                else
                {
                    int POGRNId = objBs.InsertPurchaseGRN(Convert.ToInt32(ddlPoNo.SelectedValue), Convert.ToInt32(ddlProcessOn.SelectedValue), RecDate, txtDeliveryPlace.Text, dsPONo.Tables[0].Rows[0]["RecPONo"].ToString(), YearCode, txtRecNo.Text, Convert.ToInt32(ddlPartyCode.SelectedValue), Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToDouble(txtTotalAmount.Text), txtChallanNo.Text);

                    for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
                    {
                        HiddenField hdTransId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdTransId");
                        HiddenField hdPurchaseForId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdPurchaseForId");
                        HiddenField hdPurchaseForTypeId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdPurchaseForTypeId");
                        HiddenField hdItemId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdItemId");
                        HiddenField hdColorId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdColorId");

                        HiddenField hdQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdQty");
                        HiddenField hdShrink = (HiddenField)GVItem.Rows[vLoop].FindControl("hdShrink");
                        HiddenField hdTotalQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdTotalQty");
                        //HiddenField hdRate = (HiddenField)GVItem.Rows[vLoop].FindControl("hdRate");
                        HiddenField hdAmount = (HiddenField)GVItem.Rows[vLoop].FindControl("hdAmount");

                        TextBox txtRate = (TextBox)GVItem.Rows[vLoop].FindControl("txtRate");

                        TextBox txtQty = (TextBox)GVItem.Rows[vLoop].FindControl("txtQty");
                        TextBox txtRemarks = (TextBox)GVItem.Rows[vLoop].FindControl("txtRemarks");

                        if (txtQty.Text == "")
                            txtQty.Text = "0";

                        if (Convert.ToDouble(txtQty.Text) > 0)
                        {
                            int TransSamplingCostingId = objBs.InsertTransPurchaseGRN(POGRNId, Convert.ToInt32(hdTransId.Value), Convert.ToInt32(hdPurchaseForId.Value), Convert.ToInt32(hdPurchaseForTypeId.Value), Convert.ToInt32(hdItemId.Value), Convert.ToInt32(hdColorId.Value), Convert.ToDouble(hdQty.Value), Convert.ToDouble(hdShrink.Value), Convert.ToDouble(hdTotalQty.Value), Convert.ToDouble(txtRate.Text), Convert.ToDouble(hdAmount.Value), Convert.ToDouble(txtQty.Text), txtRemarks.Text, Convert.ToInt32(ddlCompany.SelectedValue));
                        }
                    }
                }
            }
            else
            {
                // CHECK CHALLAN NUMBER
                string POGRNId = Request.QueryString.Get("POGRNId");
                DataSet dcheckchallan = objBs.check_challan(txtChallanNo.Text, ddlPartyCode.SelectedValue, POGRNId);
                if (dcheckchallan.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Challan No Already Exists.For This Particular Party Code / Name.Thank You!!!.')", true);
                    txtChallanNo.Focus();
                    return;
                }

                int Iupdatestock = objBs.UpdatestockGRN(POGRNId);

                int UPDPOID = objBs.UpdatePurchaseGRN(Convert.ToInt32(POGRNId), Convert.ToInt32(ddlProcessOn.SelectedValue), RecDate, txtDeliveryPlace.Text, "", YearCode, txtRecNo.Text, Convert.ToInt32(ddlPartyCode.SelectedValue), Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToDouble(txtTotalAmount.Text), txtChallanNo.Text, POGRNId);

                for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
                {
                    HiddenField hdTransId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdTransId");
                    HiddenField hdPurchaseForId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdPurchaseForId");
                    HiddenField hdPurchaseForTypeId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdPurchaseForTypeId");
                    HiddenField hdItemId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdItemId");
                    HiddenField hdColorId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdColorId");

                    HiddenField hdQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdQty");
                    HiddenField hdShrink = (HiddenField)GVItem.Rows[vLoop].FindControl("hdShrink");
                    HiddenField hdTotalQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdTotalQty");
                    //HiddenField hdRate = (HiddenField)GVItem.Rows[vLoop].FindControl("hdRate");
                    TextBox txtRate = (TextBox)GVItem.Rows[vLoop].FindControl("txtRate");
                    HiddenField hdAmount = (HiddenField)GVItem.Rows[vLoop].FindControl("hdAmount");

                    TextBox txtQty = (TextBox)GVItem.Rows[vLoop].FindControl("txtQty");
                    TextBox txtRemarks = (TextBox)GVItem.Rows[vLoop].FindControl("txtRemarks");

                    if (txtQty.Text == "")
                        txtQty.Text = "0";

                    if (Convert.ToDouble(txtQty.Text) > 0)
                    {
                        int TransSamplingCostingId = objBs.UpdateTransPurchaseGRN(Convert.ToInt32(POGRNId), Convert.ToInt32(hdTransId.Value), Convert.ToInt32(hdPurchaseForId.Value), Convert.ToInt32(hdPurchaseForTypeId.Value), Convert.ToInt32(hdItemId.Value), Convert.ToInt32(hdColorId.Value), Convert.ToDouble(hdQty.Value), Convert.ToDouble(hdShrink.Value), Convert.ToDouble(hdTotalQty.Value), Convert.ToDouble(txtRate.Text), Convert.ToDouble(hdAmount.Value), Convert.ToDouble(txtQty.Text), txtRemarks.Text, Convert.ToInt32(ddlCompany.SelectedValue));
                    }
                }



            }
            Response.Redirect("PurchaseGRNGrid.aspx");
        }
        protected void btnExit_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("PurchaseGRNGrid.aspx");
        }

    }
}
