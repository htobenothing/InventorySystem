<%@ Page Language="C#" AutoEventWireup="true"  EnableEventValidation="false" validateRequest="false"  CodeBehind="Login.aspx.cs" Inherits="LogicUniversity.WebForm4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    

<%--    <link rel="stylesheet" type="text/css" href="~/css/bootstrap.css"/>
   <link rel="stylesheet" type="text/css" href="~/css/Style.css"/>  
	<%--<script type="text/javascript" src="../js/jquery-1.10.2.min.js"></script>--%>
    <link href="~/DatePicker/bootstrap-datepicker.css" rel="stylesheet" />
	
	<link href="~/css/sb-admin-2.css" rel="stylesheet"/>

    <!-- Custom Fonts -->
    <link href="~/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>

    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>
    <link href="~/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="~/css/style.css" rel="stylesheet"/>


            <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>

    <title>Login Pages</title>
   
    <!-- Bootstrap Core CSS -->
    <link href="/css/bootstrap.min.css" rel="stylesheet"/>

    <!-- MetisMenu CSS -->
    <link href="/js/metisMenu.min.css" rel="stylesheet"/>

    <!-- Custom CSS -->
    <link href="/css/sb-admin-2.css" rel="stylesheet"/>

    <!-- Custom Fonts -->
    <link href="/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>

    <link rel="stylesheet" type="text/css" href="/css/style.css" />


    <style type="text/css">
        .auto-style1 {
            width: 255px;
            height: 67px;
        }
    </style>


</head>
<body>
   
    <form id="form1" runat="server">
 



     <div class="container">
        <div class="row">
            <div class="col-md-4 col-md-offset-4">
                <div class="login-panel panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <img alt="" class="auto-style1" src="Logo.png" /></h3>
                    </div>
                    <div class="panel-body">
                        <form role="form">
                            <fieldset>
                                <div class="form-group">
                                    <asp:TextBox  ID="txtemail" placeholder="Email"  class="form-control" runat="server"></asp:TextBox>
                                  
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtPassword" placeholder="Password" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                    
                                </div>
                                <%--<div class="checkbox">
                                    <label>
                                        <input name="remember" type="checkbox" value="Remember Me">Remember Me
                                    </label>
                                </div>--%>
                                <!-- Change this to a button or input when using this as a form -->
                                 <asp:Button class="btn btn-lg btn-success btn-block" ID="butSignIn" runat="server" onclick="butSignIn_Click" Text="Log In" />
                               <%-- <a href="#" class="btn btn-lg btn-success btn-block">Login</a>--%>
                            </fieldset>

                             <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- jQuery -->
    <script src="../bower_components/jquery/dist/jquery.min.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="../bower_components/bootstrap/dist/js/bootstrap.min.js"></script>

    <!-- Metis Menu Plugin JavaScript -->
    <script src="../bower_components/metisMenu/dist/metisMenu.min.js"></script>

    <!-- Custom Theme JavaScript -->
    <script src="../dist/js/sb-admin-2.js"></script>
        </form>
</body>
</html>
