<%@ Page Title="KPMG MyAccount" Language="C#" MasterPageFile="~/KPMG.master" AutoEventWireup="true" CodeFile="AccountPage.aspx.cs" Inherits="AccountPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="App_Themes/Baker/Content/Login.css" rel="stylesheet" type="text/css" />
    <script src="App_Themes/Baker/Scripts/jquery-1.10.2.js"></script>
    <script src="App_Themes/Baker/Scripts/jquery-1.11.0.min.js"></script>
    <script src="App_Themes/Baker/Scripts/jquery-1.11.2.js"></script>
    <script src="App_Themes/Baker/Scripts/jquery-migrate-1.2.1.min.js"></script>
    <script src="App_Themes/Baker/Scripts/jquery-ui-1.10.3.js"></script>
    <link href="App_Themes/Baker/Scripts/jquery-ui.css" rel="stylesheet" />
    <script src="App_Themes/Baker/Scripts/jquery-ui.js"></script>

    <script>
        $(function () {
            $("#datepicker").datepicker();
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMainContent" Runat="Server">
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
		border: 1px solid #a99f9a;
        padding: 3px 8px;
        background-color: #e3e3e3;
		cursor: pointer;
		font-size: small;
		font-weight: 600;
		margin-right: 6px;
		-moz-border-radius: 5px;
		-webkit-border-radius: 5px;
		border-radius: 5px;
	}
        .auto-style24 {
            font-size: medium;
            color: #003366;
        }
        .auto-style58 {
            width: 100%;
        }
        .auto-style60 {
            width: 216px;
        }
        .auto-style62 {
        }
        .auto-style65 {
        }
        .auto-style67 {
        }
        .auto-style68 {
            width: 130px;
        }
        .auto-style69 {
            width: 64px;
        }
        .auto-style71 {
            width: 19px;
        }
        .auto-style75 {
            width: 51px;
        }
        .auto-style76 {
        }
        .auto-style77 {
            text-align: center;
        }
        .auto-style78 {
            width: 120px;
        }
        .auto-style79 {
            width: 19px;
            height: 28px;
        }
        .auto-style80 {
            width: 130px;
            height: 28px;
        }
        .auto-style81 {
            width: 216px;
            height: 28px;
        }
        .auto-style82 {
            width: 120px;
            height: 28px;
        }
        .auto-style83 {
            height: 28px;
        }
        .auto-style84 {
            width: 19px;
            height: 22px;
        }
        .auto-style85 {
            width: 130px;
            height: 22px;
        }
        .auto-style86 {
            width: 216px;
            height: 22px;
        }
        .auto-style87 {
            width: 120px;
            height: 22px;
        }
        .auto-style88 {
            height: 22px;
        }
        .auto-style89 {
            text-align: center;
            height: 28px;
        }
        .auto-style90 {
            width: 51px;
            height: 28px;
        }
        .auto-style91 {
            width: 64px;
            height: 28px;
        }
    </style>

<div style="margin-top: 10px; margin-left:auto; margin-right:auto; width: 80%; background-color: #F7F7F7;">
    <br />
    <h2 style="margin-top:10px; margin-left:10px; font-weight:bold;">My Account Info</h2>
	<hr style="margin-top:4px; margin-bottom: 8px;" />

		<asp:Panel ID="Panel1" runat="server" Height="110%" style="margin-left: 8px">
            <table class="auto-style58">
                <tr>
                    <td class="auto-style71">&nbsp;</td>
                    <td class="auto-style68">
                        <asp:Label ID="lblUserID" runat="server" CssClass="auto-style24" Text="User ID:"></asp:Label>
                    </td>
                    <td class="auto-style60">
                        <asp:TextBox ID="txtUserID" CssClass="roundInputs" runat="server" BackColor="Silver" Width="200px" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td class="auto-style78">&nbsp;</td>
                    <td class="auto-style62" colspan="6">
                        <asp:Label ID="RewardsLabel" runat="server" CssClass="auto-style24" Font-Underline="True" Text="Membership Rewards:" ForeColor="#223C82"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style71">&nbsp;</td>
                    <td class="auto-style68">
                        <asp:Label ID="lblFirstName" runat="server" CssClass="auto-style24" Text="First Name:"></asp:Label>
                    </td>
                    <td class="auto-style60">
                        <asp:TextBox ID="txtFirstName" CssClass="roundInputs" runat="server" Width="200px" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td class="auto-style78">&nbsp;</td>
                    <td class="auto-style77">1.</td>
                    <td class="auto-style75">
                        <asp:Label ID="Reward1" runat="server" CssClass="auto-style24" Text="Name:"></asp:Label>
                    </td>
                    <td class="auto-style68">
                        <asp:TextBox ID="txtRewardName1" CssClass="roundInputs" runat="server" Width="125px"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td class="auto-style69">
                        <asp:Label ID="RewardNumber1" runat="server" CssClass="auto-style24" Text="Number:"></asp:Label>
                    </td>
                    <td class="auto-style68">
                        <asp:TextBox ID="txtRewardNumber1" CssClass="roundInputs" runat="server" Width="125px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="SaveRewardButton1" runat="server" Font-size="Small" Height="20px" Text="Save Edit" Width="100px" CssClass="roundInputs" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style71">&nbsp;</td>
                    <td class="auto-style68">
                        <asp:Label ID="lblMiddleName" runat="server" CssClass="auto-style24" Text="Middle Name:"></asp:Label>
                    </td>
                    <td class="auto-style60">
                        <asp:TextBox ID="txtMiddleName" CssClass="roundInputs" runat="server" Width="200px" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td class="auto-style78">&nbsp;</td>
                    <td class="auto-style77">2.</td>
                    <td class="auto-style75">
                        <asp:Label ID="Reward2" runat="server" CssClass="auto-style24" Text="Name:"></asp:Label>
                    </td>
                    <td class="auto-style68">
                        <asp:TextBox ID="txtRewardName2" runat="server" CssClass="roundInputs" Width="125px"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td class="auto-style69">
                        <asp:Label ID="RewardNumber2" runat="server" CssClass="auto-style24" Text="Number:"></asp:Label>
                    </td>
                    <td class="auto-style68">
                        <asp:TextBox ID="txtRewardNumber2" CssClass="roundInputs" runat="server" Width="125px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="SaveRewardButton2" runat="server" Height="20px" Text="Save Edit" Width="100px" CssClass="roundInputs" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style71">&nbsp;</td>
                    <td class="auto-style68">
                        <asp:Label ID="lblLastName" runat="server" CssClass="auto-style24" Text="Last Name:"></asp:Label>
                    </td>
                    <td class="auto-style60">
                        <asp:TextBox ID="txtLastName" CssClass="roundInputs" runat="server" Width="200px" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td class="auto-style78">&nbsp;</td>
                    <td class="auto-style77">3.</td>
                    <td class="auto-style75">
                        <asp:Label ID="Reward3" runat="server" CssClass="auto-style24" Text="Name:"></asp:Label>
                    </td>
                    <td class="auto-style68">
                        <asp:TextBox ID="txtRewardName3" runat="server" CssClass="roundInputs" Width="125px"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td class="auto-style69">
                        <asp:Label ID="RewardNumber3" runat="server" CssClass="auto-style24" Text="Number:"></asp:Label>
                    </td>
                    <td class="auto-style68">
                        <asp:TextBox ID="txtRewardNumber3" CssClass="roundInputs" runat="server" Width="125px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="SaveRewardButton3" runat="server" Height="20px" Text="Save Edit" Width="100px" CssClass="roundInputs" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style79"></td>
                    <td class="auto-style80">
                        <asp:Label ID="lblAddress1" runat="server" CssClass="auto-style24" Text="Address Line 1:"></asp:Label>
                    </td>
                    <td class="auto-style81">
                        <asp:TextBox ID="txtAddress" CssClass="roundInputs" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td class="auto-style82"></td>
                    <td class="auto-style89">4.</td>
                    <td class="auto-style90">
                        <asp:Label ID="Reward4" runat="server" CssClass="auto-style24" Text="Name:"></asp:Label>
                    </td>
                    <td class="auto-style80">
                        <asp:TextBox ID="txtRewardName4" runat="server" CssClass="roundInputs" Width="125px"></asp:TextBox>
                    </td>
                    <td class="auto-style83"></td>
                    <td class="auto-style91">
                        <asp:Label ID="RewardNumber4" runat="server" CssClass="auto-style24" Text="Number:"></asp:Label>
                    </td>
                    <td class="auto-style80">
                        <asp:TextBox ID="txtRewardNumber4" CssClass="roundInputs" runat="server" Width="125px"></asp:TextBox>
                    </td>
                    <td class="auto-style83">
                        <asp:Button ID="SaveRewardButton4" runat="server" Height="20px" Text="Save Edit" Width="100px" CssClass="roundInputs" />
                    </td>
                    <td class="auto-style83"></td>
                </tr>
                <tr>
                    <td class="auto-style71">&nbsp;</td>
                    <td class="auto-style68">
                        <asp:Label ID="lblAddress2" runat="server" CssClass="auto-style24" Text="Address Line 2:"></asp:Label>
                    </td>
                    <td class="auto-style60">
                        <asp:TextBox ID="txtAddress2" CssClass="roundInputs" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td class="auto-style78">&nbsp;</td>
                    <td class="auto-style77">5.</td>
                    <td class="auto-style75">
                        <asp:Label ID="Reward5" runat="server" CssClass="auto-style24" Text="Name:"></asp:Label>
                    </td>
                    <td class="auto-style68">
                        <asp:TextBox ID="txtRewardName5" runat="server" CssClass="roundInputs" Width="125px"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td class="auto-style69">
                        <asp:Label ID="RewardNumber5" runat="server" CssClass="auto-style24" Text="Number:"></asp:Label>
                    </td>
                    <td class="auto-style68">
                        <asp:TextBox ID="txtRewardNumber5" CssClass="roundInputs" runat="server" Width="125px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="SaveRewardButton5" runat="server" Height="20px" Text="Save Edit" Width="100px" CssClass="roundInputs" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style71">&nbsp;</td>
                    <td class="auto-style68">
                        <asp:Label ID="lblCity" runat="server" CssClass="auto-style24" Text="City:"></asp:Label>
                    </td>
                    <td class="auto-style60">
                        <asp:TextBox ID="txtCity" CssClass="roundInputs" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td class="auto-style78">&nbsp;</td>
                    <td class="auto-style65">&nbsp;</td>
                    <td class="auto-style75">&nbsp;</td>
                    <td colspan="4">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style71">&nbsp;</td>
                    <td class="auto-style68">
                        <asp:Label ID="lblState0" runat="server" CssClass="auto-style24" Text="State:"></asp:Label>
                    </td>
                    <td class="auto-style60">
                        <asp:TextBox ID="txtState" runat="server" CssClass="roundInputs" Width="200px"></asp:TextBox>
                    </td>
                    <td class="auto-style78">&nbsp;</td>
                    <td class="auto-style65" colspan="4">
                        <asp:Label ID="PaymentLabel" runat="server" CssClass="auto-style24" Font-Underline="True" ForeColor="#223C82" Text="Credit Card Information:"></asp:Label>
                    </td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td colspan="2">
                        <asp:Label ID="lblAstDropDown" runat="server" CssClass="auto-style24" Text="Assistant's Employee List" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style71">&nbsp;</td>
                    <td class="auto-style68">
                        <asp:Label ID="lblZipCode" runat="server" CssClass="auto-style24" Text="ZipCode:"></asp:Label>
                    </td>
                    <td class="auto-style60">
                        <asp:TextBox ID="txtZipCode" runat="server" CssClass="roundInputs" Width="200px"></asp:TextBox>
                    </td>
                    <td class="auto-style78">&nbsp;</td>
                    <td class="auto-style65">&nbsp;</td>
                    <td class="auto-style67" colspan="3">
                        <asp:Label ID="lblCreditCardNumber2" runat="server" CssClass="auto-style24" Text="Card Holder:"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtCCName" runat="server" CssClass="roundInputs" Width="160px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="AssistantDropDown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="AssistantDropDown_SelectedIndexChanged" Visible="false">
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style71">&nbsp;</td>
                    <td class="auto-style68">
                        <asp:Label ID="lblPhone0" runat="server" CssClass="auto-style24" Text="Telephone:"></asp:Label>
                    </td>
                    <td class="auto-style60">
                        <asp:TextBox ID="txtTelephone" runat="server" CssClass="roundInputs" Width="200px"></asp:TextBox>
                    </td>
                    <td class="auto-style78">&nbsp;</td>
                    <td class="auto-style65">&nbsp;</td>
                    <td class="auto-style67" colspan="3">
                        <asp:Label ID="lblCreditCardNumber3" runat="server" CssClass="auto-style24" Text="Credit Card Number:"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtCCNumber" runat="server" CssClass="roundInputs" Width="160px"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style71">&nbsp;</td>
                    <td class="auto-style68">
                        <asp:Label ID="lblEmail0" runat="server" CssClass="auto-style24" Text="Email:"></asp:Label>
                    </td>
                    <td class="auto-style60">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="roundInputs" Width="200px"></asp:TextBox>
                    </td>
                    <td class="auto-style78">&nbsp;</td>
                    <td class="auto-style65">&nbsp;</td>
                    <td class="auto-style67" colspan="3">
                        <asp:Label ID="lblCreditCard1" runat="server" CssClass="auto-style24" Text="Credit Card Type:"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtCCType" runat="server" CssClass="roundInputs" Width="160px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style79">&nbsp;</td>
                    <td class="auto-style80">&nbsp;</td>
                    <td class="auto-style81">
                        <asp:Button ID="SaveRewardButton9" runat="server" Height="35px" Text="Save Edits" Width="216px" OnClick="SaveRewardButton9_Click" CssClass="roundInputs" />
                    </td>
                    <td class="auto-style82">&nbsp;</td>
                    <td class="auto-style83">&nbsp;</td>
                    <td class="auto-style83" colspan="3">
                        <asp:Label ID="lblCreditCardExp0" runat="server" CssClass="auto-style24" Text="Credit Card Expiration:"></asp:Label>
                    </td>
                    <td colspan="2" class="auto-style83">
                        <asp:TextBox ID="txtCCExp" runat="server" CssClass="roundInputs" Width="160px"></asp:TextBox>
                    </td>
                    <td class="auto-style83">
                        &nbsp;</td>
                    <td class="auto-style83">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style71">&nbsp;</td>
                    <td class="auto-style68">&nbsp;</td>
                    <td class="auto-style60">&nbsp;</td>
                    <td class="auto-style78">&nbsp;</td>
                    <td class="auto-style65">&nbsp;</td>
                    <td class="auto-style67" colspan="3">&nbsp;</td>
                    <td colspan="2">
                        <asp:Button ID="SaveCCButton0" runat="server" CssClass="roundInputs" Height="35px" Text="Save Edits" Width="142px" OnClick="SaveCCButton0_Click" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style71">&nbsp;</td>
                    <td class="auto-style76" colspan="2">
                        <asp:Label ID="TSATravelLabel" runat="server" CssClass="auto-style24" Font-Underline="True" ForeColor="#223C82" Text="Manage TSA Travel Data:"></asp:Label>
                    </td>
                    <td class="auto-style78">&nbsp;</td>
                    <td class="auto-style65" colspan="4">
                        <asp:Label ID="PasswordMgmtLabel" runat="server" CssClass="auto-style24" Font-Underline="True" ForeColor="#223C82" Text="Manage Account Password:"></asp:Label>
                    </td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style71">&nbsp;</td>
                    <td class="auto-style68">
                        <asp:Label ID="lblPassportNumber" runat="server" CssClass="auto-style24" Text="Passport"></asp:Label>
                    </td>
                    <td class="auto-style60">
                        <asp:TextBox ID="txtPassportNumber" runat="server" CssClass="roundInputs" Width="200px"></asp:TextBox>
                    </td>
                    <td class="auto-style78">&nbsp;</td>
                    <td class="auto-style65">&nbsp;</td>
                    <td class="auto-style67" colspan="3">
                        <asp:Label ID="lblNewPassword0" runat="server" CssClass="auto-style24" Text="New Password:"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtNewPassword" runat="server" CssClass="roundInputs" Width="125px"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style71">&nbsp;</td>
                    <td class="auto-style68">
                        <asp:Label ID="lblPassportCountry3" runat="server" CssClass="auto-style24" Text="TSATKNNumber"></asp:Label>
                    </td>
                    <td class="auto-style60">
                        <asp:TextBox ID="txtTSAKN" runat="server" CssClass="roundInputs" Width="200px"></asp:TextBox>
                    </td>
                    <td class="auto-style78">&nbsp;</td>
                    <td class="auto-style65">&nbsp;</td>
                    <td class="auto-style67" colspan="3">
                        <asp:Label ID="lblConfirmNewPassword0" runat="server" CssClass="auto-style24" Text="Confirm New Password:"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtConfirmNewPassword0" runat="server" CssClass="roundInputs" Width="125px"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style71">&nbsp;</td>
                    <td class="auto-style68">
                        <asp:Label ID="lblPassportCountry2" runat="server" CssClass="auto-style24" Text="TSAPreCheck"></asp:Label>
                    </td>
                    <td class="auto-style60">
                        <asp:TextBox ID="txtPre" runat="server" CssClass="roundInputs" Width="200px"></asp:TextBox>
                    </td>
                    <td class="auto-style78">&nbsp;</td>
                    <td class="auto-style65">&nbsp;</td>
                    <td class="auto-style67" colspan="3">&nbsp;</td>
                    <td colspan="2">
                        <asp:Button ID="SaveNewPasswordButton1" runat="server" CssClass="roundInputs" Height="35px" Text="Save Edits" Width="142px" OnClick="SaveNewPasswordButton1_Click" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style71">&nbsp;</td>
                    <td class="auto-style68">
                        &nbsp;</td>
                    <td class="auto-style60">
                        <asp:Button ID="btnTSASave" runat="server" CssClass="roundInputs" Height="35px" OnClick="TSASave" Text="Save Edits" Width="216px" />
                    </td>
                    <td class="auto-style78">&nbsp;</td>
                    <td class="auto-style65" colspan="4">
                        <asp:Label ID="TravelPrefLabel" runat="server" CssClass="auto-style24" Font-Underline="True" ForeColor="#223C82" Text="Manage Travel Preferences:"></asp:Label>
                    </td>
                    <td colspan="2">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style71">&nbsp;</td>
                    <td class="auto-style68">&nbsp;</td>
                    <td class="auto-style60">&nbsp;</td>
                    <td class="auto-style78">&nbsp;</td>
                    <td class="auto-style65">&nbsp;</td>
                    <td class="auto-style67" colspan="3">
                        <asp:Label ID="lblHotelPrefs" runat="server" CssClass="auto-style24" Text="Hotel Name:"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:DropDownList ID="DDLHotel" runat="server" OnSelectedIndexChanged="DDLHotel_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Selected ="True">Marriott</asp:ListItem>
                            <asp:ListItem>Sheraton</asp:ListItem>
                            <asp:ListItem>Hyatt</asp:ListItem>
                            <asp:ListItem>Holiday Inn</asp:ListItem>
                            <asp:ListItem>Days Inn</asp:ListItem>
                            <asp:ListItem>Best Western</asp:ListItem>
                            <asp:ListItem>Wyndham</asp:ListItem>
                            <asp:ListItem>Choice</asp:ListItem>
                            <asp:ListItem>Accor</asp:ListItem>
                            <asp:ListItem>Hilton</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblCarPrefs0" runat="server" CssClass="auto-style24" Text="Car Transmission:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DDLCarTrans" runat="server" OnSelectedIndexChanged="DDLCarTrans_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Selected="True">Automatic</asp:ListItem>
                            <asp:ListItem>Manual</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style71">&nbsp;</td>
                    <td class="auto-style68">
                        &nbsp;</td>
                    <td class="auto-style60">
                        &nbsp;</td>
                    <td class="auto-style78">&nbsp;</td>
                    <td class="auto-style65">&nbsp;</td>
                    <td class="auto-style67" colspan="3">
                        <asp:Label ID="lblFlightCabinPref" runat="server" CssClass="auto-style24" Text="Airline Cabin Type:"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:DropDownList ID="DDLPlaneCabin" runat="server" OnSelectedIndexChanged="DDLPlaneCabin_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Selected="True">Economy</asp:ListItem>
                            <asp:ListItem>Business Class</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblTrainPrefs" runat="server" CssClass="auto-style24" Text="Train Cabin Type:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DDLTrainCabin" runat="server" OnSelectedIndexChanged="DDLTrainCabin_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Selected="True">Coach</asp:ListItem>
                            <asp:ListItem>First-Class</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style84"></td>
                    <td class="auto-style85"></td>
                    <td class="auto-style86">
                        &nbsp;</td>
                    <td class="auto-style87"></td>
                    <td class="auto-style88"></td>
                    <td class="auto-style88" colspan="3">
                        <asp:Label ID="lblFlightNamePref" runat="server" CssClass="auto-style24" Text="Airline Name:"></asp:Label>
                    </td>
                    <td colspan="2" class="auto-style88">
                        <asp:DropDownList ID="DDLAirline" runat="server" OnSelectedIndexChanged="DDLAirline_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Selected="True">United</asp:ListItem>
                            <asp:ListItem>Delta</asp:ListItem>
                            <asp:ListItem>Southwest</asp:ListItem>
                            <asp:ListItem>American</asp:ListItem>
                            <asp:ListItem>Jet Blue</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style88"></td>
                    <td class="auto-style88"></td>
                </tr>
                <tr>
                    <td class="auto-style71">&nbsp;</td>
                    <td class="auto-style68">&nbsp;</td>
                    <td class="auto-style60">&nbsp;</td>
                    <td class="auto-style78">&nbsp;</td>
                    <td class="auto-style65">&nbsp;</td>
                    <td class="auto-style67" colspan="3">
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </asp:Panel>
</div>
</asp:Content>