<%@ Page Title="" Language="C#" MasterPageFile="~/View/Departments/Dept.Master" AutoEventWireup="true" CodeBehind="ViewRequistion.aspx.cs" Inherits="LogicUniversity.View.Departments.ViewRequistion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style2 {
            width: 76px;
        }
        .auto-style4 {
            width: 113px;
            height: 42px;
        }
        .auto-style5 {
            height: 42px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
  
                    <h1 class="page-header">View Requisition</h1>
                   
       


     
        <h3>Search By ID</h3>
        
      <table>
            <tr>
                <td class="auto-style4">RequistionID:<br />
                    
                </td>
                <td class="auto-style5">
                    <asp:TextBox ID="txtID" runat="server" class="form-control" Width="200px" OnTextChanged="txtID_TextChanged"></asp:TextBox>
                </td>
            </tr>
          <tr>
              <td colspan="2"><asp:Label ID="lblReqResult" runat="server" ForeColor="Red"></asp:Label></td>
          </tr>
        </table>
    </fieldset>
   <br />
    <br />
    <fieldset>
        <h3>Search by Detail</h3>
        
    </fieldset>
     
     <table>
         <tr>
             <td class="auto-style2">&nbsp;From:<br />
                 <br />
             </td>
             <td>
   
    <asp:TextBox runat="server" class="form-control" CssClass="datepicker" ID="txtFromDate" Width="132px" AutoPostBack="True" OnTextChanged="txtFromDate_TextChanged"></asp:TextBox>
                 <br />
                 <br />
             </td>

         </tr>
         <tr>
             <td class="auto-style2">To:<br />
                 <br />
             </td>
             <td>
    <asp:TextBox runat="server" class="form-control" CssClass="datepicker" ID="txtToDate" Width="132px" AutoPostBack="True" OnTextChanged="txtToDate_TextChanged"></asp:TextBox>
                 <br />
                 <br />
             </td>
             
         </tr>
         <tr>
             <td class="auto-style2">Status</td>
             <td>
    <asp:DropDownList ID="dplStatus" runat="server" Class="btn btn-default btn-xs dropdown-toggle" Width="132px"  Height="30px">
        <asp:ListItem>All</asp:ListItem>
        <asp:ListItem>Submitted</asp:ListItem>
        <asp:ListItem>Approved</asp:ListItem>
        <asp:ListItem>Rejected</asp:ListItem>
        <asp:ListItem>Dealing</asp:ListItem>
        <asp:ListItem>Received</asp:ListItem>
    </asp:DropDownList>
             </td>
             <td>&nbsp;</td>
         </tr>
         <tr>
             <td class="auto-style2">&nbsp;</td>
             <td>
                 <br />
                 <br />
    <asp:Button ID="btnSearch" OnClick="btnSearch_Click" class="btn btn-outline btn-primary" runat="server" Text="Search"  />
                 <asp:Label ID="LBStatus" runat="server" ForeColor="Red"></asp:Label>
             </td>
             <td>&nbsp;</td>
         </tr>
     </table>
     <br />
     <br />
    <asp:GridView ID="dgvList" runat="server" class="table table-striped table-bordered table-hover dataTable no-footer" AutoGenerateColumns="False" OnRowCommand="dgvList_RowCommand" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="dgvList_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None" Height="16px" Width="748px">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField HeaderText="Requisition ID" DataField="Requisition_ID" />
            <asp:BoundField HeaderText="CreateDate" DataField="Create_Date"  DataFormatString="{0:dd/MM/yyyy}"/>
            <asp:BoundField HeaderText="Status" DataField="Status" />
            <asp:ButtonField ButtonType="Button" HeaderText="Detail" Text="View" ControlStyle-CssClass="btn btn-outline btn-primary"  CommandName="ViewDetail"/>
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
    
    <asp:Label ID="lblRequisition" runat="server" Text="RequisitionID:"></asp:Label>
    
    <asp:Label ID="lblReq" runat="server" ></asp:Label>
    <br />
    <asp:GridView ID="dgvfix" runat="server" class="table table-striped table-bordered table-hover dataTable no-footer" AutoGenerateColumns="False" Height="16px" Width="748px" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="categoryID" HeaderText="Category" />
            <asp:BoundField DataField="ItemName" HeaderText="Item Description" />
            <asp:BoundField DataField="UOM" HeaderText="UOM" />
            <asp:BoundField DataField="RequiredQty" HeaderText="Quantity" />
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
    <asp:Panel runat="server" ID="detailPanel">
    <asp:GridView ID="dgvDetail" runat="server" class="table table-striped table-bordered table-hover dataTable no-footer" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="739px">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField HeaderText="Category" DataField="categoryID" />
            <asp:BoundField DataField="ItemID" HeaderText="Item ID" />
            <asp:BoundField DataField="ItemName" HeaderText="ItemDescription" />
            <asp:BoundField HeaderText="UOM" DataField="UOM" />
            <asp:TemplateField HeaderText="Quantity">
                <ItemTemplate>
                    <asp:TextBox ID="txtQty" runat="server" class="form-control"></asp:TextBox>
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
    <asp:Label runat="server" ID="lblRemark">Remarks:</asp:Label><br />
    <asp:TextBox runat="server" class="form-control" TextMode="MultiLine" Width="612px" Rows="6" ID="txtReason"></asp:TextBox>
    <br />
    <asp:Button ID="btnResubmit" runat="server" class="btn btn-outline btn-primary" Text="Resubmit" OnClick="btnResubmit_Click" />
    </asp:Panel>

</asp:Content>
