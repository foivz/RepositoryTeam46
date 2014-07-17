<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Seds.Web.Login" %>

<%@ Register Src="~/UserControls/Message/MessageBox.ascx" TagPrefix="sng" TagName="MessageBox" %>


<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta charset="utf-8">
    <title>Administracija - prijava</title>
    <meta content="IE=edge,chrome=1" http-equiv="X-UA-Compatible">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">

    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Themes/BlueNileAdmin/lib/bootstrap/css/bootstrap.css") %>" />

    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Themes/BlueNileAdmin/stylesheets/theme.css") %>" />
    <link rel="stylesheet" href="<%= ResolveUrl("~/Themes/BlueNileAdmin/lib/font-awesome/css/font-awesome.css") %>" />
    <script src="<%= ResolveUrl("~/Scripts/jquery-1.7.1.js") %>"></script>

    <!-- Demo page code -->

    <style type="text/css">
        #line-chart {
            height: 300px;
            width: 800px;
            margin: 0px auto;
            margin-top: 1em;
        }

        .brand {
            font-family: georgia, serif;
        }

            .brand .first {
                color: #ccc;
                font-style: italic;
            }

            .brand .second {
                color: #fff;
                font-weight: bold;
            }
    </style>

    <!-- Le HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->

    <!-- Le fav and touch icons -->
    <link rel="shortcut icon" href="../assets/ico/favicon.ico">
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="../assets/ico/apple-touch-icon-144-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="../assets/ico/apple-touch-icon-114-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="../assets/ico/apple-touch-icon-72-precomposed.png">
    <link rel="apple-touch-icon-precomposed" href="../assets/ico/apple-touch-icon-57-precomposed.png">
</head>

<!--[if lt IE 7 ]> <body class="ie ie6"> <![endif]-->
<!--[if IE 7 ]> <body class="ie ie7 "> <![endif]-->
<!--[if IE 8 ]> <body class="ie ie8 "> <![endif]-->
<!--[if IE 9 ]> <body class="ie ie9 "> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!-->
<body class="">
    <!--<![endif]-->
    <form runat="server">
        <div class="navbar">
            <div class="navbar-inner">
                <ul class="nav pull-right">
                </ul>
                <a class="brand" href="<%= ResolveUrl("~/") %>"><span class="first">Administracija</span> <span class="second">SEDS</span></a>
            </div>
        </div>
        
        <sng:MessageBox runat="server" ID="MessageBox" />

        <div class="row-fluid">
            <div class="dialog">
                <div class="block">
                    <p class="block-heading">Prijava</p>
                    <div class="block-body">
                            <label>Korisničko ime</label>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="span12"></asp:TextBox>
                            <label>Lozinka</label>
                            <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="span12"></asp:TextBox>
                            <asp:Button ID="btnLogin" runat="server" Text="Prijava" CssClass="btn btn-primary pull-right" />
                            <div class="clearfix"></div>
                    </div>
                </div>               
            </div>
        </div>

        <script src="<%= ResolveUrl("~/Themes/BlueNileAdmin/lib/bootstrap/js/bootstrap.min.js") %>"></script>
        <script type="text/javascript">
            $("[rel=tooltip]").tooltip();
            $(function () {
                $('.demo-cancel-click').click(function () { return false; });
            });
        </script>
    </form>
</body>
</html>


