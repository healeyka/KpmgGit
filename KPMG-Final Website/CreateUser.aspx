<%@ Page Title="KPMG MyAccount" Language="C#" AutoEventWireup="true" CodeFile="CreateUser.aspx.cs" Inherits="AccountPage" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>KPMG Create New User</title>
    <link href="App_Themes/Baker/Content/Login.css" rel="stylesheet" />
    <script src="App_Themes/Baker/Scripts/jquery-1.11.0.min.js"></script>
    <script src="App_Themes/Baker/Scripts/jquery-migrate-1.2.1.min.js"></script>
    <script src="App_Themes/Baker/Scripts/jquery-ui-1.10.3.js"></script>
</head>

<body>
    <form id="form1" runat="server">
    <!-- Header -->
    <div class="navbar navbar-default HeaderOuterBg" role="navigation">
      <div class="container-fluid">
        <div class="navbar-header">          
          <a href="/" class="navbar-brand"><img style="border-width: 0px;" alt="" src="App_Themes/Baker/Images/kpmgsmall2.png" /></a>
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
          opacity: 0.8;
    }
    .navbar-header {
        background-color: white;
    }

	.rounded-box {
	background-color: #F7F7F7;
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
		background-color: #e3e3e3;
		border-top: 1px solid #a99f9a;
		border-left: 1px solid #a99f9a;
		border-bottom: 1px solid #a99f9a;
		border-right: 1px solid #a99f9a;
		cursor: pointer;
		font-size: 1.2em;
		font-weight: 600;
		padding-top: 3px;
		padding-bottom: 3px;
		padding-left: 8px;
		padding-right: 8px;
		margin-right: 6px;
		width: auto;
		-moz-border-radius: 5px;
		-webkit-border-radius: 5px;
		border-radius: 5px;
	}
    .auto-style1 {
        width: 100%;
    }
    .auto-style10 {
        color: #FF3300;
        width: 220px;
    }
    .auto-style11 {
        width: 220px;
    }
    .auto-style12 {
        width: 219px;
    }
    .auto-style13 {
        width: 40px;
    }
    .auto-style14 {
        width: 35px;
    }
</style>

<div style="margin-top: 10px; margin-left:auto; margin-right:auto; width: 65%; background-color: #F7F7F7; height: 100%;">
    <br />
    <h2 style="margin-top:10px; margin-left:10px; font-weight:bold;">New User Information</h2>
	<hr style="margin-top:4px; margin-bottom: 8px;" />

		<asp:Panel ID="Panel1" runat="server" Height="110%" style="margin-left: 8px">
            <table class="auto-style1">
                <tr>
                    <td class="auto-style13">&nbsp;</td>
                    <td class="auto-style10">&nbsp;</td>
                    <td class="auto-style12">&nbsp;</td>
                    <td class="auto-style11">
                        <asp:SqlDataSource ID="AddUser" runat="server" ConnectionString="<%$ ConnectionStrings:KPMGTravelConnectionString %>" SelectCommand="SELECT [Account_Type], [Account_TypeName] FROM [Account_Type]"></asp:SqlDataSource>
                    </td>
                    <td class="auto-style14">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style13">&nbsp;</td>
                    <td class="auto-style10">&nbsp;</td>
                    <td class="auto-style12">&nbsp;</td>
                    <td class="auto-style11">&nbsp;</td>
                    <td class="auto-style14">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style13">&nbsp;</td>
                    <td class="auto-style11">
                        <asp:Label ID="lblUserID" runat="server" CssClass="auto-style24" Text="Choose a User ID (Login):"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtUserID" runat="server" BackColor="Silver" CssClass="roundInputs" Height="25px" Width="90%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredUserID" runat="server" ControlToValidate="txtUserID" ErrorMessage="Please enter a user ID" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                    <td class="auto-style12">
                        <asp:Label ID="lblPass" runat="server" CssClass="auto-style24" Text="Choose a Password:"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtPass" runat="server" CssClass="roundInputs"  Height="25px" Width="90%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredPass" runat="server" ControlToValidate="txtPass" ErrorMessage="Please enter a password" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                    <td class="auto-style11">
                        <asp:Label ID="lblType" runat="server" CssClass="auto-style24" Text="Choose Your Account Type:"></asp:Label>
                        <br />
                        <asp:DropDownList ID="dropAcctType" runat="server" AutoPostBack="True" CssClass="roundInputs" DataSourceID="AddUser" DataTextField="Account_TypeName" DataValueField="Account_Type" Height="35px" Width="90%">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredType" runat="server" ControlToValidate="dropAcctType" ErrorMessage="Please select an account type" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                    <td class="auto-style14">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style13">&nbsp;</td>
                    <td class="auto-style11">
                        <asp:Label ID="lblDOB0" runat="server" CssClass="auto-style24" Text="Date of Birth:"></asp:Label>
                        <asp:TextBox ID="txtDOB0" runat="server" CssClass="roundInputs" Height="25px" Width="90%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredDOB0" runat="server" ControlToValidate="txtFirstName" ErrorMessage="Please enter a valid Date of Birth" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                    <td class="auto-style12">&nbsp;</td>
                    <td class="auto-style11">
                        &nbsp;</td>
                    <td class="auto-style14">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style13">&nbsp;</td>
                    <td class="auto-style11">&nbsp;</td>
                    <td class="auto-style12">&nbsp;</td>
                    <td class="auto-style11">&nbsp;</td>
                    <td class="auto-style14">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style13">&nbsp;</td>
                    <td class="auto-style11">
                        <asp:Label ID="lblFirstName" runat="server" CssClass="auto-style24" Text="First Name:"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="roundInputs"  Height="25px" Width="90%"></asp:TextBox>
                    </td>
                    <td class="auto-style12">
                        <asp:Label ID="lblAddress" runat="server" CssClass="auto-style24" Text="Address Line 1:"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="roundInputs" Height="25px" Width="90%"></asp:TextBox>
                    </td>
                    <td class="auto-style11">
                        <asp:Label ID="lblPhone" runat="server" CssClass="auto-style24" Text="Telephone:"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtTelephone" runat="server" CssClass="roundInputs" Height="25px" Width="90%"></asp:TextBox>
                    </td>
                    <td class="auto-style14">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style13">&nbsp;</td>
                    <td class="auto-style11">
                        <asp:Label ID="lblMiddleName" runat="server" CssClass="auto-style24" Text="Middle Name:"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtMiddleName" runat="server" CssClass="roundInputs"  Height="25px" Width="90%"></asp:TextBox>
                    </td>
                    <td class="auto-style12">
                        <asp:Label ID="lblAddress2" runat="server" CssClass="auto-style24" Text="Address Line 2:"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtAddress2" runat="server" CssClass="roundInputs" Height="25px" Width="90%"></asp:TextBox>
                    </td>
                    <td class="auto-style11">
                        <asp:Label ID="lblEmail" runat="server" CssClass="auto-style24" Text="Email:"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="roundInputs" Height="25px" Width="90%"></asp:TextBox>
                    </td>
                    <td class="auto-style14">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style13">&nbsp;</td>
                    <td class="auto-style11">
                        <asp:Label ID="lblLastName" runat="server" CssClass="auto-style24" Text="Last Name:"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtLastName" runat="server" CssClass="roundInputs"  Height="25px" Width="90%"></asp:TextBox>
                    </td>
                    <td class="auto-style12">
                        <asp:Label ID="lblCity" runat="server" CssClass="auto-style24" Text="City:"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtCity" runat="server" CssClass="roundInputs" Height="25px" Width="90%"></asp:TextBox>
                    </td>
                    <td class="auto-style11">&nbsp;<br /> </td>
                    <td class="auto-style14">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style13">&nbsp;</td>
                    <td class="auto-style11">Assistant:<br />
                        <asp:TextBox ID="AssistantName" runat="server" CssClass="roundInputs" Height="25px"  Width="90%"></asp:TextBox>
                    </td>
                    <td class="auto-style12">
                        <asp:Label ID="lblState" runat="server" CssClass="auto-style24" Text="State:"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtState" runat="server" CssClass="roundInputs" Height="25px" Width="90%"></asp:TextBox>
                    </td>
                    <td class="auto-style11">&nbsp;</td>
                    <td class="auto-style14">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style13">&nbsp;</td>
                    <td class="auto-style11">&nbsp;</td>
                    <td class="auto-style12">
                        <asp:Label ID="lblZipCode" runat="server" CssClass="auto-style24" Text="ZipCode:"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtZipCode" runat="server" CssClass="roundInputs" Height="25px" Width="90%"></asp:TextBox>
                    </td>
                    <td class="auto-style11">&nbsp;</td>
                    <td class="auto-style14">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style13">&nbsp;</td>
                    <td class="auto-style11">
                        <asp:Button ID="Button1" runat="server" CssClass="roundInputs" Height="50px" OnClick="Button1_Click" Text="Add Me!" Width="100px" />
                        <br />
                        <asp:Label ID="Status" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style12">&nbsp;</td>
                    <td class="auto-style11">&nbsp;</td>
                    <td class="auto-style14">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style13">&nbsp;</td>
                    <td class="auto-style11">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                    </td>
                    <td class="auto-style12">
                        &nbsp;</td>
                    <td class="auto-style11">&nbsp;</td>
                    <td class="auto-style14">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style13">&nbsp;</td>
                    <td class="auto-style11">&nbsp;</td>
                    <td class="auto-style12">
                        <br />
                    </td>
                    <td class="auto-style11">&nbsp;</td>
                    <td class="auto-style14">&nbsp;</td>
                </tr>
            </table>
        </asp:Panel>
</div>
</form>
</body>
</html>