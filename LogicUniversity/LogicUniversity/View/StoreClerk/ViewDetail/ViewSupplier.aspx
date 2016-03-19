<%@ Page Title="" Language="C#" MasterPageFile="~/View/StoreClerk/Clerk.Master" AutoEventWireup="true" CodeBehind="ViewSupplier.aspx.cs" Inherits="LogicUniversity.StoreClerk.View.ViewSupplier" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style7 {
            width: 120px;
        }
        .auto-style11 {
            width: 114px;
        }
        .auto-style12 {
            width: 155px;
        }
        .auto-style13 {
            width: 100px;
        }
    .auto-style14 {
        width: 156px;
    }
    .auto-style17 {
        width: 179px;
    }
    .auto-style19 {
        width: 239px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-lg-12">
                    <h1 class="page-header">Supplier List</h1>
                   
                </div>
   <table>
         <tr>
             <td align="right" class="auto-style11">Supplier Code&nbsp;&nbsp;&nbsp;</td>
             <td class="auto-style12">
                                                <asp:DropDownList ID="ddlsupplier" Class="btn btn-default btn-xs dropdown-toggle" runat="server" Height="30px" Width="156px">
                                                </asp:DropDownList>
                  
            
                                            </td>
             <td class="auto-style13">
                                               &nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="ShowBtn" class="btn btn-outline btn-primary" runat="server" Text="Show" OnClick="ShowBtn_Click"  />
                                            </td>
             <td class="auto-style7">
                                                 &nbsp;&nbsp;
                                                <asp:Button ID="Button3" class="btn btn-outline btn-primary" runat="server" Text="Show All" OnClick="ShowAll_Click" />
                                            </td>
         </tr>
     </table>
     <br />
   
     <br />
     <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="mesglbl" runat="server" Text="" Font-Size="Medium"></asp:Label>
     <asp:GridView ID="gvSupplier" runat="server" AutoGenerateColumns="False" CellPadding="4" class="table table-striped table-bordered table-hover dataTable no-footer" ForeColor="#333333" GridLines="None" Height="68px" Width="662px" OnRowCommand="gvSupplier_RowCommand" >
         <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
         <Columns>
             <asp:BoundField DataField="GST_Registration_No" HeaderText="GST Registration No" />
             <asp:BoundField DataField="Code" HeaderText="Code" />
             <asp:BoundField DataField="Name" HeaderText="Name" />
             <asp:BoundField DataField="Email" HeaderText="Email" />
             
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
                                     <td class="auto-style14">
                        <asp:Label ID="Label14" runat="server" Text="Supplier Detail" Font-Size="Large"></asp:Label>
                        <br />
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <table >
                <tr>
                    
                    <td class="auto-style17">
                        <asp:Label ID="Label3" runat="server" Text="GST Registration No"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :</td>
                    <td class="auto-style19">
                        &nbsp;<br /> <asp:Label ID="LBGSTNo" runat="server" style="text-align: left"></asp:Label>
                        <br />
                        <br />
                    </td>
                  
                </tr>
                <tr>
                    
                    <td class="auto-style17">
                        <asp:Label ID="Label4" runat="server" Text="Supplier Code"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;:<br />
                        <br />
                    </td>
                    <td class="auto-style19">
                        &nbsp;<asp:Label ID="LBSupCode" runat="server"></asp:Label>
                        <br />
                        <br />
                    </td>
                 
                </tr>
                <tr>
                   
                    <td class="auto-style17">
                        <asp:Label ID="Label5" runat="server" Text="Supplier Name"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; :<br />
                        <br />
                    </td>
                    <td class="auto-style19">
                        &nbsp;<asp:Label ID="LBSupName" runat="server"></asp:Label>
                        <br />
                        <br />
                    </td>
                    
                </tr>
               
                    <tr>
                        <td class="auto-style17">
                            <asp:Label ID="Label10" runat="server" Text="Contact Name"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :<br />
                            <br />
                        </td>
                        <td class="auto-style19">&nbsp;<asp:Label ID="LBConName" runat="server"></asp:Label>
                            <br />
                            <br />
                        </td>
                    </tr>
               
                <tr>
                    
                    <td class="auto-style17">
                        <asp:Label ID="Label11" runat="server" Text="Phone No"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :<br />
                        <br />
                    </td>
                    <td class="auto-style19">
                        &nbsp;<asp:Label ID="LBPhNo" runat="server"></asp:Label>
                        <br />
                        <br />
                    </td>
                    
                </tr>
                <tr>
                   
                    <td class="auto-style17">
                        <asp:Label ID="Label12" runat="server" Text="Fax No"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :<br />
                        <br />
                    </td>
                    <td class="auto-style19">
                        &nbsp;<asp:Label ID="LBFaxNo" runat="server"></asp:Label>
                        <br />
                        <br />
                    </td>
                    <td>&nbsp;</td>

                </tr>
                 <tr>
                   
                    <td class="auto-style17">
                        <asp:Label ID="Label6" runat="server" Text="Address"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :<br />
                        <br />
                    </td>
                    <td class="auto-style19">
                        <asp:Label ID="LBAddress" runat="server"></asp:Label>
                        <br />
                        <br />
                    </td>
                 

                </tr>
                <tr>
                  
                    <td class="auto-style17">
                        <asp:Label ID="Label13" runat="server" Text="E-mail"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :</td>
                    <td class="auto-style19">
                        &nbsp;<asp:Label ID="LBEmail" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </asp:Panel>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                                                    </asp:Content>
