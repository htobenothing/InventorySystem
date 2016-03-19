<%@ Page Title="" Language="C#" MasterPageFile="~/View/StoreClerk/Clerk.Master" AutoEventWireup="true" CodeBehind="AnalyReport.aspx.cs" Inherits="ADProject_Team1.View.StoreClerk.Report.AnalyReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
       $(document).ready(function () {
          
          
           var q = '<%= ddlType.Text%>';

           
           if (q == "Year Report") {
               $(".datepicker").datepicker({ format: "yyyy", autoclose: true, todayBtn: 'linked', viewMode: "years", minViewMode: "years" })
           } else {
               $(".datepicker").datepicker({ format: "yyyy-mm", autoclose: true, todayBtn: 'linked', viewMode: "Months", minViewMode: "months" })
           }
       });
       
    </script>

    <div class="col-lg-12">
        <h1 class="page-header">Analyst Report</h1>
    </div>


    <div>
    <div>

        <asp:Label runat="server" ID="lbltype">Report Type</asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList runat="server" ID="ddlType" Class="btn btn-default btn-xs dropdown-toggle" ClientIDMode="Static" Height="30px" Width="180px" AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
            <asp:ListItem>Year Report</asp:ListItem>
            <asp:ListItem>Month Report</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />

        <asp:Label runat="server" ID="lblStrategy">Strategy</asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList runat="server" Class="btn btn-default btn-xs dropdown-toggle" Height="30px" Width="180px" ID="ddlStrategy" AutoPostBack="True" OnSelectedIndexChanged="ddlStrategy_SelectedIndexChanged">
            <asp:ListItem>Store</asp:ListItem>
            <asp:ListItem>Department</asp:ListItem>

        </asp:DropDownList>
        <br />
        <br />
    </div>

    <asp:Panel runat="server" ID="pnMonthDep">

        <div>
            <asp:Label runat="server" ID="lblDepName">Department</asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:DropDownList runat="server" Class="btn btn-default btn-xs dropdown-toggle" Height="30px" Width="180px" ID="ddlDepName">
            </asp:DropDownList>
            <br />
            <br />
        </div>
        <div>
            <asp:Label runat="server" ID="lblCategory">Category</asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:DropDownList runat="server" Class="btn btn-default btn-xs dropdown-toggle" Height="30px" Width="180px" ID="ddlCategory">
            </asp:DropDownList>
            <br />
            <br />
        </div>

    </asp:Panel>
    <div>
        <asp:Label runat="server" ID="lblDate">Date</asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox runat="server" CssClass="datepicker" ID="txtDate"   AutoPostBack="True" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
     
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

        <br />
        <br />

        <br />

    </div>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button runat="server" class="btn btn-outline btn-primary" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />

    <br />

    <rsweb:ReportViewer ID="ReportViewer" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="900px"  Height="800px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"></rsweb:ReportViewer>
  <%--  <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    </div>
    
</asp:Content>
