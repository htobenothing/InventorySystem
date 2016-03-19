<%@ Page Title="" Language="C#" MasterPageFile="~/View/StoreClerk/Clerk.Master" AutoEventWireup="true" CodeBehind="UpdateProfile.aspx.cs" Inherits="LogicUniversity.View.StoreClerk.UpdateProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-lg-12">
        <h1 class="page-header">Maintain Profile</h1>
    </div>
    <table>
        <tr>
            <td class="auto-style2">Staff ID </td>
            <td class="auto-style1">
                :&nbsp; <asp:Label ID="lblStaffID" Text='<%# Eval("Staff_ID") %>' runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="auto-style2">
                <br />
                Staff Name </td>
            <td class="auto-style1">
                <br />
                :&nbsp; <asp:Label ID="lblStaffName" runat="server" Text='<%# Eval("Staff_Name") %>'></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="auto-style2">
                <br />
                Email&nbsp; 
                <br />
            </td>
            <td class="auto-style1">
                <br />
                :&nbsp;
                <asp:Label ID="lblemail" runat="server" Text=""></asp:Label>
                <br />
            </td>
            <td></td>
        </tr>

        <tr>
            <td class="auto-style2">
                <br />
                Phone No :</td>
            <td class="auto-style1">
                <br />
                <asp:TextBox ID="txtph" class="form-control" runat="server" Width="140"></asp:TextBox>
                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtph"  ValidationExpression="^\d{8}$" ErrorMessage="Phone Number should be 8 digits" Display="Static" ForeColor="#FF3300"></asp:RegularExpressionValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="auto-style2">
                <br />
                Password :</td>
            <td class="auto-style1">&nbsp;<br />
                <asp:Button ID="Button2" class="btn btn-outline btn-primary" runat="server" Text="Change Password" OnClick="ChangePW_Click" />
                &nbsp;
            </td>
            <td>
                <br />
            </td>
        </tr>
        <tr>
            <td class="auto-style2"></td>
            <td class="auto-style1">&nbsp;
              
                <br />
                <asp:Button ID="Button5" class="btn btn-outline btn-primary" runat="server" Text="Update" OnClick="update_Click" />
                &nbsp;&nbsp;&nbsp;
                <br />


                <br />
            </td>
            <td>
                <asp:Label ID="lbl_msg" runat="server" BackColor="#FFFF66" Font-Bold="True" ForeColor="#FF3300" Text=""></asp:Label>
            </td>
        </tr>









    </table>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
               

</asp:Content>
