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
    public partial class BuyerOrderSales : System.Web.UI.Page
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
                DataSet dsInvNo = objBs.BuyerOrderSalesInv(YearCode);
                string InvoiceNo = dsInvNo.Tables[0].Rows[0]["InvoiceNo"].ToString().PadLeft(4, '0');
                txtInvNo.Text = " INV -  " + InvoiceNo + " / " + YearCode;
                txtInvDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                DataSet dsset = objBs.getLedger_New(lblContactTypeId.Text);
                if (dsset.Tables[0].Rows.Count > 0)
                {
                    ddlPartyCode.DataSource = dsset.Tables[0];
                    ddlPartyCode.DataTextField = "CompanyCode";
                    ddlPartyCode.DataValueField = "LedgerID";
                    ddlPartyCode.DataBind();
                    ddlPartyCode.Items.Insert(0, "PartyCode");
                }


                string BuyerOrderSalesId = Request.QueryString.Get("BuyerOrderSalesId");
                if (BuyerOrderSalesId != "" && BuyerOrderSalesId != null)
                {
                    #region
                    DataSet dsBOS = objBs.GetInsertBuyerOrderSales(BuyerOrderSalesId);
                    if (dsBOS.Tables[0].Rows.Count > 0)
                    {
                        btnSubmitQty.Visible = false;
                        btnSave.Enabled = false;

                        txtInvNo.Text = dsBOS.Tables[0].Rows[0]["FullInvoiceNo"].ToString();
                        txtInvDate.Text = Convert.ToDateTime(dsBOS.Tables[0].Rows[0]["InvoiceDate"]).ToString("dd/MM/yyyy");
                        ddlPartyCode.SelectedValue = dsBOS.Tables[0].Rows[0]["BuyerId"].ToString();
                        txtNarration.Text = dsBOS.Tables[0].Rows[0]["Narrations"].ToString();

                        DataSet dsExcStyle = objBs.BuyerOrderSalesStyles1(BuyerOrderSalesId);

                        DataSet dstd = new DataSet();
                        DataTable dtddd = new DataTable();
                        DataRow drNew;
                        DataColumn dct;
                        DataTable dttt = new DataTable();

                        #region

                        dct = new DataColumn("AllID");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("BuyerOrderMasterCuttingId");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("ExcNo");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("StyleNo");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Color");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Range");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Qty");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("RowId");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("IssueQty");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Rate");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Amount");
                        dttt.Columns.Add(dct);
                        dstd.Tables.Add(dttt);

                        foreach (DataRow Dr in dsExcStyle.Tables[0].Rows)
                        {
                            drNew = dttt.NewRow();
                            drNew["AllID"] = Dr["AllID"];
                            drNew["BuyerOrderMasterCuttingId"] = Dr["BuyerOrderMasterCuttingId"];
                            drNew["ExcNo"] = Dr["ExcNo"];
                            drNew["StyleNo"] = Dr["StyleNo"];
                            drNew["Color"] = Dr["Color"];
                            drNew["Range"] = Dr["Range"];
                            drNew["Qty"] = Dr["Qty"];
                            drNew["RowId"] = Dr["RowId"];
                            drNew["IssueQty"] = Dr["IssueQty"];
                            drNew["Rate"] = Dr["Rate"];
                            drNew["Amount"] = (Convert.ToDouble(Dr["Qty"]) * Convert.ToDouble(Dr["Rate"])).ToString("f2");

                            dstd.Tables[0].Rows.Add(drNew);
                            dtddd = dstd.Tables[0];
                        }

                        #endregion

                        ViewState["CurrentTable1"] = dtddd;
                        GVItem.DataSource = dtddd;
                        GVItem.DataBind();

                        DataSet dsExcStyleSize = objBs.BuyerOrderSalesStylesSize1(BuyerOrderSalesId);

                        DataSet dstd1 = new DataSet();
                        DataTable dtddd1 = new DataTable();
                        DataRow drNew1;
                        DataColumn dct1;
                        DataTable dttt1 = new DataTable();

                        #region

                        dct1 = new DataColumn("ExcStockId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("StyleId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("ColorId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("SizeId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("Qty");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("BuyerOrderMasterCuttingId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("RangeId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("RowId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("TransSizeId");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("Size");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("IssueQty");
                        dttt1.Columns.Add(dct1);
                        dct1 = new DataColumn("Rate");
                        dttt1.Columns.Add(dct1);
                        dstd1.Tables.Add(dttt1);

                        foreach (DataRow Dr in dsExcStyleSize.Tables[0].Rows)
                        {
                            drNew1 = dttt1.NewRow();

                            drNew1["ExcStockId"] = Dr["ExcStockId"];
                            drNew1["StyleId"] = Dr["StyleId"];
                            drNew1["ColorId"] = Dr["ColorId"];
                            drNew1["SizeId"] = Dr["SizeId"];
                            drNew1["Qty"] = Dr["Qty"];
                            drNew1["BuyerOrderMasterCuttingId"] = Dr["BuyerOrderMasterCuttingId"];
                            drNew1["RangeId"] = Dr["RangeId"];
                            drNew1["RowId"] = Dr["RowId"];
                            drNew1["TransSizeId"] = Dr["TransSizeId"];
                            drNew1["Size"] = Dr["Size"];
                            drNew1["IssueQty"] = Dr["IssueQty"];
                            drNew1["Rate"] = 0;
                            dstd1.Tables[0].Rows.Add(drNew1);
                            dtddd1 = dstd1.Tables[0];

                        }

                        #endregion

                        ViewState["CurrentTable2"] = dtddd1;

                    }

                    #endregion
                }
            }

        }

        protected void ddlPartyCode_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["CurrentTable1"] = null;
            ViewState["CurrentTable2"] = null;
            GVItem.DataSource = null;
            GVItem.DataBind();
            GVSizes.DataSource = null;
            GVSizes.DataBind();

            if (ddlPartyCode.SelectedValue != "" && ddlPartyCode.SelectedValue != "0" && ddlPartyCode.SelectedValue != "PartyCode")
            {
                DataSet dsExc = objBs.BuyerOrderSalesExc(Convert.ToInt32(ddlPartyCode.SelectedValue));
                if (dsExc.Tables[0].Rows.Count > 0)
                {
                    chkExcNo.DataSource = dsExc;
                    chkExcNo.DataTextField = "ExcNo";
                    chkExcNo.DataValueField = "BuyerOrderMasterCuttingId";
                    chkExcNo.DataBind();
                }
                else
                {
                    chkExcNo.Items.Clear();
                }
            }
            else
            {
                chkExcNo.Items.Clear();
            }
            txtExcNo.Focus();

        }
        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            GVSizes.DataSource = null;
            GVSizes.DataBind();

            if (chkExcNo.SelectedIndex < 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check The Data.')", true);
                chkExcNo.Focus();
                return;
            }

            string IsFirst = "Yes"; string Ids = "All";
            foreach (ListItem listItem in chkExcNo.Items)
            {
                if (listItem.Selected)
                {
                    if (IsFirst == "Yes")
                    {
                        Ids = listItem.Value;
                        IsFirst = "No";
                    }
                    else
                    {
                        Ids = Ids + "," + listItem.Value;
                    }
                }
            }

            DataSet dsExcStyle = objBs.BuyerOrderSalesStyles(Ids);
            if (dsExcStyle.Tables[0].Rows.Count > 0)
            {
                DataSet dsExcStyleRate = objBs.BuyerOrderSalesStylesRate(Ids);

                DataSet dstd = new DataSet();
                DataTable dtddd = new DataTable();
                DataRow drNew;
                DataColumn dct;
                DataTable dttt = new DataTable();

                #region

                dct = new DataColumn("AllID");
                dttt.Columns.Add(dct);
                dct = new DataColumn("BuyerOrderMasterCuttingId");
                dttt.Columns.Add(dct);
                dct = new DataColumn("ExcNo");
                dttt.Columns.Add(dct);
                dct = new DataColumn("StyleNo");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Color");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Range");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Qty");
                dttt.Columns.Add(dct);
                dct = new DataColumn("RowId");
                dttt.Columns.Add(dct);
                dct = new DataColumn("IssueQty");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Rate");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Amount");
                dttt.Columns.Add(dct);
                dstd.Tables.Add(dttt);

                foreach (DataRow Dr in dsExcStyle.Tables[0].Rows)
                {
                    drNew = dttt.NewRow();
                    drNew["AllID"] = Dr["AllID"];
                    drNew["BuyerOrderMasterCuttingId"] = Dr["BuyerOrderMasterCuttingId"];
                    drNew["ExcNo"] = Dr["ExcNo"];
                    drNew["StyleNo"] = Dr["StyleNo"];
                    drNew["Color"] = Dr["Color"];
                    drNew["Range"] = Dr["Range"];
                    drNew["Qty"] = Dr["Qty"];
                    drNew["RowId"] = Dr["RowId"];
                    drNew["IssueQty"] = Dr["IssueQty"];

                    DataRow[] RowsStyleQty = dsExcStyleRate.Tables[0].Select("BuyerOrderMasterCuttingId='" + Dr["BuyerOrderMasterCuttingId"] + "' and RowId='" + Dr["RowId"] + "' ");
                    drNew["Rate"] = RowsStyleQty[0]["Rate"].ToString();

                    drNew["Amount"] =0;

                    dstd.Tables[0].Rows.Add(drNew);
                    dtddd = dstd.Tables[0];
                }

                #endregion

                ViewState["CurrentTable1"] = dtddd;
                GVItem.DataSource = dtddd;
                GVItem.DataBind();

                DataSet dsExcStyleSize = objBs.BuyerOrderSalesStylesSize(Ids);

                DataSet dstd1 = new DataSet();
                DataTable dtddd1 = new DataTable();
                DataRow drNew1;
                DataColumn dct1;
                DataTable dttt1 = new DataTable();

                #region

                dct1 = new DataColumn("ExcStockId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("StyleId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("ColorId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("SizeId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("Qty");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("BuyerOrderMasterCuttingId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("RangeId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("RowId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("TransSizeId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("Size");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("IssueQty");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("Rate");
                dttt1.Columns.Add(dct1);
                dstd1.Tables.Add(dttt1);

                foreach (DataRow Dr in dsExcStyleSize.Tables[0].Rows)
                {
                    drNew1 = dttt1.NewRow();

                    drNew1["ExcStockId"] = Dr["ExcStockId"];
                    drNew1["StyleId"] = Dr["StyleId"];
                    drNew1["ColorId"] = Dr["ColorId"];
                    drNew1["SizeId"] = Dr["SizeId"];
                    drNew1["Qty"] = Dr["Qty"];
                    drNew1["BuyerOrderMasterCuttingId"] = Dr["BuyerOrderMasterCuttingId"];
                    drNew1["RangeId"] = Dr["RangeId"];
                    drNew1["RowId"] = Dr["RowId"];
                    drNew1["TransSizeId"] = Dr["TransSizeId"];
                    drNew1["Size"] = Dr["Size"];
                    drNew1["IssueQty"] = Dr["IssueQty"];
                    drNew1["Rate"] = 0;
                    dstd1.Tables[0].Rows.Add(drNew1);
                    dtddd1 = dstd1.Tables[0];

                }

                #endregion

                ViewState["CurrentTable2"] = dtddd1;

            }
            else
            {
                ViewState["CurrentTable1"] = null;
                ViewState["CurrentTable2"] = null;
                GVItem.DataSource = null;
                GVItem.DataBind();
            }


        }

        protected void GVItem_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AssignQty")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    string[] Ids = e.CommandArgument.ToString().Split('#');

                    #region

                    DataTable DTStyle = (DataTable)ViewState["CurrentTable1"];
                    DataRow[] RowsStyleQty = DTStyle.Select("BuyerOrderMasterCuttingId='" + Ids[0].ToString() + "' and RowId='" + Ids[1].ToString() + "' ");

                    AllID.Text = RowsStyleQty[0]["AllID"].ToString();
                    BuyerOrderMasterCuttingId.Text = RowsStyleQty[0]["BuyerOrderMasterCuttingId"].ToString();
                    RowId.Text = RowsStyleQty[0]["RowId"].ToString();

                    ExcNo.Text = RowsStyleQty[0]["ExcNo"].ToString();
                    StyleNo.Text = RowsStyleQty[0]["StyleNo"].ToString();
                    Color.Text = RowsStyleQty[0]["Color"].ToString();
                    Range.Text = RowsStyleQty[0]["Range"].ToString();
                    Qty.Text = RowsStyleQty[0]["Qty"].ToString();
                    IssueQty.Text = RowsStyleQty[0]["IssueQty"].ToString();
                    Rate.Text = RowsStyleQty[0]["Rate"].ToString();
                    Amount.Text = RowsStyleQty[0]["Amount"].ToString();

                    #endregion

                    #region

                    DataSet dstd1 = new DataSet();
                    DataTable dtddd1 = new DataTable();

                    DataRow drNew1;
                    DataColumn dct1;

                    DataTable dttt1 = new DataTable();
                   
                    dct1 = new DataColumn("ExcStockId");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("StyleId");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("ColorId");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("SizeId");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("Qty");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("BuyerOrderMasterCuttingId");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("RangeId");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("RowId");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("TransSizeId");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("Size");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("IssueQty");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("Rate");
                    dttt1.Columns.Add(dct1);
                    dstd1.Tables.Add(dttt1);

                    DataTable DTSize = (DataTable)ViewState["CurrentTable2"];
                    DataRow[] RowsSizeQty = DTSize.Select("BuyerOrderMasterCuttingId='" + Ids[0].ToString() + "' and RowId='" + Ids[1].ToString() + "' ");

                    for (int i = 0; i < RowsSizeQty.Length; i++)
                    {
                        drNew1 = dttt1.NewRow();
                        drNew1["ExcStockId"] = RowsSizeQty[i]["ExcStockId"].ToString();
                        drNew1["StyleId"] = RowsSizeQty[i]["StyleId"].ToString();
                        drNew1["ColorId"] = RowsSizeQty[i]["ColorId"].ToString();
                        drNew1["SizeId"] = RowsSizeQty[i]["SizeId"].ToString();
                        drNew1["Qty"] = RowsSizeQty[i]["Qty"].ToString();
                        drNew1["BuyerOrderMasterCuttingId"] = RowsSizeQty[i]["BuyerOrderMasterCuttingId"].ToString();
                        drNew1["RangeId"] = RowsSizeQty[i]["RangeId"].ToString();
                        drNew1["RowId"] = RowsSizeQty[i]["RowId"].ToString();
                        drNew1["TransSizeId"] = RowsSizeQty[i]["TransSizeId"].ToString();
                        drNew1["Size"] = RowsSizeQty[i]["Size"].ToString();
                        drNew1["IssueQty"] = RowsSizeQty[i]["IssueQty"].ToString();
                        drNew1["Rate"] = RowsSizeQty[i]["Rate"].ToString();
                        dstd1.Tables[0].Rows.Add(drNew1);
                        dtddd1 = dstd1.Tables[0];
                    }

                    GVSizes.DataSource = dstd1;
                    GVSizes.DataBind();

                    #endregion
                }
            }
        }
        protected void btnSubmitQty_OnClick(object sender, EventArgs e)
        {
            if (GVSizes.Rows.Count > 0)
            {
                for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
                {
                    HiddenField hdQty = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdQty");
                    TextBox txtIssueQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtIssueQty");
                    if (txtIssueQty.Text == "")
                        txtIssueQty.Text = "0";

                    if (Convert.ToInt32(hdQty.Value) < Convert.ToInt32(txtIssueQty.Text))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check IssueQty.')", true);
                        txtIssueQty.Focus();
                        return;
                    }
                }

                #region CurrentTable Removed

                DataTable DTStyle = (DataTable)ViewState["CurrentTable1"];
                DataRow[] RowsStyleQty = DTStyle.Select("BuyerOrderMasterCuttingId='" + BuyerOrderMasterCuttingId.Text + "' and RowId='" + RowId.Text + "' ");

                for (int i = 0; i < RowsStyleQty.Length; i++)
                    RowsStyleQty[i].Delete();
                DTStyle.AcceptChanges();

                ViewState["CurrentTable1"] = DTStyle;

                DataTable DTSize = (DataTable)ViewState["CurrentTable2"];
                DataRow[] RowsSizeQty = DTSize.Select("BuyerOrderMasterCuttingId='" + BuyerOrderMasterCuttingId.Text + "' and RowId='" + RowId.Text + "' ");

                for (int i = 0; i < RowsSizeQty.Length; i++)
                    RowsSizeQty[i].Delete();
                DTSize.AcceptChanges();

                ViewState["CurrentTable2"] = DTSize;

                #endregion

                double IssueQty = 0;
                for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
                {
                    TextBox txtIssueQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtIssueQty");
                    if (txtIssueQty.Text == "")
                        txtIssueQty.Text = "0";
                    IssueQty += Convert.ToDouble(txtIssueQty.Text);
                }

                DataSet dstd = new DataSet();
                DataTable dtddd = new DataTable();
                DataRow drNew;
                DataColumn dct;
                DataTable dttt = new DataTable();

                #region

                dct = new DataColumn("AllID");
                dttt.Columns.Add(dct);
                dct = new DataColumn("BuyerOrderMasterCuttingId");
                dttt.Columns.Add(dct);
                dct = new DataColumn("ExcNo");
                dttt.Columns.Add(dct);
                dct = new DataColumn("StyleNo");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Color");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Range");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Qty");
                dttt.Columns.Add(dct);
                dct = new DataColumn("RowId");
                dttt.Columns.Add(dct);
                dct = new DataColumn("IssueQty");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Rate");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Amount");
                dttt.Columns.Add(dct);
                dstd.Tables.Add(dttt);

                DataTable dt = (DataTable)ViewState["CurrentTable1"];

                drNew = dttt.NewRow();
                drNew["AllID"] = AllID.Text;
                drNew["BuyerOrderMasterCuttingId"] = BuyerOrderMasterCuttingId.Text;
                drNew["ExcNo"] = ExcNo.Text;
                drNew["StyleNo"] = StyleNo.Text;
                drNew["Color"] = Color.Text;
                drNew["Range"] = Range.Text;
                drNew["Qty"] = Qty.Text;
                drNew["RowId"] = RowId.Text;
                drNew["IssueQty"] = IssueQty;
                drNew["Rate"] = Rate.Text;
                drNew["Amount"] = IssueQty * Convert.ToDouble(Rate.Text);
                dstd.Tables[0].Rows.Add(drNew);
                dtddd = dstd.Tables[0];
                dtddd.Merge(dt);

                #endregion

                ViewState["CurrentTable1"] = dtddd;
                GVItem.DataSource = dstd;
                GVItem.DataBind();

                DataSet dstd1 = new DataSet();
                DataTable dtddd1 = new DataTable();
                DataRow drNew1;
                DataColumn dct1;
                DataTable dttt1 = new DataTable();

                #region

                dct1 = new DataColumn("ExcStockId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("StyleId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("ColorId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("SizeId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("Qty");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("BuyerOrderMasterCuttingId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("RangeId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("RowId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("TransSizeId");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("Size");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("IssueQty");
                dttt1.Columns.Add(dct1);
                dct1 = new DataColumn("Rate");
                dttt1.Columns.Add(dct1);
                dstd1.Tables.Add(dttt1);

                DataTable dt1 = (DataTable)ViewState["CurrentTable2"];

                for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
                {
                    HiddenField hdExcStockId = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdExcStockId");
                    HiddenField hdStyleId = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdStyleId");
                    HiddenField hdColorId = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdColorId");
                    HiddenField hdSizeId = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdSizeId");
                    HiddenField hdQty = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdQty");
                    HiddenField hdBuyerOrderMasterCuttingId = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdBuyerOrderMasterCuttingId");
                    HiddenField hdRangeId = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdRangeId");
                    HiddenField hdRowId = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdRowId");
                    HiddenField hdTransSizeId = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdTransSizeId");
                    HiddenField hdSize = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdSize");

                    TextBox txtIssueQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtIssueQty");
                    if (txtIssueQty.Text == "")
                        txtIssueQty.Text = "0";

                    drNew1 = dttt1.NewRow();
                    drNew1["ExcStockId"] = hdExcStockId.Value;
                    drNew1["StyleId"] = hdStyleId.Value;
                    drNew1["ColorId"] = hdColorId.Value;
                    drNew1["SizeId"] = hdSizeId.Value;
                    drNew1["Qty"] = hdQty.Value;
                    drNew1["BuyerOrderMasterCuttingId"] = hdBuyerOrderMasterCuttingId.Value;
                    drNew1["RangeId"] = hdRangeId.Value;
                    drNew1["RowId"] = hdRowId.Value;
                    drNew1["TransSizeId"] = hdTransSizeId.Value;
                    drNew1["Size"] = hdSize.Value;
                    drNew1["IssueQty"] = txtIssueQty.Text;
                    drNew1["Rate"] = Rate.Text;
                    dstd1.Tables[0].Rows.Add(drNew1);
                    dtddd1 = dstd1.Tables[0];

                }

                dtddd1.Merge(dt1);

                #endregion

                ViewState["CurrentTable2"] = dtddd1;

            }

            GVSizes.DataSource = null;
            GVSizes.DataBind();

        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            #region Validations
            if (ddlPartyCode.SelectedValue == "" || ddlPartyCode.SelectedValue == "0" || ddlPartyCode.SelectedValue == "PartyCode")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select PartyCode.')", true);
                ddlPartyCode.Focus();
                return;
            }
            if (GVItem.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check The Data.')", true);
                return;
            }

            int IssueQty = 0;
            for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
            {
                HiddenField hdIssueQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdIssueQty");
                IssueQty += Convert.ToInt32(hdIssueQty.Value);
            }

            if (IssueQty == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check IssueQty.')", true);
                return;
            }
            #endregion

            DateTime InvDate = DateTime.ParseExact(txtInvDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DataTable DTSizes = (DataTable)ViewState["CurrentTable2"];

            int TransHistoryId = objBs.InsertBuyerOrderSalesStyles(YearCode, InvDate, Convert.ToInt32(ddlPartyCode.SelectedValue), txtNarration.Text, DTSizes);

            Response.Redirect("BuyerOrderSalesGrid.aspx");
        }
        protected void btnExit_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("BuyerOrderSalesGrid.aspx");
        }
    }
}