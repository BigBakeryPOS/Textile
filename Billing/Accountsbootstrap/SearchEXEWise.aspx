<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchEXEWise.aspx.cs"
    Inherits="Billing.Accountsbootstrap.SearchEXEWise" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head id="Head1" runat="server">
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Search EXE Wise</title>
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
    <link href="../css/chosen.css" rel="Stylesheet" />
    <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.7.2.js"></script>
    <link rel="stylesheet" href="../css/chosen.css" />
    <script type="text/javascript">
        function ReportPrint() {

            var gridData = document.getElementById('Excel');

            var windowUrl = 'about:blank';
            //set print document name for gridview
            var uniqueName = new Date();
            var windowName = 'Print_' + uniqueName.getTime();

            var prtWindow = window.open(windowUrl, windowName,
           'left=100,top=100,right=100,bottom=100,width=1100,height=1200');
            prtWindow.document.write('<html><head></head>');
            prtWindow.document.write('<body style="background:none !important">');

            prtWindow.document.write(gridData.outerHTML);
            prtWindow.document.write('</body></html>');
            prtWindow.document.close();
            prtWindow.focus();
            prtWindow.print();
            prtWindow.close();


        }
    </script>
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <asp:Label runat="server" ID="FontSize" ForeColor="White" CssClass="label" Visible="false"
        Text="17"> </asp:Label>
    <asp:Label runat="server" ID="lblContactTypeId" ForeColor="White" CssClass="label"
        Visible="false" Text="1"></asp:Label>
    <form runat="server" id="form1">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="col-lg-2">
                                <h1 class="page-header" style="text-align: center; color: #fe0002; font-size: 20px;
                                    font-weight: bold">
                                    Search EXEWise Details
                                </h1>
                            </div>
                            <div class="col-lg-2">
                                <asp:DropDownList ID="drpyear" runat="server" CssClass="form-control" OnSelectedIndexChanged="Year_selected"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    Select EXENo
                                    <asp:DropDownList ID="ddlEXCNo" runat="server" CssClass="chzn-select" Width="100%">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-2">
                            </div>
                            <div class="col-lg-1">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search"
                                    OnClick="btnSearch_OnClick" Width="110px" />
                            </div>
                            <div class="col-lg-1">
                                <asp:Button ID="btn" runat="server" Text="Print" CssClass="btn btn-info" OnClientClick="ReportPrint()"
                                    Width="110px" />
                            </div>
                        </div>
                        <div id="Excel" runat="server">
                            <div class="col-lg-12">
                                <div class="col-lg-12">
                                    <div>
                                        <b>Buyer Order:</b>
                                        <asp:GridView ID="gvBuyerOrder" runat="server" CssClass="myGridStyle" Width="100%"
                                            AutoGenerateColumns="false">
                                            <HeaderStyle BackColor="White" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                NextPageText="Next" PreviousPageText="Previous" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo  " HeaderStyle-Width="1%">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="OrderType " DataField="OrderType" Visible="false" />
                                                <asp:BoundField HeaderText="CompanyName" DataField="LedgerName" Visible="false" />
                                                <asp:BoundField HeaderText="CompanyCode" DataField="CompanyCode" Visible="false" />
                                                <asp:BoundField HeaderText="ExcNo" DataField="ExcNo" />
                                                <asp:BoundField HeaderText="BuyerPoNo" DataField="BuyerPoNo" />
                                                <asp:BoundField HeaderText="ItemCode" DataField="ItemCode" />
                                                <asp:BoundField HeaderText="Ord.Date" DataField="OrderDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField HeaderText="Ship.Date" DataField="ShipmentDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField HeaderText="Dlv.Date" ItemStyle-Font-Bold="true" DataField="DeliveryDate"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField HeaderText="B.Qty" DataField="BQty" ItemStyle-HorizontalAlign="Right" />
                                                <%--<asp:BoundField HeaderText="C.Qty" DataField="CQty" ItemStyle-HorizontalAlign="Right"
                                                    Visible="false" />
                                                <asp:BoundField HeaderText="C.Qty" DataField="MQty" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField HeaderText="S.Qty" DataField="ShippedQty" ItemStyle-HorizontalAlign="Right" />--%>
                                                <asp:BoundField HeaderText="Amount" DataField="Amount" DataFormatString="{0:f2}"
                                                    ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField HeaderText="Currency" DataField="currencyname" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="Dlv.Date" DataField="IschangedDeliveryDate" ItemStyle-HorizontalAlign="Center" />
                                                <%--  <asp:TemplateField HeaderText="RateChange" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnRateChange" runat="server" CommandArgument='<%#Eval("BuyerOrderId") %>'
                                                            CommandName="RateChange">
                                                        RC</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("BuyerOrderId") %>'
                                                            CommandName="edit">
                                                            <asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                        <asp:ImageButton ID="imgdisable" ImageUrl="~/images/edit.png" runat="server" Visible="false"
                                                            Enabled="false" ToolTip="Not Allow To Delete" />
                                                        <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("BuyerOrderId") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Exl" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnedit1" runat="server" CommandArgument='<%#Eval("BuyerOrderId") %>'
                                                            CommandName="ExportExcel">
                                                       Exl</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btndelete" runat="server" CommandArgument='<%#Eval("BuyerOrderId") %>'
                                                            CommandName="delete1" OnClientClick="alertMessage()">
                                                            <asp:Image ID="dlt" runat="server" ImageUrl="~/images/DeleteIcon_btn.png" Visible="true" /></asp:LinkButton>
                                                        <asp:ImageButton ID="imgdisable1" ImageUrl="~/images/delete.png" runat="server" Visible="false"
                                                            Enabled="false" ToolTip="Not Allow To Delete" />
                                                        <ajaxToolkit:ModalPopupExtender ID="lnkDelete_ModalPopupExtender" runat="server"
                                                            CancelControlID="ButtonDeleteCancel" OkControlID="ButtonDeleleOkay" TargetControlID="btndelete"
                                                            PopupControlID="DivDeleteConfirmation" BackgroundCssClass="ModalPopupBG">
                                                        </ajaxToolkit:ModalPopupExtender>
                                                        <ajaxToolkit:ConfirmButtonExtender ID="lnkDelete_ConfirmButtonExtender" runat="server"
                                                            TargetControlID="btndelete" Enabled="True" DisplayModalPopupID="lnkDelete_ModalPopupExtender">
                                                        </ajaxToolkit:ConfirmButtonExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </div>
                                    <hr />
                                    <div>
                                        <b>Requirement Sheet:</b>
                                        <asp:GridView ID="gvrequirmentOrder" runat="server" CssClass="myGridStyle1" EmptyDataText="No records Found"
                                            Width="100%" AutoGenerateColumns="false">
                                            <HeaderStyle BackColor="White" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                NextPageText="Next" PreviousPageText="Previous" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Requirement No" DataField="RequirementNo" Visible="false" />
                                                <asp:BoundField HeaderText="Entry Date" DataField="RequirementDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField HeaderText="ExcNo" DataField="ExcNo" />
                                                <asp:BoundField HeaderText="ShipmentDate" DataField="ShipmentDate" DataFormatString="{0:dd/MM/yyyy}" />
                                            </Columns>
                                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                        <br />
                                        <asp:Label runat="server" ID="lblColorId" ForeColor="White" CssClass="label" Visible="false"
                                            Text="18"> </asp:Label>
                                        <asp:GridView ID="gridviewstyle" AutoGenerateColumns="false" OnRowDataBound="GricViewStyle_Color"
                                            Width="100%" runat="server" Visible="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo" HeaderStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Style NO">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstyleno" runat="server" Text='<%#  Eval("styleno") %>'></asp:Label>
                                                        <asp:Label ID="lblstyleid" Visible="false" runat="server" Text='<%#  Eval("SamplingCostingId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="color Desc.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblitemcolor" runat="server" Text='<%#  Eval("Color") %>'></asp:Label>
                                                        <asp:Label ID="lblstylecolorid" runat="server" Text='<%#  Eval("StyleColorId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblitemtype" runat="server" Text='<%#  Eval("Itemgroupname") %>'></asp:Label>
                                                        <asp:Label ID="lblItemgroupId" Visible="false" runat="server" Text='<%#  Eval("ItemgroupId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Category">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcategory" runat="server" Text='<%#  Eval("Category") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblitemname" runat="server" Text='<%#  Eval("Description") %>'></asp:Label>
                                                        <asp:Label ID="lblitemid" Visible="false" runat="server" Text='<%#  Eval("itemmasterid") %>'></asp:Label>
                                                        <asp:Label ID="lblcolorid" Visible="false" runat="server" Text='<%#  Eval("colorid") %>'></asp:Label>
                                                        <asp:Label ID="lblCqty" Visible="false" runat="server" Text='<%#  Eval("Cqty") %>'></asp:Label>
                                                        <asp:Label ID="lblBQty" Visible="false" runat="server" Text='<%#  Eval("BQty") %>'></asp:Label>
                                                        <asp:Label ID="lblStotpcs" Visible="false" runat="server" Text='<%#  Eval("Stotpcs") %>'></asp:Label>
                                                        <asp:Label ID="lblPtotpcs" Visible="false" runat="server" Text='<%#  Eval("Ptotpcs") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Select Color ">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="drpcolorlist" runat="server" CssClass="chzn-select">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="Item Desc.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblitemdesc" runat="server" Text='<%#  Eval("Description") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sampling Avg.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsampleavg" runat="server" Text='<%#  Eval("SmpAvg") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Production Avg.">
                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblprodavg" runat="server" Text='<%#  Eval("prodavg") %>'></asp:Label>--%>
                                                        <asp:TextBox ID="txtprodavg" runat="server" Text='<%#  Eval("PrdAvg") %>'></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender312" runat="server"
                                                            TargetControlID="txtprodavg" ValidChars="." FilterType="Numbers,Custom" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Uom">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbluom" runat="server" Text='<%#  Eval("Units") %>'></asp:Label>
                                                        <asp:Label ID="lblunitsid" Visible="false" runat="server" Text='<%#  Eval("UOMID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <br />
                                        <asp:DropDownList ID="drpcompany" runat="server" CssClass="form-control" Visible="false"
                                            Width="50%" AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:GridView ID="gridstockdetails" CssClass="myGridStyle1" Width="100%" runat="server"
                                            AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Item Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemgroupname" runat="server" Text='<%#  Eval("Itemgroupname") %>'></asp:Label>
                                                        <asp:Label ID="lblItemgroupId" runat="server" Visible="false" Text='<%#  Eval("ItemgroupId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblitemanme" runat="server" Text='<%#  Eval("Description") %>'></asp:Label>
                                                        <asp:Label ID="lblitemid" runat="server" Visible="false" Text='<%#  Eval("itemmasterid") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Color">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblitemcolor" runat="server" Text='<%#  Eval("Itemcolor") %>'></asp:Label>
                                                        <asp:Label ID="lblitemcolorid" runat="server" Visible="false" Text='<%#  Eval("Itemcolorid") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sampling Req.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsampavg" runat="server" Text='<%#  Eval("STotalpcs") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Prod. Req.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblprodavg" runat="server" Text='<%#  Eval("PTotalpcs") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Avl.Stock">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblavlstock" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Want To Purchase Stock">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpurchasestock" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Units">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblunits" runat="server" Text='<%#  Eval("Units") %>'></asp:Label>
                                                        <asp:Label ID="lblunitsid" runat="server" Visible="false" Text='<%#  Eval("Unitsid") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <hr />
                                    <div>
                                        <b>EXEWise Opening Stock:</b>
                                        <asp:GridView ID="gvExcOpeningStock" runat="server" CssClass="myGridStyle1" Width="100%"
                                            AutoGenerateColumns="false">
                                            <HeaderStyle BackColor="White" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                NextPageText="Next" PreviousPageText="Previous" />
                                            <Columns>
                                                <asp:BoundField HeaderText="CompanyName " DataField="CompanyName" />
                                                <asp:BoundField HeaderText="ExcNo" DataField="excNo" />
                                                <asp:BoundField HeaderText="Date" DataField="DefaultDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField HeaderText="TtlQty" DataField="TtlQty" />
                                            </Columns>
                                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </div>
                                    <hr />
                                    <div>
                                        <b>ItemProcess Order:</b>
                                        <asp:GridView ID="GVItemProcessOrder" runat="server" CssClass="myGridStyle1" EmptyDataText="No records Found"
                                            Width="100%" AutoGenerateColumns="false">
                                            <HeaderStyle BackColor="White" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                NextPageText="Next" PreviousPageText="Previous" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo  " HeaderStyle-Width="1%">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Order No" DataField="FullPONo" />
                                                <asp:BoundField HeaderText="CompanyName" DataField="LedgerName" />
                                                <asp:BoundField HeaderText="CompanyCode" DataField="CompanyCode" />
                                                <asp:BoundField HeaderText="ProcessOn" DataField="category" />
                                                <asp:BoundField HeaderText="DeliveryPlace" DataField="DeliveryPlace" />
                                                <asp:BoundField HeaderText="OrderDate" DataField="OrderDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField HeaderText="FromDate" DataField="FromDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField HeaderText="ToDate" DataField="ToDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField HeaderText="Qty" DataField="Qty" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField HeaderText="TotalQty" DataField="TotalQty" DataFormatString="{0:f2}"
                                                    ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField HeaderText="IssQty" DataField="RecQty" DataFormatString="{0:f2}"
                                                    ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField HeaderText="Amount" DataField="TotalAmount" DataFormatString="{0:f2}"
                                                    ItemStyle-HorizontalAlign="Right" />
                                            </Columns>
                                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </div>
                                    <hr />
                                    <div>
                                        <b>ItemProcess Issue:</b>
                                        <asp:GridView ID="GVItemProcessOrderIssue" runat="server" CssClass="myGridStyle1"
                                            EmptyDataText="No records Found" Width="100%" AutoGenerateColumns="false">
                                            <HeaderStyle BackColor="White" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                NextPageText="Next" PreviousPageText="Previous" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo  " HeaderStyle-Width="1%">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Challan No" DataField="FullPONo" />
                                                <asp:BoundField HeaderText="Order No" DataField="OrderEntryPONo" />
                                                <asp:BoundField HeaderText="CompanyName" DataField="LedgerName" />
                                                <asp:BoundField HeaderText="CompanyCode" DataField="CompanyCode" />
                                                <asp:BoundField HeaderText="ProcessOn" DataField="category" />
                                                <asp:BoundField HeaderText="DeliveryPlace" DataField="DeliveryPlace" />
                                                <asp:BoundField HeaderText="IssueDate" DataField="IssueDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField HeaderText="OrderDate" DataField="OrderDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField HeaderText="FromDate" DataField="FromDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField HeaderText="ToDate" DataField="ToDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField HeaderText="Qty" DataField="Qty" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField HeaderText="RecQty" DataField="RecQty" DataFormatString="{0:f2}"
                                                    ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField HeaderText="Amount" DataField="TotalAmount" DataFormatString="{0:f2}"
                                                    ItemStyle-HorizontalAlign="Right" />
                                            </Columns>
                                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </div>
                                    <hr />
                                    <div>
                                        <b>ItemProcess Receive:</b>
                                        <asp:GridView ID="GVItemProcessOrderReceive" runat="server" CssClass="myGridStyle1"
                                            EmptyDataText="No records Found" Width="100%" AutoGenerateColumns="false">
                                            <HeaderStyle BackColor="White" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                NextPageText="Next" PreviousPageText="Previous" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo  " HeaderStyle-Width="1%">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Receive No" DataField="FullRecPONo" />
                                                <asp:BoundField HeaderText="Challan No" DataField="ChallanNo" />
                                                <asp:BoundField HeaderText="CompanyName" DataField="LedgerName" />
                                                <asp:BoundField HeaderText="CompanyCode" DataField="CompanyCode" />
                                                <asp:BoundField HeaderText="ProcessOn" DataField="category" />
                                                <asp:BoundField HeaderText="DeliveryPlace" DataField="DeliveryPlace" />
                                                <asp:BoundField HeaderText="ReceivedDate" DataField="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField HeaderText="Qty" DataField="Qty" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField HeaderText="Amount" DataField="TotalAmount" DataFormatString="{0:f2}"
                                                    ItemStyle-HorizontalAlign="Right" />
                                            </Columns>
                                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </div>
                                    <hr />
                                    <div>
                                        <b>Pre Cutting:</b>
                                        <asp:GridView ID="gvBuyerOrderCutting" runat="server" CssClass="myGridStyle1" Width="100%"
                                            EmptyDataText="No Records Found" AutoGenerateColumns="false">
                                            <HeaderStyle BackColor="White" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                NextPageText="Next" PreviousPageText="Previous" />
                                            <Columns>
                                                <asp:BoundField HeaderText="CuttingNo" DataField="FullCuttingNo" />
                                                <asp:BoundField HeaderText="CuttingDate" DataField="CuttingDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField HeaderText="ExcNo" DataField="ExcNo" />
                                                <asp:BoundField HeaderText="CompanyCode" DataField="CompanyCode" />
                                                <asp:BoundField HeaderText="BuyerPoNo" DataField="BuyerPoNo" />
                                                <asp:BoundField HeaderText="ItemCode" DataField="ItemCode" />
                                                <asp:BoundField HeaderText="OrderDate" DataField="OrderDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField HeaderText="ShipmentDate" DataField="ShipmentDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField HeaderText="DeliveryDate" DataField="DeliveryDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField HeaderText="CQty" DataField="CQty" />
                                                <asp:BoundField HeaderText="RecQty" DataField="RecQty" />
                                                <asp:BoundField HeaderText="DmgQty" DataField="DmgQty" />
                                                <asp:BoundField HeaderText="BalQty" DataField="BalQty" />
                                                <asp:BoundField HeaderText="Amount" DataField="Amount" DataFormatString="{0:f2}"
                                                    Visible="false" />
                                            </Columns>
                                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </div>
                                    <hr />
                                    <div>
                                        <b>Master Cutting:</b>
                                        <asp:GridView ID="gvBuyerOrderMasterCutting" runat="server" CssClass="myGridStyle1"
                                            EmptyDataText="No Records Found" Width="100%" AutoGenerateColumns="false">
                                            <HeaderStyle BackColor="White" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                NextPageText="Next" PreviousPageText="Previous" />
                                            <Columns>
                                                <asp:BoundField HeaderText="ExcNo" DataField="ExcNo" />
                                                <asp:BoundField HeaderText="MasterCuttingDate" DataField="MasterCuttingDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField HeaderText="CuttingDate" DataField="CuttingDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField HeaderText="OrderType " DataField="OrderType" />
                                                <asp:BoundField HeaderText="CompanyCode" DataField="CompanyCode" />
                                                <asp:BoundField HeaderText="DeliveryDate" DataField="DeliveryDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                <%--<asp:BoundField HeaderText="CQty" DataField="CQty" />
                                                <asp:BoundField HeaderText="RecQty" DataField="RecQty" />
                                                <asp:BoundField HeaderText="DmgQty" DataField="DmgQty" />
                                                <asp:BoundField HeaderText="BalQty" DataField="BalQty" />--%>
                                            </Columns>
                                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                        <br />
                                        <asp:GridView ID="GVItem" AutoGenerateColumns="False" runat="server" 
                                            GridLines="Both" Caption="Style Details">
                                            <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                                Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo" HeaderStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                        <asp:HiddenField ID="hdTransItemId" runat="server" Value='<%#Eval("TransItemId")  %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="StyleNo" HeaderStyle-Width="10%" ItemStyle-Width="10%"
                                                    ItemStyle-Font-Size="Large">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdStyleNoId" runat="server" Value='<%#Eval("StyleNoId")  %>' />
                                                        <asp:Label ID="lblStyleNo" runat="server" Text='<%#Eval("StyleNo")  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description" HeaderStyle-Width="40%" ItemStyle-Width="40%"
                                                    ItemStyle-Font-Size="Large">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description")  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Color" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                                                    ItemStyle-Font-Size="Large">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdColorId" runat="server" Value='<%#Eval("ColorId")  %>' />
                                                        <asp:Label ID="lblColor" runat="server" Text='<%#Eval("Color")  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="5%" ItemStyle-Width="5%"
                                                    ItemStyle-Font-Size="Large">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRate" runat="server" Text='<%#Eval("Rate")  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="5%" ItemStyle-Width="5%" ItemStyle-Font-Size="Large">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQty" runat="server" Text='<%#Eval("CQty")  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bal.Qty" HeaderStyle-Width="40px" ItemStyle-Font-Size="Large">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBalQty" runat="server" Text='<%#Eval("BalQty")  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rec.Qty" HeaderStyle-Width="40px" ItemStyle-Font-Size="Large">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRecQty" runat="server" Text='<%#Eval("RecQty")  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dmg.Qty" HeaderStyle-Width="40px" ItemStyle-Font-Size="Large">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDmgQty" runat="server" Text='<%#Eval("DmgQty")  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Size" HeaderStyle-Width="8%" ItemStyle-Width="8%"
                                                    ItemStyle-Font-Size="Large">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdRangeId" runat="server" Value='<%#Eval("RangeId")  %>' />
                                                        <asp:Label ID="lblSize" runat="server" Text='<%#Eval("Size")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Assign Qty" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1px" Visible="false"
                                                    ItemStyle-Width="1px">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdRowId" runat="server" Value='<%#Eval("RowId")  %>' />
                                                        <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("RowId") %>'
                                                            CommandName="Modify">
                                                            <asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1px"  Visible="false"
                                                    ItemStyle-Width="1px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnView" runat="server" CommandArgument='<%#Eval("RowId") %>'
                                                            CommandName="View">
                                                            <asp:Image ID="imgView" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <hr />
                                    <div>
                                        <b>Materials Issue:</b>
                                        <asp:GridView ID="gvMaterialsIssue" runat="server" CssClass="myGridStyle1" Width="100%"
                                            EmptyDataText="No Records Found" AutoGenerateColumns="false">
                                            <HeaderStyle BackColor="White" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                NextPageText="Next" PreviousPageText="Previous" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Material Issue Type" DataField="MissueType" />
                                                <asp:BoundField HeaderText="PRP.NO" DataField="PRPNO" />
                                                <asp:BoundField HeaderText="CuttingDate" DataField="MaterialDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField HeaderText="ExcNo" DataField="ExcNo" />
                                                <asp:BoundField HeaderText="Ledger Name" DataField="ledgername" />
                                                <asp:BoundField HeaderText="Issue Fro" DataField="IssueFor" />
                                            </Columns>
                                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </div>
                                    <hr />
                                    <div>
                                        <b>Process:</b>
                                        <asp:GridView ID="gvCuttingProcessEntry" runat="server" CssClass="myGridStyle1" Width="100%"
                                            AutoGenerateColumns="false">
                                            <HeaderStyle BackColor="White" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                NextPageText="Next" PreviousPageText="Previous" />
                                            <Columns>
                                                <asp:BoundField HeaderText="EntryNo " DataField="FullEntryNo" />
                                                <asp:BoundField HeaderText="EntryDate" DataField="EntryDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField HeaderText="ExcNo" DataField="ExcNo" />
                                                <asp:BoundField HeaderText="JobWorker/Inhouse" DataField="Supplier" />
                                                <asp:BoundField HeaderText="ProcessFrom " Visible="false" DataField="ProcessFrom" />
                                                <asp:BoundField HeaderText="Process " DataField="Process" />
                                                <asp:BoundField HeaderText="Dlv.Date" DataField="ToDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField HeaderText="Iss" DataField="TotalIssued" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField HeaderText="Rec" DataField="TotalReceived" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField HeaderText="Dmg" DataField="TotalDamaged" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField HeaderText="Issued Amount" DataField="Totamnt" DataFormatString="{0:f2}"
                                                    ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField HeaderText="Want To Pay" DataField="WTotamnt" DataFormatString="{0:f2}"
                                                    ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField HeaderText="Debit Amount" DataField="DTotamnt" DataFormatString="{0:f2}"
                                                    ItemStyle-HorizontalAlign="Right" />
                                            </Columns>
                                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    </form>
</body>
</html>
