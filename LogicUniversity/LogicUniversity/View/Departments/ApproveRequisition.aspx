<%@ Page Title="" Language="C#" MasterPageFile="~/View/Departments/Dept.Master" AutoEventWireup="true" CodeBehind="ApproveRequisition.aspx.cs" Inherits="LogicUniversity.View.Departments.ApproveRequisition" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
          <div class="col-lg-12">
        <h1 class="page-header">Approve Requisition</h1>

    </div>
          <asp:Label ID="lblCheckRequisition" runat="server" Text="Requisition List" Font-Bold="True" Font-Size="Large" ></asp:Label>
          
          <asp:GridView ID="GridView1" 
               AllowPaging="True" runat="server"  Height="16px" Width="626px" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnPageIndexChanging="GridView1_PageIndexChanging"  >
          <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
            <asp:BoundField HeaderText="Requisition ID" DataField="Requisition_ID" />
            <asp:BoundField HeaderText="Submission Staff Name" DataField="Staff_Name"  ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField HeaderText="Submission Date" DataField="Submission_Date" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField HeaderText=" Type" DataField="Type" />            
            <asp:ButtonField ButtonType="Button"  Text="View" HeaderText="Details" ControlStyle-CssClass="btn btn-outline btn-primary" CommandName="viewdetail"   >
<ControlStyle CssClass="btn btn-outline btn-primary"></ControlStyle>
                </asp:ButtonField>
        </Columns>  
         <EditRowStyle Height="35px" />
            <HeaderStyle Height="35px" />
            <SelectedRowStyle BackColor="#99FFCC" />
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

          <br/><br/>
          &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
          &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
          &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
          <asp:Button ID="btnApproveAll" runat="server" class="btn btn-outline btn-primary" Text="Approve All" OnClick="btnApproveAll_Click"  />
           
          <br />
          <br />
          <asp:Panel ID="Panel1" runat="server">
              <strong><span class="auto-style1">Requisition Form :</span></strong><asp:Label ID="lblRequisitionID" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
            
              <br /><br/>
              <asp:GridView ID="GridView2"  AllowPaging="True"  runat="server"  CellPadding="4" ForeColor="#333333" GridLines="None" Height="16px" Width="640px" AutoGenerateColumns="False"  CssClass="table table-bordered table-condensed" OnPageIndexChanging="GridView2_PageIndexChanging" >
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField HeaderText="Category" DataField="Category_ID" />
            <asp:BoundField HeaderText="Item Description" DataField="Item_Name" />
           <asp:BoundField HeaderText=" Quantity" DataField="Required_Qty" />
            <asp:BoundField HeaderText="UOM" DataField="UOM" />
        </Columns>
        <EditRowStyle Height="35px" />
            <HeaderStyle Height="35px" />
            <SelectedRowStyle BackColor="#99FFCC" />
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
        </asp:GridView><br/>
              <strong><span class="auto-style1">Remarks :</span></strong><asp:TextBox ID="txtRejectReason" runat="server" class="form-control" TextMode="MultiLine" Width="612px" Rows="6"></asp:TextBox>
              <br/><br/><br/>&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
              <asp:Button ID="btnApprove" runat="server"  class="btn btn-outline btn-primary" Text="Approve" OnClick="btnApprove_Click" />
              &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
              &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Button ID="btnReject" class="btn btn-outline btn-primary" runat="server" Text="Reject" OnClick="btnReject_Click1" />


          </asp:Panel>
        </div>


</asp:Content>
