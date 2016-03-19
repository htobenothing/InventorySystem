<%@ Page Title="" Language="C#" MasterPageFile="~/View/StoreClerk/Clerk.Master" AutoEventWireup="true" CodeBehind="CollectionPointCheck.aspx.cs" Inherits="LogicUniversity.StoreClerk.View.CollectionPointCheck" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <style type="text/css">
        .auto-style1 {
            width: 211px;
        }
        .auto-style2 {
            height: 20px;
        }
        .auto-style3 {
            width: 211px;
            height: 20px;
        }
        .auto-style4 {
            height: 40px;
        }
        .auto-style5 {
            width: 211px;
            height: 40px;
        }
    </style>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="col-lg-12">
        <h1 class="page-header">Collection Point Check</h1>

    </div>
    

   
    <asp:RadioButton ID="RBDP" runat="server" Checked="True" GroupName="Select" />
    <asp:Label ID="Label1" runat="server" style="font-size: small" Text="Department Name: "></asp:Label>
    <asp:DropDownList ID="DDLDP" runat="server" Class="btn btn-default btn-xs dropdown-toggle"  Height="34px" Width="165px">
    </asp:DropDownList>
    <br />
    <br />
    <br />
    <asp:RadioButton ID="RBCP" runat="server" GroupName="Select" />
    <asp:Label ID="LBCP" runat="server" style="font-size: small" Text="Collection Point: "></asp:Label>
    &nbsp;&nbsp;
    <asp:DropDownList ID="DDLCP" runat="server" Class="btn btn-default btn-xs dropdown-toggle" Height="34px" Width="165px">
    </asp:DropDownList>
    <br />
    <br />
    <br />
    <asp:Button ID="BtnView" class="btn btn-outline btn-primary" runat="server" OnClick="BtnView_Click" Text="Show" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="BtnViewall" class="btn btn-outline btn-primary" runat="server" Text="Show All" OnClick="BtnViewall_Click" />
    <br />
    <br />
    <asp:Panel ID="PanelTable" runat="server" Visible="False">
        <asp:Label ID="LabelDesc" runat="server" Text="Collection point And Department informaton: " Font-Size="Large"></asp:Label>
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-hover dataTable no-footer" OnRowCommand="GridView1_RowCommand" CellPadding="4" ForeColor="#333333" GridLines="None" Height="16px" Width="783px">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="Collection_ID" HeaderText="Cp. ID" />
                <asp:BoundField DataField="Collection_Desc" HeaderText="Cp. Description" />
                <asp:BoundField DataField="Dept_ID" HeaderText="Dept. ID" />
                <asp:ButtonField ButtonType="Button" CommandName="showdetail" HeaderText="Show Detail" Text="Show"  ControlStyle-CssClass="btn btn-outline btn-primary" />
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
        <asp:Panel ID="PanelDetail" runat="server" Visible="False">
            <table style="width:100%;" >
                <tr>
                                     <td class="auto-style5">
                                         &nbsp;
                        <asp:Label ID="Label14" runat="server" Text="Department Detail" Font-Size="Large"></asp:Label>
                        <br />
                        <br />
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <table >
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style1">
                        <asp:Label ID="Label3" runat="server" Text="Department ID "></asp:Label>
                        <br />
                        <br />
                    </td>
                    <td class="auto-style4">
                        :&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="LBDeptID" runat="server" style="text-align: left"></asp:Label>
                        <br />
                        <br />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style1">
                        <asp:Label ID="Label4" runat="server" Text="Department Name"></asp:Label>
                        <br />
                        <br />
                    </td>
                    <td class="auto-style4">
                        :&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="LBDeptName" runat="server"></asp:Label>
                        <br />
                        <br />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style4"></td>
                    <td class="auto-style5">
                        <asp:Label ID="Label5" runat="server" Text="Contact Name "></asp:Label>
                        <br />
                        <br />
                    </td>
                    <td class="auto-style4">
                        :&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="LBContName" runat="server"></asp:Label>
                        <br />
                        <br />
                    </td>
                    <td class="auto-style4"></td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <br />
                    </td>
                    <td class="auto-style3">
                        <asp:Label ID="Label10" runat="server" Text="Phone No."></asp:Label>
                        <br />
                        <br />
                    </td>
                    <td class="auto-style2">
                        :&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="LBPhNo" runat="server"></asp:Label>
                        <br />
                        <br />
                    </td>
                    <td class="auto-style2"></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style1">
                        <asp:Label ID="Label11" runat="server" Text="Head's name "></asp:Label>
                        <br />
                        <br />
                    </td>
                    <td class="auto-style4">
                        :&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="LBHeadName" runat="server"></asp:Label>
                        <br />
                        <br />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style1">
                        <asp:Label ID="Label12" runat="server" Text="Representative name "></asp:Label>
                        <br />
                        <br />
                    </td>
                    <td class="auto-style4">
                        :&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="LBRepName" runat="server"></asp:Label>
                        <br />
                        <br />
                    </td>
                    <td>&nbsp;</td>

                </tr>
                 <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style1">
                        <asp:Label ID="Label6" runat="server" Text="Collection Description "></asp:Label>
                        <br />
                        <br />
                    </td>
                    <td class="auto-style4">
                        :&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="LBCD" runat="server"></asp:Label>
                        <br />
                        <br />
                    </td>
                    <td>&nbsp;</td>

                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style1">
                        <asp:Label ID="Label13" runat="server" Text="E-mail"></asp:Label>
                    </td>
                    <td class="auto-style4">
                        :&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="LBEmail" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
                                                    
                                                                                        
</asp:Content>
