<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateSupInventory.aspx.cs" Inherits="LogicUniversity.View.Public.UpdateSupInventory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8" />



    <link rel="stylesheet" type="text/css" href="~/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="~/css/Style.css" />
    <%--<script type="text/javascript" src="../js/jquery-1.10.2.min.js"></script>--%>
    <link href="~/DatePicker/bootstrap-datepicker.css" rel="stylesheet" />



    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link href="~/css/bootstrap.min.css" rel="stylesheet" />
    <link href="~/css/style.css" rel="stylesheet" />
    <title>Logic University</title>
    <!-- Bootstrap Core CSS -->
    <%-- <link href="../css/bootstrap.min.css" rel="stylesheet"/>--%>
    <!-- MetisMenu CSS -->
    <!--<link href="../bower_components/metisMenu/dist/metisMenu.min.css" rel="stylesheet">-->
    <!-- Timeline CSS -->
    <!--<link href="../dist/css/timeline.css" rel="stylesheet">-->
    <!-- Custom CSS -->
    <link href="~/css/sb-admin-2.css" rel="stylesheet" />

    <!-- Custom Fonts -->
    <link href="~/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="form1" runat="server" style="margin-bottom:100px;">
        <div>
            <nav class="navbar navbar-default navbar-static-top" role="navigation" style="margin-bottom: 0">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand">
                        <asp:Image ID="Image1" runat="server" ImageUrl="../../Logo.png" Width="230px" Height="45px" />
                    </a>
                </div>
                <!-- /.navbar-header -->
                <ul class="nav navbar-top-links navbar-right">


                    <li class="dropdown">
                        <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                            <i class="fa fa-user fa-fw"></i><i class="fa fa-caret-down"></i>
                        </a>
                        <ul class="dropdown-menu dropdown-user">
                            

                            <li class="divider"></li>
                            <li>
                                <asp:LinkButton runat="server" ID="btnLogout" class="fa fa-sign-out fa-fw" OnClick="btnLogout_Click"> Logout</asp:LinkButton>
                                <%--  <a href="Login.aspx"><i class="fa fa-sign-out fa-fw"></i> Logout</a>--%>
                            </li>
                        </ul>
                        <!-- /.dropdown-user -->
                    </li>
                    <!-- /.dropdown -->
                </ul>
                <!-- /.navbar-top-links -->

                <!-- /.navbar-static-side -->
            </nav>

            <script type="text/javascript">
                function ConfirmOnUpdate() {
                    var confirm_value = document.createElement("INPUT");
                    confirm_value.type = "hidden";
                    confirm_value.name = "confirm_value";
                    if (confirm("Are you sure to update the inventory?")) {

                        confirm_value.value = "Yes";
                    } else {

                        confirm_value.value = "No";

                    }

                    document.forms[0].appendChild(confirm_value);
                }
            </script>
            <%--   <div class="col-lg-2">
                    <h1 class="page-header">Update Supplier Inventroy</h1>
                   
                </div--%>&nbsp;<%--<asp:Label ID="Label2" runat="server" Text="Update Supplier Inventroy" Font-Size="XX-Large"></asp:Label>--%><div class="col-lg-2">
                </div>
            <div class="col-lg-4">
                <h1 class="page-header">Inventory detail:</h1>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-hover dataTable no-footer" CellPadding="4" ForeColor="#333333" GridLines="None" Height="16px" Width="741px">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField HeaderText="Item Description" DataField="Item_Name" ItemStyle-Width="150px" >
<ItemStyle Width="150px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Inventory Qty" DataField="Inventory_Qty"  ItemStyle-Width="150px">
<ItemStyle Width="150px"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Update Qty">
                            <ItemTemplate>
                                <asp:TextBox ID="TextboxQty" class="form-control" runat="server" DataTextField="qty" DataValueField="qty">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TextboxQty" runat="server" ErrorMessage="Please input" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidatorQty" runat="server" ControlToValidate="TextboxQty" ErrorMessage="*Need Integer(0-99999)" MaximumValue="99999" MinimumValue="0" Type="Integer" ForeColor="Red" Display="Dynamic"></asp:RangeValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="150px" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
                <br />
                <div style="width: 300px; margin: 0 auto">
                    <asp:Button ID="BtnConfrim" class="btn btn-outline btn-primary" runat="server" OnClick="BtnConfrim_Click" OnClientClick="return ConfirmOnUpdate();" Text="Confirm Update" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
    <asp:Button ID="BtnRevert" class="btn btn-outline btn-primary" runat="server" Text="Reverse" OnClick="BtnRevert_Click" />
                    <br />
                    <asp:Label ID="LBStatus" runat="server" Style="font-size: medium"></asp:Label>

                </div>

            </div>

            <div class="col-lg-6" style="height:50px"></div>

        </div>
        <asp:ScriptManager runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/js/jquery.min.js" />
                <asp:ScriptReference Path="~/DatePicker/bootstrap-datepicker.js" />
                <asp:ScriptReference Path="~/js/bootstrap.min.js" />
                <asp:ScriptReference Path="~/js/metisMenu.js" />
                <asp:ScriptReference Path="~/js/sb-admin-2.js" />
            </Scripts>
        </asp:ScriptManager>
    </form>
    
</body>
</html>
