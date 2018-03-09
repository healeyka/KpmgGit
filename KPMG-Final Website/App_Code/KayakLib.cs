using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.Xml;

namespace KayakLib
{
    public class Kayak
    {
        //Keys are from http://www.kayak.com/labs/api/search/developerkey.html
        //You have to have an account and log in. If you get an "anonymous" user
        //name, it won't work.
        private String KayakUser = "EMAILADDRESS";
        private String KayakKey = "KAYAKKEY";
        private String KayakBaseUrl = "http://api.kayak.com";
        private DateTime SessionExpiration;
        public String SessionId = "";
        public String HomeAirport = "MSP";

        public Kayak()
        {
            SessionId = GetSession();
        }
        private string GetSession()
        {
            String Url = KayakBaseUrl + "/k/ident/apisession?version=1&token="
            + KayakKey;
            WebClient Web = new WebClient();
            byte[] DownloadedData = Web.DownloadData(Url);
            UTF8Encoding objUTF8 = new UTF8Encoding();
            String SessionXmlString = objUTF8.GetString(DownloadedData);
            XmlDocument KayakSessionXml = new XmlDocument();
            KayakSessionXml.LoadXml(SessionXmlString);
            XmlNode TokenNode = KayakSessionXml.SelectSingleNode("//sid");
            String Token = TokenNode.FirstChild.Value.ToString();
            return Token;
        }
        public XmlDocument FlightSearch(String DestinationAirport, DateTime
        DepartDate, DateTime ReturnDate)
        {
            String DepartDateString = DepartDate.ToShortDateString();
            String ReturnDateString = ReturnDate.ToShortDateString();
            //Start Search
            String Url = KayakBaseUrl + "/s/apisearch?";
            Url += "basicmode=true&";
            Url += "oneway=n&";
            Url += "origin=" + HomeAirport + "&";
            Url += "destination=BOS&";
            Url += "depart_date=" + DepartDateString + "&";
            Url += "return_date=" + ReturnDateString + "&";
            Url += "depart_time=a&return_time=a&";
            Url += "travelers=1&";
            Url += "cabin=e&";
            Url += "action=doFlights&";
            Url += "apimode=1&";
            Url += "_sid_=" + this.SessionId;
            WebClient Web = new WebClient();
            byte[] DownloadedData = Web.DownloadData(Url);
            UTF8Encoding objUTF8 = new UTF8Encoding();
            String InitialSearchXmlString = objUTF8.GetString(DownloadedData);
            XmlDocument SearchIdXml = new XmlDocument();

            SearchIdXml.LoadXml(InitialSearchXmlString);
            XmlNode SearchIdNode = SearchIdXml.SelectSingleNode("//searchid");
            String SearchId = SearchIdNode.FirstChild.Value.ToString();
            //Get Flight Results
            Url = KayakBaseUrl + "/s/basic/flight?";
            Url += "searchid=" + SearchId + "&";
            Url += "c=100&";
            Url += "apimode=1&";
            Url += "s=price&d=up&";
            Url += "_sid_=" + this.SessionId;
            Web = new WebClient();
            DownloadedData = Web.DownloadData(Url);
            objUTF8 = new UTF8Encoding();
            String FlightResultsXmlString = objUTF8.GetString(DownloadedData);
            XmlDocument FlightResultsXml = new XmlDocument();
            FlightResultsXml.LoadXml(FlightResultsXmlString);
            return FlightResultsXml;
        }
    }
}