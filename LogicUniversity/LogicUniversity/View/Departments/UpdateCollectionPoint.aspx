<%@ Page Title="" Language="C#" MasterPageFile="~/View/Departments/Dept.Master" AutoEventWireup="true" CodeBehind="UpdateCollectionPoint.aspx.cs" Inherits="LogicUniversity.View.Departments.UpdateCollectionPoint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
       
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function ConfirmOnUpdate() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Are you sure to update the representative and collection point?")) {

                confirm_value.value = "Yes";

            } else {

                confirm_value.value = "No";

            }

            document.forms[0].appendChild(confirm_value);
        }
    </script>
     <asp:Panel runat="server">
    <%--<div class="col-lg-12">
        <h1 class="page-header">Update Collection Point Information</h1>
    </div>--%>
    <br />
 
   
    <fieldset>
        <legend>Representative Staff:</legend>

        <table>
            <tr>
                <td style=" padding: 20px">
                    <asp:Label ID="lblStaffID" runat="server" Text="Staff ID:"></asp:Label></td>
                <td >
                    <asp:TextBox ID="TBStaffID" runat="server" CssClass="form-control"></asp:TextBox>

                </td>
                <td >
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TBStaffID" ErrorMessage="*Please Enter!" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr>
                <td style="padding: 20px">
                    <asp:Label ID="lblStaffName" runat="server" Text="Staff Name: "></asp:Label></td>
                <td style="width:200px">
                    <asp:Label ID="LBStaffName" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button2" runat="server" class="btn btn-outline btn-primary" Text="Show Name" OnClick="BtnChooseID_Click" />
                </td>
                <td style="text-align:center">
                    <asp:Button ID="Button3" runat="server" class="btn btn-outline btn-primary" Text="Reverse" OnClick="BtnReverseStaff_Click" />
                </td>
            </tr>
        </table>
    </fieldset>



    <div style="height:80px"></div>
    <fieldset>

        <legend>Collection Point:</legend>
        <table>

            <tr>
                <td>Collection Point Details:</td>

                <td style="padding: 15px">
                    <asp:DropDownList ID="DDLCP" runat="server" Class="btn btn-default btn-xs dropdown-toggle" Width="250px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: center; padding: 15px" colspan="2">
                    <asp:Button ID="Button1" runat="server" class="btn btn-outline btn-primary" OnClick="btnUpdate_Click" OnClientClick="return ConfirmOnUpdate();" Text="Update" />
                </td>
            </tr>
        </table>
    </fieldset>
        </asp:Panel>
    <%--    </asp:Panel>--%>
</asp:Content>
