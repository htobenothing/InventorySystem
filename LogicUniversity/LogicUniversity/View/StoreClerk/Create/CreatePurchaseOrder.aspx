<%@ Page Title="" Language="C#" MasterPageFile="~/View/StoreClerk/Clerk.Master" AutoEventWireup="true" CodeBehind="CreatePurchaseOrder.aspx.cs" Inherits="LogicUniversity.StoreClerk.Create.CreatePurchaseOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 39%;
        }
        .auto-style2 {
            width: 159px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="col-lg-12">
        <h1 class="page-header">Create Purchase Order</h1>

    </div>
    <br />
   
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">Expected Delivery Date:</td>
                <td><asp:TextBox ID="TbEDD" runat="server" CssClass="datepicker" OnTextChanged="TbEDD_TextChanged"></asp:TextBox>
                </td>
            </tr>
      </table>
        <%--<asp:ImageButton ID="IBCalendar" runat="server" Height="20px" ImageUrl="~/View/Store/Image/calendar.png" OnClick="IBCalendar_Click" Width="24px" />
        <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged" Visible="False">
            <WeekendDayStyle BackColor="#BEDDFE" />
        </asp:Calendar--%>
        <br />
     <asp:Panel ID="PanelCreateOrder" runat="server">
        <asp:Button ID="BtnAdd" runat="server" Text="Add" class="btn btn-outline btn-primary" OnClick="BtnAdd_Click" />
         <br />
         <br />
        <asp:GridView ID="GridViewCreateOrder" runat="server" class="table table-striped table-bordered table-hover dataTable no-footer" AutoGenerateColumns="False" OnRowCommand="GridViewCreateOrder_RowCommand" OnRowDataBound="GridViewCreateOrder_RowDataBound" style="margin-right: 49px" Height="34px" Width="911px" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="Category">
                    <ItemTemplate>
                        <asp:DropDownList ID="DropDownListCategory" Height="34px" Width="165px" Class="btn btn-default btn-xs dropdown-toggle" runat="server" AutoPostBack="true" DataTextField="category" DataValueField="category" OnSelectedIndexChanged="DropDownListCategory_SelectedIndexChanged">
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item ID">
                    <ItemTemplate>
                        <asp:DropDownList ID="DropDownListItemCode" Height="34px" Width="165px" Class="btn btn-default btn-xs dropdown-toggle" runat="server" AutoPostBack="true" DataTextField="itemID" DataValueField="itemID" OnSelectedIndexChanged="DropDownListItemCode_SelectedIndexChanged">
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:DropDownList ID="DropDownListDesc" Height="34px" Width="165px" Class="btn btn-default btn-xs dropdown-toggle" runat="server" AutoPostBack="true" DataTextField="description" DataValueField="description" OnSelectedIndexChanged="DropDownListDesc_SelectedIndexChanged">
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:TextBox ID="TextboxQty"  class="form-control" runat="server" DataTextField="qty" DataValueField="qty">
                                    </asp:TextBox>
                        <asp:RangeValidator ID="RangeValidatorQty" runat="server" ControlToValidate="TextboxQty" ErrorMessage="*Need Integer(1-9999)" MaximumValue="9999" MinimumValue="1" Type="Integer" ForeColor="Red"></asp:RangeValidator>
                    
                    </ItemTemplate>

                </asp:TemplateField>
                <asp:TemplateField HeaderText="UOM">
                    <ItemTemplate>
                        <asp:TextBox ID="TextboxUOM" class="form-control" runat="server" DataTextField="uom" DataValueField="uom" ReadOnly="true">
                                    </asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                 
                <asp:ButtonField ButtonType="Button" CommandName="DeleteItem" Text="Delete" ControlStyle-CssClass="btn btn-outline btn-primary"/>
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
        
        
        
        <asp:Label ID="LBforValidation" runat="server" ForeColor="Red"></asp:Label>
        <br />
        <asp:Label ID="LBEDDValidate" runat="server" ForeColor="Red"></asp:Label>
         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TbEDD" ErrorMessage="Incorrect date format!" ForeColor="Red" style="font-size: 14px" ValidationExpression="^\d{2}/\d{2}/\d{4}$|^\d{1}/\d{1}/\d{4}$"></asp:RegularExpressionValidator>
        <br />
        
        
        
        <asp:Button ID="BtnCreate" runat="server" class="btn btn-outline btn-primary" OnClick="BtnCreate_Click" Text="Create" />
    </asp:Panel>
    <br />
    <asp:Label ID="Label2" runat="server"></asp:Label>
    <br />
    <br />
    <br />
    <div id="DivSup1">
        <asp:Panel ID="PanelSup1" runat="server" Visible="False">
            <asp:Label ID="SupTitle1" runat="server" style="font-size: x-large" Text="Logic University"></asp:Label>
            <br />
            <asp:Label ID="SupSubtitle1" runat="server" style="font-size: medium" Text="Purchase Order"></asp:Label>
            <br />
            <asp:Label ID="LbOrderID1" runat="server" Text="Order ID: "></asp:Label>
            <br />
            &nbsp;<asp:Label ID="LbSUP1" runat="server">Supplier: ALPA</asp:Label>
            <br />
            <asp:Label ID="LbSupEDD1" runat="server" Text="Expected Delivery Date: "></asp:Label>
            <asp:GridView ID="GridViewALPH" runat="server" class="table table-striped table-bordered table-hover dataTable no-footer" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="16px" Width="786px">
                
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
        <br />
    </div>
    <div id="DivSup2">
         <asp:Panel ID="PanelSup2" runat="server" Visible="False">
             <asp:Label ID="SupTitle2" runat="server" style="font-size: x-large" Text="Logic University"></asp:Label>
             <br />
             <asp:Label ID="SupSubtitle2" runat="server" style="font-size: medium" Text="Purchase Order"></asp:Label>
             <br />
             <asp:Label ID="LbOrderID2" runat="server" Text="Order ID: "></asp:Label>
             <br />
            <asp:Label ID="LbSUP2" runat="server" Text="Supplier: BANE"></asp:Label>
             <br />
             <asp:Label ID="LbSupEDD2" runat="server" Text="Expected Delivery Date: "></asp:Label>
            <asp:GridView ID="GridViewBANE" runat="server" class="table table-striped table-bordered table-hover dataTable no-footer" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="16px" Width="792px">
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
        <br />          
    </div>
    <div id="DivSup3">
         <asp:Panel ID="PanelSup3" runat="server" Visible="False">
             <asp:Label ID="SupTitle3" runat="server" style="font-size: x-large" Text="Logic University"></asp:Label>
             <br />
             <asp:Label ID="SupSubtitle3" runat="server" style="font-size: medium" Text="Purchase Order"></asp:Label>
             <br />
             <asp:Label ID="LbOrderID3" runat="server" Text="Order ID: "></asp:Label>
             <br />
          <asp:Label ID="LbSup3" runat="server" Text="Supplier: CHEP"></asp:Label>
             <br />
             <asp:Label ID="LbSupEDD3" runat="server" Text="Expected Dellivery Date: "></asp:Label>
          <asp:GridView ID="GridViewCHEP" runat="server" class="table table-striped table-bordered table-hover dataTable no-footer" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="16px" Width="725px">
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
        <br /> 
    </div>
    <div id="DivSup4">
       <asp:Panel ID="PanelSup4" runat="server" Visible="False">
           <asp:Label ID="SupTitle4" runat="server" style="font-size: x-large" Text="Logic University"></asp:Label>
           <br />
           <asp:Label ID="SupSubtitle4" runat="server" style="font-size: medium" Text="Purchase Order"></asp:Label>
           <br />
           <asp:Label ID="LbOrderID4" runat="server" Text="Order ID: "></asp:Label>
           <br />
         <asp:Label ID="LbSup4" runat="server" Text="Supplier: OMEG"></asp:Label>
           <br />
           <asp:Label ID="LbSupEDD4" runat="server" Text="Expected Delivery Date: "></asp:Label>
         <asp:GridView ID="GridViewOMEG" runat="server" class="table table-striped table-bordered table-hover dataTable no-footer" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="16px" Width="744px">
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
    </div>
    <asp:Panel ID="PanelContorl" runat="server" Visible="False">
        <asp:Label ID="LBNotEnough" runat="server" ForeColor="Red"></asp:Label>
        <br />
        <asp:Button ID="BtnConfirm" class="btn btn-outline btn-primary" runat="server" Text="Send to Suppliers" OnClick="BtnConfirm_Click" />
        &nbsp;&nbsp; &nbsp;<asp:Button ID="BtnBack" class="btn btn-outline btn-primary" runat="server" OnClick="BtnBack_Click" Text="Back" />
    </asp:Panel>
</asp:Content>
