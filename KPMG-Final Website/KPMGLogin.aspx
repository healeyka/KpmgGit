﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KPMGLogin.aspx.cs" Inherits="KPMGLogin" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>KPMG Travel Login</title>
    <link href="App_Themes/Baker/Login.css" rel="stylesheet" type="text/css" />
    <script src="App_Themes/Baker/Scripts/jquery-1.11.0.min.js"></script>
    <script src="App_Themes/Baker/Scripts/jquery-migrate-1.2.1.min.js"></script>
    <script src="App_Themes/Baker/Scripts/jquery-ui-1.10.3.js"></script>
</head>

<body>
    <!-- Header -->
    <div class="navbar navbar-default HeaderOuterBg" role="navigation">
      <div class="container-fluid">
        <div class="navbar-header">          
          <a href="/" class="navbar-brand">
                        <img style= "border-width: 0px;" alt="" src="App_Themes/Baker/Images/kpmgsmall2.png" />
                    </a>
        </div>
      </div>
    </div>  
<!-- Header -->

<style type="text/css">
    @font-face{
	font-family: 'aller';
	src: url('fonts/Aller/Aller_Rg.ttf') format('truetype');
	font-weight: normal;
	font-style: normal;
	}
    body {background-image: url("App_Themes/Baker/Images/buildingbg.jpg");
          background-repeat: no-repeat;
          font-family: 'aller';
    }
    .navbar-header {
        background-color: white;
    }

	.rounded-box {
	background-color: white;
    opacity: 0.8;
	color: #000;
	position: relative;
	-moz-border-radius: 10px;
	-webkit-border-radius: 10px;
	border-radius: 10px;
	border-top: 1px solid #ddd;
	border-left: 1px solid #ddd;
	border-bottom: 1px solid #ddd;
	border-right: 1px solid #ddd;
	padding: 10px 10px;
	margin-left: auto;
	margin-right: auto;
}
	input[type="submit"],
	input[type="button"],
	button {
		border: 1px solid #a99f9a;
        padding: 3px 8px;
        background-color: white;
        cursor: pointer;
		font-size: 1.2em;
		font-weight: 600;
		margin-right: 6px;
		width: auto;
		-moz-border-radius: 5px;
		-webkit-border-radius: 5px;
		border-radius: 5px;
        font-family: 'aller';
        margin-left: 22px;
    }
    .auto-style1 {
        text-align: center;
    }
    .auto-style2 {
        width: 100%;
    }
    .auto-style4 {
        width: 100px;
    }
</style>

<div class="container">
	<div class="section" style="margin-left:auto; margin-right:auto;">
        <form id="form1" runat="server">
				<div class="row rounded-box" style="width: 30%; margin-top:30px;">

					<h3 style="margin-top:5px; font-weight:bold; font-size: 150%; text-align:center;">Secure Login</h3>
					<hr style="margin-top:4px; margin-bottom: 8px;" />
					<input name="__RequestVerificationToken" type="hidden" value="n_a3cK1jf6cxeZgUu1GgYrhW1jFV-JxP_ClaaY1i1J1V2QKxnWB-0XjwZXCqO-_0RQEDlYqPnR4zZE_FxmPK3xlslef5vZtgciA_P9MT1hM1" />
					

					<div>
						<label for="UserName">Username</label>
					</div>
					<div>
                        <asp:TextBox name="UserName" type="text" Text="" CssClass="txtfield" ID="UserName" runat="server" Height="25px" Width="95%" Font-Size="Large" data-val-required="The&#32;User&#32;name&#32;field&#32;is&#32;required." ></asp:TextBox><span style="font-size: 12px;"><span class="field-validation-valid" data-valmsg-for="UserName" data-valmsg-replace="true"></span></span>
					</div>

					<div style="margin-top: 10px;">
						<label class="lbl-pwd" for="Password">Password</label>
					</div>
					<div>
                        <asp:TextBox class="txtfield" type="password" ID="Password" runat="server" name="password" Height="25px" Width="95%" Font-Size="Large" data-val="true" data-val-required="The&#32;Password&#32;field&#32;is&#32;required."></asp:TextBox><span style="font-size: 12px;"><span class="field-validation-valid" data-valmsg-for="Password" data-valmsg-replace="true"></span></span>
					</div>

					<div>
						<span style="margin-right: 5px"><input data-val="true" data-val-required="The&#32;Remember&#32;me&#32;field&#32;is&#32;required." id="RememberMe" name="RememberMe" type="checkbox" value="true" /><input name="RememberMe" type="hidden" value="false" /></span><label class="cbRememberMe" for="RememberMe">Remember me</label>
					</div>

					<div style="margin-top: 10px;" class="auto-style1">
					    <table class="auto-style2">
                            <tr>
                                <td class="auto-style4">
					                &nbsp;</td>
                                <td>
					            <asp:Button ID="btnLogin" class="btn" runat="server" Text="Login" OnClick="btnLogin_Click" />
                                </td>
                                <td>
					            <asp:Button ID="btnNewUser" class="btn" runat="server" Text="New User" OnClick="btnNewUser_Click" Visible="False" />
                                </td>
                                <td class="auto-style4">
					                &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="4">
					    <asp:Label ID="Label1" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
					</div>

				</div>
        </form>
    </div>
</div>
</body>
</html>
