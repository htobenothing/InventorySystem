<%@ Page Title="" Language="C#" MasterPageFile="~/View/StoreClerk/Clerk.Master" AutoEventWireup="true" CodeBehind="ReceivedOrderPage.aspx.cs" Inherits="LogicUniversity.StoreClerk.Maintain.ReceivedOrderPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-lg-12">
                    <h1 class="page-header">Receive Purchase Order</h1>
                   
                </div>
 <%--   <asp:Panel ID="Panel1" runat="server">
        <asp:Label ID="LBPONo" runat="server" style="font-size: medium" Text="PO Number: "></asp:Label>
        <asp:TextBox ID="TBPOID" runat="server" class="form-control" Height="25px" Width="162px"></asp:TextBox>
        <asp:Button ID="BtnShow" runat="server" Text="Show" Width="70px" OnClick="BtnShow_Click" />
    </asp:Panel>--%>

    <table>
          <tr>
              <td align="right">PO Number :&nbsp;&nbsp;&nbsp;&nbsp; </td>
              <td>
                                    <asp:TextBox ID="TBPOID"  class="form-control" runat="server" Width="242px"></asp:TextBox>
              
              </td>
          </tr>
          <tr>
              <td>&nbsp;</td>
              <td>
                  <br />
                 <asp:Button ID="BtnShow" class="btn btn-outline btn-primary"  OnClick="BtnShow_Click" runat="server" Text="Show" />
                                            
                  <br />
                  <br />
              </td>
          </tr>
      </table>
    <br />
    <asp:Panel ID="PanelOrderDetail" runat="server" Visible="False">
        <asp:Label ID="LBPodetail" runat="server" Text="Pruchase Order Detail:" style="font-size: large"></asp:Label>
        <br />
        <asp:GridView ID="GridView1" runat="server" class="table table-striped table-bordered table-hover dataTable no-footer" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="16px" Width="858px">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField HeaderText="Category" DataField="Category_ID" />
                <asp:BoundField HeaderText="Item ID" DataField="Item_ID" />
                <asp:BoundField HeaderText="Description" DataField="Item_Name" />
                <asp:BoundField HeaderText="Needed Qty" DataField="Needed_Qty" />
                <asp:TemplateField HeaderText="Received Qty">
                    <ItemTemplate>
                        <asp:TextBox ID="TextboxReceiveQty" runat="server" DataTextField="rqty" DataValueField="rqty">
                                    </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TextboxReceiveQty" runat="server" ErrorMessage="Please input" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidatorQty" runat="server" ControlToValidate="TextboxReceiveQty" ErrorMessage="*Need Integer(0-9999)" MaximumValue="9999" MinimumValue="0" Type="Integer" ForeColor="Red"></asp:RangeValidator>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="UOM" DataField="UOM" />
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
        <asp:Label ID="LBRemark" runat="server" Text="Remark:" style="font-size: large"></asp:Label>
        <br />
        <asp:TextBox ID="TBRemark" runat="server" Height="65px" Width="356px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="BtnReceive" class="btn btn-outline btn-primary" runat="server" OnClick="BtnReceive_Click" Text="Confirm Receive" />
    </asp:Panel>
</asp:Content>
