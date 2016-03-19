<%@ Page Title="" Language="C#" MasterPageFile="~/View/StoreClerk/Clerk.Master" AutoEventWireup="true"CodeBehind="PurchaseOrderCancel.aspx.cs" Inherits="LogicUniversity.StoreClerk.Maintain.PurchaseOrderCancel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      
    <script type="text/javascript" >
        function ConfirmOnDelete() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Are you sure to cancel this purchase order?")) {

                confirm_value.value = "Yes";

            } else {

                confirm_value.value = "No";

            }

            document.forms[0].appendChild(confirm_value);
        }
    </script>

     <div class="col-lg-12">
                    <h1 class="page-header">Cancel Purchase Order</h1>
                   
                </div>
   
    <asp:Label ID="LBDesc1" runat="server" style="font-size: small" Text="Orders you have made: " ></asp:Label>
    <asp:GridView ID="GridViewOrder" runat="server" class="table table-striped table-bordered table-hover dataTable no-footer" AutoGenerateColumns="False" OnRowCommand="GridViewOrder_RowCommand" CellPadding="4" ForeColor="#333333" GridLines="None" Width="743px">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="Order_ID" HeaderText="Order ID" />
            <asp:BoundField DataField="Supplier" HeaderText="Supplier" />
            <asp:BoundField DataField="Create_Date" HeaderText="Create Date" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:ButtonField ButtonType="Button" CommandName="viewdetail" HeaderText="View Detail" ControlStyle-CssClass="btn btn-outline btn-primary" Text="View" >
<ControlStyle CssClass="btn btn-outline btn-primary"></ControlStyle>
            </asp:ButtonField>
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
    <br />
    <asp:Panel ID="PanelOrderDetail" runat="server" Visible="False">
        <asp:Label ID="LBOrderdetail" runat="server" style="font-size: small" Text="Order Detail: "></asp:Label>
        <br />
        <asp:GridView ID="GridViewOrderDetail" class="table table-striped table-bordered table-hover dataTable no-footer" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="744px">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="Category" HeaderText="Category" />
                <asp:BoundField DataField="Item_ID" HeaderText="Item ID" />
                <asp:BoundField DataField="Description" HeaderText="Description" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                <asp:BoundField DataField="UOM" HeaderText="UOM" />
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
        <br />
        <asp:Label ID="LBRemark" runat="server" style="font-size: small" Text="Remark: "></asp:Label>
        <br />
    
            &nbsp;<asp:TextBox ID="txtRemark" runat="server"></asp:TextBox>
        <br />&nbsp;<br /><asp:Button ID="BtnCancel" runat="server" class="btn btn-outline btn-primary" OnClick="BtnCancel_Click" OnClientClick="return ConfirmOnDelete();" Text="Cancel Order" />
&nbsp;
    </asp:Panel>
    <br />
    <br />
    <br />
</asp:Content>
