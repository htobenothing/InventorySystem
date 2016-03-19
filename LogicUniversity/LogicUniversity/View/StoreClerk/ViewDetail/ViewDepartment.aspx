<%@ Page Title="" Language="C#" MasterPageFile="~/View/StoreClerk/Clerk.Master" AutoEventWireup="true" CodeBehind="ViewDepartment.aspx.cs" Inherits="LogicUniversity.StoreClerk.View.ViewDepartment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style2 {
            width: 106px;
        }
        .auto-style4 {
            width: 109px;
        }
        .auto-style7 {
            width: 120px;
        }
        .auto-style8 {
            width: 184px;
        }
        .auto-style11 {
            width: 146px;
        }
        .auto-style12 {
            width: 134px;
        }
    .auto-style14 {
        width: 23px;
    }
    .auto-style15 {
        width: 285px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <div class="col-lg-12">
        <h1 class="page-header">Department List</h1>

    </div>

    <table>
         <tr>
             <td align="right" class="auto-style11">Department Name&nbsp;&nbsp;&nbsp; </td>
             <td class="auto-style8">
                                                <asp:DropDownList ID="ddlDept" Class="btn btn-default btn-xs dropdown-toggle"  runat="server" Height="30px" Width="156px">
                                                </asp:DropDownList>
                  
            
                                            </td>
             <td class="auto-style12">
                                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="ShowBtn" class="btn btn-outline btn-primary" runat="server" Text="Show" OnClick="ShowBtn_Click"  />
                                            </td>
             <td class="auto-style7">
                                                &nbsp;&nbsp;<asp:Button ID="Button3" class="btn btn-outline btn-primary" runat="server" Text="Show All" OnClick="ShowAll_Click" />
                                            </td>
         </tr>
     </table>

   

    
    <br />
  
     <br />
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="mesglbl" runat="server" Text="" Font-Size="Medium"></asp:Label>
    <asp:GridView ID="gvDept" runat="server"  AutoGenerateColumns="False" CellPadding="4" class="table table-striped table-bordered table-hover dataTable no-footer" ForeColor="#333333" GridLines="None" Height="68px" Width="662px" OnRowCommand="gvDept_RowCommand">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="Dept_ID" HeaderText="Dept Code" />
            <asp:BoundField DataField="Dept_Name" HeaderText="Dept Name" />
            <asp:BoundField  DataField="CollectionPoint" HeaderText="Collection Point" />

            <%--<asp:TemplateField HeaderText="Detail">
               <ItemTemplate>
                     <asp:Button ID="showDetailBtn" runat="server" class="btn btn-outline btn-primary" OnClick="ShowDetailBtn_Click" Text="Detail" />
                 </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:ButtonField ButtonType="Button" CommandName="ShowDetailBtn_Click" HeaderText="Show Detail" Text="Show"  ControlStyle-CssClass="btn btn-outline btn-primary" />
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
                      
                    <td>
                        <asp:Label ID="Label15" runat="server" Font-Size="Large" Text="Department Detail"></asp:Label>
                                     <br />
                        <br />
                                     </td>
                </tr>
            </table>
            <table >
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style11">
                        <asp:Label ID="Label3" runat="server" Text="Department ID "></asp:Label>
                        <br />
                        <br />
                    </td>
                    <td class="auto-style15">
                        :&nbsp;
                        <asp:Label ID="LBDeptID" runat="server" style="text-align: left"></asp:Label>
                        <br />
                        <br />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style11">
                        <asp:Label ID="Label4" runat="server" Text="Department Name"></asp:Label>
                        <br />
                        <br />
                    </td>
                    <td class="auto-style15">
                        :&nbsp;
                        <asp:Label ID="LBDeptName" runat="server"></asp:Label>
                        <br />
                        <br />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td></td>
                    <td class="auto-style11">
                        <asp:Label ID="Label5" runat="server" Text="Contact Name "></asp:Label>
                        <br />
                        <br />
                    </td>
                    <td class="auto-style15">
                        :&nbsp;
                        <asp:Label ID="LBContName" runat="server"></asp:Label>
                        <br />
                        <br />
                    </td>
                    <td class="auto-style4"></td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                    <td class="auto-style11">
                        <asp:Label ID="Label10" runat="server" Text="Phone No."></asp:Label>
                        <br />
                        <br />
                    </td>
                    <td class="auto-style15">
                        :&nbsp;
                        <asp:Label ID="LBPhNo" runat="server"></asp:Label>
                        <br />
                        <br />
                    </td>
                    <td class="auto-style2"></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style11">
                        <asp:Label ID="Label11" runat="server" Text="Head's name "></asp:Label>
                        <br />
                        <br />
                    </td>
                    <td class="auto-style15">
                        :&nbsp;
                        <asp:Label ID="LBHeadName" runat="server"></asp:Label>
                        <br />
                        <br />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style11">
                        <asp:Label ID="Label12" runat="server" Text="Representative name "></asp:Label>
                        <br />
                        <br />
                    </td>
                    <td class="auto-style15">
                        :&nbsp;
                        <asp:Label ID="LBRepName" runat="server"></asp:Label>
                        <br />
                        <br />
                    </td>
                    <td>&nbsp;</td>

                </tr>
                 <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style11">
                        <asp:Label ID="Label6" runat="server" Text="Collection Description "></asp:Label>
                        <br />
                        <br />
                    </td>
                    <td class="auto-style15">
                        :&nbsp;
                        <asp:Label ID="LBCD" runat="server"></asp:Label>
                        <br />
                        <br />
                    </td>
                    <td>&nbsp;</td>

                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style11">
                        <asp:Label ID="Label13" runat="server" Text="E-mail"></asp:Label>
                    </td>
                    <td class="auto-style15">
                        :&nbsp;
                        <asp:Label ID="LBEmail" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </asp:Panel>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                                                    </asp:Content>
