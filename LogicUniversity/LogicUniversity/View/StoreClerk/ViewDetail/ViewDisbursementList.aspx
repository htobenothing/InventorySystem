<%@ Page Title="" Language="C#" MasterPageFile="~/View/StoreClerk/Clerk.Master" AutoEventWireup="true" CodeBehind="ViewDisbursementList.aspx.cs" Inherits="LogicUniversity.StoreClerk.View.ViewDisbursementList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="col-lg-12">
        <h1 class="page-header">Disbursement List</h1>
    </div>






    <table>


        <tr>
            <td align="right">From&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; : </td>
            <td>

                <asp:TextBox ID="fromDate" runat="server" class="datepicker" type="text"></asp:TextBox>

                <asp:RequiredFieldValidator ID="fromDateValidation" runat="server" ControlToValidate="fromDate" ErrorMessage="Please select any date!!" ForeColor="Red"></asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
            <td align="right">
                <br />
                To&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :</td>
            <td>
                <br />
                <asp:TextBox ID="toDate" runat="server" class="datepicker" type="text"></asp:TextBox>
                <asp:RequiredFieldValidator ID="toDateValidation" runat="server" ControlToValidate="toDate" ErrorMessage="Please select any Date!!" ForeColor="#FF3300"></asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td align="right">
                <br />
                Department&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :</td>
            <td>
                <br />
                <asp:DropDownList ID="departmentDropDownList" Class="btn btn-default btn-xs dropdown-toggle" runat="server" Height="34px" Width="165px">
                    <asp:ListItem>--Select Department--</asp:ListItem>
                    <asp:ListItem>English Dept </asp:ListItem>
                    <asp:ListItem>Commerce Dept</asp:ListItem>
                    <asp:ListItem>Computer Science</asp:ListItem>
                    <asp:ListItem>Registrar Dept</asp:ListItem>
                    <asp:ListItem>Zoology Dept</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                <br />
                Status&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :</td>
            <td>
                <br />
                <asp:DropDownList ID="statusdropdownalist" Class="btn btn-default btn-xs dropdown-toggle" runat="server" Height="34px" Width="165px">
                    <asp:ListItem>--Select Status--</asp:ListItem>
                    <asp:ListItem>Submitted</asp:ListItem>
                    <asp:ListItem>Received</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <br />
                <br />
                <asp:Button ID="searchBtn" class="btn btn-outline btn-primary" runat="server" Text="Search" OnClick="searchBtn_Click" />
                <asp:Label ID="LBDateAlert" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>







    <asp:Label ID="LBStatus" runat="server" ForeColor="Blue" Style="font-size: medium" Text="There is no result." Visible="False"></asp:Label>







    <br />

    <asp:GridView ID="disbursementListView" runat="server" AutoGenerateColumns="False" CellPadding="4" class="table table-striped table-bordered table-hover dataTable no-footer" ForeColor="#333333" GridLines="None" Height="68px" Width="900px" OnRowCommand="DisbursementListGridView_RowCommand" AllowPaging="True" OnPageIndexChanging="disbursementListView_PageIndexChanging">
        <Columns>
            <asp:BoundField DataField="discriptionId" HeaderText="Disbursement ID" />
            <asp:BoundField DataField="collectionDesc" HeaderText="Collection Description" />
            <asp:BoundField DataField="CreateDate" HeaderText="Create Date" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="ReceiveDate" HeaderText="Receive Date" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="status" HeaderText="Status" />
            <%--<asp:TemplateField HeaderText="Detail" >
                <ItemTemplate>
                    <asp:Button ID="detailBtn" class="btn btn-outline btn-primary" runat="server" CommandName="ShowDetail" Text="Show Detail" />
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:ButtonField ButtonType="Button" Text="Show Detail" CommandName="ShowDetail" ControlStyle-CssClass="btn btn-outline btn-primary">
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
    <asp:Label ID="deptNameLbl" runat="server" Font-Bold="True" Font-Size="Medium" Text="Department Name: "></asp:Label>
    <asp:Label ID="deptNameValLbl" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
    <br />



    <br />



    <asp:GridView ID="disbursementDetailView" runat="server" class="table table-striped table-bordered table-hover dataTable no-footer" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="68px" Width="662px">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>

            <asp:BoundField DataField="itemId" HeaderText="Item Code" />
            <asp:BoundField DataField="itemDesc" HeaderText="Item Description" />
            <asp:BoundField DataField="qty" HeaderText="Total Quantity" />
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

</asp:Content>
