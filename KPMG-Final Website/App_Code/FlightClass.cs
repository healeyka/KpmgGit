using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FlightClass
/// </summary>
[Serializable]
public class FlightClass
{

        public FlightClass() { }

        public FlightClass(string hr, string ar, DateTime da, string fc,string ct, int lc, string ll, string tp)
        {
            departureairport = hr;
            arrivalairport = ar;
            DDate = da;
            flightcarrier = fc;
            cabintype = ct;
            layovercount = lc;
            layoverlist = ll;
            totalprice = tp;
        }

        public string departureairport { get; set; }
        public DateTime DDate { get; set; }
        public string arrivalairport { get; set; }
        public string flightcarrier { get; set; }
        public string cabintype { get; set; }
        public int layovercount { get; set; }
        public string layoverlist { get; set; }
        public string totalprice { get; set; }
}