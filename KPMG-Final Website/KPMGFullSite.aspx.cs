using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Xsl;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.ComponentModel;
using System.Net;
using System.Web.Script.Serialization;

public partial class KPMGFullSite : System.Web.UI.Page
{
    String RomeApi = "http://free.rome2rio.com/api/1.2/xml/Search?key=RNMpNhbV";
    String HotelHotwireApi = "http://api.hotwire.com/v1/search/hotel?apikey=74ymmyzw96fzwq6vmb9xaftk";
    String CarHotwireApi = "http://api.hotwire.com/v1/search/car?apikey=74ymmyzw96fzwq6vmb9xaftk";
    string[] hotelpreflist = new string[] { "Marriott", "Sheraton", "Hyatt", "Holiday Inn", "Days Inn", "Best Western", "Wyndham", "Choice", "Accor", "Hilton" };
    string[] airlinename = new string[] { "United", "Delta", "Southwest", "American", "Jet Blue" };
    string[] carrentalpreflist = new string[] { "Avis", "Hertz", "Enterprise"};
    string[] strar = new string[]{"1","A"};
    public System.Data.SqlClient.SqlDataReader reader;
    public List<Search> ls = new List<Search>();
    protected void Page_PreRender(object sender, EventArgs e)
    {
        //((string[])Session["UserIdAndAcctType"])[1].Equals("S")
        if (((string[])Session["UserIdAndAcctType"])[1].Equals("S"))
        {
            ((Button)Master.FindControl("NewBookingButton")).Visible = false;
            ((Button)Master.FindControl("BookingsButton")).Visible = false;
            ((Button)Master.FindControl("AccountButton")).Visible = false;
            HotelSearch.Visible = false;
            CarSearch.Visible = false;
            RoomsLabel.Visible = false;
            NumRooms.Visible = false;
            OccupancyLabel.Visible = false;
            Occupancy.Visible = false;
            CarPickupTimeLabel.Visible = false;
            PickupTimeDDL.Visible = false;
            CarDropoffTimeLabel.Visible = false;
            DropoffTimeDDL.Visible = false;
            CarClassLabel.Visible = false;
            CarClassDDL.Visible = false;
            CarCompanyLabel.Visible = false;
            CarCompany.Visible = false;
            FlightSearch.Checked = true;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ToText.Attributes.Add("onblur", "Validate();");
        if (!IsPostBack)
        {         
            DateTime search_date;
            bool Flight_Search;
            bool Train_Search;
            bool Hotel_Search;
            bool Car_Search;
            bool One_Way;
            bool Round_Trip;
            string From_Text;
            string To_Text;
            string From_Airport;
            string To_Airport;
            string Hotel_Rooms;
            string Hotel_Occupancy;
            string CarPickup_Time;
            string CarDropoff_Time;
            string CarClass_DLL;
            string Car_Company;
            System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            System.Data.SqlClient.SqlDataReader reader;
            sc.ConnectionString = @"Data Source=pkyqlbhc9z.database.windows.net;Initial Catalog=KPMGTravel;Persist Security Info=True;User ID=episcopd;Password=Showker93;";
            sc.Open();
            cmd.Connection = sc;
            cmd.CommandText = @"select isnull(SearchDate, ' '), isnull(FlightSearch, 0), isnull(TrainSearch, 0), isnull(HotelSearch, 0), isnull(CarSearch, 0), isnull(OneWay, 0), isnull(RoundTrip, 0), isnull(FromText, ' '), isnull(ToText, ' '), isnull(FromAirport, ' '), isnull(ToAirport, ' '), isnull(HotelRooms, ' '), isnull(HotelOccupancy, ' '), isnull(CarPickupTime, ' '), isnull(CarDropOffTime, ' '), isnull(CarClassDDL, ' '), isnull(CarCompany, ' ') from search where UserID = @UserID";
            cmd.Parameters.AddWithValue("@UserID", ((string[])Session["UserIdAndAcctType"])[0]);
            reader = cmd.ExecuteReader();
            List<DateTime> strlis = new List<DateTime>();
            while (reader.Read())
            {
                search_date = (DateTime)reader.GetValue(0);
                Flight_Search = (bool)reader.GetValue(1);
                Train_Search = (bool)reader.GetValue(2);
                Hotel_Search = (bool)reader.GetValue(3);
                Car_Search = (bool)reader.GetValue(4);
                One_Way = (bool)reader.GetValue(5);
                Round_Trip = (bool)reader.GetValue(6);
                From_Text = (string)reader.GetValue(7);
                To_Text = (string)reader.GetValue(8);
                From_Airport = (string)reader.GetValue(9);
                To_Airport = (string)reader.GetValue(10);
                Hotel_Rooms = (string)reader.GetValue(11);
                Hotel_Occupancy = (string)reader.GetValue(12);
                CarPickup_Time = (string)reader.GetValue(13);
                CarDropoff_Time = (string)reader.GetValue(14);
                CarClass_DLL = (string)reader.GetValue(15);
                Car_Company = (string)reader.GetValue(16);
                ls.Add(new Search(search_date, Flight_Search, Train_Search, Hotel_Search, Car_Search, One_Way, Round_Trip, From_Text, To_Text, From_Airport, To_Airport, Hotel_Rooms, Hotel_Occupancy, CarPickup_Time, CarDropoff_Time, CarClass_DLL, Car_Company));
            }
            Session["ls"] = ls;
            foreach (Search s in ls)
            {
                strlis.Add(s.search_date);
            }
            Session["strlis"] = strlis;
            if (!(strlis.Count == 0))
            {
                DropDownList1.DataSource = (List<DateTime>)Session["strlis"];
                DropDownList1.DataBind();
            }
            OneWayButton.Checked = true;
            FlightSearch.Checked = false;
            TrainSearch.Checked = false;

            DepartureList.Visible = true;
            ArrivalList.Visible = true;
            
           
            ReturnLabel.Visible = true;
            Calendar2.Visible = true;

            Session["PlaneTrip"] = new FlightClass[2];
            Session["TrainTrip"] = new TrainClass[2];

            
            Session["FlightBookingList"] = new List<FlightClass>();
            if (Session["FlightHistoryList"] ==null)
                Session["FlightHistoryList"] = new List<FlightClass>();
            Session["HotelBookingList"] = new List<HotelClass>();
            if (Session["HotelHistoryList"] == null)
                Session["HotelHistoryList"] = new List<HotelClass>();
            Session["CarBookingList"] = new List<CarClass>();
            if (Session["CarHistoryList"] == null)
                Session["CarHistoryList"] = new List<CarClass>();
            Session["TrainBookingList"] = new List<TrainClass>();
            if (Session["TrainHistoryList"] == null)
                Session["TrainHistoryList"] = new List<TrainClass>();
        }
    }

    public void SearchAirportsButton_Click(object sender, EventArgs e)
    {
        try
        {
            HttpWebRequest airportrequest = (HttpWebRequest)System.Net.WebRequest.Create("http://airportcode.riobard.com/search?q=" + FromText.Text + "&fmt=JSON");
            HttpWebResponse termresponse = (HttpWebResponse)airportrequest.GetResponse();
            string termres;
            using (StreamReader termresp = new StreamReader(termresponse.GetResponseStream()))
            {
                termres = termresp.ReadToEnd();
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<RootCode> rcd = new List<RootCode>();
            rcd = jss.Deserialize<List<RootCode>>(termres);
            HttpWebRequest airportarequest = (HttpWebRequest)System.Net.WebRequest.Create("http://airportcode.riobard.com/search?q=" + ToText.Text + "&fmt=JSON");
            HttpWebResponse termaresponse = (HttpWebResponse)airportarequest.GetResponse();
            using (StreamReader termresp = new StreamReader(termaresponse.GetResponseStream()))
            {
                termres = termresp.ReadToEnd();
            }
            JavaScriptSerializer jssa = new JavaScriptSerializer();
            List<RootCode> rca = new List<RootCode>();
            rca = jss.Deserialize<List<RootCode>>(termres);
            Session["rcd"] = rcd;
            Session["rca"] = rca;
            List<string> dalist = new List<string>();
            List<string> aalist = new List<string>();
            foreach (RootCode rc in (List<RootCode>)Session["rcd"])
            {
                dalist.Add(rc.code);
            }
            foreach (RootCode rc in (List<RootCode>)Session["rca"])
            {
                aalist.Add(rc.code);
            }
            Session["dlist"] = dalist;
            Session["alist"] = aalist;
            DepartureList.DataSource = ((List<string>)Session["dlist"]);
            DepartureList.DataBind();
            ArrivalList.DataSource = (List<string>)Session["alist"];
            ArrivalList.DataBind();
        }
        catch (Exception exc)
        {
            String Error = "Unable to find airports for destinations entered";
            ClientScript.RegisterStartupScript(this.GetType(), "Search Failed", "alert('" + Error + "');", true);
        }
    }

    public void FlightSearchMethod()
    {
        try
        {
            HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create("https://www.googleapis.com/qpxExpress/v1/trips/search?key=AIzaSyDXk-Wq5iTx_oTI7i55v2G0q-oWaJXXycU");
            request.ContentType = "application/json";
            request.Method = "POST";
            DateTime DDate = Calendar1.SelectedDate;
            String DepartDate = DDate.ToString("yyyy-MM-dd");
            string countint = Occupancy.SelectedValue;
            string result;
            string json = "{\"request\": {\"passengers\": {\"adultCount\": \"" + countint + "\"},\"slice\": [{\"origin\": \"" + DepartureList.SelectedValue + "\",\"destination\": \"" + ArrivalList.SelectedValue + "\",\"date\": \"" + DepartDate + "\"}],\"solutions\": \"20\"}}";
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader rdr = new StreamReader(response.GetResponseStream()))
            {
                result = rdr.ReadToEnd();
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            RootObject r = new RootObject();
            r = js.Deserialize<RootObject>(result);
            int tripcount = r.trips.tripOption.Count;
            int prefcount = tripcount / 2;
            FlightClass[] p = new FlightClass[tripcount];
            int prefcountacc = 0;
            int rn;
            Random r2 = new Random();
            Random r3 = new Random();
            string flightcarrier;        
            for (int i = 0; i < tripcount; i++)
            {
                if (prefcountacc < prefcount)
                {
                    flightcarrier = (string)Session["AirlineNamePreference"];
                    prefcountacc++;
                }
                else
                {
                    rn = r2.Next(0, 4);
                    flightcarrier = airlinename[rn];
                }
                int layovercount = r.trips.tripOption[i].slice[0].segment.Count-1;
                string[] ll = new string[layovercount];
                for (int j = 0; j < layovercount; j++)
                {
                    ll[j] = r.trips.tripOption[i].slice[0].segment[j].leg[0].destination;
                }
                string airlinecabinpref;
                if (Session["AirlineCabinPreference"] == null)
                    airlinecabinpref = "economy";
                else
                    airlinecabinpref = (string)Session["AirlineCabinPreference"];
                string layoverlist = String.Join(",", ll);
                string totalprice = r.trips.tripOption[i].saleTotal;
                p[i] = new FlightClass(DepartureList.SelectedValue,ArrivalList.SelectedValue,DDate, flightcarrier, airlinecabinpref, layovercount, layoverlist, totalprice);
            }
            Session["FlightResults"] = p;
            CustomersGridView.DataSource = (FlightClass[])Session["FlightResults"];
            CustomersGridView.DataBind();
        }
        catch (Exception exc)
        {
            String Error = "Unable to retrieve flight results";
            ClientScript.RegisterStartupScript(this.GetType(), "Search Failed", "alert('" + Error + "');", true);
        }
    }

    protected void CarSearchMethod()
    {
        try
        {
            Session["CarResults"] = null;
            string dest = ToText.Text;
            DateTime DDate = Calendar1.SelectedDate;
            String DepartDate = DDate.ToString("MM/dd/yyyy");
            DateTime RDate = Calendar2.SelectedDate;
            String ReturnDate = RDate.ToString("MM/dd/yyyy");
            string ptime = PickupTimeDDL.SelectedValue;
            string dtime = DropoffTimeDDL.SelectedValue;
            CarHotwireApi += "&dest=" + dest + "&startdate=" + DepartDate + "&enddate=" + ReturnDate + "&pickuptime=" + ptime + "&dropofftime=" + dtime + "";
            System.Net.WebRequest wrGETURL;
            wrGETURL = System.Net.WebRequest.Create(CarHotwireApi);
            System.IO.Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.Load(objStream);
            System.Xml.XmlNodeList nodelista = xmlDoc.SelectNodes("/Hotwire/Result/CarResult");            
            //System.Xml.XmlNodeList nodelista = xmlDoc.SelectNodes("/Hotwire/Result/CarResult/DailyRate");
            CarClass[] CarArray = new CarClass[nodelista.Count];
            int i = 0;    
            int prefcountacc = 0;
            int prefcount = nodelista.Count / 2;
            foreach (XmlNode xn in nodelista)
            {
                long HWRefNumber = Convert.ToInt64(xn["HWRefNumber"].InnerText);
                Double TotalPrice = Convert.ToDouble(xn["TotalPrice"].InnerText);
                String CarType = xn["CarTypeCode"].InnerText;
                //  ECAR = Economy, FCAR = Full Size, FFAR = Full Size SUV, ICAR = Midsize, 
                //  IFAR = Midsize SUV, MVAR = Minivan, SCAR = Standard, SFAR = Standard SUV,
                //  SPAR = Standard Pickup truck, STAR = Convertible, XXAR = Special car
                if (CarType.Equals("ECAR"))
                    CarType = "Economy";
                if (CarType.Equals("FCAR"))
                    CarType = "Full Size";
                if (CarType.Equals("FFAR"))
                    CarType = "Full Size SUV";
                if (CarType.Equals("ICAR"))
                    CarType = "Midsize";
                if (CarType.Equals("IFAR"))
                    CarType = "Midsize SUV";
                if (CarType.Equals("MVAR"))
                    CarType = "Minivan";
                if (CarType.Equals("SCAR"))
                    CarType = "Standard";
                if (CarType.Equals("SFAR"))
                    CarType = "Standard SUV";
                if (CarType.Equals("SPAR"))
                    CarType = "Standard Pickup truck";
                if (CarType.Equals("STAR"))
                    CarType = "Convertible";
                if (CarType.Equals("SXAR") || CarType.Equals("XXAR"))
                    CarType = "Special Car";
                if (CarType.Equals("PCAR"))
                    CarType = "Premium Car";
                if (CarType.Equals("CCAR"))
                    CarType = "Compact Car";
                Double DailyRate = Convert.ToDouble(xn["DailyRate"].InnerText);
                String DropoffDay = xn["DropoffDay"].InnerText;
                String DropoffTime = xn["DropoffTime"].InnerText;
                DateTime FullDropOffTime = Convert.ToDateTime(DropoffDay + " " + DropoffTime);
                String PickupDay = xn["PickupDay"].InnerText;
                String PickupTime = xn["PickupTime"].InnerText;
                DateTime FullPickUpDateTime = Convert.ToDateTime(PickupDay + " " + PickupTime);
                string transtype;
                if (Session["CarTransmissionPreference"]==null)
                    transtype = "automatic";
                else
                    transtype = (string)Session["CarTransmissionPreference"];
                CarArray[i] = new CarClass(CarCompany.SelectedValue,transtype, CarType, TotalPrice, DailyRate, FullDropOffTime, FullPickUpDateTime);
                i++;
            }
            Session.Add("CarResults", CarArray);
            CarHotwireApi = "http://api.hotwire.com/v1/search/car?apikey=74ymmyzw96fzwq6vmb9xaftk";
            CustomersGridView.DataSource = (CarClass[])Session["CarResults"];
            CustomersGridView.DataBind();
        }
        catch (Exception exc)
        {
            String Error = "Unable to retrieve results for cars";
            ClientScript.RegisterStartupScript(this.GetType(), "Search Failed", "alert('" + Error + "');", true);
        }
    }

    public void HotelSearchMethod()
    {
        try
        {
            String HotelLocation = ToText.Text;
            DateTime DDate = Calendar1.SelectedDate;
            String DepartDate = DDate.ToString("MM/dd/yyyy");
            DateTime RDate = Calendar2.SelectedDate;
            String ReturnDate = RDate.ToString("MM/dd/yyyy");
            String NoRooms = Convert.ToString(NumRooms.SelectedItem.Text);
            String NoPeople = Convert.ToString(Occupancy.SelectedValue);
            Session["HotelResults"] = null;
            HotelHotwireApi += "&dest=" + HotelLocation + "&rooms=" + NoRooms + "&adults=" + NoPeople + "&children=0&startdate=" + DepartDate + "&enddate=" + ReturnDate;
            System.Net.WebRequest wrGETURL;
            wrGETURL = System.Net.WebRequest.Create(HotelHotwireApi);
            System.IO.Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.Load(objStream);
            System.Xml.XmlNodeList nodelista = xmlDoc.SelectNodes("/Hotwire/Result/HotelResult");
            int i = 0;
            int prefcountacc = 0;
            int prefcount = nodelista.Count / 2;
            HotelClass[] HotelArray = new HotelClass[nodelista.Count];
            Random r = new Random();
            int rn;
            foreach (XmlNode xn in nodelista)
            {
                long HWRefNumber = Convert.ToInt64(xn["HWRefNumber"].InnerText);
                Double TotalPrice = Convert.ToDouble(xn["TotalPrice"].InnerText);
                String Inner = Convert.ToString(xn["AmenityCodes"].InnerText);
                int innercount = 0;
                if (!(Inner.Length == 0))
                    innercount = Convert.ToInt16(Inner.Length) / 2;
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < Inner.Length; j++)
                {
                    if (j % 2 == 0 && j != 0)
                        sb.Append(" ");
                    sb.Append(Inner[j]);
                }
                string formatted = sb.ToString();
                String[] Amen = formatted.Split(new Char[] { ' ' });
                for (int k = 0; k < Amen.Length; k++)
                {
                    if (Amen[k].Equals("BC"))
                        Amen[k] = "Business center";
                    else if (Amen[k].Equals("PE"))
                        Amen[k] = "Pet friendly";
                    else if (Amen[k].Equals("CI"))
                        Amen[k] = "Free Internet";
                    else if (Amen[k].Equals("FP"))
                        Amen[k] = "Free Parking";
                    else if (Amen[k].Equals("HS"))
                        Amen[k] = "Internet Access";
                    else if (Amen[k].Equals("FC"))
                        Amen[k] = "Fitness Center";
                    else if (Amen[k].Equals("CB"))
                        Amen[k] = "Free Breakfast";
                    else if (Amen[k].Equals("PO"))
                        Amen[k] = "Pool";
                }
                string amenlist = String.Join(",", Amen);
                String CheckInDate = xn["CheckInDate"].InnerText;
                String CheckOutDate = xn["CheckOutDate"].InnerText;
                Double AveragePricePerNight = Convert.ToDouble(xn["AveragePricePerNight"].InnerText);
                string hotelname;
                if (prefcountacc < prefcount)
                {
                    hotelname = (string)Session["HotelNamePreference"];
                    prefcountacc++;
                }
                else
                {
                    rn = r.Next(0, 9);
                    hotelname = hotelpreflist[rn];
                }
                HotelArray[i] = new HotelClass(hotelname, TotalPrice, amenlist, CheckInDate, CheckOutDate, AveragePricePerNight);
                i++;
            }
            Session.Add("HotelResults", HotelArray);
            HotelHotwireApi = "http://api.hotwire.com/v1/search/hotel?apikey=74ymmyzw96fzwq6vmb9xaftk";
            CustomersGridView.DataSource = (HotelClass[])Session["HotelResults"];
            CustomersGridView.DataBind();
        }
        catch (Exception exc)
        {
            String Error = "Choose new destination for hotel booking";
            ClientScript.RegisterStartupScript(this.GetType(), "Search Failed", "alert('" + Error + "');", true);
        }
    }

    protected void TrainSearchMethod()
    {
        try
        {
            Session["TrainResults"] = null;
            string oName = FromText.Text;
            string dName = ToText.Text;
            RomeApi += "&oName=" + oName + "&dName=" + dName + "";
            System.Net.WebRequest wrGETURL;
            wrGETURL = System.Net.WebRequest.Create(RomeApi);
            System.IO.Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.Load(objStream);
            XmlNamespaceManager mgr = new XmlNamespaceManager(xmlDoc.NameTable);
            mgr.AddNamespace("ns", "http://www.rome2rio.com/api/1.2/xml");
            System.Xml.XmlNode pricenode = xmlDoc.SelectSingleNode("//ns:SearchResponse/ns:Route[@name='Train']/ns:IndicativePrice", mgr);
            System.Xml.XmlNode freqnode = xmlDoc.SelectSingleNode("//ns:SearchResponse/ns:Route[@name='Train']/ns:TransitSegment/ns:TransitItinerary/ns:TransitLeg/ns:TransitHop", mgr);
            System.Xml.XmlNode timenode = xmlDoc.SelectSingleNode("//ns:SearchResponse/ns:Route[@name='Train']", mgr);
            System.Xml.XmlNode carriernode = xmlDoc.SelectSingleNode("//ns:SearchResponse/ns:Route[@name='Train']/ns:TransitSegment/ns:TransitItinerary/ns:TransitLeg/ns:TransitHop/ns:TransitHopAgency", mgr);
            double usd = Convert.ToDouble(pricenode.Attributes[0].Value);
            string carrier = carriernode.Attributes[0].Value;
            string time = timenode.Attributes[2].Value;
            TrainClass[] array = new TrainClass[1];
            string cabintype;
            if (Session["TrainCabinPreference"] == null)
                cabintype = "Coach";
            else
                cabintype = (string)Session["TrainCabinPreference"];
            Random r3 = new Random();
            long HWRefNumber = Convert.ToInt64(r3.Next(0, 1000000000));
            array[0] = new TrainClass(usd, carrier, cabintype, time);
            Session.Add("TrainResults", array);
            RomeApi = "http://free.rome2rio.com/api/1.2/xml/Search?key=RNMpNhbV";
            CustomersGridView.DataSource = (TrainClass[])Session["TrainResults"];
            CustomersGridView.DataBind();
        }
        catch (Exception exc)
        {
            String Error = "The train route to this destination is too long or does not exist";
            ClientScript.RegisterStartupScript(this.GetType(), "Search Failed", "alert('" + Error + "');", true);
        }
    }

    [Serializable]
    class ahop
    {
        public ahop() { }

        public ahop(string dterm, string aterm, string dt, string at, string al, string flight)
        {
            departure = dterm;
            arrival = aterm;
            departuretime = dt;
            arrivaltime = at;
            airline = al;
            flightid = flight;
        }

        public string departure { get; set; }
        public string arrival { get; set; }
        public string departuretime { get; set; }
        public string arrivaltime { get; set; }
        public string airline { get; set; }
        public string flightid { get; set; }

    }

    protected void fromtoswap()
    {
        string temp = "";
        temp = FromText.Text;
        FromText.Text = ToText.Text;
        ToText.Text = temp;
    }

    //protected void OnewayOrRoundClick(object sender, EventArgs e)
    //{
    //    if (OneWayButton.Checked)
    //    {
    //        ReturnLabel.Visible = false;
    //        Calendar2.Visible = false;
    //        ArrivalTimeDDL.Visible = false;
    //    }
    //    if (RoundTripButton.Checked)
    //    {
    //        ReturnLabel.Visible = true;
    //        Calendar2.Visible = true;
    //        ArrivalTimeDDL.Visible = true;
    //    }
    //}

    protected void MyAccount_Click(object sender, EventArgs e)
    {
        Response.Redirect("AccountPage.aspx");
    }

    protected void CustomersGridView_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
    {
        if (((string)Session["stringflag"]).Equals("flighthotelcar"))
        {
            if ((int)Session["intflag"] == 0)
            {
                ((List<FlightClass>)Session["FlightBookingList"]).Add((FlightClass)(((FlightClass[])Session["FlightResults"])[e.NewSelectedIndex]));
                ((List<FlightClass>)Session["FlightHistoryList"]).Add((FlightClass)(((FlightClass[])Session["FlightResults"])[e.NewSelectedIndex]));
                CustomersGridView.DataSource = (HotelClass[])Session["HotelResults"];
                CustomersGridView.DataBind();
                ResultsLabel.Text = "Hotel Results:";
            }
            if (((int)Session["intflag"]) == 1)
            {
                ((List<HotelClass>)Session["HotelBookingList"]).Add((HotelClass)(((HotelClass[])Session["HotelResults"])[e.NewSelectedIndex]));
                ((List<HotelClass>)Session["HotelHistoryList"]).Add((HotelClass)(((HotelClass[])Session["HotelResults"])[e.NewSelectedIndex]));
                CustomersGridView.DataSource = (CarClass[])Session["CarResults"];
                CustomersGridView.DataBind();
                ResultsLabel.Text = "Car Results:";
            }
            if (((int)Session["intflag"]) == 2)
            {
                ((List<CarClass>)Session["CarBookingList"]).Add((CarClass)(((CarClass[])Session["CarResults"])[e.NewSelectedIndex]));
                ((List<CarClass>)Session["CarHistoryList"]).Add((CarClass)(((CarClass[])Session["CarResults"])[e.NewSelectedIndex]));
                Response.Redirect("TripPage.aspx");
            }
            Session["intflag"] = (int)Session["intflag"] + 1;
        }
        if (((string)Session["stringflag"]).Equals("flighthotelcarround"))
        {
            if ((int)Session["intflag"] == 0)
            {
                ((List<FlightClass>)Session["FlightBookingList"]).Add((FlightClass)(((FlightClass[])Session["FlightResults"])[e.NewSelectedIndex]));
                ((List<FlightClass>)Session["FlightHistoryList"]).Add((FlightClass)(((FlightClass[])Session["FlightResults"])[e.NewSelectedIndex]));
                string tempr = ArrivalList.SelectedValue;
                string tempd = DepartureList.SelectedValue;
                DepartureList.DataSource = (List<string>)Session["alist"];
                DepartureList.DataBind();
                ArrivalList.DataSource = (List<string>)Session["dlist"];
                ArrivalList.DataBind();
                DepartureList.SelectedValue = tempr;
                ArrivalList.SelectedValue = tempd;
                FlightSearchMethod();
                ResultsLabel.Text = "Return Flight Results:";
            }
            if ((int)Session["intflag"] == 1)
            {
                ((List<FlightClass>)Session["FlightBookingList"]).Add((FlightClass)(((FlightClass[])Session["FlightResults"])[e.NewSelectedIndex]));
                ((List<FlightClass>)Session["FlightHistoryList"]).Add((FlightClass)(((FlightClass[])Session["FlightResults"])[e.NewSelectedIndex]));
                CustomersGridView.DataSource = (HotelClass[])Session["HotelResults"];
                CustomersGridView.DataBind();
                ResultsLabel.Text = "Hotel Results:";
            }
            if (((int)Session["intflag"]) == 2)
            {
                ((List<HotelClass>)Session["HotelBookingList"]).Add((HotelClass)(((HotelClass[])Session["HotelResults"])[e.NewSelectedIndex]));
                ((List<HotelClass>)Session["HotelHistoryList"]).Add((HotelClass)(((HotelClass[])Session["HotelResults"])[e.NewSelectedIndex]));
                CustomersGridView.DataSource = (CarClass[])Session["CarResults"];
                CustomersGridView.DataBind();
                ResultsLabel.Text = "Car Results:";
            }
            if (((int)Session["intflag"]) == 3)
            {
                ((List<CarClass>)Session["CarBookingList"]).Add((CarClass)(((CarClass[])Session["CarResults"])[e.NewSelectedIndex]));
                ((List<CarClass>)Session["CarHistoryList"]).Add((CarClass)(((CarClass[])Session["CarResults"])[e.NewSelectedIndex]));
                Response.Redirect("TripPage.aspx");
            }
            Session["intflag"] = (int)Session["intflag"] + 1;
        }
        if (((string)Session["stringflag"]).Equals("trainhotelcar"))
        {
            if (((int)Session["intflag"]) == 0)
            {
                ((List<TrainClass>)Session["TrainBookingList"]).Add((TrainClass)(((TrainClass[])Session["TrainResults"])[e.NewSelectedIndex]));
                ((List<TrainClass>)Session["TrainHistoryList"]).Add((TrainClass)(((TrainClass[])Session["TrainResults"])[e.NewSelectedIndex]));
                CustomersGridView.DataSource = (HotelClass[])Session["HotelResults"];
                CustomersGridView.DataBind();
                ResultsLabel.Text = "Hotel Results:";
            }
            if (((int)Session["intflag"]) == 1)
            {
                ((List<HotelClass>)Session["HotelBookingList"]).Add((HotelClass)(((HotelClass[])Session["HotelResults"])[e.NewSelectedIndex]));
                ((List<HotelClass>)Session["HotelHistoryList"]).Add((HotelClass)(((HotelClass[])Session["HotelResults"])[e.NewSelectedIndex]));
                CustomersGridView.DataSource = (CarClass[])Session["CarResults"];
                CustomersGridView.DataBind();
                ResultsLabel.Text = "Car Results:";
            }
            if (((int)Session["intflag"]) == 2)
            {
                ((List<CarClass>)Session["CarBookingList"]).Add((CarClass)(((CarClass[])Session["CarResults"])[e.NewSelectedIndex]));
                ((List<CarClass>)Session["CarHistoryList"]).Add((CarClass)(((CarClass[])Session["CarResults"])[e.NewSelectedIndex]));
                Response.Redirect("TripPage.aspx");
            }
            Session["intflag"] = (int)Session["intflag"] + 1;
        }
        if (((string)Session["stringflag"]).Equals("trainhotelcarround"))
        {
            if (((int)Session["intflag"]) == 0)
            {
                ((List<TrainClass>)Session["TrainBookingList"]).Add((TrainClass)(((TrainClass[])Session["TrainResults"])[e.NewSelectedIndex]));
                ((List<TrainClass>)Session["TrainHistoryList"]).Add((TrainClass)(((TrainClass[])Session["TrainResults"])[e.NewSelectedIndex]));
                fromtoswap();
                TrainSearchMethod();
                CustomersGridView.DataSource = (TrainClass[])Session["TrainResults"];
                CustomersGridView.DataBind();
                ResultsLabel.Text = "Return Train Results:";
            }
            if (((int)Session["intflag"]) == 1)
            {
                ((List<TrainClass>)Session["TrainBookingList"]).Add((TrainClass)(((TrainClass[])Session["TrainResults"])[e.NewSelectedIndex]));
                ((List<TrainClass>)Session["TrainHistoryList"]).Add((TrainClass)(((TrainClass[])Session["TrainResults"])[e.NewSelectedIndex]));
                CustomersGridView.DataSource = (HotelClass[])Session["HotelResults"];
                CustomersGridView.DataBind();
                ResultsLabel.Text = "Hotel Results:";
            }
            if (((int)Session["intflag"]) == 2)
            {
                ((List<HotelClass>)Session["HotelBookingList"]).Add((HotelClass)(((HotelClass[])Session["HotelResults"])[e.NewSelectedIndex]));
                ((List<HotelClass>)Session["HotelHistoryList"]).Add((HotelClass)(((HotelClass[])Session["HotelResults"])[e.NewSelectedIndex]));
                CustomersGridView.DataSource = (CarClass[])Session["CarResults"];
                CustomersGridView.DataBind();
                ResultsLabel.Text = "Car Results:";
            }
            if (((int)Session["intflag"]) == 3)
            {
                ((List<CarClass>)Session["CarBookingList"]).Add((CarClass)(((CarClass[])Session["CarResults"])[e.NewSelectedIndex]));
                ((List<CarClass>)Session["CarHistoryList"]).Add((CarClass)(((CarClass[])Session["CarResults"])[e.NewSelectedIndex]));
                Response.Redirect("TripPage.aspx");
            }
            Session["intflag"] = (int)Session["intflag"] + 1;
        }
        if (((string)Session["stringflag"]).Equals("flighthotel"))
        {
            if (((int)Session["intflag"]) == 0)
            {
                ((List<FlightClass>)Session["FlightBookingList"]).Add((FlightClass)(((FlightClass[])Session["FlightResults"])[e.NewSelectedIndex]));
                ((List<FlightClass>)Session["FlightHistoryList"]).Add((FlightClass)(((FlightClass[])Session["FlightResults"])[e.NewSelectedIndex]));
                CustomersGridView.DataSource = (HotelClass[])Session["HotelResults"];
                CustomersGridView.DataBind();
                ResultsLabel.Text = "Hotel Results:";
            }
            if (((int)Session["intflag"]) == 1)
            {
                ResultsLabel.Text = "Hotel Results:";
                ((List<HotelClass>)Session["HotelBookingList"]).Add((HotelClass)(((HotelClass[])Session["HotelResults"])[e.NewSelectedIndex]));
                ((List<HotelClass>)Session["HotelHistoryList"]).Add((HotelClass)(((HotelClass[])Session["HotelResults"])[e.NewSelectedIndex]));
                Response.Redirect("TripPage.aspx");
            }
            Session["intflag"] = (int)Session["intflag"] + 1;
        }
        if (((string)Session["stringflag"]).Equals("flighthotelround"))
        {
            if (((int)Session["intflag"]) == 0)
            {
                ((List<FlightClass>)Session["FlightBookingList"]).Add((FlightClass)(((FlightClass[])Session["FlightResults"])[e.NewSelectedIndex]));
                ((List<FlightClass>)Session["FlightHistoryList"]).Add((FlightClass)(((FlightClass[])Session["FlightResults"])[e.NewSelectedIndex]));
                string tempr = ArrivalList.SelectedValue;
                string tempd = DepartureList.SelectedValue;
                DepartureList.DataSource = (List<string>)Session["alist"];
                DepartureList.DataBind();
                ArrivalList.DataSource = (List<string>)Session["dlist"];
                ArrivalList.DataBind();
                DepartureList.SelectedValue = tempr;
                ArrivalList.SelectedValue = tempd;
                FlightSearchMethod();
                ResultsLabel.Text = "Return Flight Results:";
            }
            if (((int)Session["intflag"]) == 1)
            {
                ((List<FlightClass>)Session["FlightBookingList"]).Add((FlightClass)(((FlightClass[])Session["FlightResults"])[e.NewSelectedIndex]));
                ((List<FlightClass>)Session["FlightHistoryList"]).Add((FlightClass)(((FlightClass[])Session["FlightResults"])[e.NewSelectedIndex]));
                CustomersGridView.DataSource = (HotelClass[])Session["HotelResults"];
                CustomersGridView.DataBind();
                ResultsLabel.Text = "Hotel Results:";
            }
            if (((int)Session["intflag"]) == 2)
            {
                ((List<HotelClass>)Session["HotelBookingList"]).Add((HotelClass)(((HotelClass[])Session["HotelResults"])[e.NewSelectedIndex]));
                ((List<HotelClass>)Session["HotelHistoryList"]).Add((HotelClass)(((HotelClass[])Session["HotelResults"])[e.NewSelectedIndex]));
                Response.Redirect("TripPage.aspx");
            }
            Session["intflag"] = (int)Session["intflag"] + 1;
        }
        if (((string)Session["stringflag"]).Equals("flightcar"))
        {
            if (((int)Session["intflag"]) == 0)
            {
                ((List<FlightClass>)Session["FlightBookingList"]).Add((FlightClass)(((FlightClass[])Session["FlightResults"])[e.NewSelectedIndex]));
                ((List<FlightClass>)Session["FlightHistoryList"]).Add((FlightClass)(((FlightClass[])Session["FlightResults"])[e.NewSelectedIndex]));
                CustomersGridView.DataSource = (CarClass[])Session["CarResults"];
                CustomersGridView.DataBind();
                ResultsLabel.Text = "Car Results:";
            }
            if (((int)Session["intflag"]) == 1)
            {
                ((List<CarClass>)Session["CarBookingList"]).Add((CarClass)(((CarClass[])Session["CarResults"])[e.NewSelectedIndex]));
                ((List<CarClass>)Session["CarHistoryList"]).Add((CarClass)(((CarClass[])Session["CarResults"])[e.NewSelectedIndex]));
                Response.Redirect("TripPage.aspx");
            }
            Session["intflag"] = (int)Session["intflag"] + 1;
        }
        if (((string)Session["stringflag"]).Equals("flightcarround"))
        {
            if (((int)Session["intflag"]) == 0)
            {
                ((List<FlightClass>)Session["FlightBookingList"]).Add((FlightClass)(((FlightClass[])Session["FlightResults"])[e.NewSelectedIndex]));
                ((List<FlightClass>)Session["FlightHistoryList"]).Add((FlightClass)(((FlightClass[])Session["FlightResults"])[e.NewSelectedIndex]));
                string tempr = ArrivalList.SelectedValue;
                string tempd = DepartureList.SelectedValue;
                DepartureList.DataSource = (List<string>)Session["alist"];
                DepartureList.DataBind();
                ArrivalList.DataSource = (List<string>)Session["dlist"];
                ArrivalList.DataBind();
                DepartureList.SelectedValue = tempr;
                ArrivalList.SelectedValue = tempd;
                FlightSearchMethod();
                ResultsLabel.Text = "Return Flight Results:";
            }
            if (((int)Session["intflag"]) == 1)
            {
                ((List<FlightClass>)Session["FlightBookingList"]).Add((FlightClass)(((FlightClass[])Session["FlightResults"])[e.NewSelectedIndex]));
                ((List<FlightClass>)Session["FlightHistoryList"]).Add((FlightClass)(((FlightClass[])Session["FlightResults"])[e.NewSelectedIndex]));
                CustomersGridView.DataSource = (CarClass[])Session["CarResults"];
                CustomersGridView.DataBind();
                ResultsLabel.Text = "Car Results:";
            }
            if (((int)Session["intflag"]) == 2)
            {
                ((List<CarClass>)Session["CarBookingList"]).Add((CarClass)(((CarClass[])Session["CarResults"])[e.NewSelectedIndex]));
                ((List<CarClass>)Session["CarHistoryList"]).Add((CarClass)(((CarClass[])Session["CarResults"])[e.NewSelectedIndex]));
                Response.Redirect("TripPage.aspx");
            }
            Session["intflag"] = (int)Session["intflag"] + 1;
        }
        if (((string)Session["stringflag"]).Equals("traincar"))
        {
            if (((int)Session["intflag"]) == 0)
            {
                ((List<TrainClass>)Session["TrainBookingList"]).Add((TrainClass)(((TrainClass[])Session["TrainResults"])[e.NewSelectedIndex]));
                ((List<TrainClass>)Session["TrainHistoryList"]).Add((TrainClass)(((TrainClass[])Session["TrainResults"])[e.NewSelectedIndex]));
                CustomersGridView.DataSource = (CarClass[])Session["CarResults"];
                CustomersGridView.DataBind();
                ResultsLabel.Text = "Car Results:";
            }
            if (((int)Session["intflag"]) == 1)
            {
                ((List<CarClass>)Session["CarBookingList"]).Add((CarClass)(((CarClass[])Session["CarResults"])[e.NewSelectedIndex]));
                ((List<CarClass>)Session["CarHistoryList"]).Add((CarClass)(((CarClass[])Session["CarResults"])[e.NewSelectedIndex]));
                Response.Redirect("TripPage.aspx");
            }
            Session["intflag"] = (int)Session["intflag"] + 1;
        }
        if (((string)Session["stringflag"]).Equals("traincarround"))
        {
            if (((int)Session["intflag"]) == 0)
            {
                ((List<TrainClass>)Session["TrainBookingList"]).Add((TrainClass)(((TrainClass[])Session["TrainResults"])[e.NewSelectedIndex]));
                ((List<TrainClass>)Session["TrainHistoryList"]).Add((TrainClass)(((TrainClass[])Session["TrainResults"])[e.NewSelectedIndex]));
                fromtoswap();
                TrainSearchMethod();
                CustomersGridView.DataSource = (TrainClass[])Session["TrainResults"];
                CustomersGridView.DataBind();
                ResultsLabel.Text = "Return Train Results:";
            }
            if (((int)Session["intflag"]) == 1)
            {
                ((List<TrainClass>)Session["TrainBookingList"]).Add((TrainClass)(((TrainClass[])Session["TrainResults"])[e.NewSelectedIndex]));
                ((List<TrainClass>)Session["TrainHistoryList"]).Add((TrainClass)(((TrainClass[])Session["TrainResults"])[e.NewSelectedIndex]));
                CustomersGridView.DataSource = (CarClass[])Session["CarResults"];
                CustomersGridView.DataBind();
                ResultsLabel.Text = "Car Results:";
            }
            if (((int)Session["intflag"]) == 2)
            {
                ((List<CarClass>)Session["CarBookingList"]).Add((CarClass)(((CarClass[])Session["CarResults"])[e.NewSelectedIndex]));
                ((List<CarClass>)Session["CarHistoryList"]).Add((CarClass)(((CarClass[])Session["CarResults"])[e.NewSelectedIndex]));
                Response.Redirect("TripPage.aspx");
            }
            Session["intflag"] = (int)Session["intflag"] + 1;
        }
        if (((string)Session["stringflag"]).Equals("trainhotel"))
        {
            if (((int)Session["intflag"]) == 0)
            {
                ((List<TrainClass>)Session["TrainBookingList"]).Add((TrainClass)(((TrainClass[])Session["TrainResults"])[e.NewSelectedIndex]));
                ((List<TrainClass>)Session["TrainHistoryList"]).Add((TrainClass)(((TrainClass[])Session["TrainResults"])[e.NewSelectedIndex]));
                CustomersGridView.DataSource = (HotelClass[])Session["HotelResults"];
                CustomersGridView.DataBind();
                ResultsLabel.Text = "Hotel Results:";
            }
            if (((int)Session["intflag"]) == 1)
            {
                ((List<HotelClass>)Session["HotelBookingList"]).Add((HotelClass)(((HotelClass[])Session["HotelResults"])[e.NewSelectedIndex]));
                ((List<HotelClass>)Session["HotelHistoryList"]).Add((HotelClass)(((HotelClass[])Session["HotelResults"])[e.NewSelectedIndex]));
                Response.Redirect("TripPage.aspx");
            }
            Session["intflag"] = (int)Session["intflag"] + 1;
        }
        if (((string)Session["stringflag"]).Equals("trainhotelround"))
        {
            if (((int)Session["intflag"]) == 0)
            {
                ((List<TrainClass>)Session["TrainBookingList"]).Add((TrainClass)(((TrainClass[])Session["TrainResults"])[e.NewSelectedIndex]));
                ((List<TrainClass>)Session["TrainHistoryList"]).Add((TrainClass)(((TrainClass[])Session["TrainResults"])[e.NewSelectedIndex]));
                fromtoswap();
                TrainSearchMethod();
                CustomersGridView.DataSource = (TrainClass[])Session["TrainResults"];
                CustomersGridView.DataBind();
                ResultsLabel.Text = "Train Return Results:";
            }
            if (((int)Session["intflag"]) == 1)
            {
                ((List<TrainClass>)Session["TrainBookingList"]).Add((TrainClass)(((TrainClass[])Session["TrainResults"])[e.NewSelectedIndex]));
                ((List<TrainClass>)Session["TrainHistoryList"]).Add((TrainClass)(((TrainClass[])Session["TrainResults"])[e.NewSelectedIndex]));
                CustomersGridView.DataSource = (HotelClass[])Session["HotelResults"];
                CustomersGridView.DataBind();
                ResultsLabel.Text = "Hotel Results:";
            }
            if (((int)Session["intflag"]) == 2)
            {
                ((List<HotelClass>)Session["HotelBookingList"]).Add((HotelClass)(((HotelClass[])Session["HotelResults"])[e.NewSelectedIndex]));
                ((List<HotelClass>)Session["HotelHistoryList"]).Add((HotelClass)(((HotelClass[])Session["HotelResults"])[e.NewSelectedIndex]));
                Response.Redirect("TripPage.aspx");
            }
            Session["intflag"] = (int)Session["intflag"] + 1;
        }
        if (((string)Session["stringflag"]).Equals("train"))
        {
            ResultsLabel.Text = "Train Departure Results:";
            ((List<TrainClass>)Session["TrainBookingList"]).Add((TrainClass)(((TrainClass[])Session["TrainResults"])[e.NewSelectedIndex]));
            ((List<TrainClass>)Session["TrainHistoryList"]).Add((TrainClass)(((TrainClass[])Session["TrainResults"])[e.NewSelectedIndex]));
            Response.Redirect("TripPage.aspx");
        }
        if (((string)Session["stringflag"]).Equals("flight"))
        {
            ResultsLabel.Text = "Flight Departure Results:";
            ((List<FlightClass>)Session["FlightBookingList"]).Add((FlightClass)(((FlightClass[])Session["FlightResults"])[e.NewSelectedIndex]));
            ((List<FlightClass>)Session["FlightHistoryList"]).Add((FlightClass)(((FlightClass[])Session["FlightResults"])[e.NewSelectedIndex]));
            Response.Redirect("TripPage.aspx");
        }
        if (((string)Session["stringflag"]).Equals("car"))
        {
            ResultsLabel.Text = "Car Results:";
            ((List<CarClass>)Session["CarBookingList"]).Add((CarClass)(((CarClass[])Session["CarResults"])[e.NewSelectedIndex]));
            ((List<CarClass>)Session["CarHistoryList"]).Add((CarClass)(((CarClass[])Session["CarResults"])[e.NewSelectedIndex]));
            Response.Redirect("TripPage.aspx");
        }
        if (((string)Session["stringflag"]).Equals("hotel"))
        {
            ResultsLabel.Text = "Hotel Results:";
            ((List<HotelClass>)Session["HotelBookingList"]).Add((HotelClass)(((HotelClass[])Session["HotelResults"])[e.NewSelectedIndex]));
            ((List<HotelClass>)Session["HotelHistoryList"]).Add((HotelClass)(((HotelClass[])Session["HotelResults"])[e.NewSelectedIndex]));
            Response.Redirect("TripPage.aspx");
        }
        if (((string)Session["stringflag"]).Equals("flightround"))
        {
            if (((int)Session["intflag"]) == 0)
            {
                ((List<FlightClass>)Session["FlightBookingList"]).Add((FlightClass)(((FlightClass[])Session["FlightResults"])[e.NewSelectedIndex]));
                ((List<FlightClass>)Session["FlightHistoryList"]).Add((FlightClass)(((FlightClass[])Session["FlightResults"])[e.NewSelectedIndex]));
                string tempr = ArrivalList.SelectedValue;
                string tempd = DepartureList.SelectedValue;
                DepartureList.DataSource = (List<string>)Session["alist"];
                DepartureList.DataBind();
                ArrivalList.DataSource = (List<string>)Session["dlist"];
                ArrivalList.DataBind();
                DepartureList.SelectedValue = tempr;
                ArrivalList.SelectedValue = tempd;
                FlightSearchMethod();
                ResultsLabel.Text = "Return Flight Results:";
            }
            if (((int)Session["intflag"]) == 1)
            {
                ResultsLabel.Text = "Return Flight Results:";
                ((List<FlightClass>)Session["FlightBookingList"]).Add((FlightClass)(((FlightClass[])Session["FlightResults"])[e.NewSelectedIndex]));
                ((List<FlightClass>)Session["FlightHistoryList"]).Add((FlightClass)(((FlightClass[])Session["FlightResults"])[e.NewSelectedIndex]));
                Response.Redirect("TripPage.aspx");
            }           
            Session["intflag"] = (int)Session["intflag"] + 1;
        }
        if (((string)Session["stringflag"]).Equals("trainround"))
        {
            if (((int)Session["intflag"]) == 0)
            {
                ((List<TrainClass>)Session["TrainBookingList"]).Add((TrainClass)(((TrainClass[])Session["TrainResults"])[e.NewSelectedIndex]));
                ((List<TrainClass>)Session["TrainHistoryList"]).Add((TrainClass)(((TrainClass[])Session["TrainResults"])[e.NewSelectedIndex]));
                fromtoswap();
                TrainSearchMethod();
                CustomersGridView.DataSource = (TrainClass[])Session["TrainResults"];
                CustomersGridView.DataBind();
                ResultsLabel.Text = "Train Return Results:";
            }
            if (((int)Session["intflag"]) == 1)
            {
                ResultsLabel.Text = "Train Return Results:";
                ((List<TrainClass>)Session["TrainBookingList"]).Add((TrainClass)(((TrainClass[])Session["TrainResults"])[e.NewSelectedIndex]));
                ((List<TrainClass>)Session["TrainHistoryList"]).Add((TrainClass)(((TrainClass[])Session["TrainResults"])[e.NewSelectedIndex]));
                Response.Redirect("TripPage.aspx");
            }
            Session["intflag"] = (int)Session["intflag"] + 1;
        }
    }

    protected void MasterSearch_Click(object sender, EventArgs e)
    {
        Session["DestinationForWeather"] = ToText.Text;
        if (FlightSearch.Checked && (ArrivalList.SelectedIndex == -1 || DepartureList.SelectedIndex == -1))
        {
            String Error = "Search for Departure and Destination airports first";
            ClientScript.RegisterStartupScript(this.GetType(), "Search Failed", "alert('" + Error + "');", true);
            return;
        }
        if (SaveSearch.Checked)
        {
            DateTime dt = System.DateTime.Now;
            dt = dt.AddTicks(-dt.Ticks % 10000000);
            System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
            sc.ConnectionString = @"Data Source=pkyqlbhc9z.database.windows.net;Initial Catalog=KPMGTravel;Persist Security Info=True;User ID=episcopd;Password=Showker93;";
            System.Data.SqlClient.SqlCommand insert = new System.Data.SqlClient.SqlCommand();
            sc.Open();
            insert.Connection = sc;
            insert.CommandText = @"Insert into Search (UserID, SearchDate, FlightSearch,TrainSearch,HotelSearch,CarSearch,OneWay,RoundTrip,FromText,ToText,FromAirport,ToAirport,HotelRooms,HotelOccupancy,CarPickUpTime,CarDropOffTime,CarClassDDL,CarCompany) Values (@UserID,@SearchDateTime,@Flight,@Train,@Hotel,@Car,@OneWay,@RoundTrip,@FromText,@ToText,@FromAirport,@ToAirport,@HotelRooms,@HotelOccupancy,@CarPickUpTime,@CarDropOffTime,@CarClassDDL,@CarCompany)";
            insert.Parameters.AddWithValue("@UserID", ((string[])Session["UserIdAndAcctType"])[0]);
            insert.Parameters.AddWithValue("@SearchDateTime", dt);
            insert.Parameters.AddWithValue("@Flight", FlightSearch.Checked);
            insert.Parameters.AddWithValue("@Train", TrainSearch.Checked);
            insert.Parameters.AddWithValue("@Hotel", HotelSearch.Checked);
            insert.Parameters.AddWithValue("@Car", CarSearch.Checked);
            insert.Parameters.AddWithValue("@OneWay", OneWayButton.Checked);
            insert.Parameters.AddWithValue("@RoundTrip", RoundTripButton.Checked);
            insert.Parameters.AddWithValue("@FromText", FromText.Text);
            insert.Parameters.AddWithValue("@ToText", ToText.Text);
            insert.Parameters.AddWithValue("@FromAirport", DepartureList.SelectedValue);
            insert.Parameters.AddWithValue("@ToAirport", ArrivalList.SelectedValue);
            //insert.Parameters.AddWithValue("@DepartDate", Calendar1.SelectedDate);
            //insert.Parameters.AddWithValue("@ReturnDate", Calendar2.SelectedDate);
            insert.Parameters.AddWithValue("@HotelRooms", NumRooms.SelectedValue);
            insert.Parameters.AddWithValue("@HotelOccupancy", Occupancy.SelectedValue);
            insert.Parameters.AddWithValue("@CarPickUpTime", PickupTimeDDL.SelectedValue);
            insert.Parameters.AddWithValue("@CarDropOffTime", DropoffTimeDDL.SelectedValue);
            insert.Parameters.AddWithValue("@CarClassDDL", CarClassDDL.SelectedValue);
            insert.Parameters.AddWithValue("@CarCompany", CarCompany.SelectedValue);
            insert.ExecuteNonQuery();
        }
        if (FlightSearch.Checked && HotelSearch.Checked && CarSearch.Checked && !RoundTripButton.Checked)
        {
            Session["stringflag"] = "flighthotelcar";
            Session["intflag"] = 0;
            FlightSearchMethod();
            HotelSearchMethod();
            CarSearchMethod();
            CustomersGridView.DataSource = (FlightClass[])Session["FlightResults"];
            CustomersGridView.DataBind();
            ResultsLabel.Text = "Departure Flight Results:";
        }
        if (FlightSearch.Checked && HotelSearch.Checked && CarSearch.Checked && RoundTripButton.Checked)
        {
            Session["stringflag"] = "flighthotelcarround";
            Session["intflag"] = 0;
            FlightSearchMethod();
            HotelSearchMethod();
            CarSearchMethod();
            CustomersGridView.DataSource = (FlightClass[])Session["FlightResults"];
            CustomersGridView.DataBind();
            ResultsLabel.Text = "Departure Flight Results:";
        }
        if (TrainSearch.Checked && HotelSearch.Checked && CarSearch.Checked && !RoundTripButton.Checked)
        {
            Session["stringflag"] = "trainhotelcar";
            Session["intflag"] = 0;
            TrainSearchMethod();
            HotelSearchMethod();
            CarSearchMethod();
            CustomersGridView.DataSource = (TrainClass[])Session["TrainResults"];
            CustomersGridView.DataBind();
            ResultsLabel.Text = "Departure Train Results:";
        }
        if (TrainSearch.Checked && HotelSearch.Checked && CarSearch.Checked && RoundTripButton.Checked)
        {
            Session["stringflag"] = "trainhotelcarround";
            Session["intflag"] = 0;
            TrainSearchMethod();
            HotelSearchMethod();
            CarSearchMethod();
            CustomersGridView.DataSource = (TrainClass[])Session["TrainResults"];
            CustomersGridView.DataBind();
            ResultsLabel.Text = "Departure Train Results:";
        }
        if (FlightSearch.Checked && HotelSearch.Checked && !CarSearch.Checked && !RoundTripButton.Checked)
        {
            Session["stringflag"] = "flighthotel";
            Session["intflag"] = 0;
            FlightSearchMethod();
            HotelSearchMethod();
            CustomersGridView.DataSource = (FlightClass[])Session["FlightResults"];
            CustomersGridView.DataBind();
            ResultsLabel.Text = "Departure Flight Results:";
        }
        if (FlightSearch.Checked && HotelSearch.Checked && !CarSearch.Checked && RoundTripButton.Checked)
        {
            Session["stringflag"] = "flighthotelround";
            Session["intflag"] = 0;
            FlightSearchMethod();
            HotelSearchMethod();
            CustomersGridView.DataSource = (FlightClass[])Session["FlightResults"];
            CustomersGridView.DataBind();
            ResultsLabel.Text = "Departure Flight Results:";
        }
        if (FlightSearch.Checked && CarSearch.Checked && !HotelSearch.Checked && !RoundTripButton.Checked)
        {
            Session["stringflag"] = "flightcar";
            Session["intflag"] = 0;
            FlightSearchMethod();
            CarSearchMethod();
            CustomersGridView.DataSource = (FlightClass[])Session["FlightResults"];
            CustomersGridView.DataBind();
            ResultsLabel.Text = "Departure Flight Results:";
        }
        if (FlightSearch.Checked && CarSearch.Checked && !HotelSearch.Checked && RoundTripButton.Checked)
        {
            Session["stringflag"] = "flightcarround";
            Session["intflag"] = 0;
            FlightSearchMethod();
            CarSearchMethod();
            CustomersGridView.DataSource = (FlightClass[])Session["FlightResults"];
            CustomersGridView.DataBind();
            ResultsLabel.Text = "Departure Flight Results:";
        }
        if (TrainSearch.Checked && CarSearch.Checked && !HotelSearch.Checked && !RoundTripButton.Checked)
        {
            Session["stringflag"] = "traincar";
            Session["intflag"] = 0;
            TrainSearchMethod();
            CarSearchMethod();
            CustomersGridView.DataSource = (TrainClass[])Session["TrainResults"];
            CustomersGridView.DataBind();
            ResultsLabel.Text = "Departure Train Results:";
        }
        if (TrainSearch.Checked && CarSearch.Checked && !HotelSearch.Checked && RoundTripButton.Checked)
        {
            Session["stringflag"] = "traincarround";
            Session["intflag"] = 0;
            TrainSearchMethod();
            CarSearchMethod();
            CustomersGridView.DataSource = (TrainClass[])Session["TrainResults"];
            CustomersGridView.DataBind();
            ResultsLabel.Text = "Departure Train Results:";
        }
        if (TrainSearch.Checked && HotelSearch.Checked && !CarSearch.Checked && !RoundTripButton.Checked)
        {
            Session["stringflag"] = "trainhotel";
            Session["intflag"] = 0;
            TrainSearchMethod();
            HotelSearchMethod();
            CustomersGridView.DataSource = (TrainClass[])Session["TrainResults"];
            CustomersGridView.DataBind();
            ResultsLabel.Text = "Departure Train Results:";
        }
        if (TrainSearch.Checked && HotelSearch.Checked && !CarSearch.Checked && RoundTripButton.Checked)
        {
            Session["stringflag"] = "trainhotelround";
            Session["intflag"] = 0;
            TrainSearchMethod();
            HotelSearchMethod();
            CustomersGridView.DataSource = (TrainClass[])Session["TrainResults"];
            CustomersGridView.DataBind();
            ResultsLabel.Text = "Departure Train Results:";
        }
        if (TrainSearch.Checked && !CarSearch.Checked && !HotelSearch.Checked && !RoundTripButton.Checked)
        {
            Session["stringflag"] = "train";
            Session["intflag"] = 0;
            TrainSearchMethod();
            CustomersGridView.DataSource = (TrainClass[])Session["TrainResults"];
            CustomersGridView.DataBind();
            ResultsLabel.Text = "Departure Train Results:";
        }
        if (FlightSearch.Checked && !CarSearch.Checked && !HotelSearch.Checked && !RoundTripButton.Checked)
        {
            Session["stringflag"] = "flight";
            Session["intflag"] = 0;
            FlightSearchMethod();
            CustomersGridView.DataSource = (FlightClass[])Session["FlightResults"];
            CustomersGridView.DataBind();
            ResultsLabel.Text = "Departure Flight Results:";
        }
        if (TrainSearch.Checked && !CarSearch.Checked && !HotelSearch.Checked && RoundTripButton.Checked)
        {
            Session["stringflag"] = "trainround";
            Session["intflag"] = 0;
            TrainSearchMethod();
            CustomersGridView.DataSource = (TrainClass[])Session["TrainResults"];
            CustomersGridView.DataBind();
            ResultsLabel.Text = "Departure Train Results:";
        }
        if (FlightSearch.Checked && !CarSearch.Checked && !HotelSearch.Checked && RoundTripButton.Checked)
        {
            Session["stringflag"] = "flightround";
            Session["intflag"] = 0;
            FlightSearchMethod();
            CustomersGridView.DataSource = (FlightClass[])Session["FlightResults"];
            CustomersGridView.DataBind();
            ResultsLabel.Text = "Departure Flight Results:";
        }
        if (CarSearch.Checked && !TrainSearch.Checked && !FlightSearch.Checked && !HotelSearch.Checked && !RoundTripButton.Checked)
        {
            Session["stringflag"] = "car";
            Session["intflag"] = 0;
            CarSearchMethod();
            CustomersGridView.DataSource = (CarClass[])Session["CarResults"];
            CustomersGridView.DataBind();
            ResultsLabel.Text = "Car Results:";
        }
        if (HotelSearch.Checked && !TrainSearch.Checked && !FlightSearch.Checked && !CarSearch.Checked && !RoundTripButton.Checked)
        {
            Session["stringflag"] = "hotel";
            Session["intflag"] = 0;
            HotelSearchMethod();
            CustomersGridView.DataSource = (HotelClass[])Session["HotelResults"];
            CustomersGridView.DataBind();
            ResultsLabel.Text = "Hotel Results:";
        }
    }
    public class Airport
    {
        public string kind { get; set; }
        public string code { get; set; }
        public string city { get; set; }
        public string name { get; set; }
    }

    public class City
    {
        public string kind { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }

    public class Aircraft
    {
        public string kind { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }

    public class Tax
    {
        public string kind { get; set; }
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Carrier
    {
        public string kind { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }

    public class Data
    {
        public string kind { get; set; }
        public List<Airport> airport { get; set; }
        public List<City> city { get; set; }
        public List<Aircraft> aircraft { get; set; }
        public List<Tax> tax { get; set; }
        public List<Carrier> carrier { get; set; }
    }

    public class Flight
    {
        public string carrier { get; set; }
        public string number { get; set; }
    }

    public class Leg
    {
        public string kind { get; set; }
        public string id { get; set; }
        public string aircraft { get; set; }
        public string arrivalTime { get; set; }
        public string departureTime { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public string destinationTerminal { get; set; }
        public int duration { get; set; }
        public int onTimePerformance { get; set; }
        public int mileage { get; set; }
        public bool secure { get; set; }
        public string originTerminal { get; set; }
        public string operatingDisclosure { get; set; }
    }

    public class Segment
    {
        public string kind { get; set; }
        public int duration { get; set; }
        public Flight flight { get; set; }
        public string id { get; set; }
        public string cabin { get; set; }
        public string bookingCode { get; set; }
        public int bookingCodeCount { get; set; }
        public string marriedSegmentGroup { get; set; }
        public List<Leg> leg { get; set; }
        public int connectionDuration { get; set; }
    }

    public class Slouse
    {
        public string kind { get; set; }
        public int duration { get; set; }
        public List<Segment> segment { get; set; }
    }

    public class Fare
    {
        public string kind { get; set; }
        public string id { get; set; }
        public string carrier { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public string basisCode { get; set; }
    }

    public class BagDescriptor
    {
        public string kind { get; set; }
        public string commercialName { get; set; }
        public int count { get; set; }
        public List<string> description { get; set; }
        public string subcode { get; set; }
    }

    public class FreeBaggageOption
    {
        public string kind { get; set; }
        public List<BagDescriptor> bagDescriptor { get; set; }
        public int pieces { get; set; }
    }

    public class SegmentPricing
    {
        public string kind { get; set; }
        public string fareId { get; set; }
        public string segmentId { get; set; }
        public List<FreeBaggageOption> freeBaggageOption { get; set; }
    }

    public class Passengers
    {
        public string kind { get; set; }
        public int adultCount { get; set; }
    }

    public class Tax2
    {
        public string kind { get; set; }
        public string id { get; set; }
        public string chargeType { get; set; }
        public string code { get; set; }
        public string country { get; set; }
        public string salePrice { get; set; }
    }

    public class Pricing
    {
        public string kind { get; set; }
        public List<Fare> fare { get; set; }
        public List<SegmentPricing> segmentPricing { get; set; }
        public string baseFareTotal { get; set; }
        public string saleFareTotal { get; set; }
        public string saleTaxTotal { get; set; }
        public string saleTotal { get; set; }
        public Passengers passengers { get; set; }
        public List<Tax2> tax { get; set; }
        public string fareCalculation { get; set; }
        public string latestTicketingTime { get; set; }
        public string ptc { get; set; }
    }

    public class TripOption
    {
        public string kind { get; set; }
        public string saleTotal { get; set; }
        public string id { get; set; }
        public List<Slouse> slice { get; set; }
        public List<Pricing> pricing { get; set; }
    }

    public class Trips
    {
        public string kind { get; set; }
        public string requestId { get; set; }
        public Data data { get; set; }
        public List<TripOption> tripOption { get; set; }
    }

    public class RootObject
    {
        public string kind { get; set; }
        public Trips trips { get; set; }
    }

    public class RootCode
    {
        public string code { get; set; }
        public string name { get; set; }
        public string location { get; set; }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime dt = Convert.ToDateTime(DropDownList1.SelectedValue);
        foreach (Search sr in (List<Search>)Session["ls"])
        {
            sr.search_date = sr.search_date.AddTicks(-sr.search_date.Ticks % 10000000);
            int x = DateTime.Compare(sr.search_date, dt);
            if (DateTime.Compare(sr.search_date,dt)== 0)
            {
                FlightSearch.Checked = sr.Flight_Search;
                TrainSearch.Checked = sr.Train_Search;
                HotelSearch.Checked = sr.Hotel_Search;
                CarSearch.Checked = sr.Car_Search;
                OneWayButton.Checked = sr.One_Way;
                RoundTripButton.Checked = sr.Round_Trip;
                FromText.Text = sr.From_Text;
                ToText.Text = sr.To_Text;
                DepartureList.Items.Add(sr.From_Airport);
                DepartureList.SelectedValue = sr.From_Airport;
                ArrivalList.Items.Add(sr.To_Airport);
                ArrivalList.SelectedValue = sr.To_Airport;
                NumRooms.SelectedValue = sr.Hotel_Rooms;
                Occupancy.SelectedValue = sr.Hotel_Occupancy;
                PickupTimeDDL.SelectedValue = sr.CarPickup_Time;
                DropoffTimeDDL.SelectedValue = sr.CarDropoff_Time;
                CarClassDDL.SelectedValue = sr.CarClass_DLL;
                CarCompany.SelectedValue = sr.Car_Company;
            }
        }
    }
}