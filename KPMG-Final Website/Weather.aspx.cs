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

public partial class Weather : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void getWeatherData()
    {
        // load xml result from Google weather
        //XDocument xd = XDocument.Load("http://www.google.com/ig/api?weather=" + txtZip.Text);

        //XDocument xd = XDocument.Load("http://api.openweathermap.org/data/2.5/forecast/daily?q=Harrisonburg,%20VA&mode=xml&units=imperial&cnt=1");
        XDocument xd = XDocument.Load("http://api.openweathermap.org/data/2.5/forecast/daily?q=" + txtZip.Text + ",&mode=xml&units=imperial&cnt=1");

        //GET CITY-LOCATION NAME
        var location = from locationData in xd.Descendants("location") select locationData;

        var forcastInfo = from forecastinfo in xd.Root.Descendants("forecast").DescendantNodes() select forecastinfo;
        var temperature = from tempInfo in xd.Root.Descendants("forecast").Descendants("time") select tempInfo;

        foreach (var item in temperature)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("");
            sb.Append("Forecast for ");

            // retriving information from nodes child attribute
            var nameOne = from tempInfo in xd.Root.Descendants("location").Descendants("name") select tempInfo;
            foreach (var n in nameOne)
            {
                sb.Append(n.Value);
            }
            //sb.Append("<i>(Date: " + item.Element("time").Attribute("day").Value + ")</i>");
            sb.Append("<i>(DayTemp: " + item.Element("temperature").Attribute("day").Value + ")</i>");
            sb.Append("<i>(MinTemp: " + item.Element("temperature").Attribute("min").Value + ")</i>");
            //sb.Append("<i>(MinTemp: " + (Convert.ToDateTime(item.Element("current_date_time").Attribute("data").Value)).ToUniversalTime().AddHours(-8).ToString() + ")</i>");
            sb.Append("<i>(MaxTemp:" + item.Element("temperature").Attribute("max").Value + ")</i>");
            sb.Append("<i>(Cloud Cover:" + item.Element("clouds").Attribute("value").Value + ")</i>");
            sb.Append("<i>(Precipitation:" + item.Element("clouds").Attribute("all").Value + "%" + ")</i>");
            Panel1.GroupingText = sb.ToString();
        }

        //foreach (var item in current_conditions)
        //{
        //    currCondition.Text = item.Element("condition").Attribute("data").Value;
        //    temp_f.Text = item.Element("temp_f").Attribute("data").Value;
        //    temp_c.Text = item.Element("temp_c").Attribute("data").Value;
        //    humidity.Text = item.Element("humidity").Attribute("data").Value;
        //    icon.ImageUrl = "http://www.google.com" + item.Element("icon").Attribute("data").Value;
        //    wind_condition.Text = item.Element("wind_condition").Attribute("data").Value;
        //}
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        getWeatherData();
    }
} 