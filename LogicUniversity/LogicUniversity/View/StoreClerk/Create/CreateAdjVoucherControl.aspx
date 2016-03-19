<%@ Page Title="" Language="C#" MasterPageFile="~/View/StoreClerk/Clerk.Master" AutoEventWireup="true" CodeBehind="CreateAdjVoucherControl.aspx.cs" Inherits="ADProject_Team1.View.Store.CeateAdjVoucherConrtol" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-lg-12">
                    <h1 class="page-header">Create Adjustment Voucher</h1>
                </div>
        &nbsp;<asp:Label ID="dateLbl" runat="server" Font-Bold="True" Text="Date:"></asp:Label>
        <asp:Label ID="TodayDateLbl" runat="server" Text="Today"></asp:Label>
    <br>
        </br>
    <asp:GridView ID="AdjVouGridview" runat="server" AutoGenerateColumns="False" OnRowDataBound="AdjVouGridview_RowDataBound" OnRowDeleting="AdjVouGridview_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None">
         <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
         <Columns>
                 <asp:TemplateField HeaderText="Category">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DropDownListCategory" Class="btn btn-default btn-xs dropdown-toggle" AutoPostBack="true" runat="server"  DataValueField="category" DataTextField="category" OnSelectedIndexChanged="DropDownListCategory_SelectedIndexChanged">
                                    </asp:DropDownList>  
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DropDownListCategory"
                                     ErrorMessage="Please select category first!!" Display="Dynamic" ValidateRequestMode="Enabled"></asp:RequiredFieldValidator>                             
                             
                                </ItemTemplate>
                            </asp:TemplateField> 

                <asp:TemplateField HeaderText="Item ID">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DropDownListItemCode" Class="btn btn-default btn-xs dropdown-toggle" AutoPostBack="true" runat="server" DataValueField="itemID" DataTextField="itemID" OnSelectedIndexChanged="DropDownListItemCode_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>

                <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DropDownListItemName" Class="btn btn-default btn-xs dropdown-toggle" AutoPostBack="true" runat="server" DataValueField="description" DataTextField="description" OnSelectedIndexChanged="DropDownListItemName_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>

                <asp:TemplateField HeaderText="Price">
                                <ItemTemplate>
                                    <asp:Textbox ID="priceTxt" class="form-control" runat= "server" DataValueField="qty" DataTextField="qty" ReadOnly="true">
                                    </asp:Textbox>
                                </ItemTemplate>
                            </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:TextBox ID="qtyTxt" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                         runat="server" ControlToValidate="qtyTxt" ErrorMessage=
                                        "Please enter number only!" ValidationExpression="^-?[0-9]\d*(\.\d+)?$" ForeColor="Red" Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="qtyTxt" ErrorMessage="Please enter quantity!!"  ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
             
 <asp:ButtonField ButtonType="Button" Text="Delete"  CommandName="Delete" ControlStyle-CssClass="btn btn-outline btn-primary" >
             
<ControlStyle CssClass="btn btn-outline btn-primary"></ControlStyle>
                 </asp:ButtonField>
             
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
    <%--<asp:ValidationSummary ID="ValidationSummary1" runat="server" />--%>
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
        <asp:Button ID="NewBtn" runat="server" class="btn btn-outline btn-primary" Text="Add " OnClick="NewBtn_Click" />
    &nbsp;<br 
    <br />
    <asp:Label ID="RemarlLbl" runat="server" Font-Bold="True" Text="Remark:"></asp:Label>
    <br />
    <br />
    <textarea id="remarkTxt" name="S1" rows="6" cols="60" runat="server" ></textarea>
        <br />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="submitBtn" runat="server" class="btn btn-outline btn-primary" OnClick="submitBtn_Click" Text="Create" />
</asp:Content>
