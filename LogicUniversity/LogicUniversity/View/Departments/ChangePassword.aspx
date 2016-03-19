<%@ Page Title="" Language="C#" MasterPageFile="~/View/Departments/Dept.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="LogicUniversity.View.Departments.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-lg-12">
        <h1 class="page-header">Change Password</h1>
    </div>
    <br />
    <table>
        <tr>
            <td class="auto-style2">Current password</td>
            <td class="auto-style1">
                <asp:TextBox ID="txt_cpassword" class="form-control" runat="server" AutoPostBack="True" OnTextChanged="txt_cpassword_TextChanged"></asp:TextBox>
            </td>
            <td>&nbsp;&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_cpassword" ErrorMessage="Please enter Current password" ForeColor="#990000"></asp:RequiredFieldValidator>
                <br />
                &nbsp;
                <asp:Label ID="lblCP" runat="server" ForeColor="#990000" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">
                <br />
                New password</td>
            <td class="auto-style1">
                <br />
                <asp:TextBox ID="txt_npassword" class="form-control" runat="server" OnTextChanged="txt_npassword_TextChanged" AutoPostBack="True"></asp:TextBox>
            </td>
            <td>&nbsp;&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_npassword" ErrorMessage="Please enter New password" ForeColor="#990000"></asp:RequiredFieldValidator>
                <br />
                &nbsp;
                <asp:Label ID="lblSame" Font-Bold="True" ForeColor="#FF3300" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">
                <br />
                Confirm password</td>
            <td class="auto-style1">
                <br />
                <asp:TextBox ID="txt_ccpassword" class="form-control" runat="server" TextMode="Password"></asp:TextBox>

            </td>
            <td>&nbsp;&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_ccpassword" ErrorMessage="Please enter Confirm  password" ForeColor="#990000"></asp:RequiredFieldValidator>
                <br />
                &nbsp;
                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="txt_npassword" ControlToValidate="txt_ccpassword" ErrorMessage="Password Mismatch" ForeColor="#990000"></asp:CompareValidator>
            </td>
        </tr>

        <tr>
            <td class="auto-style2">
                <br />
                <br />
                <asp:HyperLink ID="HyperLink2" runat="server" class="btn btn-outline btn-primary" NavigateUrl="UpdateProfile.aspx">Back</asp:HyperLink>

                <br />
            </td>
            <td class="auto-style1">
                <br />
                <br />
                <asp:Button ID="btn_update0" runat="server" class="btn btn-outline btn-primary"
                    OnClick="btn_update_Click"
                    Text="Update" />
            </td>
            <td>&nbsp;&nbsp;
                <br />
                <br />
                <asp:Label ID="lblUpadate" Font-Bold="True" BackColor="#FFFF66" ForeColor="#FF3300" runat="server" Text=""></asp:Label>
            </td>
        </tr>











    </table>




</asp:Content>
