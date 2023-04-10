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
    public partial class PurchaseOrderPendingReport : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        double TtlQty = 0; double TtlRecQty = 0; double TtlBalQty = 0;
        double TtlReqSheetQty = 0; double TtlReqSheetSamplingQty = 0; double TtlReqSheetProdQty = 0;

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
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                DataSet dsItem = objBs.getAlliItems();
                if (dsItem.Tables[0].Rows.Count > 0)
                {
                    ddlItem.DataSource = dsItem.Tables[0];
                    ddlItem.DataTextField = "Description";
                    ddlItem.DataValueField = "ItemMasterId";
                    ddlItem.DataBind();
                    ddlItem.Items.Insert(0, "All");
                }

                DataSet dsset = objBs.getLedger_New(lblContactTypeId.Text);
                if (dsset.Tables[0].Rows.Count > 0)
                {
                    ddlPartyCode.DataSource = dsset.Tables[0];
                    ddlPartyCode.DataTextField = "CompanyCode";
                    ddlPartyCode.DataValueField = "LedgerID";
                    ddlPartyCode.DataBind();
                    ddlPartyCode.Items.Insert(0, "All");

                    ddlPartyName.DataSource = dsset.Tables[0];
                    ddlPartyName.DataTextField = "LedgerName";
                    ddlPartyName.DataValueField = "LedgerID";
                    ddlPartyName.DataBind();
                    ddlPartyName.Items.Insert(0, "All");
                }

                DataSet dsPONo = objBs.getPurchaseOrderReportPONo();
                if (dsPONo.Tables[0].Rows.Count > 0)
                {
                    chkPONo.DataSource = dsPONo.Tables[0];
                    chkPONo.DataTextField = "FullPONo";
                    chkPONo.DataValueField = "POId";
                    chkPONo.DataBind();

                }

            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            #region

            DateTime From = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime To = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string ItemPOId = "";
            string IsFirst = "Yes";
            foreach (ListItem listItem in chkPONo.Items)
            {
                #region
                if (chkPONo.SelectedIndex < 0)
                {
                    if (IsFirst == "Yes")
                    {
                        ItemPOId = listItem.Value;
                        IsFirst = "No";
                    }
                    else
                    {
                        ItemPOId = ItemPOId + "," + listItem.Value;
                    }
                }
                else
                {
                    if (listItem.Selected)
                    {
                        if (IsFirst == "Yes")
                        {
                            ItemPOId = listItem.Value;
                            IsFirst = "No";
                        }
                        else
                        {
                            ItemPOId = ItemPOId + "," + listItem.Value;
                        }
                    }
                }
                #endregion
            }

            string ReportType = "";
            if (ddlReportType.SelectedValue == "1")
            {
                ReportType = "All";
            }
            else if (ddlReportType.SelectedValue == "2")
            {
                ReportType = ">";
            }
            else if (ddlReportType.SelectedValue == "3")
            {
                ReportType = "<=";
            }
            //DataSet dsStyles = objBs.getTransPurchaseOrderPending_Report(ddlItem.SelectedValue, ReportType, ItemPOId, chkUseDate.Checked, From, To, ddlPartyCode.SelectedValue);
            //if (dsStyles.Tables[0].Rows.Count > 0)
            //{
            //    gvPOPending.DataSource = dsStyles;
            //    gvPOPending.DataBind();
            //}
            //else
            //{
            //    gvPOPending.DataSource = null;
            //    gvPOPending.DataBind();
            //}


            DataTable dttPO;
            DataRow drNewPO;
            DataColumn dctPO;
            DataSet dstdPO = new DataSet();
            dttPO = new DataTable();

            dctPO = new DataColumn("FullPONo");
            dttPO.Columns.Add(dctPO);
            dctPO = new DataColumn("OrderDate");
            dttPO.Columns.Add(dctPO);
            dctPO = new DataColumn("Party");
            dttPO.Columns.Add(dctPO);
            dctPO = new DataColumn("purchasefortype");
            dttPO.Columns.Add(dctPO);
            dctPO = new DataColumn("Item");
            dttPO.Columns.Add(dctPO);
            dctPO = new DataColumn("Color");
            dttPO.Columns.Add(dctPO);
            dctPO = new DataColumn("ReqSheetQty");
            dttPO.Columns.Add(dctPO);
            dctPO = new DataColumn("ReqSheetSampling");
            dttPO.Columns.Add(dctPO);
            dctPO = new DataColumn("ReqSheetProd");
            dttPO.Columns.Add(dctPO);
            dctPO = new DataColumn("Qty");
            dttPO.Columns.Add(dctPO);
            dctPO = new DataColumn("RecQty");
            dttPO.Columns.Add(dctPO);
            dctPO = new DataColumn("BalQty");
            dttPO.Columns.Add(dctPO);
            dctPO = new DataColumn("Units");
            dttPO.Columns.Add(dctPO);

            dstdPO.Tables.Add(dttPO);

            DataSet dsStyles1 = objBs.getTransPurchaseOrderPending_Report(ddlItem.SelectedValue, ReportType, ItemPOId, chkUseDate.Checked, From, To, ddlPartyCode.SelectedValue);
            if (dsStyles1.Tables[0].Rows.Count > 0)
            {
                for (int p = 0; p < dsStyles1.Tables[0].Rows.Count; p++)
                {
                    drNewPO = dttPO.NewRow();

                    drNewPO["FullPONo"] = dsStyles1.Tables[0].Rows[p]["FullPONo"].ToString();
                    drNewPO["OrderDate"] = Convert.ToDateTime(dsStyles1.Tables[0].Rows[p]["OrderDate"]).ToString("dd/MM/yyyy");
                    drNewPO["Party"] = dsStyles1.Tables[0].Rows[p]["Party"].ToString();
                    drNewPO["purchasefortype"] = dsStyles1.Tables[0].Rows[p]["purchasefortype"].ToString();
                    drNewPO["Item"] = dsStyles1.Tables[0].Rows[p]["Item"].ToString();
                    drNewPO["Color"] = dsStyles1.Tables[0].Rows[p]["Color"].ToString();
                    drNewPO["Qty"] = dsStyles1.Tables[0].Rows[p]["Qty"].ToString();
                    drNewPO["RecQty"] = Convert.ToDouble(dsStyles1.Tables[0].Rows[p]["RecQty"]).ToString("f2");
                    drNewPO["BalQty"] = dsStyles1.Tables[0].Rows[p]["BalQty"].ToString();

                    DataSet dstyleno = objBs.getstyledescr(dsStyles1.Tables[0].Rows[p]["BuyerOrderId"].ToString());

                    DataSet getcolorchnagedetails1 = objBs.getcolorchnagedescription_Update_Report(dsStyles1.Tables[0].Rows[p]["BuyerOrderId"].ToString(), dsStyles1.Tables[0].Rows[p]["ItemId"].ToString(), dsStyles1.Tables[0].Rows[p]["ColorId"].ToString());
                    if (getcolorchnagedetails1.Tables[0].Rows.Count > 0)
                    {
                        if (dstyleno.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtt;
                            DataRow drNew;
                            DataColumn dct;
                            DataSet dstd = new DataSet();
                            dtt = new DataTable();


                            dct = new DataColumn("PTotalpcs");
                            dtt.Columns.Add(dct);

                            dstd.Tables.Add(dtt);

                            for (int vLoop = 0; vLoop < getcolorchnagedetails1.Tables[0].Rows.Count; vLoop++)
                            {
                                drNew = dtt.NewRow();

                                drNew["PTotalpcs"] = (Convert.ToDouble(getcolorchnagedetails1.Tables[0].Rows[vLoop]["Cqty"]) * Convert.ToDouble(getcolorchnagedetails1.Tables[0].Rows[vLoop]["PrdAvg"]));
                                //drNew[""] = ;
                                dstd.Tables[0].Rows.Add(drNew);
                            }

                            if (dstd.Tables[0].Rows.Count > 0)
                            {
                                double qyu = 0;
                                for (int vLoop1 = 0; vLoop1 < dstd.Tables[0].Rows.Count; vLoop1++)
                                {
                                    qyu = qyu + Convert.ToDouble(dstd.Tables[0].Rows[vLoop1]["PTotalpcs"]);
                                }


                                drNewPO["ReqSheetProd"] = qyu;// g.PTotalpcs.ToString();
                                drNewPO["ReqSheetSampling"] = "0";
                                drNewPO["ReqSheetQty"] = "0";
                            }
                            else
                            {
                                drNewPO["ReqSheetSampling"] = "0";
                                drNewPO["ReqSheetProd"] = "0";
                                drNewPO["ReqSheetQty"] = "0";
                            }
                        }
                        else
                        {
                            drNewPO["ReqSheetSampling"] = "0";
                            drNewPO["ReqSheetProd"] = "0";
                            drNewPO["ReqSheetQty"] = "0";
                        }


                    }
                    else
                    {
                        drNewPO["ReqSheetSampling"] = "0";
                        drNewPO["ReqSheetProd"] = "0";
                        drNewPO["ReqSheetQty"] = "0";
                    }

                    //DataSet getcolorchnagedetails = objBs.getcolorchnagedescription_Update_Report(dsStyles1.Tables[0].Rows[p]["BuyerOrderId"].ToString(), dsStyles1.Tables[0].Rows[p]["ItemId"].ToString(), dsStyles1.Tables[0].Rows[p]["ColorId"].ToString());
                    //if (getcolorchnagedetails.Tables[0].Rows.Count > 0)
                    //{
                    //    var result = from r in getcolorchnagedetails.Tables[0].AsEnumerable()
                    //                 group r by new
                    //                 {
                    //                     itemmasterid = r["itemmasterid"],
                    //                     Itemgroupid = r["Itemgroupid"],
                    //                     Itemcolorid = r["colorid"],
                    //                     Itemcolor = r["Itemcolor"],
                    //                     Itemgroupname = r["Itemgroupname"],
                    //                     Description = r["Description"],
                    //                     Units = r["Units"],
                    //                     Unitsid = r["Uomid"]
                    //                 } into g
                    //                 select new
                    //                 {
                    //                     Itemgroupname = g.Key.Itemgroupname,
                    //                     Itemgroupid = g.Key.Itemgroupid,

                    //                     Description = g.Key.Description,
                    //                     itemmasterid = g.Key.itemmasterid,

                    //                     Itemcolor = g.Key.Itemcolor,
                    //                     Itemcolorid = g.Key.Itemcolorid,

                    //                     Units = g.Key.Units,
                    //                     Unitsid = g.Key.Unitsid,
                    //                     STotalpcs = g.Sum(x => Convert.ToDouble(x["Stotpcs"])), //Cqty*SmpAvg
                    //                     PTotalpcs = g.Sum(x => Convert.ToDouble(x["Ptotpcs"])),//Cqty*PrdAvg
                    //                     Totalpcs = g.Sum(x => Convert.ToDouble(x["Cqty"])),
                    //                 };

                    //    foreach (var g in result)
                    //    {
                    //        //drNew1 = dtt1.NewRow();
                    //        //drNew1["Itemgroupname"] = g.Itemgroupname;
                    //        //drNew1["Itemgroupid"] = g.Itemgroupid;
                    //        //drNew1["Description"] = g.Description;
                    //        //drNew1["itemmasterid"] = g.itemmasterid;
                    //        //drNew1["Itemcolor"] = g.Itemcolor;
                    //        //drNew1["Itemcolorid"] = g.Itemcolorid;
                    //        //drNew1["Units"] = g.Units;
                    //        //drNew1["Unitsid"] = g.Unitsid;
                    //        //drNew1["STotalpcs"] = g.STotalpcs;
                    //        //drNew1["PTotalpcs"] = g.PTotalpcs;
                    //        //drNew1["Totalpcs"] = g.Totalpcs;
                    //        //ds1.Tables[0].Rows.Add(drNew1);
                    //        drNewPO["ReqSheetSampling"] = g.STotalpcs.ToString();
                    //        drNewPO["ReqSheetProd"] = g.PTotalpcs.ToString();
                    //        drNewPO["ReqSheetQty"] = g.Totalpcs.ToString();
                    //    }
                    //}
                    //else
                    //{
                    //    drNewPO["ReqSheetSampling"] = "0";
                    //    drNewPO["ReqSheetProd"] = "0";
                    //    drNewPO["ReqSheetQty"] = "0";
                    //}

                    dstdPO.Tables[0].Rows.Add(drNewPO);
                }
            }

            if (dstdPO.Tables[0].Rows.Count > 0)
            {
                gvPOPending.DataSource = dstdPO;
                gvPOPending.DataBind();
            }
            else
            {
                gvPOPending.DataSource = null;
                gvPOPending.DataBind();
            }

            #endregion
        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename= PurchaseOrderPendingReport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            Excel.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void gvOrderReport_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TtlReqSheetQty += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ReqSheetQty"));
                TtlReqSheetSamplingQty += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ReqSheetSampling"));
                TtlReqSheetProdQty += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ReqSheetProd"));
                TtlQty += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Qty"));
                TtlRecQty += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "RecQty"));
                TtlBalQty += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "BalQty"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[5].Text = "Total";
                e.Row.Cells[6].Text = TtlReqSheetQty.ToString("f2");
                e.Row.Cells[7].Text = TtlReqSheetSamplingQty.ToString("f2");
                e.Row.Cells[8].Text = TtlReqSheetProdQty.ToString("f2");
                e.Row.Cells[9].Text = TtlQty.ToString("f2");
                e.Row.Cells[10].Text = TtlRecQty.ToString("f2");
                e.Row.Cells[11].Text = TtlBalQty.ToString("f2");
            }
        }
    }
}


