<%@ Page Title="" Language="C#" MasterPageFile="~/View/StoreClerk/Clerk.Master" AutoEventWireup="true" CodeBehind="AuthorizeAdjVoucher.aspx.cs" Inherits="LogicUniversity.Supervisor.AuthorizeAdjVoucher" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="col-lg-12">
        <h1 class="page-header">Authorize Adjustment Voucher</h1>
    </div>


     <asp:Label ID="LBStatus" runat="server" ForeColor="Blue" style="font-size: large" Text="There is no adjustment voucher need to be approved now." Visible="False"></asp:Label>


    <asp:GridView ID="VoucherSummaryView" runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-hover dataTable no-footer" OnRowCommand="VoucherSummaryView_RowCommand" Width="800px" CellPadding="4" ForeColor="#333333" GridLines="None" Height="281px" >
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="VoucherId" HeaderText="Voucher ID" />
            <asp:BoundField DataField="issuedate" HeaderText="Date Issue" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="staffname" HeaderText="Clerk" />
   <%--   <asp:TemplateField HeaderText="Detail" >
             <ItemTemplate>
                    <asp:Button ID="Button1" class="btn btn-outline btn-primary" CommandName="VoucherDetail" runat="server" Text="Detail" />
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:ButtonField ButtonType="Button" Text="Voucher Detail"  CommandName="VoucherDetail" ControlStyle-CssClass="btn btn-outline btn-primary" >
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
    <br />
    <br />
    <asp:GridView ID="voucherDetailView" runat="server" AutoGenerateColumns="False" onrowdatabound="voucherDetailView_RowDataBound" class="table table-striped table-bordered table-hover dataTable no-footer" Height="63px"  Width="709px" ViewStateMode="Enabled" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField Datafield="Category" HeaderText="Item Category" />
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
     <asp:Panel ID="PanelRemark" runat="server">
         <asp:Label ID="remarkLbl" runat="server" Font-Bold="True" Font-Size="Medium" Text="Remark:"></asp:Label>
         <br />
         <textarea id="remarkTxtArea" name="S1" rows="6" cols="60" runat="server"></textarea><br />
         <br />
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Button ID="approveBtn" runat="server" class="btn btn-outline btn-primary" Text="Approve" Width="177px" OnClick="approveBtn_Click" />
     </asp:Panel>
    <br />
    </asp:Content>
