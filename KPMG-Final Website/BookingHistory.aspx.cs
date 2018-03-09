using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Xml.Linq;
using System.Text; 

public partial class BookingHistory : System.Web.UI.Page
{
    String seoul = "http://api.openweathermap.org/data/2.5/forecast/daily?q=Seoul&mode=xml&units=imperial&cnt=1";
    String richmond = "http://api.openweathermap.org/data/2.5/forecast/daily?q=Richmond,%20VA&mode=xml&units=imperial&cnt=1";
    String harrisionburg = "http://api.openweathermap.org/data/2.5/forecast/daily?q=Harrisonburg,%20VA&mode=xml&units=imperial&cnt=1";
    String weather = "http://www.google.com/ig/api?";

    public System.Data.SqlClient.SqlDataReader reader;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            try
            {
                System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                sc.ConnectionString = @"Data Source=pkyqlbhc9z.database.windows.net;Initial Catalog=KPMGTravel;Persist Security Info=True;User ID=episcopd;Password=Showker93;";

                sc.Open();
                cmd.Connection = sc;
                    
                       
                cmd.CommandText = @"SELECT Airline_Booking.Airline, Convert(varchar, Airline_Booking.Deptime, 101) As DepartTime, Convert(varchar, Airline_Booking.Arrivetime, 101) As Arrivetime, cast(Reservation.PurchaseAmount as decimal(10,2)) As PurchaseAmount, Airline_Booking.Seat_class
                                    FROM Airline_Booking INNER JOIN Reservation ON Airline_Booking.aReservationID = Reservation.ReservationID INNER JOIN Trip ON Reservation.TripID = Trip.TripID
                                    WHERE (Trip.UserID = @UserID) AND (Airline_Booking.Deptime >= DATEADD(d, DATEDIFF(d, 0, GETDATE()), 0))";         
                cmd.Parameters.AddWithValue("@UserID", ((string[])Session["ActiveUserIdAndAcctType"])[0]);
                reader = cmd.ExecuteReader();
                GVCurrentFlights.DataSource = reader;
                GVCurrentFlights.DataBind();
                reader.Close();
                cmd.CommandText = "";
                cmd.CommandText = @"SELECT Airline_Booking.Airline, Convert(varchar, Airline_Booking.Deptime, 101) As DepartTime, Convert(varchar, Airline_Booking.Arrivetime, 101) As Arrivetime, cast(Reservation.PurchaseAmount as decimal(10,2)) As PurchaseAmount, Airline_Booking.Seat_class
                                    FROM Airline_Booking INNER JOIN Reservation ON Airline_Booking.aReservationID = Reservation.ReservationID INNER JOIN Trip ON Reservation.TripID = Trip.TripID
                                    WHERE (Trip.UserID = @UserID) AND (Airline_Booking.Deptime < DATEADD(d, DATEDIFF(d, 0, GETDATE()), 0))";
                reader = cmd.ExecuteReader();
                GVFlightHistory.DataSource = reader;
                GVFlightHistory.DataBind();
                reader.Close();

                cmd.CommandText = @"SELECT cast(Reservation.PurchaseAmount as decimal(10,2)) As PurchaseAmount, Train_Booking.TrainName, Convert(varchar, Train_Booking.DeptDate, 101) As DepartDate, 
                                Convert(varchar, Train_Booking.ArriveDate, 101) As ArriveDate, Train_Booking.SeatNo
                                FROM Reservation INNER JOIN Trip ON Reservation.TripID = Trip.TripID INNER JOIN
                                Train_Booking ON Reservation.ReservationID = Train_Booking.tReservationID
                                WHERE (Trip.UserID = @UserID) AND (Train_Booking.DeptDate >= DATEADD(d, DATEDIFF(d, 0, GETDATE()), 0))";
                reader = cmd.ExecuteReader();
                GVCurrentTrains.DataSource = reader;
                GVCurrentTrains.DataBind();
                reader.Close();
                cmd.CommandText = @"SELECT cast(Reservation.PurchaseAmount as decimal(10,2)) As PurchaseAmount, Train_Booking.TrainName, Convert(varchar, Train_Booking.DeptDate, 101) As DepartDate, 
                                Convert(varchar, Train_Booking.ArriveDate, 101) As ArriveDate, Train_Booking.SeatNo
                                FROM Reservation INNER JOIN Trip ON Reservation.TripID = Trip.TripID INNER JOIN
                                Train_Booking ON Reservation.ReservationID = Train_Booking.tReservationID
                                WHERE (Trip.UserID = @UserID) AND (Train_Booking.DeptDate < DATEADD(d, DATEDIFF(d, 0, GETDATE()), 0))";
                reader = cmd.ExecuteReader();
                GVTrainsHistory.DataSource = reader;
                GVTrainsHistory.DataBind();
                reader.Close();

                cmd.CommandText = @"SELECT cast(Reservation.PurchaseAmount as decimal(10,2)) As PurchaseAmount, Hotel_Booking.HotelName, Hotel_Booking.RoomType, Hotel_Booking.RoomRate, Convert(varchar, Hotel_Booking.CheckIn, 101) As CheckinDate, 
                                Convert(varchar, Hotel_Booking.CheckOut, 101) As CheckoutDate, Hotel_Booking.Amenities
                                FROM Reservation INNER JOIN
                                Trip ON Reservation.TripID = Trip.TripID INNER JOIN
                                Hotel_Booking ON Reservation.ReservationID = Hotel_Booking.hReservationID
                                WHERE (Trip.UserID = @userID) AND (Hotel_Booking.CheckOut >=  DATEADD(d, DATEDIFF(d, 0, GETDATE()), 0))";
                reader = cmd.ExecuteReader();
                GVCurrentHotels.DataSource = reader;
                GVCurrentHotels.DataBind();
                reader.Close();
                cmd.CommandText = @"SELECT cast(Reservation.PurchaseAmount as decimal(10,2)) As PurchaseAmount, Hotel_Booking.HotelName, Hotel_Booking.RoomType, Hotel_Booking.RoomRate, Convert(varchar, Hotel_Booking.CheckIn, 101) As CheckinDate, 
                                Convert(varchar, Hotel_Booking.CheckOut, 101) As CheckoutDate, Hotel_Booking.Amenities
                                FROM Reservation INNER JOIN
                                Trip ON Reservation.TripID = Trip.TripID INNER JOIN
                                Hotel_Booking ON Reservation.ReservationID = Hotel_Booking.hReservationID
                                WHERE (Trip.UserID = @userID) AND (Hotel_Booking.CheckOut <  DATEADD(d, DATEDIFF(d, 0, GETDATE()), 0))";
                reader = cmd.ExecuteReader();
                GVHotelHistory.DataSource = reader;
                GVHotelHistory.DataBind();
                reader.Close();

                cmd.CommandText = @"SELECT Car_Booking.RentalVendor, Car_Booking.PickupDate, Car_Booking.DropOffDate, Car_Booking.VehicleClass, Reservation.PurchaseAmount FROM Reservation INNER JOIN Trip ON Reservation.TripID = Trip.TripID INNER JOIN Car_Booking ON Reservation.ReservationID = Car_Booking.cReservationID WHERE (Trip.UserID = @UserID) AND (Car_Booking.DropOffDate >= DATEADD(d, DATEDIFF(d, 0, GETDATE()), 0))";
                reader = cmd.ExecuteReader();
                GVCurrentCars.DataSource = reader;
                GVCurrentCars.DataBind();
                reader.Close();
                cmd.CommandText = @"SELECT Car_Booking.RentalVendor, Car_Booking.PickupDate, Car_Booking.DropOffDate, Car_Booking.VehicleClass, Reservation.PurchaseAmount FROM Reservation INNER JOIN Trip ON Reservation.TripID = Trip.TripID INNER JOIN Car_Booking ON Reservation.ReservationID = Car_Booking.cReservationID WHERE (Trip.UserID = @UserID) AND (Car_Booking.DropOffDate < DATEADD(d, DATEDIFF(d, 0, GETDATE()), 0))";
                reader = cmd.ExecuteReader();
                GVCarHistory.DataSource = reader;
                GVCarHistory.DataBind();
                reader.Close();

                /*cmd.CommandText = @"select Purchase Amount, Reserved By, Airline, FromAirport, Deptime, Seat_class from Airline_Booking INNER JOIN Trip on Airline_Booking.UserID=
                                    WHERE Trip.UserID = @UserID";
                reader = cmd.ExecuteReader();
                GVCurrentFlights.DataSource = reader;
                GVCurrentFlights.DataBind();
                reader.Close();*/

                //BookingHistoryGridView
            }
            catch (Exception)
            {
                //Display Error for not being able to connect to database
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error Connecting to Database.');", true);
            }
        }

    }

    protected void list()
    {
        if (Session["List"] == null)
        {

        }
    }
    protected void Booking_Click(object sender, EventArgs e)
    {
        Response.Redirect("KPMGFullSite.aspx");


        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    Response.ClearContent();
        //    Response.AddHeader("content-disposition", "attachment; filename=gvtoexcel.xls");
        //    Response.ContentType = "application/excel";
        //    System.IO.StringWriter sw = new System.IO.StringWriter();
        //    HtmlTextWriter htw = new HtmlTextWriter(sw);
        //    GVCurrentFlights.RenderControl(htw);
        //    Response.Write(sw.ToString());
        //    Response.End();
        //}
    }

    public void getWeatherData(String City)
    {

        XDocument xd = XDocument.Load("http://api.openweathermap.org/data/2.5/forecast/daily?q=" + City + ",&mode=xml&units=imperial&cnt=1");

        var location = from locationData in xd.Descendants("location") select locationData;

        var forcastInfo = from forecastinfo in xd.Root.Descendants("forecast").DescendantNodes() select forecastinfo;
        var temperature = from tempInfo in xd.Root.Descendants("forecast").Descendants("time") select tempInfo;

        foreach (var item in temperature)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("");
            sb.Append("Forecast for ");

            var nameOne = from tempInfo in xd.Root.Descendants("location").Descendants("name") select tempInfo;
            foreach (var n in nameOne)
            {
                sb.Append(n.Value);
            }
            sb.Append("<i>(DayTemp: " + item.Element("temperature").Attribute("day").Value + ")</i>");
            sb.Append("<i>(MinTemp: " + item.Element("temperature").Attribute("min").Value + ")</i>");
            sb.Append("<i>(MaxTemp:" + item.Element("temperature").Attribute("max").Value + ")</i>");
            sb.Append("<i>(Cloud Cover:" + item.Element("clouds").Attribute("value").Value + ")</i>");
            sb.Append("<i>(Precipitation:" + item.Element("clouds").Attribute("all").Value + "%" + ")</i>");

            Panel6.GroupingText = sb.ToString();
        }
    }

    protected void ShowWeather_Click(object sender, EventArgs e)
    {
        String City = "London";
        getWeatherData(City);
    }
}