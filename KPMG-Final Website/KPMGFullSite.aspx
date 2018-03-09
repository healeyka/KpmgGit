<%@ Page Title="KPMG Travel Reservation Site" Language="C#" MasterPageFile="~/KPMG.master" AutoEventWireup="true" CodeFile="KPMGFullSite.aspx.cs" Inherits="KPMGFullSite" %>
<%@ MasterType virtualpath="~/KPMG.master" %>
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
<script type="text/javascript">
    function Validate() {

        /* KPMGFullSite.SearchAirportsButton_Click(); */       
        document.getElementById('<%= SearchAirportsButton.ClientID %>').click();       
    }

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
                    

      #map-canvas {
        height: 100%;
        margin: 0px;
        padding: 0px
      }


        .auto-style24 {
        cursor: pointer;
        -moz-border-radius: 5px;
        -webkit-border-radius: 5px;
        border-radius: 5px;
        font-size: medium;
        border: 1px solid #a99f9a;
        margin-right: 6px;
        padding: 3px 8px;
        background-color: #e3e3e3;
    }
    .auto-style27 {
        width: 70px;
    }
    .auto-style28 {
        width: 45px;
    }
    .auto-style29 {
    }
    .auto-style32 {
        width: 848px;
    }
    .auto-style33 {
        width: 50px;
    }
    .auto-style39 {
        width: 45px;
        height: 25px;
    }
    .auto-style42 {
        width: 70px;
        height: 25px;
    }
    .auto-style43 {
        width: 50px;
        height: 25px;
    }
    .auto-style47 {
        width: 210px;
    }
    .auto-style48 {
        height: 25px;
        width: 210px;
    }
    .auto-style49 {
        width: 45px;
        height: 22px;
    }
    .auto-style50 {
        width: 210px;
        height: 22px;
    }
    .auto-style51 {
        width: 70px;
        height: 22px;
    }
    .auto-style52 {
        height: 22px;
    }
    .auto-style53 {
        width: 848px;
        height: 22px;
    }
    .auto-style54 {
        width: 50px;
        height: 22px;
    }
    .auto-style55 {
        width: 45px;
        height: 20px;
    }
    .auto-style56 {
        width: 210px;
        height: 20px;
    }
    .auto-style57 {
        width: 70px;
        height: 20px;
    }
    .auto-style58 {
        width: 50px;
        height: 20px;
    }
    </style>

<div style="margin-top: 10px; margin-left:auto; margin-right:auto; width: 80%; background-color: #F7F7F7;">
    <br />
    <h2 style="margin-top:10px; margin-left:10px; font-weight:bold;">Travel Reservation Search&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Radiobutton ID="FlightSearch" Font-size="Small" GroupName="travel" name="Radio" runat="server" CssClass="roundInputs" Text="Flight Search" Height="25px" Width="125px" BackColor="#4E4A45" ForeColor="#E4E4E4" />
                <asp:Radiobutton ID="TrainSearch" Font-size="Small" GroupName="travel" name="Radio" runat="server" CssClass="roundInputs" Text="Train Search" Height="25px" Width="125px" BackColor="#4E4A45" ForeColor="#E4E4E4" />     
                <asp:Checkbox ID="HotelSearch" Font-size="Small" runat="server" CssClass="roundInputs" Text="Hotel Search" Height="25px" Width="125px" BackColor="#4E4A45" ForeColor="#E4E4E4" />
                <asp:Checkbox ID="CarSearch" Font-size="Small" runat="server" CssClass="roundInputs" Text="Car Search" Height="25px" Width="125px" BackColor="#4E4A45" ForeColor="#E4E4E4" />
    </h2>

                <hr style="margin-top:4px; margin-bottom: 8px;" />

        <table class="auto-style60">
            <tr>
                <td class="auto-style28">&nbsp;</td>
                <td class="auto-style29" colspan="2">                           
                <asp:Radiobutton ID="OneWayButton" runat="server" CssClass="roundInputs" GroupName="2" Text="One-Way" Height="25px" Width="125px" AutoPostBack="true" OnChecked="OnewayClick" BackColor="Silver" />
                <asp:Radiobutton ID="RoundTripButton" runat="server" CssClass="roundInputs" GroupName="2" Text="Round-Trip" Height="25px" Width="125px" AutoPostBack="true" OnChecked="RoundTripClick" BackColor="Silver" />
                    </td>
                <td class="auto-style27">&nbsp;</td>
                <td colspan="2" rowspan="9">

            <!--    GRIDVIEW GOES HERE     -->
                    <asp:Panel ID="Panel1" runat="server" BorderColor="#223C72" BorderStyle="Double" Height="402px" Width="100%" BackColor="Silver">
                        <asp:Label ID="ResultsLabel" runat="server" style="font-size: large" Text="Search Results:"></asp:Label>
                        <div style="width: 100%; height: 380px; overflow: scroll">
                            <asp:GridView ID="CustomersGridView" runat="server" AutoGenerateColumns="true" AutoGenerateSelectButton="true" 
                                BackColor="Silver" Height="380px" style="color: #333333; background-color: #E3E3E3" Width="100%" OnSelectedIndexChanging="CustomersGridView_SelectedIndexChanged">
                            </asp:GridView>
                        </div>
                    </asp:Panel>
                </td>
                <td class="auto-style33">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style28">&nbsp;</td>
                <td class="auto-style29" colspan="2">                           
                    &nbsp;</td>
                <td class="auto-style27">&nbsp;</td>
                <td class="auto-style33">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style28">&nbsp;</td>
                <td class="auto-style47">
                <asp:TextBox placeholder="Travel From (에서 여행)" name="FromLabel" type="text" CssClass="roundInputs" ID="FromText" runat="server" Height="25px" Width="90%" ></asp:TextBox>
                    <br />
                    <asp:DropDownList ID="DepartureList" runat="server" CssClass="roundInputs" Height="35px" Width="100%" >
                    </asp:DropDownList>
                    <br />
                </td>
                <td class="auto-style47">
                    <asp:TextBox placeholder="Travel To (여행)" ID="ToText" runat="server" CssClass="roundInputs" Height="25px" name="ToText" type="text" Width="90%"></asp:TextBox>
                    <br />
                    <asp:DropDownList ID="ArrivalList" CssClass="roundInputs" runat="server" Height="35px" Width="100%" >
                    </asp:DropDownList>
                    <br />
                </td>
                <td class="auto-style27">&nbsp;</td>
                <td class="auto-style33">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style28">&nbsp;</td>
                <td class="auto-style47">
                    &nbsp;</td>
                <td class="auto-style47">
                    &nbsp;</td>
                <td class="auto-style27">&nbsp;</td>
                <td class="auto-style33">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style28">&nbsp;</td>
                <td class="auto-style47">
                <asp:Label ID="DepartLabel" runat="server" Text="Depart Date"></asp:Label>

                <asp:Calendar ID="Calendar1" runat="server" BorderWidth="2px" BorderStyle="Outset" 
                    DayNameFormat="FirstLetter" CellPadding="4" ForeColor="Black" Font-Size="8pt" 
                    Font-Names="Verdana" BorderColor="#E4DA85" BackColor="white" Height="50px" Width="100%">
                    <DayHeaderStyle Font-Size="7pt" Font-Bold="True" BackColor="#E9E19A"></DayHeaderStyle>
                    <SelectedDayStyle Font-Bold="True" ForeColor="White" BackColor="#D5D900"></SelectedDayStyle>
                </asp:Calendar>
                </td>
                <td class="auto-style47">
                    <asp:Label ID="ReturnLabel" runat="server" Text="Return Date"></asp:Label>

                    <asp:Calendar ID="Calendar2" runat="server" BackColor="white" BorderColor="#E4DA85" 
                        BorderStyle="Outset" BorderWidth="2px" CellPadding="4" DayNameFormat="FirstLetter" 
                        Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="50px" Width="100%">
                        <DayHeaderStyle BackColor="#E9E19A" Font-Bold="True" Font-Size="7pt" />
                        <SelectedDayStyle BackColor="#D5D900" Font-Bold="True" ForeColor="White" />
                    </asp:Calendar>
                </td>
                <td class="auto-style27">&nbsp;</td>
                <td class="auto-style33">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style28">&nbsp;</td>
                <td class="auto-style47">

                    &nbsp;</td>
                <td class="auto-style47">

                    &nbsp;</td>
                <td class="auto-style27">&nbsp;</td>
                <td class="auto-style33">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style28">&nbsp;</td>
                <td class="auto-style47">

                <asp:Label ID="RoomsLabel" runat="server" Text="Hotel Rooms"></asp:Label>
                <asp:DropDownList ID="NumRooms" runat="server" CssClass="roundInputs" Height="35px" Width="100%" required="required">
                    <asp:ListItem Selected="True">1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                </asp:DropDownList>

                    </td>
                <td class="auto-style47">

                <asp:Label ID="OccupancyLabel" runat="server" Text="Hotel Occupancy"></asp:Label>
                <asp:DropDownList ID="Occupancy" runat="server" CssClass="roundInputs" Height="35px" Width="100%" required="required">
                    <asp:ListItem Selected="True">1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                </asp:DropDownList> 

                    </td>
                <td class="auto-style27">&nbsp;</td>
                <td class="auto-style33">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style39"></td>
                <td class="auto-style48">
                
                                          
                    <asp:Label ID="CarPickupTimeLabel" runat="server" Text="Car Pick-Up Time"></asp:Label>

                <asp:DropDownList ID="PickupTimeDDL" runat="server" CssClass="roundInputs" Height="35px" Width="100%" required="required">
                    <asp:ListItem  selected="True" value="8:00"> Early Morning </asp:ListItem>
                    <asp:ListItem  value="10:00"> Morning </asp:ListItem>
                    <asp:ListItem  value="12:00"> 12:00 Noon </asp:ListItem>
                    <asp:ListItem  value="13:00"> Afternoon </asp:ListItem>
                    <asp:ListItem  value="17:00"> Evening </asp:ListItem>
                    <asp:ListItem  value="19:00"> Late Evening </asp:ListItem>
                    <asp:ListItem  value="23:00"> 12:00 Midnight </asp:ListItem>
                    <asp:ListItem  value=" "> --------------- </asp:ListItem>
                    <asp:ListItem value="1:00"> 1:00 a.m. </asp:ListItem>
                    <asp:ListItem value="2:00"> 2:00 a.m. </asp:ListItem>
                    <asp:ListItem value="3:00"> 3:00 a.m. </asp:ListItem>
                    <asp:ListItem value="4:00"> 4:00 a.m. </asp:ListItem>
                    <asp:ListItem value="5:00"> 5:00 a.m. </asp:ListItem>
                    <asp:ListItem value="6:00"> 6:00 a.m. </asp:ListItem>
                    <asp:ListItem value="7:00"> 7:00 a.m. </asp:ListItem>
                    <asp:ListItem value="8:00"> 8:00 a.m. </asp:ListItem>
                    <asp:ListItem value="9:00"> 9:00 a.m. </asp:ListItem>
                    <asp:ListItem value="10:00"> 10:00 a.m. </asp:ListItem>
                    <asp:ListItem value="11:00"> 11:00 a.m. </asp:ListItem>
                    <asp:ListItem value="12:00"> 12:00 p.m. </asp:ListItem>
                    <asp:ListItem value="13:00"> 1:00 p.m. </asp:ListItem>
                    <asp:ListItem value="14:00"> 2:00 p.m. </asp:ListItem>
                    <asp:ListItem value="15:00"> 3:00 p.m. </asp:ListItem>
                    <asp:ListItem value="16:00"> 4:00 p.m. </asp:ListItem>
                    <asp:ListItem value="17:00"> 5:00 p.m. </asp:ListItem>
                    <asp:ListItem value="18:00"> 6:00 p.m. </asp:ListItem>
                    <asp:ListItem value="19:00"> 7:00 p.m. </asp:ListItem>
                    <asp:ListItem value="20:00"> 8:00 p.m. </asp:ListItem>
                    <asp:ListItem value="21:00"> 9:00 p.m. </asp:ListItem>
                    <asp:ListItem value="22:00"> 10:00 p.m. </asp:ListItem>
                    <asp:ListItem value="23:00"> 11:00 p.m. </asp:ListItem>
                </asp:DropDownList>
                </td>
                <td class="auto-style48">
                
                                          
                    <asp:Label ID="CarDropoffTimeLabel" runat="server" Text="Car Drop-Off Time"></asp:Label>

                <asp:DropDownList ID="DropoffTimeDDL" runat="server" CssClass="roundInputs" Height="35px" Width="100%" required="required">
                    <asp:ListItem  selected="True" value="8:00"> Early Morning </asp:ListItem>
                    <asp:ListItem  value="10:00"> Morning </asp:ListItem>
                    <asp:ListItem  value="12:00"> 12:00 Noon </asp:ListItem>
                    <asp:ListItem  value="13:00"> Afternoon </asp:ListItem>
                    <asp:ListItem  value="17:00"> Evening </asp:ListItem>
                    <asp:ListItem  value="19:00"> Late Evening </asp:ListItem>
                    <asp:ListItem  value="23:00"> 12:00 Midnight </asp:ListItem>
                    <asp:ListItem  value=" "> --------------- </asp:ListItem>
                    <asp:ListItem value="1:00"> 1:00 a.m. </asp:ListItem>
                    <asp:ListItem value="2:00"> 2:00 a.m. </asp:ListItem>
                    <asp:ListItem value="3:00"> 3:00 a.m. </asp:ListItem>
                    <asp:ListItem value="4:00"> 4:00 a.m. </asp:ListItem>
                    <asp:ListItem value="5:00"> 5:00 a.m. </asp:ListItem>
                    <asp:ListItem value="6:00"> 6:00 a.m. </asp:ListItem>
                    <asp:ListItem value="7:00"> 7:00 a.m. </asp:ListItem>
                    <asp:ListItem value="8:00"> 8:00 a.m. </asp:ListItem>
                    <asp:ListItem value="9:00"> 9:00 a.m. </asp:ListItem>
                    <asp:ListItem value="10:00"> 10:00 a.m. </asp:ListItem>
                    <asp:ListItem value="11:00"> 11:00 a.m. </asp:ListItem>
                    <asp:ListItem value="12:00"> 12:00 p.m. </asp:ListItem>
                    <asp:ListItem value="13:00"> 1:00 p.m. </asp:ListItem>
                    <asp:ListItem value="14:00"> 2:00 p.m. </asp:ListItem>
                    <asp:ListItem value="15:00"> 3:00 p.m. </asp:ListItem>
                    <asp:ListItem value="16:00"> 4:00 p.m. </asp:ListItem>
                    <asp:ListItem value="17:00"> 5:00 p.m. </asp:ListItem>
                    <asp:ListItem value="18:00"> 6:00 p.m. </asp:ListItem>
                    <asp:ListItem value="19:00"> 7:00 p.m. </asp:ListItem>
                    <asp:ListItem value="20:00"> 8:00 p.m. </asp:ListItem>
                    <asp:ListItem value="21:00"> 9:00 p.m. </asp:ListItem>
                    <asp:ListItem value="22:00"> 10:00 p.m. </asp:ListItem>
                    <asp:ListItem value="23:00"> 11:00 p.m. </asp:ListItem>
                </asp:DropDownList>
                </td>
                <td class="auto-style42"></td>
                <td class="auto-style43"></td>
            </tr>
            <tr>
                <td class="auto-style55"></td>
                <td class="auto-style56">

                                          
                    <asp:Label ID="CarClassLabel" runat="server" Text="Vehicle Class"></asp:Label>

                    <asp:DropDownList ID="CarClassDDL" runat="server" CssClass="roundInputs" Height="35px" required="required" Width="100%">
                        <asp:ListItem>Compact</asp:ListItem>
                        <asp:ListItem>Economy</asp:ListItem>
                        <asp:ListItem>Standard</asp:ListItem>
                        <asp:ListItem>Midsize</asp:ListItem>
                        <asp:ListItem>Full Size</asp:ListItem>
                    </asp:DropDownList>


                    </td>
                <td class="auto-style56">

                                          
                    <asp:Label ID="CarCompanyLabel" runat="server" Text="Car Rental Company"></asp:Label>

                    <br />
                <asp:DropDownList ID="CarCompany" runat="server" CssClass="roundInputs" Height="35px" Width="100%" required="required">
                    <asp:ListItem Selected="True">Enterprise</asp:ListItem>
                    <asp:ListItem>Hertz</asp:ListItem>
                    <asp:ListItem>Avis</asp:ListItem>
                </asp:DropDownList>

                </td>
                <td class="auto-style57"></td>
                <td class="auto-style58"></td>
            </tr>
            <tr>
                <td class="auto-style49"></td>
                <td class="auto-style50">
                
                                          
                    <br />


                <asp:Button ID="MasterSearch" runat="server" Text="Retrieve Results" CssClass="auto-style24" BackColor="#DA7E28" BorderColor="Yellow" Height="57px" Width="147px" OnClick="MasterSearch_Click" />


                    </td>
                <td class="auto-style50">
                
                                          
                    <br />
                <asp:CheckBox ID="SaveSearch" Font-size="Small" GroupName="travel" name="Radio" runat="server" CssClass="roundInputs" Text="Save Search" Height="25px" Width="99px" BackColor="#4E4A45" ForeColor="#E4E4E4" />
                
                                          
                    <br />
                    <asp:Label ID="PastSearchesLabel" runat="server" Text="Past Searches"></asp:Label>
                    <br />
                    <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                
                                          
                    </td>
                <td class="auto-style51">&nbsp;</td>
                <td class="auto-style52"></td>
                <td class="auto-style53">
                    <br />
                </td>
                <td class="auto-style54"></td>
            </tr>
            <tr>
                <td class="auto-style28">&nbsp;</td>
                <td class="auto-style47">

                                          
                    &nbsp;</td>
                <td class="auto-style47">

                    &nbsp;</td>
                <td class="auto-style27">&nbsp;</td>
                <td>&nbsp;</td>
                <td class="auto-style32">&nbsp;</td>
                <td class="auto-style33">
                    <asp:Button ID="SearchAirportsButton" CssClass="roundInputs" runat="server" visible="true" Text="Search Airports" Width="1%" OnClick="SearchAirportsButton_Click" Height="1%"/>
                </td>
            </tr>
            </table>
</div>
</asp:Content>