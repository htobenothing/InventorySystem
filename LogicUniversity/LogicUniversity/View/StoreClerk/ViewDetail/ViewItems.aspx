<%@ Page Title="" Language="C#" MasterPageFile="~/View/StoreClerk/Clerk.Master" AutoEventWireup="true" CodeBehind="ViewItems.aspx.cs" Inherits="LogicUniversity.StoreClerk.View.ViewItems" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 250px;
        }

        .auto-style3 {
            width: 175px;
        }

        .auto-style4 {
            width: 6px;
        }

        .auto-style5 {
            width: 48px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-lg-12">
        <h1 class="page-header">View Item Information</h1>
    </div>

    <br />
    <br />
    <table>
        <tr>
            <td class="auto-style4">&nbsp;</td>
            <td class="auto-style5">Category: 
                <br />
                <br />
            </td>
            <td class="auto-style1">
                <asp:DropDownList ID="ddlCate" runat="server" Class="btn btn-default btn-xs dropdown-toggle" AutoPostBack="True" OnSelectedIndexChanged="ddlCate_SelectedIndexChanged" Height="34px" Width="165px">
                </asp:DropDownList>
                <br />
                <br />
            </td>

        </tr>
        <tr>
            <td class="auto-style4"></td>
            <td class="auto-style5">Item ID: 
                <br />
                <br />
            </td>
            <td class="auto-style3">
                <asp:DropDownList ID="ddlItemID" Class="btn btn-default btn-xs dropdown-toggle" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemID_SelectedIndexChanged" Height="34px" Width="165px">
                </asp:DropDownList>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td class="auto-style4">&nbsp;</td>
            <td class="auto-style5">Description: 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                &nbsp;&nbsp;&nbsp;
                <br />
            </td>
            <td class="auto-style1">
                <asp:DropDownList ID="ddlDesc" Class="btn btn-default btn-xs dropdown-toggle" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDesc_SelectedIndexChanged" Height="34px" Width="165px">
                </asp:DropDownList>
                <br />
                <br />
            </td>

        </tr>
        <tr>
            <td class="auto-style4">&nbsp;</td>
            <td class="auto-style5">&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp; </td>
            <td class="auto-style1">&nbsp;<asp:Button ID="BtnShow" runat="server" class="btn btn-outline btn-primary" OnClick="BtnShow_Click" Text="Show" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="BtnShowAll" class="btn btn-outline btn-primary" runat="server" Text="Show All" OnClick="BtnShowAll_Click" />
            </td>

        </tr>
    </table>
    <br />
    <br />
    <asp:Panel ID="PanelTable" runat="server" Visible="False" Style="text-align: center">

        <br />
        <asp:GridView ID="GridViewItems" class="table table-striped table-bordered table-hover dataTable no-footer" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="GridViewItems_PageIndexChanging" OnRowCommand="GridViewItems_RowCommand" CellPadding="4" ForeColor="#333333" GridLines="None" Height="21px" Width="925px">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="Category_ID" HeaderText="Category" />
                <asp:BoundField DataField="Item_ID" HeaderText="Item ID" />
                <asp:BoundField DataField="Item_Name" HeaderText="Description" />
                <asp:BoundField DataField="Reorder_Level" HeaderText="Reorder Level" />
                <asp:BoundField DataField="Reorder_Qty" HeaderText="Reorder Qty" />
                <asp:BoundField DataField="UOM" HeaderText="UOM" />
                <asp:BoundField DataField="Bin_ID" HeaderText="Bin ID" />
                <asp:BoundField DataField="FirstSupplier_ID" HeaderText="1st Supplier" />
                <asp:BoundField DataField="SecondSupplier_ID" HeaderText="2nd Supplier" />
                <asp:BoundField DataField="ThirdSupplier_ID" HeaderText="3rd Supplier" />
                <asp:ButtonField ButtonType="Button" HeaderText="View Transaction" Text="Show" CommandName="viewdetail" ControlStyle-CssClass="btn btn-outline btn-primary" />
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
    </asp:Panel>
    <asp:Panel ID="PanelDetail" runat="server" Visible="False">
        <div>
            <asp:Image runat="server" ImageUrl="~/picture/red.png" Height="15px" Width="15px" /><asp:Label runat="server" Text="Sent to departments"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Image runat="server" ImageUrl="~/picture/green.png" Height="15px" Width="15px" /><asp:Label runat="server" Text="Received from suppliers"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Image runat="server" ImageUrl="~/picture/yellow.png" Height="15px" Width="15px" /><asp:Label runat="server" Text="Adjustment"></asp:Label>
        </div>
        <asp:Label ID="Label5" runat="server" Text="Item Transaction: "></asp:Label>
        <asp:GridView ID="GridViewItemDetail" runat="server" AutoGenerateColumns="False" CellPadding="4" class="table table-striped table-bordered table-hover dataTable no-footer" ForeColor="#333333" GridLines="None" Height="21px" Width="925px" AllowPaging="True" OnPageIndexChanging="GridViewItemDetail_PageIndexChanging">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="Update_Date" HeaderText="Update date" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="Reason" HeaderText="Reason" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                <asp:BoundField DataField="Balance" HeaderText="Balance" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"  />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
    </asp:Panel>

    <br />
    <br />
</asp:Content>
