<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="Billing.HeaderMaster.Header" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html>
<!doctype html>
<html>
<head>
    <meta charset='utf-8'>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="../Styles/styles.css">
    <script src="http://code.jquery.com/jquery-latest.min.js" type="text/javascript"></script>
    <script src="../Scripts/script.js"></script>
    <link href="../css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../css/sb-admin-2.css" rel="stylesheet" type="text/css" />
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div style="background-color: #336699">
        <table style="width: 100%">
            <tr>
                <td style="width: 15%">
                    <div class="container-fluid" style="background-color: #336699; padding-left: 51px">
                        <a href="../Accountsbootstrap/Home_Page.aspx">
                            <img style="width: 110px; height: 45px" src="../images/ae_logo.png" alt="logo" /></a>
                    </div>
                </td>
                <td style="width: 58%" align="center">
                    <label style="font-size: 20px; color: Black;">
                        Production Management System</label>
                </td>
                <td style="width: 27%">
                    <asp:Label runat="server" ID="lblUserID" Style="font-size: small; color: White; text-align: center"
                        CssClass="label" Visible="false "> </asp:Label>
                    <asp:Label runat="server" ID="lblUser" Style="font-size: small; color: White; text-align: right;
                        color: #a7afc3; text-transform: uppercase;" CssClass="label" Visible="true"> 
                    </asp:Label>
                    <asp:Label ID="Label1" Style="font-size: small; color: White; text-align: right;
                        color: #a7afc3; text-transform: capitalize;" runat="server" CssClass="label"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div id='cssmenu'>
        <ul>
            <li id="dashboard" runat="server" visible="false"><a href="../Accountsbootstrap/Home_Page.aspx">
                dashboard</a> </li>
            <li style="float: left" runat="server" visible="false" id="MMaster"><a href='#'>Master</a>
                <ul>
                    <li runat="server" visible="false" id="companymaster"><a href="../Accountsbootstrap/CompanyGrid.aspx">
                        Company Master</a></li>
                    <li runat="server" id="godownmaster" visible="false"><a href="../Accountsbootstrap/GodownMaster.aspx">
                        Go down Master</a></li>
                    <li runat="server" visible="false" id="itemhead"><a href="../Accountsbootstrap/categorygrid.aspx">
                        Item Head</a></li>
                    <li runat="server" visible="false" id="itemgroup"><a href="../Accountsbootstrap/Itemgroup.aspx">
                        Item Group</a></li>
                    <li runat="server" visible="false" id="itemtype"><a href="../Accountsbootstrap/ItemType.aspx">
                        ItemType</a></li>
                    <li runat="server" visible="false" id="measurement"><a href="../Accountsbootstrap/UOM.aspx">
                        Measurement Type</a></li>
                    <li runat="server" id="color" visible="false"><a href="../Accountsbootstrap/Color.aspx">
                        Color</a></li>
                    <li runat="server" id="size" visible="false"><a href="../Accountsbootstrap/Size.aspx">
                        Size</a></li>
                    <li runat="server" id="sizerange" visible="false"><a href="../Accountsbootstrap/SizeRange.aspx">
                        SizeRange</a></li>
                    <li runat="server" id="process" visible="false"><a href="../Accountsbootstrap/Process.aspx">
                        Process</a></li>
                    <li runat="server" id="partytype" visible="false"><a href="../Accountsbootstrap/PartyType.aspx">
                        PartyType</a></li>
                    <li runat="server" id="partymaster" visible="false"><a href="../Accountsbootstrap/PartyMasterGrid.aspx">
                        Party Master</a></li>
                    <li runat="server" id="Category" visible="false"><a href="../Accountsbootstrap/Category.aspx">
                        Category Master</a></li>
                    <li runat="server" id="CurrencyMaster" visible="false"><a href="../Accountsbootstrap/CurrencyMaster.aspx">
                        Currency Master</a></li>
                    <li runat="server" visible="false" id="deptmaster"><a href="../Accountsbootstrap/Dept_Master.aspx">
                        Department Master</a></li>
                    <li runat="server" visible="false" id="desg"><a href="../Accountsbootstrap/Dillo_Designation.aspx">
                        Designation Master</a></li>
                    <li runat="server" visible="false" id="empmaster"><a href="../Accountsbootstrap/Emp_gird.aspx">
                        Employee Master</a></li>
                </ul>
            </li>
            <li runat="server" visible="false" id="Leadgenerate"><a href="../Accountsbootstrap/LeadGrid.aspx">
                Lead Generate</a></li>
            <li style="float: left" runat="server" visible="false" id="Msampandcost"><a href='#'>
                Sampling/Costing/CAD Process</a>
                <ul>
                    <li runat="server" id="ItemMasterGroup" visible="false"><a href="../Accountsbootstrap/ItemMasterGroupGrid.aspx">
                        Item Master</a></li>
                    <li runat="server" id="SamplingandCosting" visible="false"><a href="../Accountsbootstrap/SamplingandCostingGrid.aspx">
                        Sampling & Costing </a></li>
                    <li runat="server" id="cadentry" visible="false"><a href="../Accountsbootstrap/CAD_Entry_Grid.aspx">
                        CAD Entry </a></li>
                </ul>
            </li>
            <li style="float: left" runat="server" visible="false" id="Mticket"><a href='#'>Ticketing
                Process </a>
                <ul>
                    <li runat="server" id="RaiseTicket" visible="false"><a href="../Accountsbootstrap/TicketRaise.aspx">
                        Raise Ticket</a></li>
                    <li runat="server" id="TicketAdmin" visible="false"><a href="../Accountsbootstrap/Ticket.aspx">
                        Admin Ticket</a></li>
                </ul>
            </li>
            <li style="float: left" runat="server" visible="false" id="Mapproval"><a href='#'>Approval
                Screen</a>
                <ul>
                    <li runat="server" id="partyapproval" visible="false"><a href="../Accountsbootstrap/PArtyMaster_Approval.aspx">
                        Party Master Approval </a></li>
                    <li runat="server" id="costingapproval" visible="false"><a href="../Accountsbootstrap/SamplingCosting_Approval.aspx">
                        Sampling And Costing Approval </a></li>
                </ul>
            </li>
            <li style="float: left" runat="server" visible="false" id="MorderProcess"><a href='#'>
                Buyer Order/Req.Process</a>
                <ul>
                    <li runat="server" id="BuyerOrderMaster" visible="false"><a href="../Accountsbootstrap/BuyerOrderMasterGrid.aspx">
                        BuyerOrder Master</a></li>
                    <li runat="server" id="Li2" visible="true"><a href="../Accountsbootstrap/SupplierOrderMasterGrid.aspx">
                        SampleOrder Master</a></li>
                    <li runat="server" id="requirementsheet" visible="false"><a href="../Accountsbootstrap/RequirementSheetGrid.aspx">
                        RequirementSheet </a></li>
                </ul>
            </li>
            <li style="float: left" runat="server" visible="false" id="Mopstock"><a href='#'>Opening
                Stock </a>
                <ul>
                    <li runat="server" id="MaterialOpstock" visible="false"><a href="../Accountsbootstrap/MaterialOpeningStockGrid.aspx">
                        Material OpeningStock </a></li>
                    <li runat="server" id="ExcOpstock" visible="false"><a href="../Accountsbootstrap/ExcOpeningStockGrid.aspx">
                        ExcNo Stock OpeningStock </a></li>
                </ul>
            </li>
            <li style="float: left" runat="server" visible="false" id="Mposcreen"><a href='#'>PO
                and GRN </a>
                <ul>
                    <li runat="server" id="Poorder" visible="false"><a href="../Accountsbootstrap/PurchaseOrderGrid.aspx">
                        Purchase Order </a></li>
                    <li runat="server" id="poGrn" visible="false"><a href="../Accountsbootstrap/PurchaseGRNGrid.aspx">
                        Purchase GRN </a></li>
                </ul>
            </li>
            <li style="float: left" runat="server" visible="false" id="Mitemprocess"><a href='#'>
                Item Process </a>
                <ul>
                    <li runat="server" id="StockTransfer" visible="true"><a href="../Accountsbootstrap/StockTransferGrid.aspx">
                        Stock Transfer </a></li>
                    <li runat="server" id="Li1" visible="true"><a href="../Accountsbootstrap/ItemProcessOrderEntryGrid.aspx">
                        Item Process Order </a></li>
                    <li runat="server" id="Itemissue" visible="false"><a href="../Accountsbootstrap/ItemProcessOrderGrid.aspx">
                        Item Process Issue </a></li>
                    <li runat="server" id="Itemreceive" visible="false"><a href="../Accountsbootstrap/ItemProcessReceiveGrid.aspx">
                        Item Process Receive </a></li>
                </ul>
            </li>
            <li style="float: left" runat="server" visible="false" id="McuttingProcess"><a href='#'>
                Cutting/Production Process </a>
                <ul>
                    <li runat="server" id="precutting" visible="false"><a href="../Accountsbootstrap/BuyerOrderCuttingGrid.aspx">
                        Pre Cutting Process </a></li>
                    <li runat="server" id="mastercutting" visible="false"><a href="../Accountsbootstrap/BuyerOrderMasterCuttingGrid.aspx">
                        Master Cutting Process </a></li>
                    <li runat="server" id="MaterialsIssue" visible="false"><a href="../Accountsbootstrap/MaterialsIssueGrid.aspx">
                        Materials Issue</a></li>
                </ul>
            </li>
            <li style="float: left" runat="server" visible="false" id="ProductionOrder"><a href='#'>
                Production Process </a>
                <ul>
                    <li runat="server" id="processentry" visible="false"><a href="../Accountsbootstrap/CuttingProcessEntryGrid.aspx">
                        Process Entry</a></li>
                </ul>
            </li>
            <li id="BuyerOrderSales" runat="server" visible="false"><a href="../Accountsbootstrap/BuyerOrderSalesGrid.aspx">
                BuyerOrder Sales</a> </li>
            <li id="Mreport" visible="false" style="float: left" runat="server"><a href="#">Reports</a>
                <ul>
                    <li runat="server" visible="false" id="RNewcostingreport"><a href="../Accountsbootstrap/CostingDetails.aspx">
                        Latest Costing Details Report </a></li>
                    <%-- <li runat="server" visible="false" id="ProductionStatus"><a href="../Accountsbootstrap/ProductionStatus.aspx">
                        Production Status </a></li>--%>
                    <li runat="server" visible="false" id="ProductionStatus"><a href="../Accountsbootstrap/ProductionStatusNew.aspx">
                        Production Status </a></li>
                    <li runat="server" id="Challan" visible="false"><a href="../Accountsbootstrap/ChallanInNew.aspx">
                        Challan New Report </a></li>
                    <%--<li id="Challan" visible="false" runat="server"><a href="#">Challan Reports</a>
                        <ul>
                            <li runat="server" visible="true" id="Li2"><a href="../Accountsbootstrap/ChallanInNew.aspx">
                                Challan New Report </a></li>
                            <li runat="server" visible="false" id="ChallanIn"><a href="../Accountsbootstrap/ChallanIn.aspx">
                                Challan In </a></li>
                            <li runat="server" visible="false" id="ChallanOut"><a href="../Accountsbootstrap/ChallanOut.aspx">
                                Challan Out </a></li>
                        </ul>
                    </li>--%>
                    <li runat="server" visible="false" id="PartyWiseProductionStatus"><a href="../Accountsbootstrap/PartyWiseProductionStatus.aspx">
                        PartyWise Production Status </a></li>
                    <%-- <li runat="server" visible="false" id="RequirementSheetDetails"><a href="../Accountsbootstrap/RequirementSheetDetails.aspx">
                        Requirement Sheet Details </a></li>--%>
                    <li runat="server" visible="false" id="RequirementSheetDetails"><a href="../Accountsbootstrap/RequirementSheetDetailsNew.aspx">
                        Requirement Sheet Details </a></li>
                    <li runat="server" visible="false" id="SwatchSheet"><a href="../Accountsbootstrap/SwatchSheet.aspx">
                        Swatch Sheet </a></li>
                    <li id="BuyerOrderReport" visible="false" runat="server"><a href="#">BuyerOrder Report</a>
                        <ul>
                            <li runat="server" visible="false" id="RBuyerorderreport"><a href="../Accountsbootstrap/BuyerOrderReport.aspx">
                                BuyerOrder Report</a></li>
                            <li runat="server" visible="false" id="BuyerOrderPendingReport"><a href="../Accountsbootstrap/BuyerOrderPendingReport.aspx">
                                BuyerOrder Pending Report </a></li>
                            <li runat="server" visible="false" id="BuyerOrderBookingSummary"><a href="../Accountsbootstrap/BuyerOrderBookingSummary.aspx">
                                BuyerOrder BookingSummary </a></li>
                            <li runat="server" visible="false" id="BuyerOrderDetails"><a href="../Accountsbootstrap/BuyerOrderDetails.aspx">
                                BuyerOrder Details</a></li>
                            <li runat="server" visible="false" id="BuyerOrderBookingStyles"><a href="../Accountsbootstrap/BuyerOrderBookingStyles.aspx">
                                BuyerOrder BookingStyles </a></li>
                        </ul>
                    </li>
                    <li runat="server" visible="false" id="Rcostingreport"><a href="../Accountsbootstrap/SamplingandCostingReport.aspx">
                        Sampling & Costing Report</a></li>
                    <li id="ItemProcessOrder" visible="false" runat="server"><a href="#">ItemProcess Report</a>
                        <ul>
                            <li runat="server" visible="false" id="ItemProcessOrderEntryReport"><a href="../Accountsbootstrap/ItemProcessOrderEntryReport.aspx">
                                ItemProcess Entry Report </a></li>
                            <li runat="server" visible="true" id="Li5"><a href="../Accountsbootstrap/ItemProcessOrderEntryReportNEW.aspx">
                                ItemProcess Entry ReportNEW </a></li>
                            <li runat="server" visible="false" id="Ritemissue"><a href="../Accountsbootstrap/ItemProcessOrderReport.aspx">
                                ItemProcess Issue Report </a></li>
                            <li runat="server" visible="false" id="Ritemreceive"><a href="../Accountsbootstrap/ItemProcessReceiveReport.aspx">
                                ItemProcess Receive Report </a></li>
                        </ul>
                    </li>
                    <li id="POandGRN" visible="false" runat="server"><a href="#">POandGRN Report</a>
                        <ul>
                            <li runat="server" visible="false" id="Rpoorder"><a href="../Accountsbootstrap/PurchaseOrderReportNew.aspx">
                                PurchaseOrder Report </a></li>
                            <%-- <li runat="server" visible="false" id="Rpoorder"><a href="../Accountsbootstrap/PurchaseOrderReport.aspx">
                                PurchaseOrder Report </a></li>
                            <li runat="server" visible="false" id="RpoGrn"><a href="../Accountsbootstrap/PurchaseGRNReport.aspx">
                                PurchaseGRN Report </a></li>--%>
                            <li runat="server" visible="false" id="PurchaseOrderPendingReport"><a href="../Accountsbootstrap/PurchaseOrderPendingReport.aspx">
                                PO Pending Report </a></li>
                        </ul>
                    </li>
                    <li id="StockReport" visible="false" runat="server"><a href="#">Stock Report</a>
                        <ul>
                            <li runat="server" visible="false" id="MaterialStockReport"><a href="../Accountsbootstrap/MaterialStockReport.aspx">
                                Material Stock Report </a></li>
                            <li runat="server" visible="false" id="StockLedger"><a href="../Accountsbootstrap/StockLedger.aspx">
                                Stock Ledger </a></li>
                            <li runat="server" visible="false" id="StockStatement"><a href="../Accountsbootstrap/StockStatement.aspx">
                                Stock Statement </a></li>
                        </ul>
                    </li>
                    <li id="CuttingReport" visible="false" runat="server"><a href="../Accountsbootstrap/cuttingdetailsnew.aspx">
                        Cutting Report</a>
                        <%--<ul>
                            <li runat="server" visible="false" id="CuttingDetails"><a href="../Accountsbootstrap/CuttingDetails.aspx">
                                Cutting Details </a></li>
                            <li runat="server" visible="false" id="CuttingQtyDetails"><a href="../Accountsbootstrap/CuttingQtyDetails.aspx">
                                Cutting Qty Details </a></li>
                            <li runat="server" visible="false" id="MasterCuttingDetailReport"><a href="../Accountsbootstrap/MasterCuttingDetailReport.aspx">
                                MasterCutting Detail Report </a></li>
                        </ul>--%>
                    </li>
                    <li id="ProductionReports" visible="false" runat="server"><a href="#">Production Report</a>
                        <ul>
                            <li runat="server" visible="false" id="ProductionReport"><a href="../Accountsbootstrap/ProductionReport.aspx">
                                Production Report </a></li>
                            <li runat="server" visible="false" id="ProductionDetailReport"><a href="../Accountsbootstrap/ProductionDetailReport.aspx">
                                Production Detail Report </a></li>
                        </ul>
                    </li>
                    <li runat="server" visible="true" id="Li3"><a href="../Accountsbootstrap/SearchEXEWise.aspx">
                        Search EXE Wise Report </a></li>
                    <li runat="server" visible="true" id="Li4"><a href="../Accountsbootstrap/SearchPartyWise.aspx">
                        Search Party Wise Report </a></li>
                    <li id="Li6" visible="true" runat="server"><a href="#">Production Status Summary Report</a>
                        <ul>
                            <li runat="server" visible="true" id="Li7"><a href="../Accountsbootstrap/CustomerWiseSummaryProductionStatus.aspx">
                                Date Wise Summary ProductionStatus Report </a></li>
                            <li runat="server" visible="true" id="Li8"><a href="../Accountsbootstrap/CustomerWiseSummaryProductionStatus1.aspx">
                                Customer Wise Summary ProductionStatus Report </a></li>
                            <li runat="server" visible="true" id="Li13"><a href="../Accountsbootstrap/CustomerWiseSummaryProductionStatus_Jobworker.aspx">
                                Customer Wise Contractor Wise Breakup Report </a></li>
                            <li runat="server" visible="true" id="Li9"><a href="../Accountsbootstrap/CustomerWiseContractorWiseBreakup.aspx">
                                Contractor Wise Production Report </a></li>
                            <li runat="server" visible="true" id="Li10"><a href="../Accountsbootstrap/ProcessWiseContractorWiseBreakup.aspx">
                                Process Wise Production Report </a></li>
                            <li runat="server" visible="true" id="Li11"><a href="../Accountsbootstrap/ContractorWiseProductionSummary.aspx">
                                Contractor Wise Production Summary Report </a></li>
                            <li runat="server" visible="true" id="Li12"><a href="../Accountsbootstrap/ReadyForDespatchReport.aspx">
                                Ready For Despatch Report </a></li>
                        </ul>
                    </li>
                </ul>
            </li>
            <li id="MAdmn" visible="false" style="float: left" runat="server"><a href="#">Admin
            </a>
                <ul>
                    <li runat="server" visible="false" id="FullFormReport"><a href="../Accountsbootstrap/FullFormReport.aspx">
                        Day Report</a></li>
                    <li runat="server" visible="false" id="usercreate"><a href="../Accountsbootstrap/usercreategrid.aspx">
                        User Create</a></li>
                </ul>
            </li>
            <li><a href="../Accountsbootstrap/ExeWisecostingReport.aspx">EXC Wise Costing Report</a></li>
            <li><a href="../Accountsbootstrap/login.aspx">Sign Out</a></li>
        </ul>
    </div>
</body>
<html>
