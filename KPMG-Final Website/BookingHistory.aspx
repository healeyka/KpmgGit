<%@ Page Title="" Language="C#" MasterPageFile="~/KPMG.master" AutoEventWireup="true" CodeFile="BookingHistory.aspx.cs" Inherits="BookingHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="App_Themes/Baker/Content/Login.css" rel="stylesheet" type="text/css" />
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
	background-color: white;
    /*opacity: 0.8;*/
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
		font-size: small;
		font-weight: 600;
		margin-right: 6px;
		width: auto;
		-moz-border-radius: 5px;
		-webkit-border-radius: 5px;
		border-radius: 5px;
        font-family: 'aller';
        margin-left: 22px;
    }
    .auto-style2 {
        width: 100%;
    }
    .auto-style11 {
        width: 42px;
    }
    .auto-style18 {
        width: 25px;
    }
    .auto-style19 {
        height: 21px;
        width: 25px;
    }
    .auto-style24 {
        width: 410px;
    }
    .auto-style25 {
        height: 21px;
        width: 410px;
    }
    .auto-style27 {
        width: 460px;
        height: 152px;
    }
    .auto-style28 {
        width: 25px;
        height: 152px;
    }
    .auto-style29 {
        width: 461px;
        height: 152px;
    }
    .auto-style30 {
        width: 469px;
    }
    .auto-style32 {
        height: 21px;
        width: 42px;
    }
    .auto-style33 {
        width: 42px;
        height: 152px;
    }
    .auto-style34 {
        width: 461px;
    }
    .auto-style36 {
        height: 21px;
        width: 461px;
    }
    </style>


<div style="margin-top: 10px; margin-left:auto; margin-right:auto; width: 80%; background-color: #F7F7F7;">
    <br />
    <h2 style="margin-top:10px; margin-left:10px; font-weight:bold;">My Bookings</h2>
	<hr style="margin-top:4px; margin-bottom: 8px;" />

		<asp:Panel ID="Panel0" runat="server" Height="110%" style="margin-left: 8px">
            <table class="auto-style2">
                <tr>
                    <td class="auto-style11">&nbsp;</td>
                    <td class="auto-style34">
                        <center><h3><u>Current Bookings</u></h3></center>
                    </td>
                    <td class="auto-style18">&nbsp;</td>
                    <td class="auto-style34">
                        <center><h3><u>Past Bookings</u></h3></center>
                    </td>
                    <td class="auto-style11" rowspan="8">
                    </td>
                </tr>
                <tr>
                    <td class="auto-style11">&nbsp;</td>
                    <td class="auto-style34">
                        <asp:Panel ID="Panel1" runat="server" BackColor="Silver" BorderColor="#223C72" BorderStyle="Double" Height="140px" Width="100%">
                            <asp:Label ID="CurrentFlights" runat="server" style="font-size: medium" Text="Flights:"></asp:Label>
                            <div style="width: 100%; height: 120px; overflow: scroll">
                                <asp:GridView ID="GVCurrentFlights" runat="server" AutoGenerateColumns="true" BackColor="White" Height="120px" style="color: #333333; background-color: #E3E3E3" Width="100%">
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </td>
                    <td class="auto-style18">
                    </td>
                    <td class="auto-style34">
                        <asp:Panel ID="Panel5" runat="server" BackColor="Silver" BorderColor="#223C72" BorderStyle="Double" Height="140px" Width="100%">
                            <asp:Label ID="FlightHistoryLabel" runat="server" style="font-size: medium" Text="Flight History:"></asp:Label>
                            <div style="width: 100%; height: 120px; overflow: scroll">
                                <asp:GridView ID="GVFlightHistory" runat="server" AutoGenerateColumns="True" BackColor="White" Height="120px" style="color: #333333; background-color: #E3E3E3" Width="100%" AllowSorting="True">
                                </asp:GridView>                           
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style11">&nbsp;</td>
                    <td class="auto-style34">&nbsp;</td>
                    <td class="auto-style18">&nbsp;</td>
                    <td class="auto-style34"></td>
                </tr>
                <tr>
                    <td class="auto-style32"></td>
                    <td class="auto-style36">
                        <asp:Panel ID="Panel2" runat="server" BackColor="Silver" BorderColor="#223C72" BorderStyle="Double" Height="140px" Width="100%" >
                            <asp:Label ID="CurrentTrains" runat="server" style="font-size: medium" Text="Trains:"></asp:Label>
                            <div style="width: 100%; height: 120px; overflow: scroll">
                                <asp:GridView ID="GVCurrentTrains" runat="server" AutoGenerateColumns="true" 
                                    BackColor="White" style="color: #333333; background-color: #E3E3E3" Height="120px"  Width="100%">
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </td>
                    <td class="auto-style19">&nbsp;</td>
                    <td class="auto-style34">
                        <asp:Panel ID="Panel6" runat="server" BackColor="Silver" BorderColor="#223C72" BorderStyle="Double" Height="140px" Width="100%">
                            <asp:Label ID="TrainsHistoryLabel" runat="server" style="font-size: medium" Text="Trains History:"></asp:Label>
                            <div style="width: 100%; height: 120px; overflow: scroll">
                                <asp:GridView ID="GVTrainsHistory" runat="server" AutoGenerateColumns="True" BackColor="White" Height="120px" style="color: #333333; background-color: #E3E3E3" Width="100%">
                                </asp:GridView>                           
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style32">&nbsp;</td>
                    <td class="auto-style36">&nbsp;</td>
                    <td class="auto-style19">&nbsp;</td>
                    <td class="auto-style34"></td>
                </tr>
                <tr>
                    <td class="auto-style33"></td>
                    <td class="auto-style29">
                        <asp:Panel ID="Panel3" runat="server" BackColor="Silver" BorderColor="#223C72" BorderStyle="Double" Height="140px" Width="100%" >
                            <asp:Label ID="CurrentHotels" runat="server" style="font-size: medium" Text="Hotels:"></asp:Label>
                            <div style="width: 100%; height: 120px; overflow: scroll">
                                <asp:GridView ID="GVCurrentHotels" runat="server" AutoGenerateColumns="true" 
                                    BackColor="White" style="color: #333333; background-color: #E3E3E3" Height="120px"  Width="100%">
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </td>
                    <td class="auto-style28"></td>
                    <td class="auto-style29">
                        <asp:Panel ID="Panel7" runat="server" BackColor="Silver" BorderColor="#223C72" BorderStyle="Double" Height="140px" Width="100%">
                            <asp:Label ID="HotelHistoryLabel" runat="server" style="font-size: medium" Text="Hotel History:"></asp:Label>
                            <div style="width: 100%; height: 120px; overflow: scroll">
                                <asp:GridView ID="GVHotelHistory" runat="server" AutoGenerateColumns="True" BackColor="White" Height="120px" style="color: #333333; background-color: #E3E3E3" Width="100%">
                                </asp:GridView>                               
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style11">&nbsp;</td>
                    <td class="auto-style34">&nbsp;</td>
                    <td class="auto-style18">&nbsp;</td>
                    <td class="auto-style34">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style11">&nbsp;</td>
                    <td class="auto-style34">
                        <asp:Panel ID="Panel4" runat="server" BackColor="Silver" BorderColor="#223C72" BorderStyle="Double" Height="140px" Width="100%" >
                            <asp:Label ID="CurrentCars" runat="server" style="font-size: medium" Text="Cars:"></asp:Label>
                            <div style="width: 100%; height: 120px; overflow: scroll">
                                <asp:GridView ID="GVCurrentCars" runat="server" AutoGenerateColumns="true" 
                                    BackColor="White" style="color: #333333; background-color: #E3E3E3" Height="120px"  Width="100%">
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </td>
                    <td class="auto-style18">&nbsp;</td>
                    <td class="auto-style34">
                        <asp:Panel ID="Panel8" runat="server" BackColor="Silver" BorderColor="#223C72" BorderStyle="Double" Height="140px" Width="100%">
                            <asp:Label ID="CarHistoryLabel" runat="server" style="font-size: medium" Text="Car History:"></asp:Label>
                            <div style="width: 100%; height: 120px; overflow: scroll">
                                <asp:GridView ID="GVCarHistory" runat="server" AutoGenerateColumns="True" BackColor="White" Height="120px" style="color: #333333; background-color: #E3E3E3" Width="100%" >
                                </asp:GridView>                                
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <br /><br /><br /><br />
        </asp:Panel>
</div>
</asp:Content>