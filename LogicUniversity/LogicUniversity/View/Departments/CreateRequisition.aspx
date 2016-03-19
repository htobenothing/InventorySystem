<%@ Page Title="" Language="C#" MasterPageFile="~/View/Departments/Dept.Master" AutoEventWireup="true" CodeBehind="CreateRequisition.aspx.cs" Inherits="LogicUniversity.Departments.CreateRequisition" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="col-lg-12">
                    <h1 class="page-header">Create Requisition</h1>
                   
                </div>
    <br />
    <asp:Panel ID="PanelCreateOrder" runat="server">
        Requisition Type&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList Height="34px" Width="165px" Class="btn btn-default btn-xs dropdown-toggle" ID="ddlRequisitiontype" runat="server" >
            <asp:ListItem Text="Normal" Value="Normal"></asp:ListItem>
            <asp:ListItem Text="Urgent" Value="Urgent"></asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <br />
        <asp:Button ID="BtnAdd" class="btn btn-outline btn-primary" runat="server" Text="Add" OnClick="BtnAdd_Click" />
        <br />
        <br />
        <asp:GridView ID="GridViewCreateRequisition" runat="server" class="table table-striped table-bordered table-hover dataTable no-footer" AutoGenerateColumns="False" OnRowCommand="GridViewCreateRequisition_RowCommand" OnRowDataBound="GridViewCreateRequisition_RowDataBound" Style="margin-right: 49px" CellPadding="4" ForeColor="#333333" GridLines="None" Height="16px" Width="807px">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="Category">
                    <ItemTemplate>
                        <asp:DropDownList ID="DropDownListCategory" Height="34px" Width="165px" Class="btn btn-default btn-xs dropdown-toggle" runat="server" AutoPostBack="true" DataTextField="category" OnSelectedIndexChanged="DropDownListCategory_SelectedIndexChanged">
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:DropDownList ID="DropDownListDesc" Height="34px" Width="165px" Class="btn btn-default btn-xs dropdown-toggle" runat="server" AutoPostBack="true" DataTextField="description" OnSelectedIndexChanged="DropDownListDesc_SelectedIndexChanged">
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:TextBox ID="TextboxQty" class="form-control" runat="server" DataTextField="qty" DataValueField="qty">
                        </asp:TextBox>
                        <asp:RangeValidator ID="rvdQty" runat="server" Type="Integer" MinimumValue="0" MaximumValue="5000" ControlToValidate="TextboxQty" ErrorMessage="Enter an integer (0-5000)" Display="Dynamic" ForeColor="Red"></asp:RangeValidator>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UOM">
                    <ItemTemplate>
                        <asp:Label ID="lblUOM" runat="server" DataTextField="uom" DataValueField="uom"> 
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:ButtonField ButtonType="Button" CommandName="DeleteItem"  ControlStyle-CssClass="btn btn-outline btn-primary" Text="Delete" />
               
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
        <asp:Label ID="lblError" runat="server"></asp:Label>
        <br />
        <asp:Button ID="BtnCreate" runat="server" class="btn btn-outline btn-primary" OnClick="BtnCreate_Click" Text="Create Requisition" />
        <br />
        <br />
        <br />
    </asp:Panel>

</asp:Content>
