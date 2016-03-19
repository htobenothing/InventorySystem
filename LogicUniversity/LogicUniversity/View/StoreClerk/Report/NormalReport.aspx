<%@ Page Title="" Language="C#" MasterPageFile="~/View/StoreClerk/Clerk.Master" AutoEventWireup="true" CodeBehind="NormalReport.aspx.cs" Inherits="ADProject_Team1.View.StoreClerk.Report.AnalysisReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            var q = '<%= ddldepNor.Text%>';
            
            if (q == "Store") {
                $('#lblDate').hide();
                $('#txtDate').hide();
            } else {
                $('#lblDate').show();
                $('#txtDate').show();
            }
                
            $(".datepicker").datepicker({ format: "yyyy-mm", autoclose: true, todayBtn: 'linked', viewMode: "Months", minViewMode: "months" })
        });
    </script>
   
     <div class="col-lg-12">
        <h1 class="page-header">Normal Report</h1>
    </div>
    
    
    <div>
    <asp:Label runat="server">Strategy</asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:DropDownList runat="server" ID="ddldepNor" Height="30px" Width="180px" AutoPostBack="True" OnSelectedIndexChanged="ddldepNor_SelectedIndexChanged">
        <asp:ListItem>Department</asp:ListItem>
        <asp:ListItem>Store</asp:ListItem>
    </asp:DropDownList>
        <br />
        <br />
        </div>
    <div>
        <asp:Label runat="server" Text="Date" ID="lblDate" ClientIDMode="Static"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox runat="server" CssClass="datepicker" ID="txtDate" ClientIDMode="Static"></asp:TextBox>
        <asp:Label ID="lblError" runat="server" ForeColor="Red" ></asp:Label>
        <br />
        <br />
    </div>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button runat="server" Text="Search" class="btn btn-outline btn-primary" ID="btnSearch" OnClick="btnSearch_Click"/>
    <br />
    <rsweb:ReportViewer ID="ReportViewer" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="1000px" Height="1000px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
    </rsweb:ReportViewer>
   <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
 
</asp:Content>
