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
    public partial class SearchPartyWise : System.Web.UI.Page
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
                DataSet dsParty = objBs.GetPartyname();
                if (dsParty.Tables[0].Rows.Count > 0)
                {
                    ddlParty.DataSource = dsParty.Tables[0];
                    ddlParty.DataTextField = "Ledgername";
                    ddlParty.DataValueField = "LedgerID";
                    ddlParty.DataBind();
                    ddlParty.Items.Insert(0, "Select Party");
                }

                DataSet dcompany = objBs.GetCompanyDetails();
                if (dcompany.Tables[0].Rows.Count > 0)
                {
                    drpcompany.DataSource = dcompany.Tables[0];
                    drpcompany.DataTextField = "Companyname";
                    drpcompany.DataValueField = "comapanyId";
                    drpcompany.DataBind();
                    //  ddlBrand.Items.Insert(0, "Select Company");
                }

            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            if ((ddlParty.SelectedValue == "") || (ddlParty.SelectedValue == "0") || (ddlParty.SelectedValue == "Select Party"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Party.');", true);
                ddlParty.Focus();
                return;
            }

            //PartyMaster
            DataSet dsEXCPartyMaster = objBs.SearchPartyWise_PartyMaster(ddlParty.SelectedValue);
            if (dsEXCPartyMaster.Tables[0].Rows.Count > 0)
            {
                gvcust.DataSource = dsEXCPartyMaster;
                gvcust.DataBind();
            }
            else
            {
                gvcust.DataSource = null;
                gvcust.DataBind();
            }


            //BuyerOrder
            DataSet dsEXCBuyerOrder = objBs.SearchPartyWise_BuyerOrder(ddlParty.SelectedValue);
            if (dsEXCBuyerOrder.Tables[0].Rows.Count > 0)
            {
                gvBuyerOrder.DataSource = dsEXCBuyerOrder;
                gvBuyerOrder.DataBind();
            }
            else
            {
                gvBuyerOrder.DataSource = null;
                gvBuyerOrder.DataBind();
            }


            //RequirementSheet
            DataSet dsRequirementSheet = objBs.SearchPartyWise_RequirementSheet(ddlParty.SelectedValue);
            if (dsRequirementSheet.Tables[0].Rows.Count > 0)
            {
                gvrequirmentOrder.DataSource = dsRequirementSheet;
                gvrequirmentOrder.DataBind();
            }
            else
            {
                gvrequirmentOrder.DataSource = null;
                gvrequirmentOrder.DataBind();
            }

            //DataSet dstyleno = objBs.getstyledescr_RequirementSheet(ddlParty.SelectedValue);
            //if (dstyleno.Tables[0].Rows.Count > 0)
            //{
            //    DataSet getcolorchnagedetails = objBs.getcolorchnagedescription_Update_RequirementSheet(ddlParty.SelectedValue);
            //    if (getcolorchnagedetails.Tables[0].Rows.Count > 0)
            //    {
            //        gridviewstyle.DataSource = getcolorchnagedetails;
            //        gridviewstyle.DataBind();

            //        //for (int vLoop = 0; vLoop < gridviewstyle.Rows.Count; vLoop++)
            //        //{
            //        //    Label lblcolorid = (Label)gridviewstyle.Rows[vLoop].FindControl("lblcolorid");

            //        //    DropDownList drpcolorlist = (DropDownList)gridviewstyle.Rows[vLoop].FindControl("drpcolorlist");
            //        //    drpcolorlist.SelectedValue = lblcolorid.Text;
            //        //}
            //    }
            //    else
            //    {
            //        gridviewstyle.DataSource = null;
            //        gridviewstyle.DataBind();
            //    }

            //    if (dstyleno.Tables[0].Rows.Count > 0)
            //    {
            //        color_change(sender, e);
            //    }
            //}

            //ExcOpeningStock
            DataSet dsExcOpeningStock = objBs.SearchPartyWise_OpeningStock(ddlParty.SelectedValue);
            if (dsExcOpeningStock.Tables[0].Rows.Count > 0)
            {
                gvExcOpeningStock.DataSource = dsExcOpeningStock;
                gvExcOpeningStock.DataBind();
            }
            else
            {
                gvExcOpeningStock.DataSource = null;
                gvExcOpeningStock.DataBind();
            }


            //ItemProcessOrder
            DataSet dsItemProcessOrder = objBs.SearchPartyWise_ItemProcessOrder(ddlParty.SelectedValue);
            if (dsItemProcessOrder.Tables[0].Rows.Count > 0)
            {
                GVItemProcessOrder.DataSource = dsItemProcessOrder;
                GVItemProcessOrder.DataBind();
            }

            else
            {
                GVItemProcessOrder.DataSource = null;
                GVItemProcessOrder.DataBind();
            }

            //ItemProcessIssue
            DataSet dsItemProcessIssue = objBs.SearchPartyWise_ItemProcessIssue(ddlParty.SelectedValue);
            if (dsItemProcessIssue.Tables[0].Rows.Count > 0)
            {
                GVItemProcessOrderIssue.DataSource = dsItemProcessIssue;
                GVItemProcessOrderIssue.DataBind();
            }
            else
            {
                GVItemProcessOrderIssue.DataSource = null;
                GVItemProcessOrderIssue.DataBind();
            }

            //ItemProcessReceive
            DataSet dsItemProcessReceive = objBs.SearchPartyWise_ItemProcessReceive(ddlParty.SelectedValue);
            if (dsItemProcessReceive.Tables[0].Rows.Count > 0)
            {
                GVItemProcessOrderReceive.DataSource = dsItemProcessReceive;
                GVItemProcessOrderReceive.DataBind();
            }
            else
            {
                GVItemProcessOrderReceive.DataSource = null;
                GVItemProcessOrderReceive.DataBind();
            }

            //PurchaseOrder
            DataSet dsPurchaseOrder = objBs.SearchPartyWise_PurchaseOrder(ddlParty.SelectedValue);
            if (dsPurchaseOrder.Tables[0].Rows.Count > 0)
            {
                GVPurchaseOrder.DataSource = dsPurchaseOrder;
                GVPurchaseOrder.DataBind();
            }
            else
            {
                GVPurchaseOrder.DataSource = null;
                GVPurchaseOrder.DataBind();
            }

            //PurchaseGRN
            DataSet dsPurchaseGRN = objBs.SearchPartyWise_PurchaseGRN(ddlParty.SelectedValue);
            if (dsPurchaseGRN.Tables[0].Rows.Count > 0)
            {
                GVPurchaseGRN.DataSource = dsPurchaseGRN;
                GVPurchaseGRN.DataBind();
            }
            else
            {
                GVPurchaseGRN.DataSource = null;
                GVPurchaseGRN.DataBind();
            }

            //PreCutting
            DataSet dsPreCutting = objBs.SearchPartyWise_PreCutting(ddlParty.SelectedValue);
            if (dsPreCutting.Tables[0].Rows.Count > 0)
            {
                gvBuyerOrderCutting.DataSource = dsPreCutting;
                gvBuyerOrderCutting.DataBind();
            }
            else
            {
                gvBuyerOrderCutting.DataSource = null;
                gvBuyerOrderCutting.DataBind();
            }

            ////MasterCutting
            //DataSet dsMasterCutting = objBs.SearchPartyWise_MasterCutting(ddlParty.SelectedValue);
            //if (dsMasterCutting.Tables[0].Rows.Count > 0)
            //{
            //    gvBuyerOrderMasterCutting.DataSource = dsMasterCutting;
            //    gvBuyerOrderMasterCutting.DataBind();
            //}
            //else
            //{
            //    gvBuyerOrderMasterCutting.DataSource = null;
            //    gvBuyerOrderMasterCutting.DataBind();
            //}

            //MasterCutting
            DataSet dsMasterCutting = objBs.SearchPartyWise_MasterCutting(ddlParty.SelectedValue);
            if (dsMasterCutting.Tables[0].Rows.Count > 0)
            {
                gvBuyerOrderMasterCutting.DataSource = dsMasterCutting;
                gvBuyerOrderMasterCutting.DataBind();

                DataSet dstd = new DataSet();
                DataTable dtddd = new DataTable();
                DataRow drNew;
                DataColumn dct;
                DataTable dttt = new DataTable();

                dct = new DataColumn("TransItemId");
                dttt.Columns.Add(dct);
                dct = new DataColumn("RowId");
                dttt.Columns.Add(dct);
                dct = new DataColumn("StyleNo");
                dttt.Columns.Add(dct);
                dct = new DataColumn("StyleNoId");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Description");
                dttt.Columns.Add(dct);
                dct = new DataColumn("ColorId");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Color");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Rate");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Qty");
                dttt.Columns.Add(dct);
                dct = new DataColumn("AffectedQty");
                dttt.Columns.Add(dct);
                dct = new DataColumn("RangeId");
                dttt.Columns.Add(dct);
                dct = new DataColumn("CQty");
                dttt.Columns.Add(dct);
                dct = new DataColumn("CRatio");
                dttt.Columns.Add(dct);
                dct = new DataColumn("Size");
                dttt.Columns.Add(dct);

                dct = new DataColumn("BalQty");
                dttt.Columns.Add(dct);
                dct = new DataColumn("RecQty");
                dttt.Columns.Add(dct);
                dct = new DataColumn("DmgQty");
                dttt.Columns.Add(dct);

                dstd.Tables.Add(dttt);

                for (int vLoop = 0; vLoop < dsMasterCutting.Tables[0].Rows.Count; vLoop++)
                {
                    DataSet dsBuyerOrderCuttingrItems = objBs.GetTransBuyerOrderCuttingItems_Master(Convert.ToInt32(dsMasterCutting.Tables[0].Rows[vLoop]["BuyerOrderMasterCuttingId"]));
                    if (dsBuyerOrderCuttingrItems.Tables[0].Rows.Count > 0)
                    {
                        #region


                        foreach (DataRow Dr in dsBuyerOrderCuttingrItems.Tables[0].Rows)
                        {
                            drNew = dttt.NewRow();

                            drNew["TransItemId"] = Dr["TransItemId"];
                            drNew["RowId"] = Dr["RowId"];
                            drNew["StyleNo"] = Dr["StyleNo"];
                            drNew["StyleNoId"] = Dr["StyleNoId"];
                            drNew["Description"] = Dr["Description"];
                            drNew["ColorId"] = Dr["ColorId"];
                            drNew["Color"] = Dr["Color"];
                            drNew["Rate"] = Dr["Rate"];
                            drNew["Qty"] = Dr["Qty"];
                            drNew["AffectedQty"] = Dr["AffectedQty"];
                            drNew["RangeId"] = Dr["RangeId"];
                            drNew["CQty"] = Dr["CQty"];
                            drNew["CRatio"] = Dr["CRatio"];
                            drNew["Size"] = Dr["Range"];

                            drNew["BalQty"] = 0;// Dr["BalQty"];
                            drNew["RecQty"] = Dr["RecQty"];
                            drNew["DmgQty"] = Dr["DmgQty"];

                            dstd.Tables[0].Rows.Add(drNew);
                            dtddd = dstd.Tables[0];
                        }

                        #endregion
                    }
                }
                // ViewState["CurrentTable1"] = dtddd;
                GVItem.DataSource = dtddd;
                GVItem.DataBind();

                GVItem.Columns[6].Visible = false;
            }
            else
            {
                gvBuyerOrderMasterCutting.DataSource = null;
                gvBuyerOrderMasterCutting.DataBind();

                GVItem.DataSource = null;
                GVItem.DataBind();
            }


            //MaterialIssue
            DataSet dsMaterialIssue = objBs.SearchPartyWise_MaterialIssue(ddlParty.SelectedValue);
            if (dsMaterialIssue.Tables[0].Rows.Count > 0)
            {
                gvMaterialsIssue.DataSource = dsMaterialIssue;
                gvMaterialsIssue.DataBind();
            }
            else
            {
                gvMaterialsIssue.DataSource = null;
                gvMaterialsIssue.DataBind();
            }

            //Process
            DataSet dsProcess = objBs.SearchPartyWise_Process(ddlParty.SelectedValue);
            if (dsProcess.Tables[0].Rows.Count > 0)
            {
                gvCuttingProcessEntry.DataSource = dsProcess;
                gvCuttingProcessEntry.DataBind();
            }
            else
            {
                gvCuttingProcessEntry.DataSource = null;
                gvCuttingProcessEntry.DataBind();
            }
        }


        protected void color_change(object sender, EventArgs e)
        {
            DataTable dtt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dtt = new DataTable();

            dct = new DataColumn("styleno");
            dtt.Columns.Add(dct);
            dct = new DataColumn("SamplingCostingId");
            dtt.Columns.Add(dct);

            dct = new DataColumn("Color");
            dtt.Columns.Add(dct);

            dct = new DataColumn("AffectedQty");
            dtt.Columns.Add(dct);

            dct = new DataColumn("Description");
            dtt.Columns.Add(dct);
            dct = new DataColumn("itemmasterid");
            dtt.Columns.Add(dct);


            dct = new DataColumn("Itemcolor");
            dtt.Columns.Add(dct);

            dct = new DataColumn("Itemcolorid");
            dtt.Columns.Add(dct);

            dct = new DataColumn("Quantity");
            dtt.Columns.Add(dct);

            dct = new DataColumn("PQuantity");
            dtt.Columns.Add(dct);

            dct = new DataColumn("Units");
            dtt.Columns.Add(dct);


            dct = new DataColumn("Itemgroupname");
            dtt.Columns.Add(dct);

            dct = new DataColumn("Itemgroupid");
            dtt.Columns.Add(dct);

            dct = new DataColumn("STotalpcs");
            dtt.Columns.Add(dct);

            dct = new DataColumn("PTotalpcs");
            dtt.Columns.Add(dct);

            dct = new DataColumn("Unitsid");
            dtt.Columns.Add(dct);

            //dct = new DataColumn("Units");
            //dtt.Columns.Add(dct);

            dstd.Tables.Add(dtt);

            for (int vLoop = 0; vLoop < gridviewstyle.Rows.Count; vLoop++)
            {
                Label lblstyleno = (Label)gridviewstyle.Rows[vLoop].FindControl("lblstyleno");
                Label lblstyleid = (Label)gridviewstyle.Rows[vLoop].FindControl("lblstyleid");
                Label lblitemcolor = (Label)gridviewstyle.Rows[vLoop].FindControl("lblitemcolor");
                Label lblitemtype = (Label)gridviewstyle.Rows[vLoop].FindControl("lblitemtype");
                Label lblItemgroupId = (Label)gridviewstyle.Rows[vLoop].FindControl("lblItemgroupId");
                Label lblcategory = (Label)gridviewstyle.Rows[vLoop].FindControl("lblcategory");
                Label lblitemname = (Label)gridviewstyle.Rows[vLoop].FindControl("lblitemname");
                Label lblitemid = (Label)gridviewstyle.Rows[vLoop].FindControl("lblitemid");
                Label lblcolorid = (Label)gridviewstyle.Rows[vLoop].FindControl("lblcolorid");
                Label lblsampleavg = (Label)gridviewstyle.Rows[vLoop].FindControl("lblsampleavg");
                TextBox txtprodavg = (TextBox)gridviewstyle.Rows[vLoop].FindControl("txtprodavg");

                Label lbluom = (Label)gridviewstyle.Rows[vLoop].FindControl("lbluom");

                Label lblCqty = (Label)gridviewstyle.Rows[vLoop].FindControl("lblCqty");
                Label lblBQty = (Label)gridviewstyle.Rows[vLoop].FindControl("lblBQty");
                Label lblStotpcs = (Label)gridviewstyle.Rows[vLoop].FindControl("lblStotpcs");
                Label lblPtotpcs = (Label)gridviewstyle.Rows[vLoop].FindControl("lblPtotpcs");

                Label lblunitsid = (Label)gridviewstyle.Rows[vLoop].FindControl("lblunitsid");

                DropDownList drpcolorlist = (DropDownList)gridviewstyle.Rows[vLoop].FindControl("drpcolorlist");
                //drpcolorlist.SelectedValue = lblcolorid.Text;


                if (lblsampleavg.Text == "")
                    lblsampleavg.Text = "0";

                if (txtprodavg.Text == "")
                    txtprodavg.Text = "0";

                if (lblCqty.Text == "")
                    lblCqty.Text = "0";

                drNew = dtt.NewRow();

                drNew["styleno"] = lblstyleno.Text;
                drNew["SamplingCostingId"] = lblstyleid.Text;
                drNew["Color"] = lblitemcolor.Text;
                drNew["AffectedQty"] = lblCqty.Text;
                drNew["Description"] = lblitemname.Text;
                drNew["itemmasterid"] = lblitemid.Text;
                drNew["Itemcolor"] = drpcolorlist.SelectedItem.Text;
                drNew["Itemcolorid"] = drpcolorlist.SelectedValue;
                drNew["Quantity"] = lblsampleavg.Text;
                drNew["PQuantity"] = txtprodavg.Text;
                drNew["Units"] = lbluom.Text;
                drNew["Unitsid"] = lblunitsid.Text;
                drNew["Itemgroupname"] = lblitemtype.Text;
                drNew["Itemgroupid"] = lblItemgroupId.Text;

                drNew["STotalpcs"] = (Convert.ToDouble(lblCqty.Text) * Convert.ToDouble(lblsampleavg.Text));
                drNew["PTotalpcs"] = (Convert.ToDouble(lblCqty.Text) * Convert.ToDouble(txtprodavg.Text)); ;
                //drNew[""] = ;
                dstd.Tables[0].Rows.Add(drNew);
            }

            DataSet ds1;
            DataTable dtt1;
            DataRow drNew1;
            DataColumn dc;
            ds1 = new DataSet();
            dtt1 = new DataTable();
            dc = new DataColumn("Itemgroupname");
            dtt1.Columns.Add(dc);
            dc = new DataColumn("Itemgroupid");
            dtt1.Columns.Add(dc);

            dc = new DataColumn("Description");
            dtt1.Columns.Add(dc);

            dc = new DataColumn("Totalpcs");
            dtt1.Columns.Add(dc);


            dc = new DataColumn("itemmasterid");
            dtt1.Columns.Add(dc);

            dc = new DataColumn("Itemcolor");
            dtt1.Columns.Add(dc);

            dc = new DataColumn("Itemcolorid");
            dtt1.Columns.Add(dc);

            dc = new DataColumn("STotalpcs");
            dtt1.Columns.Add(dc);

            dc = new DataColumn("PTotalpcs");
            dtt1.Columns.Add(dc);

            dc = new DataColumn("Units");
            dtt1.Columns.Add(dc);

            dc = new DataColumn("Unitsid");
            dtt1.Columns.Add(dc);

            ds1.Tables.Add(dtt1);

            #region Color Wise Item Details /Change Item Category/Calculate Required Item

            var result = from r in dstd.Tables[0].AsEnumerable()
                         group r by new
                         {
                             itemmasterid = r["itemmasterid"],
                             Itemgroupid = r["Itemgroupid"],
                             Itemcolorid = r["Itemcolorid"],
                             Itemcolor = r["Itemcolor"],
                             Itemgroupname = r["Itemgroupname"],
                             Description = r["Description"],
                             Units = r["Units"],
                             Unitsid = r["Unitsid"]
                         } into g
                         select new
                         {
                             Itemgroupname = g.Key.Itemgroupname,
                             Itemgroupid = g.Key.Itemgroupid,

                             Description = g.Key.Description,
                             itemmasterid = g.Key.itemmasterid,

                             Itemcolor = g.Key.Itemcolor,
                             Itemcolorid = g.Key.Itemcolorid,

                             Units = g.Key.Units,
                             Unitsid = g.Key.Unitsid,
                             STotalpcs = g.Sum(x => Convert.ToDouble(x["STotalpcs"])),
                             PTotalpcs = g.Sum(x => Convert.ToDouble(x["PTotalpcs"])),
                             Totalpcs = g.Sum(x => Convert.ToDouble(x["AffectedQty"])),
                         };

            foreach (var g in result)
            {
                drNew1 = dtt1.NewRow();
                drNew1["Itemgroupname"] = g.Itemgroupname;
                drNew1["Itemgroupid"] = g.Itemgroupid;

                drNew1["Description"] = g.Description;
                drNew1["itemmasterid"] = g.itemmasterid;

                drNew1["Itemcolor"] = g.Itemcolor;
                drNew1["Itemcolorid"] = g.Itemcolorid;

                drNew1["Units"] = g.Units;
                drNew1["Unitsid"] = g.Unitsid;

                drNew1["STotalpcs"] = g.STotalpcs;
                drNew1["PTotalpcs"] = g.PTotalpcs;

                drNew1["Totalpcs"] = g.Totalpcs;

                ds1.Tables[0].Rows.Add(drNew1);
            }


            #endregion


            #region GET AVALIABLE STOCK


            if (ds1.Tables[0].Rows.Count > 0)
            {
                gridstockdetails.DataSource = ds1;
                gridstockdetails.DataBind();

                for (int vLoop = 0; vLoop < gridstockdetails.Rows.Count; vLoop++)
                {
                    Label lblitemid = (Label)gridstockdetails.Rows[vLoop].FindControl("lblitemid");
                    Label lblitemcolorid = (Label)gridstockdetails.Rows[vLoop].FindControl("lblitemcolorid");

                    Label lblprodavg = (Label)gridstockdetails.Rows[vLoop].FindControl("lblprodavg");
                    Label lblavlstock = (Label)gridstockdetails.Rows[vLoop].FindControl("lblavlstock");

                    Label lblpurchasestock = (Label)gridstockdetails.Rows[vLoop].FindControl("lblpurchasestock");

                    // Get STock

                    DataSet dsstock = objBs.GetAvlStock(lblitemid.Text, lblitemcolorid.Text, drpcompany.SelectedValue);
                    if (dsstock.Tables[0].Rows.Count > 0)
                    {
                        lblavlstock.Text = Convert.ToDouble(dsstock.Tables[0].Rows[0]["Qty"]).ToString("0.00");
                    }
                    else
                    {
                        lblavlstock.Text = "0.00";

                    }

                    double getremainstock = Convert.ToDouble(lblprodavg.Text) - Convert.ToDouble(lblavlstock.Text);
                    if (getremainstock < 0)
                    {
                        lblpurchasestock.Text = "0.00";
                    }
                    else
                    {
                        lblpurchasestock.Text = getremainstock.ToString("0.00");
                    }


                }
            }
            else
            {
                gridstockdetails.DataSource = null;
                gridstockdetails.DataBind();
            }
        }
            #endregion
        protected void GricViewStyle_Color(object sender, GridViewRowEventArgs e)
        {
            DataSet dsColor = objBs.gridColor();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var ddlColor = (DropDownList)e.Row.FindControl("drpcolorlist");
                ddlColor.DataSource = dsColor.Tables[0];
                ddlColor.DataTextField = "Color";
                ddlColor.DataValueField = "ColorID";
                ddlColor.DataBind();
                //if (btnSave.Text == "Save")
                //{
                ddlColor.SelectedValue = lblColorId.Text;
                //}
                //else
                //{
                // ddlColor.Items.Insert(0, "Select Color");
                //}
            }

        }

        protected void gvCostingDetails2_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                {
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[0].Font.Bold = true;
                    e.Row.Cells[0].Font.Size = Convert.ToInt32(FontSize.Text);
                }
            }
        }
    }
}


