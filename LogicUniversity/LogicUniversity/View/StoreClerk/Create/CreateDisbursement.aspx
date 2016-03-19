<%@ Page Title="" Language="C#" MasterPageFile="~/View/StoreClerk/Clerk.Master" AutoEventWireup="true" CodeBehind="CreateDisbursement.aspx.cs" Inherits="ADProject_Team1.View.Store.CreateDisbursement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-lg-12">
        <h1 class="page-header">Create Disbursement</h1>
    </div>
    <div >
    <asp:Label ID="LBStatus" runat="server" ForeColor="Blue" style="font-size: large" Text="There is no Requisition left,Come later please." Visible="False"></asp:Label>

    <asp:GridView ID="requisitionSummaryView" class="table table-striped table-bordered table-hover dataTable no-footer" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="563px" AllowPaging="True" OnPageIndexChanging="requisitionSummaryView_PageIndexChanging">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="requisitionId" HeaderText="Requisition ID" />
            <asp:BoundField DataField="departmentName" HeaderText="Department Name" />           
            <asp:BoundField DataField="orderType" HeaderText="Order Type" />
            <asp:TemplateField HeaderText="Select">
                <ItemTemplate>
                    <asp:CheckBox ID="selectChkBox" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <%--<EmptyDataTemplate>
            <asp:CheckBox ID="selectChkBox" runat="server" />
        </EmptyDataTemplate>--%>
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

    <asp:Button ID="showRetreivalBtn" class="btn btn-outline btn-primary" runat="server" Text="Show Retrieval List" OnClick="showRetreivalBtn_Click" Width="188px" />

    <br />
    <br />
    <asp:Label ID="Retreivallabl" runat="server" Font-Bold="True" Font-Size="Medium" Text="Retrieval List:"></asp:Label>
&nbsp;

    <br />
    <br />
    <asp:GridView ID="itemGridView" runat="server" class="table table-striped table-bordered table-hover dataTable no-footer" AutoGenerateColumns="False" OnRowEditing="itemGridView_RowEditing" Width="563px" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="BinNo" HeaderText="Bin No." />
            <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
            <asp:BoundField DataField="Decription" HeaderText="Description" />
            <asp:BoundField DataField="needed" HeaderText="Needed" />
            <asp:TemplateField HeaderText="Providing">
                <ItemTemplate>
                    <asp:TextBox ID="ProvidingTxtBox" class="form-control" runat="server" AutoPostBack="true" ></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="^[0-9]*$" ErrorMessage="Please enter Numeric number!!!" ControlToValidate="ProvidingTxtBox" Display="Dynamic" ></asp:RegularExpressionValidator>
                </ItemTemplate>
            </asp:TemplateField>
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
    <asp:Button ID="showDisbursementBtn" class="btn btn-outline btn-primary" runat="server" Text="Show Disbursement List" Width="194px" OnClick="showDisbursementBtn_Clicks" />
    <br />
 
    
    <asp:Panel ID="Panel1" runat="server" Height="55px">
        <br />
        <br />
        <br />
        <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
    </asp:Panel>
    <%--<br />

    <br />--%>
</div>
</asp:Content>
