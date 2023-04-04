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
using System.Diagnostics;

namespace Billing.Accountsbootstrap
{
    public partial class ExeWisecostingReport : System.Web.UI.Page
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
                DataSet dsExcNo = objBs.getAllExcNo("All");
                if (dsExcNo.Tables[0].Rows.Count > 0)
                {
                    ddlExcNo.DataSource = dsExcNo.Tables[0];
                    ddlExcNo.DataTextField = "ExcNo";
                    ddlExcNo.DataValueField = "BuyerOrderId";
                    ddlExcNo.DataBind();
                    ddlExcNo.Items.Insert(0, "Select ExcNo");
                }

            }
        }


        protected void btnExit_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("Cuttingdetailsnew.aspx");
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            if (ddlExcNo.SelectedValue != "" && ddlExcNo.SelectedValue != "" && ddlExcNo.SelectedValue != "Select ExcNo")
            {
                DataSet ds = objBs.getBuyerOrdervaluesExcel(Convert.ToInt32(ddlExcNo.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    #region

                    DataTable dt1 = new DataTable();
                    dt1.Columns.Add(new DataColumn("Column1"));
                    dt1.Columns.Add(new DataColumn("Column2"));
                    dt1.Columns.Add(new DataColumn("Column3"));
                    dt1.Columns.Add(new DataColumn("Column4"));
                    dt1.Columns.Add(new DataColumn("Column5"));
                    dt1.Columns.Add(new DataColumn("Column6"));

                    DataRow DR1 = dt1.NewRow();
                    DR1["Column1"] = "ExcNo.:";
                    DR1["Column2"] = ds.Tables[0].Rows[0]["ExcNo"].ToString();
                    DR1["Column3"] = "Main Fabric :";
                    DR1["Column4"] = ds.Tables[0].Rows[0]["ItemCode"].ToString();
                    DR1["Column5"] = "Delivery Date:";
                    DR1["Column6"] = Convert.ToDateTime(ds.Tables[0].Rows[0]["DeliveryDate"]).ToString("dd/MM/yyyy");
                    dt1.Rows.Add(DR1);

                    DataRow DR2 = dt1.NewRow();
                    DR2["Column1"] = "Buyer PONo.:";
                    DR2["Column2"] = ds.Tables[0].Rows[0]["BuyerPONo"].ToString();
                    DR2["Column3"] = "Shipment Mode :";
                    DR2["Column4"] = ds.Tables[0].Rows[0]["ShipmentMode"].ToString();
                    DR2["Column5"] = "Order Date:";
                    DR2["Column6"] = Convert.ToDateTime(ds.Tables[0].Rows[0]["OrderDate"]).ToString("dd/MM/yyyy");
                    dt1.Rows.Add(DR2);

                    #endregion
                    gvCuttingDetails1.DataSource = dt1;
                    gvCuttingDetails1.DataBind();
                }


                #region Cutting Wastage

                DataSet gvcuttingwastage = objBs.gvcuttignwastage(ddlExcNo.SelectedValue, lblcuttingwastage.Text);
                if (gvcuttingwastage.Tables[0].Rows.Count > 0)
                {
                    #region

                    DataTable dt1 = new DataTable();
                    dt1.Columns.Add(new DataColumn("styleno"));
                    dt1.Columns.Add(new DataColumn("SamplingCostingId"));
                    dt1.Columns.Add(new DataColumn("desc"));
                    dt1.Columns.Add(new DataColumn("color"));
                    dt1.Columns.Add(new DataColumn("Qty"));
                    dt1.Columns.Add(new DataColumn("Cqty"));
                    dt1.Columns.Add(new DataColumn("Rstock"));
                    dt1.Columns.Add(new DataColumn("Ustock"));
                    dt1.Columns.Add(new DataColumn("Cuavg"));
                    dt1.Columns.Add(new DataColumn("Cosavg"));
                    dt1.Columns.Add(new DataColumn("Diff"));
                    dt1.Columns.Add(new DataColumn("cutwasteQty"));
                    dt1.Columns.Add(new DataColumn("cutwastePerc"));


                    for (int i = 0; i < gvcuttingwastage.Tables[0].Rows.Count; i++)
                    {

                        string SamplingCostingId = gvcuttingwastage.Tables[0].Rows[i]["SamplingCostingId"].ToString();
                        string StyleNo = gvcuttingwastage.Tables[0].Rows[i]["StyleNo"].ToString();
                        string Description = gvcuttingwastage.Tables[0].Rows[i]["Description"].ToString();
                        string Color = gvcuttingwastage.Tables[0].Rows[i]["Color"].ToString();

                        double Qty = Convert.ToDouble(gvcuttingwastage.Tables[0].Rows[i]["oriQty"]);
                        double CQty = Convert.ToDouble(gvcuttingwastage.Tables[0].Rows[i]["CQty"]);

                        double RequiredStock = Convert.ToDouble(gvcuttingwastage.Tables[0].Rows[i]["RequiredStock"]);
                        double UsedQty = Convert.ToDouble(gvcuttingwastage.Tables[0].Rows[i]["UsedQty"]);



                        double cuttingavg = Convert.ToDouble(gvcuttingwastage.Tables[0].Rows[i]["cuttingavg"]);
                        double costingavg = Convert.ToDouble(gvcuttingwastage.Tables[0].Rows[i]["costingavg"]);


                        double Difference = costingavg - cuttingavg;

                        double cuttingwasteqty = UsedQty - (CQty * costingavg);

                        double cuwasterper = (cuttingwasteqty / UsedQty) * 100;

                        DataRow DR1 = dt1.NewRow();
                        DR1["styleno"] = StyleNo.ToString();
                        DR1["SamplingCostingId"] = SamplingCostingId.ToString();
                        DR1["desc"] = Description.ToString();
                        DR1["color"] = Color.ToString();
                        DR1["Qty"] = Convert.ToDouble(Qty).ToString("0.00");
                        DR1["CQty"] = Convert.ToDouble(CQty).ToString("0.00");
                        DR1["Rstock"] = Convert.ToDouble(RequiredStock).ToString("0.00");
                        DR1["Ustock"] = Convert.ToDouble(UsedQty).ToString("0.00");
                        DR1["Cuavg"] = Convert.ToDouble(cuttingavg).ToString("0.00");
                        DR1["Cosavg"] = Convert.ToDouble(costingavg).ToString("0.00");
                        DR1["Diff"] = Convert.ToDouble(Difference).ToString("0.00");
                        DR1["cutwasteQty"] = Convert.ToDouble(cuttingwasteqty).ToString("0.00");
                        DR1["cutwastePerc"] = Convert.ToDouble(cuwasterper).ToString("0.00");
                        dt1.Rows.Add(DR1);

                    }

                    gridcuttingwastage.DataSource = dt1;
                    gridcuttingwastage.DataBind();
                }
                else
                {
                    gridcuttingwastage.DataSource = null;
                    gridcuttingwastage.DataBind();
                }

                #endregion

                // Get Accessories 

                string cond = "";

                if (gridcuttingwastage.Rows.Count > 0)
                {

                    DataTable dt3 = new DataTable();
                    DataTable dt2 = new DataTable();
                    dt2.Columns.Add(new DataColumn("StyleNo"));
                    dt2.Columns.Add(new DataColumn("Scolor"));
                    dt2.Columns.Add(new DataColumn("Description"));
                    dt2.Columns.Add(new DataColumn("itemname"));
                    dt2.Columns.Add(new DataColumn("color"));
                    dt2.Columns.Add(new DataColumn("peccutting"));
                    dt2.Columns.Add(new DataColumn("PrdAvg"));
                    dt2.Columns.Add(new DataColumn("ProductionAvg"));
                    dt2.Columns.Add(new DataColumn("Rate"));
                    dt2.Columns.Add(new DataColumn("valu"));


                    for (int vLoop = 0; vLoop < gridcuttingwastage.Rows.Count; vLoop++)
                    {

                        Label lblsamplingcosting = (Label)gridcuttingwastage.Rows[vLoop].FindControl("lblsamplingcosting");
                        Label lblcolor = (Label)gridcuttingwastage.Rows[vLoop].FindControl("lblcolor");
                        double totrate = 0;

                        DataTable getaccdetails = objBs.gvgetaccessoriesdetails(ddlExcNo.SelectedValue, lblsamplingcosting.Text, lblcuttingwastage.Text);
                        if (getaccdetails.Rows.Count > 0)
                        {
                            for (int i = 0; i < getaccdetails.Rows.Count; i++)
                            {

                                string StyleNo = getaccdetails.Rows[i]["StyleNo"].ToString();
                                string Description = getaccdetails.Rows[i]["Description"].ToString();

                                string itemname = getaccdetails.Rows[i]["itemname"].ToString();
                                string color = getaccdetails.Rows[i]["color"].ToString();
                                string peccutting = getaccdetails.Rows[i]["peccutting"].ToString();
                                string PrdAvg = getaccdetails.Rows[i]["PrdAvg"].ToString();
                                string ProductionAvg = getaccdetails.Rows[i]["ProductionAvg"].ToString();
                                string Rate = getaccdetails.Rows[i]["Rate"].ToString();
                                string valu = getaccdetails.Rows[i]["valu"].ToString();
                                totrate += Convert.ToDouble(valu);


                                DataRowView row = getaccdetails.DefaultView[i];
                                if (i == getaccdetails.DefaultView.Count - 1)
                                {
                                    // dosomething

                                    DataRow DR2 = dt2.NewRow();
                                    DR2["styleno"] = StyleNo.ToString();
                                    DR2["Description"] = Description.ToString();
                                    DR2["Scolor"] = lblcolor.Text;
                                    DR2["itemname"] = itemname.ToString();
                                    DR2["color"] = color.ToString();
                                    DR2["peccutting"] = Convert.ToDouble(peccutting).ToString("0.00");
                                    DR2["PrdAvg"] = Convert.ToDouble(PrdAvg).ToString("0.00");
                                    DR2["ProductionAvg"] = Convert.ToDouble(ProductionAvg).ToString("0.00");
                                    DR2["Rate"] = Convert.ToDouble(Rate).ToString("0.00");
                                    DR2["valu"] = Convert.ToDouble(valu).ToString("0.00");
                                    dt2.Rows.Add(DR2);
                                    dt3.Merge(dt2);


                                    DR2 = dt2.NewRow();
                                    DR2["styleno"] = "";
                                    DR2["Description"] = "";
                                    DR2["Scolor"] = "";
                                    DR2["itemname"] = "";
                                    DR2["color"] = "";
                                    DR2["peccutting"] = "";
                                    DR2["PrdAvg"] = "";
                                    DR2["ProductionAvg"] = "";
                                    DR2["Rate"] = "Total ";
                                    DR2["valu"] = Convert.ToDouble(totrate).ToString("0.00");
                                    dt2.Rows.Add(DR2);
                                    dt3.Merge(dt2);

                                }
                                else
                                {
                                    DataRow DR2 = dt2.NewRow();
                                    DR2["styleno"] = StyleNo.ToString();
                                    DR2["Description"] = Description.ToString();
                                    DR2["Scolor"] = lblcolor.Text;
                                    DR2["itemname"] = itemname.ToString();
                                    DR2["color"] = (color).ToString();
                                    DR2["peccutting"] = Convert.ToDouble(peccutting).ToString("0.00");
                                    DR2["PrdAvg"] = Convert.ToDouble(PrdAvg).ToString("0.00");
                                    DR2["ProductionAvg"] = Convert.ToDouble(ProductionAvg).ToString("0.00");
                                    DR2["Rate"] = Convert.ToDouble(Rate).ToString("0.00");
                                    DR2["valu"] = Convert.ToDouble(valu).ToString("0.00");
                                    dt2.Rows.Add(DR2);
                                    dt3.Merge(dt2);
                                }


                            }



                        }
                        else
                        {
                            //gvaccdetails.DataSource = null;
                            //gvaccdetails.DataBind();
                        }
                    }
                    gvaccdetails.DataSource = dt2;
                    gvaccdetails.DataBind();
                    //cond += " '" + lblAvlStock.Text + "' ,";
                }
                else
                {
                    gvaccdetails.DataSource = null;
                    gvaccdetails.DataBind();
                }


                DataTable dsgetproductionwastage = objBs.gvproductionwastage(ddlExcNo.SelectedValue);
                if (dsgetproductionwastage.Rows.Count > 0)
                {
                    Gridproductionwastage.DataSource = dsgetproductionwastage;
                    Gridproductionwastage.DataBind();
                }
                else
                {
                    Gridproductionwastage.DataSource = null;
                    Gridproductionwastage.DataBind();
                }

                DataTable dsgetShippingwastage = objBs.gvshippingwastagesummary(ddlExcNo.SelectedValue);
                if (dsgetShippingwastage.Rows.Count > 0)
                {
                    gridshippingwastage.DataSource = dsgetShippingwastage;
                    gridshippingwastage.DataBind();
                }
                else
                {
                    gridshippingwastage.DataSource = null;
                    gridshippingwastage.DataBind();
                }

                // COSTING PROCESS
                DataSet bindallprocess = objBs.getallprocessfromrequirementssheet(ddlExcNo.SelectedValue);
                if (bindallprocess.Tables[0].Rows.Count > 0)
                {

                    DataTable dtt = new DataTable();
                    //  dtt.Columns.Add(new DataColumn("Samplingid"));
                    dtt.Columns.Add(new DataColumn("Styleno"));
                    dtt.Columns.Add(new DataColumn("Desc"));
                    dtt.Columns.Add(new DataColumn("Color"));
                    dtt.Columns.Add(new DataColumn("FabricRate"));
                    dtt.Columns.Add(new DataColumn("fabricon"));
                    dtt.Columns.Add(new DataColumn("perpcsrate"));
                    dtt.Columns.Add(new DataColumn("rejection"));
                    dtt.Columns.Add(new DataColumn("Access"));
                    foreach (DataRow dr in bindallprocess.Tables[0].Rows)
                    {
                        dtt.Columns.Add(new DataColumn(dr["Process"].ToString()));
                    }
                    dtt.Columns.Add(new DataColumn("Overhead"));
                    dtt.Columns.Add(new DataColumn("TotalCost"));
                    dtt.Columns.Add(new DataColumn("Saleprice"));
                    dtt.Columns.Add(new DataColumn("PL"));



                    // Get fabricRate and  access rate First
                    DataSet getallstylenowithcolor = objBs.getallstylebaswedonbuyerorder(ddlExcNo.SelectedValue);
                    foreach (DataRow dr in getallstylenowithcolor.Tables[0].Rows)
                    {
                        int Total = 0;

                        DataRow DRM1 = dtt.NewRow();
                        DRM1["Styleno"] = dr["StyleNo"];
                        DRM1["desc"] = dr["Description"];
                        DRM1["Color"] = dr["Color"];
                        string colorid = dr["Colorid"].ToString();
                        string samplingid = dr["SamplingCostingId"].ToString();
                        double totprodcost = Convert.ToDouble(dr["TotalPrdCostINR"]);


                        // get fabric rate
                        DataSet getfabrate = objBs.getfabrcirate(ddlExcNo.SelectedValue, samplingid, colorid, lblcuttingwastage.Text);
                        if (getfabrate.Tables[0].Rows.Count > 0)
                        {

                            double fabrate = Convert.ToDouble(getfabrate.Tables[0].Rows[0]["rate"]);
                            double fabcon = Convert.ToDouble(getfabrate.Tables[0].Rows[0]["cuttingavg"]);


                            DRM1["FabricRate"] = fabrate.ToString("0.00");
                            DRM1["fabricon"] = fabcon.ToString("0.00");
                            DRM1["perpcsrate"] = (fabcon * fabrate).ToString("0.00");
                            double fabpercost = (fabcon * fabrate);
                            DRM1["rejection"] = "0";

                            // GET Access Rate
                            double Accrate = 0;
                            DataSet getaccessrate = objBs.getaccessirate(ddlExcNo.SelectedValue, samplingid, colorid, lblcuttingwastage.Text);
                            if (getaccessrate.Tables[0].Rows.Count > 0)
                            {
                                Accrate = Convert.ToDouble(getaccessrate.Tables[0].Rows[0]["access"]);
                                DRM1["Access"] = Accrate.ToString("0.00");
                            }

                            double totprodrate = 0;
                            foreach (DataRow drr in bindallprocess.Tables[0].Rows)
                            {
                                string ProcessId = drr["ProcessId"].ToString();
                                string Process = drr["Process"].ToString();

                                if (ProcessId != "5")
                                {
                                    // Get process Rate Bind
                                    DataSet getallprocessrate = objBs.getprocessrate(ddlExcNo.SelectedValue, samplingid, colorid, ProcessId);
                                    foreach (DataRow DRsSs in getallprocessrate.Tables[0].Rows)
                                    {
                                        string Rate = DRsSs["Rate"].ToString();
                                        string PId = DRsSs["PId"].ToString();
                                        string process = DRsSs["Process"].ToString();

                                        DataRow[] RowsQtyy = getallstylenowithcolor.Tables[0].Select("SamplingCostingId='" + dr["SamplingCostingId"] + "' and Colorid='" + dr["Colorid"] + "' ");
                                        if (RowsQtyy.Length > 0)
                                        {
                                            DRM1[process] = Rate;
                                            totprodrate +=  Convert.ToDouble(Rate);
                                            //dr[date.ToString("dd/MM/yy")] = dAmt.ToString("f2");
                                        }
                                        else
                                        {
                                            DRM1[process] = "0";
                                        }
                                    }
                                    if(DRM1[Process].ToString() == "")
                                    {
                                        DRM1[Process] = "0";
                                    }
                                }
                                //else
                                //{
                                //    DRM1[Process] = "0";
                                //}




                            }
                            DRM1["Overhead"] = lblboverheadcost.Text;
                            DRM1["TotalCost"] = Convert.ToDouble(totprodrate + Convert.ToDouble(lblboverheadcost.Text) + fabpercost + Accrate).ToString("0.00");
                            DRM1["Saleprice"] = totprodcost;
                            DRM1["PL"] = Convert.ToDouble((totprodrate + Convert.ToDouble(lblboverheadcost.Text) + fabpercost + Accrate) - totprodcost).ToString("0.00");
                            dtt.Rows.Add(DRM1);

                        }

                        #region old code

                        // cond = cond.TrimEnd(',');
                        // cond = cond.Replace(",", "or");
                        //  if (cond != "")
                        //  {


                        //  }


                        //DataSet dsBOSketch = objBs.BuyerOrderSketches(ddlExcNo.SelectedValue);
                        //if (dsBOSketch.Tables[0].Rows.Count > 0)
                        //{
                        //    #region

                        //    DataTable DTS = new DataTable();
                        //    DTS.Columns.Add(new DataColumn("Sketch1"));
                        //    DTS.Columns.Add(new DataColumn("Sketch2"));
                        //    DTS.Columns.Add(new DataColumn("Sketch3"));
                        //    DTS.Columns.Add(new DataColumn("Sketch4"));
                        //    DTS.Columns.Add(new DataColumn("Sketch5"));
                        //    DTS.Columns.Add(new DataColumn("Sketch6"));
                        //    DTS.Columns.Add(new DataColumn("Sketch7"));

                        //    DataRow DR1 = DTS.NewRow();

                        //    if (dsBOSketch.Tables[0].Rows.Count >= 1)
                        //    {
                        //        DR1["Sketch1"] = dsBOSketch.Tables[0].Rows[0]["Sketch"].ToString();
                        //    }
                        //    if (dsBOSketch.Tables[0].Rows.Count >= 2)
                        //    {
                        //        DR1["Sketch2"] = dsBOSketch.Tables[0].Rows[1]["Sketch"].ToString();
                        //    }
                        //    if (dsBOSketch.Tables[0].Rows.Count >= 3)
                        //    {
                        //        DR1["Sketch3"] = dsBOSketch.Tables[0].Rows[2]["Sketch"].ToString();
                        //    }
                        //    if (dsBOSketch.Tables[0].Rows.Count >= 4)
                        //    {
                        //        DR1["Sketch4"] = dsBOSketch.Tables[0].Rows[3]["Sketch"].ToString();
                        //    }
                        //    if (dsBOSketch.Tables[0].Rows.Count >= 5)
                        //    {
                        //        DR1["Sketch5"] = dsBOSketch.Tables[0].Rows[4]["Sketch"].ToString();
                        //    }
                        //    if (dsBOSketch.Tables[0].Rows.Count >= 6)
                        //    {
                        //        DR1["Sketch6"] = dsBOSketch.Tables[0].Rows[5]["Sketch"].ToString();
                        //    }
                        //    if (dsBOSketch.Tables[0].Rows.Count >= 7)
                        //    {
                        //        DR1["Sketch7"] = dsBOSketch.Tables[0].Rows[6]["Sketch"].ToString();
                        //    }

                        //    DTS.Rows.Add(DR1);

                        //    #endregion
                        //    gvCuttingImages.DataSource = DTS;
                        //    gvCuttingImages.DataBind();
                        //}

                        //DataSet dsStyle = objBs.getCuttingQty1(ddlExcNo.SelectedValue);
                        //DataSet dsQty = objBs.getCuttingQty2(ddlExcNo.SelectedValue);
                        //DataSet dsCQty = objBs.getCuttingQty3(ddlExcNo.SelectedValue);

                        //#region

                        //DataSet dsSizes = objBs.getBuyerOrderSizesExcelAll(ddlExcNo.SelectedValue);
                        //RowCount.Text = (3 + Convert.ToInt32(dsSizes.Tables[0].Rows.Count.ToString())).ToString();

                        //DataTable dt = new DataTable();
                        //dt.Columns.Add(new DataColumn("StyleNo"));
                        //dt.Columns.Add(new DataColumn("Description"));
                        //dt.Columns.Add(new DataColumn("Color"));
                        //foreach (DataRow dr in dsSizes.Tables[0].Rows)
                        //{
                        //    dt.Columns.Add(new DataColumn(dr["Size"].ToString()));
                        //}
                        //dt.Columns.Add(new DataColumn("Total"));

                        //foreach (DataRow dr in dsStyle.Tables[0].Rows)
                        //{
                        //    int Total = 0;

                        //    DataRow DRM = dt.NewRow();
                        //    DRM["StyleNo"] = dr["StyleNo"];
                        //    DRM["Description"] = dr["Description"];
                        //    DRM["Color"] = dr["Color"];

                        //    Total += Convert.ToInt32(dr["Qty"]);

                        //    foreach (DataRow DRsS in dsSizes.Tables[0].Rows)
                        //    {
                        //        string Size = DRsS["Size"].ToString();
                        //        string SizeId = DRsS["SizeId"].ToString();

                        //        DataRow[] RowsQty = dsQty.Tables[0].Select("BuyerOrderId='" + dr["BuyerOrderId"] + "' and RowId='" + dr["RowId"] + "'  and SizeId='" + SizeId + "' ");
                        //        if (RowsQty.Length > 0)
                        //        {
                        //            DRM[Size] = RowsQty[0]["Qty"];
                        //        }
                        //        else
                        //        {
                        //            DRM[Size] = "0";
                        //        }
                        //    }

                        //    DRM["Total"] = Total;
                        //    dt.Rows.Add(DRM);


                        //    //Cutting Qty 
                        //    DataRow DRM1 = dt.NewRow();
                        //    DRM1["Color"] = "Cutting";

                        //    Total = 0;

                        //    foreach (DataRow DRsS in dsSizes.Tables[0].Rows)
                        //    {
                        //        string Size = DRsS["Size"].ToString();
                        //        string SizeId = DRsS["SizeId"].ToString();

                        //        DataRow[] RowsQty = dsCQty.Tables[0].Select("BuyerOrderId='" + dr["BuyerOrderId"] + "' and RowId='" + dr["RowId"] + "'  and SizeId='" + SizeId + "' ");
                        //        if (RowsQty.Length > 0)
                        //        {
                        //            DRM1[Size] = RowsQty[0]["RecQty"];
                        //            Total += Convert.ToInt32(RowsQty[0]["RecQty"]);
                        //        }
                        //        else
                        //        {
                        //            DRM1[Size] = "0";
                        //        }
                        //    }

                        //    DRM1["Total"] = Total;
                        //    dt.Rows.Add(DRM1);

                        //}




                        //    if (dt.Rows.Count > 0)
                        //    {
                        //        gvCuttingDetails2.DataSource = dt;
                        //        gvCuttingDetails2.DataBind();

                        //    }
                        //    else
                        //    {
                        //        gvCuttingDetails2.DataSource = null;
                        //        gvCuttingDetails2.DataBind();
                        //    }


                        //}
                        //else
                        //{
                        //    gvCuttingDetails1.DataSource = null;
                        //    gvCuttingDetails1.DataBind();
                        //}
                        #endregion
                    }
                    gridcosting.DataSource = dtt;
                    gridcosting.DataBind();
                }


                // OVer all Summary

                DataTable dt4 = new DataTable();
                //DataTable dt2 = new DataTable();
                dt4.Columns.Add(new DataColumn("StyleNo"));
                dt4.Columns.Add(new DataColumn("Color"));
                dt4.Columns.Add(new DataColumn("Description"));
                dt4.Columns.Add(new DataColumn("shippedqty"));
                dt4.Columns.Add(new DataColumn("currencyname"));
                dt4.Columns.Add(new DataColumn("value"));
                dt4.Columns.Add(new DataColumn("rate"));
                dt4.Columns.Add(new DataColumn("totshippingvalue"));
                dt4.Columns.Add(new DataColumn("salevalue"));
                dt4.Columns.Add(new DataColumn("costvalueINR"));
                dt4.Columns.Add(new DataColumn("NetprofitPL"));

                DataSet bindshippercostingpl = objBs.getoverallcostingpl(ddlExcNo.SelectedValue);
                if (bindshippercostingpl.Tables[0].Rows.Count > 0)
                {

                    for(int z =0; z < bindshippercostingpl.Tables[0].Rows.Count; z++)
                    {
                        string StyleNo = bindshippercostingpl.Tables[0].Rows[z]["StyleNo"].ToString();
                        string Description = bindshippercostingpl.Tables[0].Rows[z]["Description"].ToString();
                        string color = bindshippercostingpl.Tables[0].Rows[z]["color"].ToString();

                        string stylenoid = bindshippercostingpl.Tables[0].Rows[z]["stylenoid"].ToString();
                        string ColorId = bindshippercostingpl.Tables[0].Rows[z]["ColorId"].ToString();
                        string currencyname = bindshippercostingpl.Tables[0].Rows[z]["currencyname"].ToString();
                        string value = bindshippercostingpl.Tables[0].Rows[z]["value"].ToString();
                        string rate = bindshippercostingpl.Tables[0].Rows[z]["rate"].ToString();

                        DataRow DR4 = dt4.NewRow();
                        DR4["styleno"] = StyleNo.ToString();
                        DR4["Description"] = Description.ToString();
                        DR4["color"] = color.ToString();

                        double shipqty = 0;
                        DataSet getshipqty = objBs.getshippedqty(ddlExcNo.SelectedValue, stylenoid, ColorId);
                        if (getshipqty.Tables[0].Rows.Count > 0)
                        {
                            shipqty = Convert.ToDouble(getshipqty.Tables[0].Rows[0]["shipqty"]);
                            DR4["shippedqty"] = shipqty.ToString();
                        }


                        DR4["currencyname"] = currencyname.ToString();
                        DR4["value"] = currencyname.ToString() + Convert.ToDouble(value).ToString("");
                        DR4["rate"] = Convert.ToDouble(rate).ToString("0.00");
                        DR4["totshippingvalue"] = currencyname.ToString() + Convert.ToDouble(shipqty * Convert.ToDouble(rate)).ToString("0.00");
                        DR4["salevalue"] = currencyname.ToString() + (Convert.ToDouble(shipqty * Convert.ToDouble(rate)) * Convert.ToDouble(value)).ToString("0.00");
                        double totalcost = 0;

                        if (gridcosting.Rows.Count > 0)
                        {
                            for (int y = 0; y < gridcosting.Rows.Count; y++)
                            {
                                Label lblStyleno = (Label)gridcosting.Rows[y].FindControl("lblStyleno");
                                Label lblcolor = (Label)gridcosting.Rows[y].FindControl("lblcolor");
                                Label lblTotalCost = (Label)gridcosting.Rows[y].FindControl("lblTotalCost");
                                

                                if (lblStyleno.Text == StyleNo && lblcolor.Text == color)
                                {
                                    totalcost = Convert.ToDouble(lblTotalCost.Text);
                                }

                            }
                        }
                        DR4["costvalueINR"] = Convert.ToDouble(shipqty * totalcost).ToString("0.00");
                        DR4["NetprofitPL"] = Convert.ToDouble((Convert.ToDouble(shipqty * Convert.ToDouble(rate)) * Convert.ToDouble(value)) - Convert.ToDouble(shipqty * totalcost)).ToString("0.00");
                        dt4.Rows.Add(DR4);


                    }
                    gridshippingcostingpl.DataSource = dt4;
                    gridshippingcostingpl.DataBind();
                }


                #endregion
            }
        }



        protected void gvCuttingDetails2_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[2].Text == "Cutting")
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#d0edef");
                    e.Row.Cells[0].BackColor = System.Drawing.Color.White;
                    e.Row.Cells[1].BackColor = System.Drawing.Color.White;

                    //e.Row.ForeColor = System.Drawing.Color.Red;
                    //e.Row.Cells[2].ForeColor = System.Drawing.Color.Black;

                    for (int i = 3; i < Convert.ToInt32(RowCount.Text) + 1; i++)
                    {
                        if (e.Row.Cells[i].Text != "0")
                        {
                            e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
        }
    }
}


//#endregion