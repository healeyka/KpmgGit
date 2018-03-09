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

public partial class StudentSearch : System.Web.UI.Page
{
    String RomeApi = "http://free.rome2rio.com/api/1.2/xml/Search?key=RNMpNhbV";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FlightToggle_Click(null, null);
        }
        if (IsPostBack)
        {
            GridView1.DataSource = (plane[])Session["PlaneResults"];
        }
    }

    protected void FlightSearch_Click(object sender, EventArgs e)
    {
        string depart = FromText.Text;
        string arrive = ToText.Text;
        Session["PlaneResults"] = null;
        RomeApi += "&oName=" + depart + "&dName=" + arrive + "";
        System.Net.WebRequest wrGETURL;
        wrGETURL = System.Net.WebRequest.Create(RomeApi);
        System.IO.Stream objStream;
        objStream = wrGETURL.GetResponse().GetResponseStream();
        System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
        xmlDoc.Load(objStream);
        XmlNamespaceManager mgr = new XmlNamespaceManager(xmlDoc.NameTable);
        mgr.AddNamespace("ns", "http://www.rome2rio.com/api/1.2/xml");
        System.Xml.XmlNodeList flightnode = xmlDoc.SelectNodes("//ns:SearchResponse/ns:Route[@name='Fly']/ns:FlightSegment/ns:FlightItinerary", mgr);
        System.Xml.XmlNodeList flighthop = xmlDoc.SelectNodes("//ns:SearchResponse/ns:Route[@name='Fly']/ns:FlightSegment/ns:FlightItinerary/ns:FlightLeg/ns:FlightHop", mgr);
        System.Xml.XmlNodeList flightprice = xmlDoc.SelectNodes("//ns:SearchResponse/ns:Route[@name='Fly']/ns:FlightSegment/ns:FlightItinerary/ns:FlightLeg/ns:IndicativePrice", mgr);
        plane[] flights = new plane[flightnode.Count];
        int j = 0;
        foreach (XmlNode xn in flightnode)
        {
            double monies = (Convert.ToDouble(xn.FirstChild.FirstChild.Attributes[0].Value));
            ahop[] ahops = new ahop[xn.FirstChild.ChildNodes.Count - 1];
            System.Xml.XmlNodeList hoplist = xn.FirstChild.ChildNodes;
            int k = 0;
            foreach (XmlNode xhop in hoplist)
            {
                if (xhop.Name.Equals("IndicativePrice"))
                    continue;
                string dterm = xhop.Attributes[0].Value;
                string aterm = xhop.Attributes[1].Value;
                string dtime = xhop.Attributes[2].Value;
                string atime = xhop.Attributes[3].Value;
                string airline = xhop.Attributes[5].Value;
                string flightid = xhop.Attributes[5].Value + xhop.Attributes[7].Value;
                ahops[k] = new ahop(dterm, aterm, dtime, atime, airline, flightid);
                k++;
            }
            string departure = ahops[0].departure;
            string departuretime = ahops[0].departuretime;
            string arrival = ahops[ahops.Length - 1].arrival;
            string arrivaltime = ahops[ahops.Length - 1].arrivaltime;
            flights[j] = new plane(departure, departuretime, arrival, arrivaltime, monies);
            j++;
        }
        Session.Add("PlaneResults", flights);
        RomeApi = "http://free.rome2rio.com/api/1.2/xml/Search?key=RNMpNhbV";
        GridView1.DataSource = (plane[])Session["PlaneResults"];
        GridView1.DataBind();
    }

    protected void TrainSearch_Click(object sender, EventArgs e)
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
        array[0] = new TrainClass(usd, carrier, time);
        Session.Add("TrainResults", array);
        RomeApi = "http://free.rome2rio.com/api/1.2/xml/Search?key=RNMpNhbV";
        GridView1.DataSource = (TrainClass[])Session["TrainResults"];
        GridView1.DataBind();
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

    [Serializable]
    class plane
    {
        public plane() { }

        public plane(string d, string dt, string a, string at, double mone)
        {
            departure = d;
            departuretime = dt;
            arrival = a;
            arrivaltime = at;
            mon = mone;
        }
        public string departure { get; set; }
        public string departuretime { get; set; }
        public string arrival { get; set; }
        public string arrivaltime { get; set; }
        public double mon { get; set; }
    }

    [Serializable]
    class TrainClass
    {
        public TrainClass() { }

        public TrainClass(double numb, string Carrier, string Time)
        {
            Dollar = numb;
            carriertype = Carrier;
            Duration = Time;
        }
        public double Dollar { get; set; }
        public string carriertype { get; set; }
        public String Duration { get; set; }
    }

    protected void FlightToggle_Click(object sender, EventArgs e)
    {
        FlightSearch.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1F396E");
        TrainSearch.BorderColor = System.Drawing.ColorTranslator.FromHtml("#DCC4C4");
        ReturnLabel.Visible = false;
        Calendar2.Visible = false;
        FlightSearchButton.Visible = true;
        TrainSearchButton.Visible = false;
    }
    protected void TrainToggle_Click(object sender, EventArgs e)
    {
        FlightSearch.BorderColor = System.Drawing.ColorTranslator.FromHtml("#DCC4C4");
        TrainSearch.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1F396E");
        ReturnLabel.Visible = false;
        Calendar2.Visible = false;
        FlightSearchButton.Visible = false;
        TrainSearchButton.Visible = true;
    }

    protected void OneWay_Click(object sender, EventArgs e)
    {
        ReturnLabel.Visible = false;
        Calendar2.Visible = false;

    }
    protected void RoundTrip_Click(object sender, EventArgs e)
    {
        ReturnLabel.Visible = true;
        Calendar2.Visible = true;
    }
}