<%@ Page Title="" Language="C#" MasterPageFile="~/View/StoreClerk/Clerk.Master" AutoEventWireup="true" CodeBehind="PurchaseOrder.aspx.cs" Inherits="LogicUniversity.StoreClerk.View.PurchaseOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 595px;
            height: 239px;
        }

        .auto-style2 {
            width: 85px;
        }

        .auto-style3 {
            width: 104px;
        }

        .auto-style4 {
            width: 157px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-lg-12">
        <h1 class="page-header">Purchase Order</h1>
    </div>

    <asp:Panel ID="Panel1" runat="server">

        <div class="container-fluid" id="rcorners1">
            <div class="row">
                <div class="col-md-4">
                    <div class="tabbable" id="tabs-733222">
                        <ul class="nav nav-tabs">
                            <li class="active">
                                <a href="#panel-360657" data-toggle="tab">Simple Search</a>
                            </li>
                            <li>
                                <a href="#panel-261375" data-toggle="tab">Advance Search</a>
                            </li>
                        </ul>
                        <div class="tab-content">
                            <div class="tab-pane active" id="panel-360657">
                                <br />
                                <table class="auto-style1">
                                    <tr>
                                        <td class="auto-style4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; PO Number&nbsp;&nbsp;&nbsp;   </td>
                                        <td>&nbsp;
                                            <asp:TextBox ID="POtxt" class="form-control" OnTextChanged="POtxt_TextChanged" runat="server" Height="33px" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; From:</td>
                                        <td>

                                            <br />
                                            <div id="maindiv">
                                                <asp:TextBox ID="fromDate" AutoPostBack="True" OnTextChanged="fromDate_TextChanged" CssClass="datepicker" runat="server"></asp:TextBox>
                                                <asp:Label ID="lblFDate" ForeColor="Red" runat="server" Text=""></asp:Label>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="fromDate" ErrorMessage="Incorrect date format" ForeColor="Red" ValidationExpression="^\d{2}/\d{2}/\d{4}$|^\d{1}/\d{1}/\d{4}$"></asp:RegularExpressionValidator>
                                            </div>
                                        </td>

                                    </tr>

                                    <tr>

                                        <td class="auto-style4">&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;To:</td>
                                        <td>

                                            <br />
                                            <div id="maindiv0">
                                                <asp:TextBox ID="toDate" OnTextChanged="toDate_TextChanged" CssClass="datepicker" runat="server"></asp:TextBox>

                                                <asp:Label ID="lbltoDate" ForeColor="Red" runat="server" Text=""></asp:Label>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="toDate" ErrorMessage="Incorrect date format" ForeColor="Red" ValidationExpression="^\d{2}/\d{2}/\d{4}$|^\d{1}/\d{1}/\d{4}$"></asp:RegularExpressionValidator>

                                            </div>
                                        </td>


                                    </tr>


                                    <tr>
                                        <td class="auto-style4">&nbsp;</td>
                                        <td>
                                            <br />
                                            <asp:Button ID="BtnSearch" OnClick="Btn_Search" class="btn btn-outline btn-primary" runat="server" Text="Search" />
                                            <br />
                                        </td>
                                    </tr>
                                </table>

                                <asp:Label ID="lblmesg" runat="server" Text=""></asp:Label>
                                <asp:GridView ID="gvPO" runat="server" AutoGenerateColumns="False" CellPadding="4" class="table table-striped table-bordered table-hover dataTable no-footer" ForeColor="#333333" GridLines="None" Height="68px" Width="845px" AllowPaging="True" OnPageIndexChanging="gvPO_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="Order_ID" HeaderText="PO Number" />
                                        <asp:BoundField DataField="Supplier_Name" HeaderText="Supplier" />
                                        <asp:BoundField DataField="Order_Date" HeaderText="Order Date" DataFormatString="{0:yyyy/MM/dd}" />
                                        <asp:BoundField DataField="Received_Date" HeaderText="Received Date" DataFormatString="{0:yyyy/MM/dd}" />
                                        <asp:BoundField DataField="Ord_Staff" HeaderText="Order Staff" />
                                        <asp:BoundField DataField="Rec_Staff" HeaderText="Received Staff" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" />
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />

                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                                </asp:GridView>
                            </div>


                            <div class="tab-pane" id="panel-261375">


                                <%-- <asp:ScriptManager ID="ScriptManager"
                                    runat="server" />--%>
                                <asp:UpdatePanel ID="UpdatePanel"
                                    UpdateMode="Conditional"
                                    runat="server">
                                    <ContentTemplate>
                                        <fieldset>
                                            <br />
                                            <br />
                                            <table class="auto-style1">
                                                <tr>

                                                    <td class="auto-style2">

                                                        <table frame="box">

                                                            <tr>
                                                                <td class="auto-style3">&nbsp; 
                            <br />
                                                                    &nbsp;&nbsp;
                            Supplier
                            <br />
                                                                </td>
                                                                <td class="auto-style3">
                                                                    <br />
                                                                    <asp:DropDownList ID="ddlSup" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged" Class="btn btn-default btn-xs dropdown-toggle" Height="27px" Width="122px">
                                                                    </asp:DropDownList>
                                                                    <br />

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp; 
                            <br />
                                                                    &nbsp;&nbsp;
                            Status
                            <br />
                                                                </td>
                                                                <td>
                                                                    <br />
                                                                    <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" Class="btn btn-default btn-xs dropdown-toggle" Height="27px" Width="122px">
                                                                        <asp:ListItem>-Select Status-</asp:ListItem>
                                                                        <asp:ListItem>Submitted</asp:ListItem>
                                                                        <asp:ListItem>Received</asp:ListItem>
                                                                        <asp:ListItem>Cancelled</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;
                            <br />
                                                                </td>
                                                                <td>
                                                                    <br />

                                                                    <br />
                                                                    <br />
                                                                </td>
                                                            </tr>

                                                        </table>


                                                    </td>

                                                    <td class="auto-style2">
                                                        <table frame="box">
                                                            <tr>
                                                                <td>&nbsp;<br />
                                                                    &nbsp;&nbsp; Order Staff<br />
                                                                </td>
                                                                <td>
                                                                    <br />
                                                                    <asp:DropDownList ID="ddlorderBy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOrderBy_SelectedIndexChanged" Class="btn btn-default btn-xs dropdown-toggle" Height="27px" Width="122px">
                                                                    </asp:DropDownList>
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;<br />
                                                                    &nbsp;&nbsp; Status<br />
                                                                </td>
                                                                <td>
                                                                    <br />
                                                                    <asp:DropDownList ID="ddl_Status" runat="server" OnSelectedIndexChanged="ddl_Status_SelectedIndexChanged" AutoPostBack="true" Class="btn btn-default btn-xs dropdown-toggle" Height="27px" Width="122px">

                                                                        <%-- <asp:ListItem >Pending</asp:ListItem>
                                                     <asp:ListItem >Received</asp:ListItem>--%>
                                                                    </asp:DropDownList>
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;<br />
                                                                    &nbsp;&nbsp; Received Staff<br />
                                                                    <br />
                                                                </td>
                                                                <td>
                                                                    <br />
                                                                    <asp:DropDownList ID="ddlRecBy" runat="server" OnSelectedIndexChanged="ddlRecBy_SelectedIndexChanged" Class="btn btn-default btn-xs dropdown-toggle" Height="27px" Width="122px">
                                                                    </asp:DropDownList>
                                                                    <br />
                                                                    <br />
                                                                </td>
                                                            </tr>

                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style2">&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>

                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <asp:Button ID="BtnSearch3" runat="server" OnClick="btn_Search2" class="btn btn-outline btn-primary" Text="Search" />
                                            <br />
                                            <br />
                                            <br />
                                            <asp:Label ID="lblerror" BackColor="#FFFF66" ForeColor="#FF3300" runat="server" Text=""></asp:Label>
                                            <asp:GridView ID="gvPO2" runat="server" AutoGenerateColumns="False" CellPadding="4" class="table table-striped table-bordered table-hover dataTable no-footer" ForeColor="#333333" GridLines="None" Height="68px" Width="845px" AllowPaging="True" OnPageIndexChanging="gvPO2_PageIndexChanging">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="Order_ID" HeaderText="PO Number" />
                                                    <asp:BoundField DataField="Supplier_Name" HeaderText="Supplier" />
                                                    <asp:BoundField DataField="Order_Date" HeaderText="Order Date" DataFormatString="{0:yyyy/MM/dd}" />
                                                    <asp:BoundField DataField="Received_Date" HeaderText="Received Date" DataFormatString="{0:yyyy/MM/dd}" />
                                                    <asp:BoundField DataField="Ord_Staff" HeaderText="Order Staff" />
                                                    <asp:BoundField DataField="Rec_Staff" HeaderText="Received Staff" />
                                                    <asp:BoundField DataField="Status" HeaderText="Status" />
                                                </Columns>
                                                <EditRowStyle BackColor="#999999" />
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />

                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                                            </asp:GridView>
                                        </fieldset>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <div class="col-md-4"></div>
        </div>

    </asp:Panel>
    <br />
    <br />
    <br />
    <%--<asp:GridView ID="gvPO1" runat="server" AutoGenerateColumns="False" CellPadding="4" class="table table-striped table-bordered table-hover dataTable no-footer" ForeColor="#333333" GridLines="None" Height="68px" Width="845px">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="Order_ID" HeaderText="PO Number" />
            <asp:BoundField DataField="Supplier_Name" HeaderText="Supplier" />
            <asp:BoundField DataField="Order_Date" HeaderText="Order Date" />
            <asp:BoundField DataField="Received_Date" HeaderText="Received Date" />
            <asp:BoundField DataField="Ord_Staff" HeaderText="Order Staff" />
            <asp:BoundField DataField="Rec_Staff" HeaderText="Received Staff" />
            <asp:BoundField DataField="Status" HeaderText="Status" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />

        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

    </asp:GridView>--%>
</asp:Content>
