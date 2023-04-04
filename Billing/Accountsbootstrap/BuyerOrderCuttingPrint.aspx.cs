using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using DataLayer;
using CommonLayer;
using System.Text;
using System.Data;
using System.Globalization;

namespace Billing.Accountsbootstrap
{
    public partial class BuyerOrderCuttingPrint : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();

        double Qty = 0; double Amount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string BuyerOrderCuttingId = Request.QueryString.Get("BuyerOrderCuttingId");
                if (BuyerOrderCuttingId != null && BuyerOrderCuttingId != "")
                {
                    int TotalQty = 0;

                    #region BuyerOrderCutting Print

                    DataSet ds = objBs.BuyerOrderCuttingPrint(Convert.ToInt32(BuyerOrderCuttingId));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataSet dsCompanyDetails = objBs.GetSelectCompanyDetails(Convert.ToInt32(ds.Tables[0].Rows[0]["CompanyId"].ToString()));
                        lblFCompany.Text = dsCompanyDetails.Tables[0].Rows[0]["CompanyName"].ToString();
                        lblCoName.Text = dsCompanyDetails.Tables[0].Rows[0]["CompanyName"].ToString();

                        lblFAddress.Text = dsCompanyDetails.Tables[0].Rows[0]["Address"].ToString();
                        lblFAreaandPincode.Text = dsCompanyDetails.Tables[0].Rows[0]["Area"].ToString() + " - " + dsCompanyDetails.Tables[0].Rows[0]["Pincode"].ToString();
                        lblFGST.Text = dsCompanyDetails.Tables[0].Rows[0]["Tin"].ToString();

                        lblFPhone.Text = dsCompanyDetails.Tables[0].Rows[0]["PhoneNo"].ToString();
                        lblFMobile.Text = dsCompanyDetails.Tables[0].Rows[0]["MobileNo"].ToString();

                        lblFax.Text = dsCompanyDetails.Tables[0].Rows[0]["Fax"].ToString();
                        lblFEmail.Text = dsCompanyDetails.Tables[0].Rows[0]["Email"].ToString();

                        lblcompanyname.Text = ds.Tables[0].Rows[0]["LedgerName"].ToString();
                        lbladdress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                        lblCityandPincode.Text = ds.Tables[0].Rows[0]["City"].ToString() + " - " + ds.Tables[0].Rows[0]["Pincode"].ToString();
                        lblArea.Text = ds.Tables[0].Rows[0]["Area"].ToString();

                        lblphoneno.Text = ds.Tables[0].Rows[0]["PhoneNo"].ToString();
                        lblGST.Text = ds.Tables[0].Rows[0]["GSTIN"].ToString();

                        lblProcessOrderNo.Text = ds.Tables[0].Rows[0]["FullCuttingNo"].ToString();
                        lblOrderDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["CuttingDate"]).ToString("dd/MM/yyyy");
                        lblOrderDateBetween.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["FromDate"]).ToString("dd/MM/yyyy") + "  To  " + Convert.ToDateTime(ds.Tables[0].Rows[0]["ToDate"]).ToString("dd/MM/yyyy");
                        

                        #region

                        DataTable DTS = new DataTable();
                        DTS.Columns.Add(new DataColumn("ExcNo"));
                        DTS.Columns.Add(new DataColumn("StyleNo"));
                        DTS.Columns.Add(new DataColumn("Description"));
                        DTS.Columns.Add(new DataColumn("Color"));
                        DTS.Columns.Add(new DataColumn("Qty"));

                        DataSet dsSizes = objBs.getCuttingEntrySizesPrint(Convert.ToInt32(BuyerOrderCuttingId));
                        foreach (DataRow dr in dsSizes.Tables[0].Rows)
                        {
                            DTS.Columns.Add(new DataColumn(dr["Size"].ToString()));
                        }

                        DataSet dsSizesQty = objBs.getCuttingEntrySizesQtyPrint(Convert.ToInt32(BuyerOrderCuttingId));

                        DataSet dsStyle = objBs.getCuttingEntryStylePrint(Convert.ToInt32(BuyerOrderCuttingId));
                        foreach (DataRow dr in dsStyle.Tables[0].Rows)
                        {

                            DataRow DR1 = DTS.NewRow();
                            DR1["ExcNo"] = dr["ExcNo"];
                            DR1["StyleNo"] = dr["StyleNo"];
                            DR1["Description"] = dr["Description"];
                            DR1["Color"] = dr["Color"];
                            DR1["Qty"] = dr["CQty"];

                            TotalQty += Convert.ToInt32(dr["CQty"]);

                            foreach (DataRow DRsS in dsSizes.Tables[0].Rows)
                            {
                                string Size = DRsS["Size"].ToString();
                                string SizeId = DRsS["SizeId"].ToString();

                                DataRow[] RowsQty = dsSizesQty.Tables[0].Select("BuyerOrderCuttingId='" + dr["BuyerOrderCuttingId"] + "' and RowId='" + dr["RowId"] + "' and SizeId='" + SizeId + "' ");
                                if (RowsQty.Length > 0)
                                {
                                    DR1[Size] = RowsQty[0]["CQty"];
                                }
                                else
                                {
                                    DR1[Size] = "0";
                                }
                            }

                            DTS.Rows.Add(DR1);

                        }

                        DataRow DRS2 = DTS.NewRow();
                        DRS2["Color"] = "Total :";
                        DRS2["Qty"] = TotalQty.ToString("f0");
                        DTS.Rows.Add(DRS2);

                        gvCuttingProcessEntryStyles.DataSource = DTS;
                        gvCuttingProcessEntryStyles.DataBind();

                        #endregion

                        DateTime From = DateTime.ParseExact(Convert.ToDateTime(ds.Tables[0].Rows[0]["FromDate"]).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime To = DateTime.ParseExact(Convert.ToDateTime(ds.Tables[0].Rows[0]["ToDate"]).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);


                        DataSet dsRS = objBs.RequirementSheetDetails1(ds.Tables[0].Rows[0]["BuyerOrderId"].ToString(),Convert.ToBoolean(false), From, To, "All");
                        if (dsRS.Tables[0].Rows.Count > 0)
                        {
                            DataSet dsRSStyle = objBs.RequirementSheetDetails2(ds.Tables[0].Rows[0]["BuyerOrderId"].ToString(), Convert.ToBoolean(false), From, To, "All", "", "1");

                            #region

                            DataTable DT = new DataTable();

                            DT.Columns.Add(new DataColumn("Column1"));
                            DT.Columns.Add(new DataColumn("Column2"));
                            DT.Columns.Add(new DataColumn("Column3"));
                            DT.Columns.Add(new DataColumn("Column4"));
                            DT.Columns.Add(new DataColumn("Column5"));

                            foreach (DataRow DR in dsRS.Tables[0].Rows)
                            {
                                DataRow DR1 = DT.NewRow();
                                DR1["Column1"] = "Order Code :";
                                DR1["Column2"] = DR["ExcNo"].ToString();
                                DR1["Column3"] = "Shipment Date :";
                                DR1["Column4"] = Convert.ToDateTime(DR["ShipmentDate"]).ToString("dd/MM/yyyy");
                                DT.Rows.Add(DR1);

                                DataRow DR2 = DT.NewRow();
                                DR2["Column1"] = "Buyer PoNo :";
                                DR2["Column2"] = DR["BuyerPoNo"].ToString();
                                DR2["Column3"] = "Issue Date :";
                                DR2["Column4"] = Convert.ToDateTime(DR["OrderDate"]).ToString("dd/MM/yyyy");
                                DT.Rows.Add(DR2);

                                DataRow DR3 = DT.NewRow();
                                DR3["Column1"] = "Main Fabric :";
                                DR3["Column2"] = DR["description"].ToString();
                                DR3["Column3"] = "";
                                DR3["Column4"] = "";
                                DT.Rows.Add(DR3);

                                DataRow DR4 = DT.NewRow();
                                DR4["Column1"] = "";
                                DT.Rows.Add(DR4);

                                DataRow[] Rows = dsRSStyle.Tables[0].Select("RequirementId='" + DR["RequirementId"] + "'");
                                for (int i = 0; i < Rows.Length; i++)
                                {
                                    DataRow DR5 = DT.NewRow();
                                    DR5["Column1"] = Rows[i]["Itemgroupname"].ToString();
                                    DR5["Column2"] = Rows[i]["Description"].ToString();
                                    DR5["Column3"] = Rows[i]["Color"].ToString();
                                    DR5["Column4"] = Rows[i]["ProductionQty"].ToString();
                                    DR5["Column5"] = Rows[i]["Units"].ToString();
                                    DT.Rows.Add(DR5);
                                }

                                DataRow DR6 = DT.NewRow();
                                DR6["Column1"] = "";
                                DT.Rows.Add(DR6);

                                DataRow DR7 = DT.NewRow();
                                DR7["Column1"] = "";
                                DT.Rows.Add(DR7);

                            }

                            #endregion

                            gvRequirementSheetDetails.Caption = "RequirementSheet Details";
                            gvRequirementSheetDetails.DataSource = DT;
                            gvRequirementSheetDetails.DataBind();
                        }
                        else
                        {
                            gvRequirementSheetDetails.DataSource = null;
                            gvRequirementSheetDetails.DataBind();
                        }
                    }


                    #endregion


                    DateTime FromDate = DateTime.ParseExact(Convert.ToDateTime(ds.Tables[0].Rows[0]["FromDate"]).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime ToDate = DateTime.ParseExact(Convert.ToDateTime(ds.Tables[0].Rows[0]["ToDate"]).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    int TotalDays = Convert.ToInt32((ToDate - FromDate).TotalDays);
                    double PerDayQty = TotalQty / TotalDays;

                    DataTable DTT = new DataTable();
                    DTT.Columns.Add(new DataColumn("Date"));
                    DTT.Columns.Add(new DataColumn("Qty"));

                    for (int i = 1; i <= TotalDays; i++)
                    {
                        DataRow DR1 = DTT.NewRow();
                        DR1["Date"] = Convert.ToDateTime(FromDate.AddDays(i)).ToString("dd/MM/yyyy");
                        DR1["Qty"] = PerDayQty;
                        DTT.Rows.Add(DR1);
                    }

                    gvDailyTarget.DataSource = DTT;
                    gvDailyTarget.DataBind();

                    DataSet dsSketch = objBs.getCuttingEntrySketchPrint(Convert.ToInt32(BuyerOrderCuttingId));
                    if (dsSketch.Tables[0].Rows.Count > 0)
                    {
                        #region

                        DataTable DTBOS = new DataTable();
                        DTBOS.Columns.Add(new DataColumn("Sketch1"));
                        DTBOS.Columns.Add(new DataColumn("Sketch2"));
                        DTBOS.Columns.Add(new DataColumn("Sketch3"));
                        DTBOS.Columns.Add(new DataColumn("Sketch4"));
                        DTBOS.Columns.Add(new DataColumn("Sketch5"));
                        DTBOS.Columns.Add(new DataColumn("Sketch6"));
                        DTBOS.Columns.Add(new DataColumn("Sketch7"));

                        DataRow DR1 = DTBOS.NewRow();

                        if (dsSketch.Tables[0].Rows.Count >= 1)
                        {
                            string sketch1 = dsSketch.Tables[0].Rows[0]["Sketch"].ToString();

                            if (sketch1 != "")
                            {

                                DR1["Sketch1"] = dsSketch.Tables[0].Rows[0]["Sketch"].ToString();
                            }
                            else
                            {
                                DR1["Sketch1"] = "~/sampling/noimage2.jpg";
                            }
                        }
                        else
                        {
                            gvProcessEntryStylesImages.Columns[0].Visible = false;
                        }
                        if (dsSketch.Tables[0].Rows.Count >= 2)
                        {
                           // DR1["Sketch2"] = dsSketch.Tables[0].Rows[1]["Sketch"].ToString();
                            string sketch2 = dsSketch.Tables[0].Rows[1]["Sketch"].ToString();

                            if (sketch2 != "")
                            {

                                DR1["Sketch2"] = dsSketch.Tables[0].Rows[1]["Sketch"].ToString();
                            }
                            else
                            {
                                DR1["Sketch2"] = "~/sampling/noimage2.jpg";
                            }
                        }
                        else
                        {
                            gvProcessEntryStylesImages.Columns[1].Visible = false;
                        }
                        if (dsSketch.Tables[0].Rows.Count >= 3)
                        {
                          //  DR1["Sketch3"] = dsSketch.Tables[0].Rows[2]["Sketch"].ToString();
                            string sketch3 = dsSketch.Tables[0].Rows[2]["Sketch"].ToString();

                            if (sketch3 != "")
                            {

                                DR1["Sketch3"] = dsSketch.Tables[0].Rows[2]["Sketch"].ToString();
                            }
                            else
                            {
                                DR1["Sketch3"] = "~/sampling/noimage2.jpg";
                            }
                        }
                        else
                        {
                            gvProcessEntryStylesImages.Columns[2].Visible = false;
                        }
                        if (dsSketch.Tables[0].Rows.Count >= 4)
                        {
                           // DR1["Sketch4"] = dsSketch.Tables[0].Rows[3]["Sketch"].ToString();
                            string sketch4 = dsSketch.Tables[0].Rows[3]["Sketch"].ToString();

                            if (sketch4 != "")
                            {

                                DR1["Sketch4"] = dsSketch.Tables[0].Rows[3]["Sketch"].ToString();
                            }
                            else
                            {
                                DR1["Sketch4"] = "~/sampling/noimage2.jpg";
                            }
                        }
                        else
                        {
                            gvProcessEntryStylesImages.Columns[3].Visible = false;
                        }
                        if (dsSketch.Tables[0].Rows.Count >= 5)
                        {
                           // DR1["Sketch5"] = dsSketch.Tables[0].Rows[4]["Sketch"].ToString();
                            string sketch5 = dsSketch.Tables[0].Rows[4]["Sketch"].ToString();

                            if (sketch5 != "")
                            {

                                DR1["Sketch5"] = dsSketch.Tables[0].Rows[4]["Sketch"].ToString();
                            }
                            else
                            {
                                DR1["Sketch5"] = "~/sampling/noimage2.jpg";
                            }
                        }
                        else
                        {
                            gvProcessEntryStylesImages.Columns[4].Visible = false;
                        }
                        if (dsSketch.Tables[0].Rows.Count >= 6)
                        {
                           // DR1["Sketch6"] = dsSketch.Tables[0].Rows[5]["Sketch"].ToString();
                            string sketch6 = dsSketch.Tables[0].Rows[5]["Sketch"].ToString();

                            if (sketch6 != "")
                            {

                                DR1["Sketch6"] = dsSketch.Tables[0].Rows[5]["Sketch"].ToString();
                            }
                            else
                            {
                                DR1["Sketch6"] = "~/sampling/noimage2.jpg";
                            }
                        }
                        else
                        {
                            gvProcessEntryStylesImages.Columns[5].Visible = false;
                        }
                        if (dsSketch.Tables[0].Rows.Count >= 7)
                        {
                           // DR1["Sketch7"] = dsSketch.Tables[0].Rows[6]["Sketch"].ToString();
                            string sketch7 = dsSketch.Tables[0].Rows[6]["Sketch"].ToString();

                            if (sketch7 != "")
                            {

                                DR1["Sketch7"] = dsSketch.Tables[0].Rows[6]["Sketch"].ToString();
                            }
                            else
                            {
                                DR1["Sketch7"] = "~/sampling/noimage2.jpg";
                            }
                        }
                        else
                        {
                            gvProcessEntryStylesImages.Columns[6].Visible = false;
                        }

                        DTBOS.Rows.Add(DR1);

                        #endregion

                        gvProcessEntryStylesImages.DataSource = DTBOS;
                        gvProcessEntryStylesImages.DataBind();
                    }
                    
                }
            }
        }

        protected void gvItemProcessOrder_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Qty += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Qty"));
                Amount += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[4].Text = "Total :";
                e.Row.Cells[6].Text = Qty.ToString("f2");
                e.Row.Cells[8].Text = Amount.ToString("f2");
            }
        }

        protected void btnexit_Click(object sender, EventArgs e)
        {
            Response.Redirect("BuyerOrderCuttingGrid.aspx");
        }

    }
}