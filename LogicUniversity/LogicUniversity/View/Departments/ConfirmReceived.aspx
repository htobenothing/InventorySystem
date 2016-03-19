<%@ Page Title="" Language="C#" MasterPageFile="~/View/Departments/Dept.Master" AutoEventWireup="true" CodeBehind="ConfirmReceived.aspx.cs" Inherits="LogicUniversity.Departments.ConfirmReceived" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 14%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-lg-12">
        <h1 class="page-header">Confirmed Received</h1>

    </div>


    <br />
    <table class="auto-style1">
        <tr>
            <td>Disbursement&nbsp;&nbsp;ID:&nbsp; </td>
            <td>


                <asp:TextBox ID="txtDisbursmentID" class="form-control" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    <asp:Button runat="server" Text="search" class="btn btn-outline btn-primary" ID="btnSearch" OnClick="btnSearch_Click" />
    <br />
    <asp:Label ID="LBStatus" runat="server" ForeColor="Blue" Style="font-size: large"></asp:Label>
    <asp:GridView ID="dgvDisbursmentItem" class="table table-striped table-bordered table-hover dataTable no-footer" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="16px" Width="848px">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
            <asp:BoundField DataField="ItemDes" HeaderText="Item Description" />
            <asp:BoundField DataField="NeedQty" HeaderText="Need Quantity" />
            <asp:TemplateField HeaderText="Received Quantity">

                <ItemTemplate>
                    <asp:TextBox ID="txtQty" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtQty" ErrorMessage="Pls enter Quantity" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RangeValidator Type="Integer" MinimumValue="0" MaximumValue="5000" runat="server" ControlToValidate="txtQty" ErrorMessage="Quantity Range 0 to 5000" Display="Dynamic"></asp:RangeValidator>
                </ItemTemplate>

            </asp:TemplateField>
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
    <p>
        <asp:CheckBox ID="cbxReserved" Text="Need send the rest item" runat="server" />
    </p>
    <p>
        &nbsp;
    </p>
    <p>
        <asp:Button ID="btnConfirm" runat="server" class="btn btn-outline btn-primary" Text="Confirm Received" OnClick="btnConfirm_Click" />
    </p>
    <p>
        &nbsp;
    </p>
    <p>
        <asp:Label ID="lblReqtype" runat="server" Text="Rquisition Type"></asp:Label>
        <asp:Label ID="lblSpecial" runat="server" Text="Special" ForeColor="Red"></asp:Label>
    </p>
    <p>
        <asp:GridView ID="dgvSpeRequisition" runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-hover dataTable no-footer" OnRowCommand="dgvSpeRequisition_RowCommand" CellPadding="4" ForeColor="#333333" GridLines="None" Height="21px" Width="897px">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="itemName" HeaderText="Item Description" />
                <asp:BoundField DataField="restQty" HeaderText="Need Quantity" />
                <asp:BoundField DataField="uom" HeaderText="UOM" />
                <asp:ButtonField ButtonType="Button" Text="Delete" ControlStyle-CssClass="btn btn-outline btn-primary" CommandName="DeleteItem">
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
    </p>
    <p>
        <asp:Button ID="btnCreate" class="btn btn-outline btn-primary" runat="server" Text="Create" OnClick="btnCreate_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnCancel" class="btn btn-outline btn-primary" runat="server" Text="cancel" OnClick="btnCancel_Click" />
    </p>
    <p>
        &nbsp;
    </p>

</asp:Content>
