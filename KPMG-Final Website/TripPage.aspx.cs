using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Drawing;
using System.Configuration;
using System.IO;

public partial class TripPage : System.Web.UI.Page
{
    const Double KorWonRate = 0.00092;
    public System.Data.SqlClient.SqlDataReader reader;

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (((string[])Session["UserIdAndAcctType"])[1].Equals("S"))
        {
            ((Button)Master.FindControl("NewBookingButton")).Visible = false;
            ((Button)Master.FindControl("BookingsButton")).Visible = false;
            ((Button)Master.FindControl("AccountButton")).Visible = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        getWeatherData(Convert.ToString(Session["DestinationForWeather"]));
        Double Total = 0;
        try
        {
            if (Session["FlightBookingList"] != null)
            {
                FlightSummaryGridView.DataSource = (List<FlightClass>)Session["FlightBookingList"];
                FlightSummaryGridView.DataBind();
            }
            if (Session["CarBookingList"] != null)
            {
                CarSummaryGridView.DataSource = (List<CarClass>)Session["CarBookingList"];
                CarSummaryGridView.DataBind();
            }
            if (Session["HotelBookingList"] != null)
            {
                HotelSummaryGridView.DataSource = (List<HotelClass>)Session["HotelBookingList"];
                HotelSummaryGridView.DataBind();
            }
            if (Session["TrainBookingList"] != null)
            {
                TrainSummaryGridView.DataSource = (List<TrainClass>)Session["TrainBookingList"];
                TrainSummaryGridView.DataBind();
            }

            foreach (FlightClass fc in (List<FlightClass>)Session["FlightBookingList"])
            {
                String code = (fc.totalprice.Substring(0, 2));
                if (code == "KRW")
                {
                    Double tempMoney = Convert.ToDouble(fc.totalprice.Substring(3));
                    Total += (tempMoney * KorWonRate);
                }
                else
                {
                    String temp = (fc.totalprice.Substring(3));
                    Total += Convert.ToDouble(temp);
                }
            }
            foreach (TrainClass tc in (List<TrainClass>)Session["TrainBookingList"])
            {
                Total += Convert.ToDouble(tc.Cost);
            }
            foreach (HotelClass hc in (List<HotelClass>)Session["HotelBookingList"])
            {
                Total += Convert.ToDouble(hc.TotalPrice);
            }
            foreach (CarClass cc in (List<CarClass>)Session["CarBookingList"])
            {
                Total += Convert.ToDouble(cc.TotalPrice);
            }

            TripTotal.Text = Convert.ToString(Total);

            if (Total > 1200.00)
            {
                lblOutPolicy.Visible = true;
                DDLReason.Visible = true;
            }
        }
        catch (Exception b)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error Connecting to Database.');", true);
        }
    }
    protected void MyAccount_Click(object sender, EventArgs e)
    {

    }
    protected void BookButton_Click(object sender, EventArgs e)
    {
        try
        {                      

            string reservedby = ((string[])Session["UserIdAndAcctType"])[0];
            //Things related for the SQL Insert statements are here.
            System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
            sc.ConnectionString = @"Data Source=pkyqlbhc9z.database.windows.net;Initial Catalog=KPMGTravel;Persist Security Info=True;User ID=episcopd;Password=Showker93;";

            sc.Open();

            System.Data.SqlClient.SqlCommand insert = new System.Data.SqlClient.SqlCommand();
            System.Data.SqlClient.SqlCommand grab = new System.Data.SqlClient.SqlCommand();
            grab.Connection = sc;

            insert.Connection = sc;
            Random r = new Random();
            Double TripTotalDB = Convert.ToDouble(TripTotal.Text);
            insert.CommandText = "";
            insert.CommandText = @"INSERT INTO Trip (Trip_Name, Trip_Expense, UserID) VALUES (@Tripname, @TripTotal, @UserID);";
            //insert.Parameters.AddWithValue("@TripID", tid);
            insert.Parameters.AddWithValue("@Tripname", txtTripName.Text);
            insert.Parameters.AddWithValue("@TripTotal", TripTotalDB);
            insert.Parameters.AddWithValue("@UserID", ((string[])Session["ActiveUserIdAndAcctType"])[0]);
            insert.ExecuteNonQuery();
            grab.CommandText = "";
            grab.CommandText = @"select max(TripID) from Trip";
            long tid = (Int64)grab.ExecuteScalar();
            if (Session["FlightBookingList"] != null)
            {
                int i = 0;
                foreach (FlightClass fc in (List<FlightClass>)Session["FlightBookingList"])
                {
                    Decimal PurchaseAmountDB = Convert.ToDecimal(fc.totalprice.Substring(3));
                    //long ReservedByDB = Convert.ToInt64(Session["ActiveUserIdAndAcctType"]);
                    //        DateTime ResvDateDBTemp = DateTime.Now;
                    //String ReservationDateDB = ResvDateDBTemp.ToShortDateString();
                    //        DateTime StartDateDBTemp = Session["DepartDate"];
                    //String StartDateDB = StartDateDBTemp.ToShortDateString();
                    //        DateTime EndDateDBTemp = DateTime.Now;
                    //String EndDateDB = EndDateDBTemp.ToShortDateString();                 
                    //String ReservationTypeDB = "type";
                    //int StudentBookingDB = 2;
                    insert.CommandText = "";
                    if (i == 0)
                    {
                        insert.CommandText = @"INSERT INTO Reservation (TripID, PurchaseAmount, ReservedBy) VALUES (@tid, @PurchaseAmountDB, @reservedby);";
                        insert.Parameters.AddWithValue("@tid", tid);
                        insert.Parameters.AddWithValue("@PurchaseAmountDB", PurchaseAmountDB);
                        insert.Parameters.AddWithValue("@reservedby", reservedby);
                    }
                    else
                    {
                        insert.CommandText = @"INSERT INTO Reservation (TripID, PurchaseAmount, ReservedBy) VALUES (@tid232, @PurchaseAmountDB232, @reservedby232);";
                        insert.Parameters.AddWithValue("@tid232", tid);
                        insert.Parameters.AddWithValue("@PurchaseAmountDB232", PurchaseAmountDB);
                        insert.Parameters.AddWithValue("@reservedby232", reservedby);
                    }
                    insert.ExecuteNonQuery();
                    grab.CommandText = "";
                    grab.CommandText = @"select max(ReservationID) from Reservation";
                    long rid = (Int64)grab.ExecuteScalar();
                    insert.CommandText = "";
                    if (i == 0)
                    {
                        insert.CommandText = @"INSERT INTO Airline_Booking (aReservationID, Airline, FromAirport, ToAirport, Deptime, Seat_class, layovercount) VALUES (@aReservationID, @flightcarrier, @dairport, @arrivalaiport, @Deptime, @cabtype, @layovercount);";
                        insert.Parameters.AddWithValue("@aReservationID", rid);
                        insert.Parameters.AddWithValue("@flightcarrier", fc.flightcarrier);
                        insert.Parameters.AddWithValue("@dairport", fc.departureairport);
                        insert.Parameters.AddWithValue("@arrivalaiport", fc.arrivalairport);
                        insert.Parameters.AddWithValue("@Deptime", fc.DDate);
                        insert.Parameters.AddWithValue("@cabtype", fc.cabintype);
                        insert.Parameters.AddWithValue("@layovercount", fc.layovercount);
                    }
                    else
                    {
                        insert.CommandText = @"INSERT INTO Airline_Booking (aReservationID, Airline, FromAirport, ToAirport, Deptime, Seat_class, layovercount) VALUES (@aReservationID1, @flightcarrier1, @dairport1, @arrivalaiport1, @Deptime1, @cabtype1, @layovercount1);";
                        insert.Parameters.AddWithValue("@aReservationID1", rid);
                        insert.Parameters.AddWithValue("@flightcarrier1", fc.flightcarrier);
                        insert.Parameters.AddWithValue("@dairport1", fc.departureairport);
                        insert.Parameters.AddWithValue("@arrivalaiport1", fc.arrivalairport);
                        insert.Parameters.AddWithValue("@Deptime1", fc.DDate);
                        insert.Parameters.AddWithValue("@cabtype1", fc.cabintype);
                        insert.Parameters.AddWithValue("@layovercount1", fc.layovercount);
                    }
                    insert.ExecuteNonQuery();
                    //INSERT INTO Airline_Booking VALUES (aReservationID[BIGINT]PKFK'2000', Airline[VARCHAR](30)'United', FromAirport[VARCHAR](30)'IAD', ToAirport [VARCHAR](30)'JFK', 
                    //Deptime[SMALLDATETIME]'01-20-2015 15:30:00',Arrivetime[SMALLDATETIME]'01-24-2015 18:04:00', Seat_class[VARCHAR](15)'Economy', SeatNo[VARCHAR](10)'E5')
                    i++;
                }
                insert.Parameters.Clear();
            }
            if (Session["CarBookingList"] != null)
            {
                foreach (CarClass cc in (List<CarClass>)Session["CarBookingList"])
                {
                    Decimal PurchaseAmountDB = Convert.ToDecimal(cc.TotalPrice);
                    insert.CommandText = "";
                    insert.CommandText = @"INSERT INTO Reservation (TripID, PurchaseAmount, ReservedBy) VALUES (@tid1, @PurchaseAmountDB1, @reservedby1);";
                    insert.Parameters.AddWithValue("@tid1", tid);
                    insert.Parameters.AddWithValue("@PurchaseAmountDB1", PurchaseAmountDB);
                    insert.Parameters.AddWithValue("@reservedby1", reservedby);
                    insert.ExecuteNonQuery();
                    grab.CommandText = "";
                    grab.CommandText = @"select max(ReservationID) from Reservation";
                    long rid = (Int64)grab.ExecuteScalar();
                    insert.CommandText = "";
                    insert.CommandText = @"INSERT INTO Car_Booking (cReservationID, RentalVendor, VehicleClass, PickupDate, DropOffDate, TransmissionType) VALUES (@cReservationID, @CarRentalVendor, @CarType, @pickdate, @dropdate, @ttype);";
                    insert.Parameters.AddWithValue("@cReservationID", rid);
                    insert.Parameters.AddWithValue("@CarRentalVendor", cc.CarRentalCompany);
                    insert.Parameters.AddWithValue("@CarType", cc.CarType);
                    insert.Parameters.AddWithValue("@pickdate", cc.FullPickUpDateTime);
                    insert.Parameters.AddWithValue("@dropdate", cc.FullDropOffTime);
                    insert.Parameters.AddWithValue("@ttype", cc.transmissiontype);
                    insert.ExecuteNonQuery();
                }
                insert.Parameters.Clear();
            }
            if (Session["HotelBookingList"] != null)
            {
                foreach (HotelClass hc in (List<HotelClass>)Session["HotelBookingList"])
                {
                    Decimal PurchaseAmountDB = Convert.ToDecimal(hc.TotalPrice);
                    insert.CommandText = "";
                    insert.CommandText = @"INSERT INTO Reservation (TripID, PurchaseAmount,ReservedBy) VALUES (@tid2,@PurchaseAmountDB2,@reservedby2);";
                    insert.Parameters.AddWithValue("@tid2", tid);
                    insert.Parameters.AddWithValue("@PurchaseAmountDB2", PurchaseAmountDB);
                    insert.Parameters.AddWithValue("@reservedby2", reservedby);
                    insert.ExecuteNonQuery();
                    grab.CommandText = "";
                    grab.CommandText = @"select max(ReservationID) from Reservation;";
                    long rid = (Int64)grab.ExecuteScalar();
                    insert.CommandText = "";
                    insert.CommandText = @"INSERT INTO Hotel_Booking (hReservationID, HotelName, CheckIn, CheckOut) VALUES (@hReservationID, @HotelName, @checkindate, @checkoutdate);";
                    insert.Parameters.AddWithValue("@hReservationID", rid);
                    insert.Parameters.AddWithValue("@HotelName", hc.HotelName);
                    insert.Parameters.AddWithValue("@checkindate", hc.CheckInDate);
                    insert.Parameters.AddWithValue("@checkoutdate", hc.CheckOutDate);
                    insert.ExecuteNonQuery();
                }
                insert.Parameters.Clear();
            }
            if (Session["TrainBookingList"] != null)
            {
                int i = 0;
                foreach (TrainClass tc in (List<TrainClass>)Session["TrainBookingList"])
                {
                    Decimal PurchaseAmountDB = Convert.ToDecimal(tc.Cost);
                    insert.CommandText = "";
                    if (i == 0)
                    {
                        insert.CommandText = @"INSERT INTO Reservation (TripID, PurchaseAmount, ReservedBy) VALUES (@tid3,@PurchaseAmountDB3,@reservedby3);";
                        insert.Parameters.AddWithValue("@tid3", tid);
                        insert.Parameters.AddWithValue("@PurchaseAmountDB3", PurchaseAmountDB);
                        insert.Parameters.AddWithValue("@reservedby3", reservedby);
                    }
                    else
                    {
                        insert.CommandText = @"INSERT INTO Reservation (TripID, PurchaseAmount, ReservedBy) VALUES (@tid14,@PurchaseAmountDB14,@reservedby14);";
                        insert.Parameters.AddWithValue("@tid14", tid);
                        insert.Parameters.AddWithValue("@PurchaseAmountDB14", PurchaseAmountDB);
                        insert.Parameters.AddWithValue("@reservedby14", reservedby);
                    }
                    insert.ExecuteNonQuery();
                    grab.CommandText = "";
                    grab.CommandText = @"select max(ReservationID) from Reservation";
                    long rid = (Int64)grab.ExecuteScalar();
                    insert.CommandText = "";
                    if (i == 0)
                    {
                        insert.CommandText = @"INSERT INTO Train_Booking (tReservationID) VALUES (@tReservationID);";
                        insert.Parameters.AddWithValue("@tReservationID", rid);
                    }
                    else
                    {
                        insert.CommandText = @"INSERT INTO Train_Booking (tReservationID) VALUES (@tReservationID2);";
                        insert.Parameters.AddWithValue("@tReservationID2", rid);
                    }
                    insert.ExecuteNonQuery();
                    i++;
                }
            }
            sc.Close();
            SendEmailConfirmation(null, null);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Your reservations have been booked.');", true);
        }
        catch (Exception f)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error Connecting to Database.');", true);
        }
    }

    private void SendEmailConfirmation(object sender, EventArgs e)
    {
        try
        {
            using (System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage("kpmgsystemuser@gmail.com", "bakeres@dukes.jmu.edu"))
            {
                mm.Subject = "Your Reservations Have Been Booked!";
                string body = "Hello,";
                body += "<br /><br />Just a quick note to let you know that your reservations have been booked.";
                body += "<br />Thank you for using the KPMG Reservation System for your traveling needs.";
                body += "<br /><br />Thanks!";
                body += "<br />The KPMG Reservation System Team.";
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                System.Net.NetworkCredential NetworkCred = new NetworkCredential("kpmgsystemuser@gmail.com", "kpmgcis484");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
        }
        catch (Exception k)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error Sending Email Confirmation.');", true);
        }
    }

    public void getWeatherData(String City)
    {
        try
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
        catch (Exception e)
        {

        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

        /* Verifies that the control is rendered */

    }
    protected void FlightSummaryGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    FlightSummaryGridView.PageIndex = e.NewPageIndex;
    }
    protected void ExportToExcel_Click(object sender, EventArgs e)
    {
        string filepath = "C:\\Users\\Public\\Desktop\\gridview.xls";
        FileInfo FI = new FileInfo(filepath);
        StringWriter stringWriter = new StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWriter);
        DataGrid DataGrd = new DataGrid();
        DataGrd.DataSource = (List<FlightClass>)Session["FlightBookingList"];
        DataGrd.DataBind();  
        DataGrd.RenderControl(htmlWrite);
        string directory = filepath.Substring(0, filepath.LastIndexOf("\\"));// GetDirectory(Path);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        System.IO.StreamWriter vw = new System.IO.StreamWriter(filepath, true);
        stringWriter.ToString().Normalize();
        vw.Write(stringWriter.ToString());
        vw.Flush();
        vw.Close();
        WriteAttachment(FI.Name, "application/vnd.ms-excel", stringWriter.ToString());
    }
    public static void WriteAttachment(string FileName, string FileType, string content)
    {
        HttpResponse Response = System.Web.HttpContext.Current.Response;
        Response.ClearHeaders();
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
        Response.ContentType = FileType;
        Response.Write(content);
        Response.End();
    }
}