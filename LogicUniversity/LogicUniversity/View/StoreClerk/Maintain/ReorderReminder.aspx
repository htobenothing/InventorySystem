<%@ Page Title="" Language="C#" MasterPageFile="~/View/StoreClerk/Clerk.Master" AutoEventWireup="true" CodeBehind="ReorderReminder.aspx.cs" Inherits="LogicUniversity.StoreClerk.Maintain.ReorderReminder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="col-lg-12">
                    <h1 class="page-header">Reorder Reminder</h1>
                   
                </div>
      <asp:Panel ID="PanelMain" runat="server">
    
        <br />
        <asp:Label ID="Label1" runat="server" style="font-size: small" Text="Generate Time: "></asp:Label>
        <asp:Label ID="LBTime" runat="server" ForeColor="Red" style="font-size: small"></asp:Label>
        <br />
        <asp:Label ID="LBDesc" runat="server" style="font-size: small" Text="The following items have fallen below re-order level:"></asp:Label>
          <br />
          <asp:Image runat="server"  ImageUrl="~/picture/red.png" Height="15px" Width="15px" /><asp:Label runat="server" Text="Inventory lower than re-order level"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Image runat="server"  ImageUrl="~/picture/yellow.png" Height="15px" Width="15px" /><asp:Label runat="server" Text="Inventory add ordering quantity higher than re-order level"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <br />
          <asp:Panel ID="PanelTable" runat="server">
              <asp:GridView ID="GridViewROtable" runat="server" AutoGenerateColumns="False" CellPadding="4" class="table table-striped table-bordered table-hover dataTable no-footer" ForeColor="#333333" GridLines="None" Height="16px" Width="895px">
                  <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                  <Columns>
                      <asp:TemplateField HeaderText="Reorder">
                          <ItemTemplate>
                              <asp:CheckBox ID="CheckBoxReorder" runat="server" />
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField DataField="Category_ID" HeaderText="Category" />
                      <asp:BoundField DataField="Item_ID" HeaderText="Item ID" />
                      <asp:BoundField DataField="Item_Name" HeaderText="Description" />
                      <asp:BoundField DataField="Inventory" HeaderText="Qty in Inventroy" />
                      <asp:BoundField DataField="InventoryAddOrder" HeaderText="Qty Inventroy add Ordering" />
                      <asp:BoundField DataField="Reorder_Level" HeaderText="Reorder Level" />
                      <asp:BoundField DataField="Reorder_Qty" HeaderText="Reorder Qty" />
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
              <asp:CheckBox ID="CheckBoxReorderAll" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBoxReorderAll_CheckedChanged" style="font-size: small" Text="Reorder All" />
              <asp:CheckBox ID="CBReorderAllRed" runat="server" AutoPostBack="True" OnCheckedChanged="CBReorderAllRed_CheckedChanged" style="font-size: small" Text="Reorder All Red" />
              <br />
              <asp:Button ID="BtnCreateOrder" runat="server" class="btn btn-outline btn-primary" OnClick="BtnCreateOrder_Click" Text="Create Order" />
          </asp:Panel>
        <br />
        &nbsp;&nbsp;
        <br />
    </asp:Panel>
    <br />
        <asp:Panel ID="Panel1" runat="server" Visible="False">
            <asp:Label ID="SupTitle1" runat="server" style="font-size: x-large" Text="Logic University"></asp:Label>
            <br />
            <asp:Label ID="SupSubtitle1" runat="server" style="font-size: medium" Text="Purchase Order"></asp:Label>
            <br />
            <asp:Label ID="LbOrderID1" runat="server" Text="Order ID: "></asp:Label>
            <br />
            &nbsp;<asp:Label ID="LbSUP1" runat="server">Supplier: ALPA</asp:Label>
            <br />
            <asp:GridView ID="GridViewALPH" runat="server" class="table table-striped table-bordered table-hover dataTable no-footer" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="16px" Width="807px">
                
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
         </asp:Panel>
            <br />
            <asp:Panel ID="Panel2" runat="server" Visible="False">
                <asp:Label ID="SupTitle2" runat="server" style="font-size: x-large" Text="Logic University"></asp:Label>
                <br />
                <asp:Label ID="SupSubtitle2" runat="server" style="font-size: medium" Text="Purchase Order"></asp:Label>
                <br />
                <asp:Label ID="LbOrderID2" runat="server" Text="Order ID: "></asp:Label>
                <br />
                <asp:Label ID="LbSUP2" runat="server" Text="Supplier: BANE"></asp:Label>
                <br />
                <asp:GridView ID="GridViewBANE" class="table table-striped table-bordered table-hover dataTable no-footer" runat="server" AutoGenerateColumns="False" Height="28px" Width="688px" CellPadding="4" ForeColor="#333333" GridLines="None">
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
            </asp:Panel>
            <asp:Panel ID="Panel3" runat="server" Visible="False">
                <asp:Label ID="SupTitle3" runat="server" style="font-size: x-large" Text="Logic University"></asp:Label>
                <br />
                <asp:Label ID="SupSubtitle3" runat="server" style="font-size: medium" Text="Purchase Order"></asp:Label>
                <br />
                <asp:Label ID="LbOrderID3" runat="server" Text="Order ID: "></asp:Label>
                <br />
                <asp:Label ID="LbSup3" runat="server" Text="Supplier: CHEP"></asp:Label>
                <br />
                <asp:GridView ID="GridViewCHEP" class="table table-striped table-bordered table-hover dataTable no-footer" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="24px" Width="740px">
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
            </asp:Panel>
            <asp:Panel ID="Panel4" runat="server" Visible="False">
                <asp:Label ID="SupTitle4" runat="server" style="font-size: x-large" Text="Logic University"></asp:Label>
                <br />
                <asp:Label ID="SupSubtitle4" runat="server" style="font-size: medium" Text="Purchase Order"></asp:Label>
                <br />
                <asp:Label ID="LbOrderID4" runat="server" Text="Order ID: "></asp:Label>
                <br />
                <asp:Label ID="LbSup4" runat="server" Text="Supplier: OMEG"></asp:Label>
                <br />
                <asp:GridView ID="GridViewOMEG" class="table table-striped table-bordered table-hover dataTable no-footer" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="16px" Width="747px">
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
            </asp:Panel>
            <asp:Panel ID="Panelcontrol" runat="server" Visible="False">
                <asp:Label ID="LBNotEnough" runat="server" ForeColor="Red"></asp:Label>
                <br />
                <asp:Button ID="BtnSubmit" runat="server" class="btn btn-outline btn-primary" OnClick="BtnConfirm_Click" Text="Submit to Suppliers" />
                &nbsp; &nbsp;<asp:Button ID="BtnBack" class="btn btn-outline btn-primary" runat="server" OnClick="BtnBack_Click" Text="Back" />
            </asp:Panel>
            <br />

</asp:Content>
