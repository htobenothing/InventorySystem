<%@ Page Title="" Language="C#" MasterPageFile="~/View/StoreClerk/Clerk.Master" AutoEventWireup="true" CodeBehind="SearchAdjustmentVoucher.aspx.cs" Inherits="LogicUniversity.StoreClerk.View.AdjustmentVoucher" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
   
      <div class="col-lg-12">
        <h1 class="page-header">Adjustment Voucher</h1>
    </div>
             
   
    

    
   
  <table>
        
        
        <tr>
            <td align="right">From&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; : </td>
            <td>
                
                <asp:TextBox ID="fromDate" runat="server" class="datepicker" type="text" > </asp:TextBox>
                  <asp:RequiredFieldValidator ID="fromDateValidation" runat="server" ControlToValidate="fromDate" ErrorMessage="Please select any Date!!" ForeColor="#FF3300"></asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
            <td align="right">
                <br />
                To&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :</td>
            <td>
                <br />
                <asp:TextBox ID="toDate" runat="server" class="datepicker" type="text"> </asp:TextBox>
                  <asp:RequiredFieldValidator ID="toDateValidation" runat="server" ControlToValidate="toDate" ErrorMessage="Please select any Date!!" ForeColor="#FF3300"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                <br />
                Status&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :</td>
            <td>
                    <br />
                    <asp:DropDownList ID="statusDropdownlist" Class="btn btn-default btn-xs dropdown-toggle" runat="server" Height="34px" Width="165px">
                    </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                        <br />
                        <br />
                        <asp:Button ID="searchBtn"  class="btn btn-outline btn-primary" runat="server" Text="Search" OnClick="searchBtn_Click" />
                        <asp:Label ID="LBDateAlert" runat="server" ForeColor="Red"></asp:Label>
                    </td>
        </tr>
    </table>   
    
             
   
    

    
   
      <asp:Label ID="LBStatus" runat="server" ForeColor="Blue" style="font-size: medium" Text="There is no result." Visible="False"></asp:Label>
    
             
   
    

    
   
   <br />
     <asp:GridView ID="vouchersGridView" runat="server" AutoGenerateColumns="False" CellPadding="4" class="table table-striped table-bordered table-hover dataTable no-footer" ForeColor="#333333" GridLines="None" Height="68px" Width="662px" OnRowCommand="vouchersGridView_RowCommand" AllowPaging="True" OnPageIndexChanging="vouchersGridView_PageIndexChanging" >
          <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
          <Columns>
              <asp:BoundField Datafield="voucher_Id" HeaderText="Voucher ID" />
              <asp:BoundField Datafield="IssueDate" HeaderText="Issue Date" DataFormatString="{0:yyyy-MM-dd}" />
              <asp:BoundField Datafield="IssueBy" HeaderText="Issue By" />
              <asp:BoundField Datafield="status" HeaderText="Authorized Status" />
              <%--<asp:TemplateField HeaderText="Detail" >
                <ItemTemplate>
                    <asp:Button ID="detailBtn" class="btn btn-outline btn-primary" runat="server" CommandName="ShowDetail" Text="Show Detail" />
                </ItemTemplate>
            </asp:TemplateField>--%>
              <asp:ButtonField ButtonType="Button" Text="Show Detail"  CommandName="ShowDetail" ControlStyle-CssClass="btn btn-outline btn-primary" >
<ControlStyle CssClass="btn btn-outline btn-primary"></ControlStyle>
              </asp:ButtonField>
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
    
             
   
    
      <br />
    
             
   
    
    <br />
      <asp:Label ID="detailLbl" runat="server" Font-Bold="True" Font-Size="Small" Text="Voucher Detail:"></asp:Label>
    <br />
    <asp:GridView ID="VoucherDetailGridView" runat="server" AutoGenerateColumns="False" CellPadding="4" class="table table-striped table-bordered table-hover dataTable no-footer" ForeColor="#333333" GridLines="None" Height="68px" Width="662px">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField Datafield="Category" HeaderText="Item Catagory" />
            <asp:BoundField Datafield="Code" HeaderText="Item Code" />
            <asp:BoundField Datafield="Description" HeaderText="Description" />
            <asp:BoundField Datafield="Price" HeaderText="Price" />
          
            <asp:BoundField Datafield="QtyAdj" HeaderText="Quantity Adjusted" />
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
      <asp:Label ID="remarkLbl" runat="server" Font-Bold="True" Font-Size="Small" Text="Remark:"></asp:Label>
               

   
      <br />
      <textarea id="remarkArea" cols="20" name="S1" rows="2" runat="server"></textarea><br />
    <br />

   
</asp:Content>
