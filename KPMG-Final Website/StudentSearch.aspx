<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMasterPage.master" AutoEventWireup="true" CodeFile="StudentSearch.aspx.cs" Inherits="StudentSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="App_Themes/Baker/Content/Login.css" rel="stylesheet" type="text/css" />
    <script src="https://maps.googleapis.com/maps/api/js"></script>
    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&signed_in=true&libraries=weather"></script>

<%--    <script>

        function initialize() {
            var mapOptions = {
                zoom: 4,
                center: new google.maps.LatLng(49.265984, -123.127491)
            };

            var map = new google.maps.Map(document.getElementById('<%= Page.Master.FindControl("cpMainContent").FindControl("Button1").ClientID %>'), mapOptions);

            var weatherLayer = new google.maps.weather.WeatherLayer({
                temperatureUnits: google.maps.weather.TemperatureUnit.FAHRENHEIT
            });
            weatherLayer.setMap(map);

            var cloudLayer = new google.maps.weather.CloudLayer();
            cloudLayer.setMap(map);
        }
        google.maps.event.addDomListener(window, 'load', initialize);

    </script>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cpMainContent" Runat="Server">
    <style type="text/css">
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
		    width: auto;
		    -moz-border-radius: 5px;
		    -webkit-border-radius: 5px;
		    border-radius: 5px;
	}
    .auto-style4 {
        width: 330px;
    }
                

      #map-canvas {
        height: 100%;
        margin: 0px;
        padding: 0px
      }


        .auto-style11 {
            width: 200px;
        }
        

        .auto-style13 {
            width: 200px;
            height: 38px;
        }


        .auto-style15 {
            width: 500px;
        }
        .auto-style16 {
            width: 500px;
            height: 38px;
        }


        .auto-style17 {
            width: 17px;
        }
        .auto-style18 {
            width: 17px;
            height: 38px;
        }


    </style>

<div style="margin-top: 10px; margin-left:auto; margin-right:auto; width: 80%; background-color: #F7F7F7;">
    <br />
    <h2 style="margin-top:10px; margin-left:10px; font-weight:bold;">Travel Reservation Search&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="FlightSearch" runat="server" CssClass="roundInputs" OnClick="FlightToggle_Click" Text="Flight Search" Height="25px" Width="125px" />
                <asp:Button ID="TrainSearch" runat="server" CssClass="roundInputs" OnClick="TrainToggle_Click" Text="Train Search" Height="25px" Width="125px" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </h2>
	<hr style="margin-top:4px; margin-bottom: 8px;" />

        <table style="width: 100%;">
        <tr>
            <td class="auto-style17">                           
                &nbsp;</td>
            <td class="auto-style4" colspan="2">                           
                <asp:Button ID="OneWayButton" runat="server" CssClass="roundInputs" OnClick="OneWay_Click" Text="One-Way" Height="25px" Width="125px" />
                <asp:Button ID="RoundTripButton" runat="server" CssClass="roundInputs" OnClick="RoundTrip_Click" Text="Round-Trip" Height="25px" Width="125px" />
                    </td>
            <td class="auto-style15" rowspan="3"> 
                
                                          
            <!--    GRIDVIEW GOES HERE     -->
            <asp:Panel ID="Panel1" runat="server" Height="332px" Width="100%" BorderColor="#223C72" BorderStyle="Double">
            <asp:Label ID="ResultsLabel" runat="server" Text="Search Results:" style="font-size: large"></asp:Label>
            <div style="width: 100%; height: 310px; overflow: scroll">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns ="true" AutoGenerateSelectButton ="true" Height="310px" Width="100%" style="color: #333333; background-color: #E3E3E3" BackColor="Silver">
                </asp:GridView>
            </div>
            </asp:Panel>


            </td>
        </tr>
        <tr>
            <td class="auto-style17">
                &nbsp;</td>
            <td class="auto-style4" colspan="2">
                <div style="margin-top:10px;">
                <asp:Label ID="FromLabel" runat="server" Text="Travel From"></asp:Label>

                <br />
                <asp:TextBox name="FromLabel" type="text" CssClass="roundInputs" ID="FromText" runat="server" Height="25px" Width="60%" ></asp:TextBox>
                    <br />
                    <asp:Label ID="ToLabel" runat="server" Text="Travel To"></asp:Label>
                    <br />
                    <asp:TextBox ID="ToText" runat="server" CssClass="roundInputs" Height="25px" name="ToText" type="text" Width="60%"></asp:TextBox>
                <br />
                </div> 
                </td>
        </tr>
        <tr>
            <td class="auto-style17">
                &nbsp;</td>
            <td class="auto-style11">
                <div style="margin-top:10px;">
                <asp:Label ID="DepartLabel" runat="server" Text="Depart Date"></asp:Label>
                </div>
                <div style="height:30%; width:80%">
                <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
                </div>
                </td>
            <td class="auto-style11">
                                <div style="margin-top:10px;">
                <asp:Label ID="ReturnLabel" runat="server" Text="Return Date"></asp:Label>
                </div>
                <div style="height:30%; width:80%">
                <asp:Calendar ID="Calendar2" runat="server"></asp:Calendar>
                </div>
            </td>
        </tr>
        <tr>
            <td class="auto-style18">
                &nbsp;</td>

            <td class="auto-style13">
                <div style="margin-top:5px;">
                <asp:Label ID="DepartTimeLabel" runat="server" Text="Departing Time"></asp:Label>
                <br />
                <asp:DropDownList ID="DepartTimeDDL" runat="server" CssClass="roundInputs" Height="35px" Width="65%" required="required">
                    <asp:ListItem selected="True" value=" "> Anytime </asp:ListItem>
                    <asp:ListItem  value="Early Morning"> Early Morning </asp:ListItem>
                    <asp:ListItem  value="Morning"> Morning </asp:ListItem>
                    <asp:ListItem  value="12:00 Noon"> 12:00 Noon </asp:ListItem>
                    <asp:ListItem  value="Afternoon"> Afternoon </asp:ListItem>
                    <asp:ListItem  value="Evening"> Evening </asp:ListItem>
                    <asp:ListItem  value="Late Evening"> Late Evening </asp:ListItem>
                    <asp:ListItem  value="12:00 Midnight"> 12:00 Midnight </asp:ListItem>
                    <asp:ListItem  value=" "> --------------- </asp:ListItem>
                    <asp:ListItem value="1:00 a.m."> 1:00 a.m. </asp:ListItem>
                    <asp:ListItem value="2:00 a.m."> 2:00 a.m. </asp:ListItem>
                    <asp:ListItem value="3:00 a.m."> 3:00 a.m. </asp:ListItem>
                    <asp:ListItem value="4:00 a.m."> 4:00 a.m. </asp:ListItem>
                    <asp:ListItem value="5:00 a.m."> 5:00 a.m. </asp:ListItem>
                    <asp:ListItem value="6:00 a.m."> 6:00 a.m. </asp:ListItem>
                    <asp:ListItem value="7:00 a.m."> 7:00 a.m. </asp:ListItem>
                    <asp:ListItem value="8:00 a.m."> 8:00 a.m. </asp:ListItem>
                    <asp:ListItem value="9:00 a.m."> 9:00 a.m. </asp:ListItem>
                    <asp:ListItem value="10:00 a.m."> 10:00 a.m. </asp:ListItem>
                    <asp:ListItem value="11:00 a.m."> 11:00 a.m. </asp:ListItem>
                    <asp:ListItem value="12:00 a.m."> 12:00 p.m. </asp:ListItem>
                    <asp:ListItem value="1:00 p.m."> 1:00 p.m. </asp:ListItem>
                    <asp:ListItem value="2:00 p.m."> 2:00 p.m. </asp:ListItem>
                    <asp:ListItem value="3:00 p.m."> 3:00 p.m. </asp:ListItem>
                    <asp:ListItem value="4:00 p.m."> 4:00 p.m. </asp:ListItem>
                    <asp:ListItem value="5:00 p.m."> 5:00 p.m. </asp:ListItem>
                    <asp:ListItem value="6:00 p.m."> 6:00 p.m. </asp:ListItem>
                    <asp:ListItem value="7:00 p.m."> 7:00 p.m. </asp:ListItem>
                    <asp:ListItem value="8:00 p.m."> 8:00 p.m. </asp:ListItem>
                    <asp:ListItem value="9:00 p.m."> 9:00 p.m. </asp:ListItem>
                    <asp:ListItem value="10:00 p.m."> 10:00 p.m. </asp:ListItem>
                    <asp:ListItem value="11:00 p.m."> 11:00 p.m. </asp:ListItem>
                    <asp:ListItem value="12:00 a.m."> 12:00 a.m. </asp:ListItem>
                </asp:DropDownList>
                </div>
            </td>

            <td class="auto-style13">
            <div style="margin-top:5px;">
            <asp:Label ID="ArrivalTimeLabel" runat="server" Text="Arrival Time"></asp:Label>
                <br />
                <asp:DropDownList ID="ArrivalTimeDDL" runat="server" CssClass="roundInputs" Height="35px" Width="65%" required="required">
                    <asp:ListItem selected="True" value=" "> Anytime </asp:ListItem>
                    <asp:ListItem  value="Early Morning"> Early Morning </asp:ListItem>
                    <asp:ListItem  value="Morning"> Morning </asp:ListItem>
                    <asp:ListItem  value="12:00 Noon"> 12:00 Noon </asp:ListItem>
                    <asp:ListItem  value="Afternoon"> Afternoon </asp:ListItem>
                    <asp:ListItem  value="Evening"> Evening </asp:ListItem>
                    <asp:ListItem  value="Late Evening"> Late Evening </asp:ListItem>
                    <asp:ListItem  value="12:00 Midnight"> 12:00 Midnight </asp:ListItem>
                    <asp:ListItem  value=" "> --------------- </asp:ListItem>
                    <asp:ListItem value="1:00 a.m."> 1:00 a.m. </asp:ListItem>
                    <asp:ListItem value="2:00 a.m."> 2:00 a.m. </asp:ListItem>
                    <asp:ListItem value="3:00 a.m."> 3:00 a.m. </asp:ListItem>
                    <asp:ListItem value="4:00 a.m."> 4:00 a.m. </asp:ListItem>
                    <asp:ListItem value="5:00 a.m."> 5:00 a.m. </asp:ListItem>
                    <asp:ListItem value="6:00 a.m."> 6:00 a.m. </asp:ListItem>
                    <asp:ListItem value="7:00 a.m."> 7:00 a.m. </asp:ListItem>
                    <asp:ListItem value="8:00 a.m."> 8:00 a.m. </asp:ListItem>
                    <asp:ListItem value="9:00 a.m."> 9:00 a.m. </asp:ListItem>
                    <asp:ListItem value="10:00 a.m."> 10:00 a.m. </asp:ListItem>
                    <asp:ListItem value="11:00 a.m."> 11:00 a.m. </asp:ListItem>
                    <asp:ListItem value="12:00 a.m."> 12:00 p.m. </asp:ListItem>
                    <asp:ListItem value="1:00 p.m."> 1:00 p.m. </asp:ListItem>
                    <asp:ListItem value="2:00 p.m."> 2:00 p.m. </asp:ListItem>
                    <asp:ListItem value="3:00 p.m."> 3:00 p.m. </asp:ListItem>
                    <asp:ListItem value="4:00 p.m."> 4:00 p.m. </asp:ListItem>
                    <asp:ListItem value="5:00 p.m."> 5:00 p.m. </asp:ListItem>
                    <asp:ListItem value="6:00 p.m."> 6:00 p.m. </asp:ListItem>
                    <asp:ListItem value="7:00 p.m."> 7:00 p.m. </asp:ListItem>
                    <asp:ListItem value="8:00 p.m."> 8:00 p.m. </asp:ListItem>
                    <asp:ListItem value="9:00 p.m."> 9:00 p.m. </asp:ListItem>
                    <asp:ListItem value="10:00 p.m."> 10:00 p.m. </asp:ListItem>
                    <asp:ListItem value="11:00 p.m."> 11:00 p.m. </asp:ListItem>
                    <asp:ListItem value="12:00 a.m."> 12:00 a.m. </asp:ListItem>
                </asp:DropDownList>
                </div>
            </td>

            <td class="auto-style16">
                </td>

        </tr>
        <tr>
            <td class="auto-style17">
                &nbsp;</td>
            <td class="auto-style4" colspan="2">
                <div style="margin-top:5px;">
                <asp:Button ID="FlightSearchButton" runat="server" Text="Search" CssClass="roundInputs" OnClick="FlightSearch_Click" />
                <asp:Button ID="TrainSearchButton" runat="server" Text="Search" CssClass="roundInputs" OnClick="TrainSearch_Click" />
                    <br />
                </div>
            </td>
            <td class="auto-style15">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style17">
                &nbsp;</td>
            <td class="auto-style4" colspan="2">
                &nbsp;</td>
            <td class="auto-style15">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style17">
                &nbsp;</td>
            <td class="auto-style4" colspan="2">
                &nbsp;</td>
            <td class="auto-style15">
                &nbsp;</td>
        </tr>
        </table>
</div>
</asp:Content>

