<%@ Page Title="" Language="C#" MasterPageFile="~/View/Departments/Dept.Master" AutoEventWireup="true" CodeBehind="DelegateStaff.aspx.cs" Inherits="LogicUniversity.View.Departments.DelegateStaff" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            font-size: x-large;
        }
        .auto-style2 {
            height: 20px;
        }
        .auto-style3 {
            height: 40px;
            width: 109px;
        }
        .auto-style4 {
            height: 20px;
            width: 109px;
        }
        .auto-style5 {
            height: 40px;
            width: 103px;
        }
        .auto-style6 {
            height: 20px;
            width: 114px;
        }
        .auto-style7 {
            height: 40px;
            width: 114px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
           <div class="col-lg-12">
                    <h1 class="page-header">Delegate Authority</h1>
                </div>
        <p class="auto-style1">
            <asp:Label ID="lblStaffList" runat="server" Text="Staff List :" Font-Size="Medium" style="font-size: x-large"></asp:Label>
        </p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" Width="802px">
             <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="Staff_Name" HeaderText="Staff Name" />
                <asp:BoundField DataField="Staff_ID" HeaderText="Staff Id" />
                <asp:TemplateField HeaderText="Delegate">
                    <ItemTemplate>
                        <asp:Button  ControlStyle-CssClass="btn btn-outline btn-primary" ID="DelegateBtn" runat="server" CommandName="delegateStaff" OnClick="DelegateBtn_Click"></asp:Button>
                    </ItemTemplate>
                </asp:TemplateField>

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

        <asp:Panel ID="PanelDetail" runat="server" Visible="False">
            <br/>
            <span class="auto-style1">Authority Staff Name</span>:<asp:Label ID="lblStaffName" runat="server" style="font-size: x-large" ForeColor="#6666FF"></asp:Label>
            <br />
            <br />
            <span class="auto-style1">Delegate Duration :</span><br />
            <div class="modal-body">
                <table class="table-products">
                    <tr class="auto-style5">
                        <td style="text-align:right" class="auto-style3">                          
                                <strong>From </strong>
                          </td>
                        <td style="width: 100px; height: 40px;text-align:center">  
                            &nbsp;  
                            <asp:TextBox ID="txtStartDate" class="form-control" CssClass="datepicker" runat="server" Width="150px"></asp:TextBox>
                            <br />
                        </td>
                        <td style="width: 50px; height: 40px;"></td>
                        <td style="text-align:right" class="auto-style7">
                           
                                <strong>To </strong>
                            </td>
                        <td style=" height: 40px;text-align:center">
                            
                            <asp:TextBox ID="txtEndDate" class="form-control" CssClass="datepicker" runat="server" Width="150px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="auto-style5">
                        <td class="auto-style4" >                          
                                </td>
                        <td class="auto-style2" style="height: 40px;text-align:left">  
                            <%--<asp:RegularExpressionValidator ID="Warning1" runat="server" ControlToValidate="txtStartDate" Display="Dynamic" ErrorMessage="Date format is wrong" ForeColor="Red" ValidationExpression="^\d{2}/\d{2}/\d{4}$|^\d{1}/\d{1}/\d{4}$"></asp:RegularExpressionValidator>--%>
                            <br />
                            <asp:Label runat="server" ID="lblfrmDate" ForeColor="Red"></asp:Label>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate" Display="Dynamic" ErrorMessage="Start Date Can't Be empty." ForeColor="Red"></asp:RequiredFieldValidator>--%>
                        </td>
                        <td class="auto-style2" style="width: 50px; height: 40px;" >
                           
                                </td>
                        <td class="auto-style6">
                            
                            </td>
                        <td class="auto-style2" style="height: 40px;text-align:left">
                            <%--<asp:RegularExpressionValidator ID="Warning2" runat="server" ControlToValidate="txtEndDate" Display="Dynamic" ErrorMessage="Date format is wrong" ForeColor="Red" ValidationExpression="^\d{2}/\d{2}/\d{4}$|^\d{1}/\d{1}/\d{4}$"></asp:RegularExpressionValidator>--%>
                            <br />
                            <%--<asp:Label runat="server" ID="lbltoDate"></asp:Label>--%>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate" Display="Dynamic"  ForeColor="Red" ErrorMessage="End Date Can't Be empty"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                </table>
                <br />

                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:Label ID="LBStatus" runat="server" ForeColor="Red"></asp:Label>
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                <asp:Button ID="btnDelgtConfirm" runat="server" class="btn btn-outline btn-primary" OnClick="DelegateConfirmBtn_Click" Text="Confirm" />
            </div>
        </asp:Panel>

        <br />

        <br />

        <div class="modal-footer">
        </div>
    </div>
</asp:Content>
