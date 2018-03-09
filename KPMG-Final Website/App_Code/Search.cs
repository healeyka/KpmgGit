using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Search
/// </summary>
public class Search
{
	public Search()	{}
    
    public Search(DateTime sd, bool fs, bool ts, bool hs, bool cs,bool ow, bool rt, string ft, string tt, string fa, string ta, string hr, string ho, string cpt, string cdt, string ccddl, string cc)
        {
            search_date = sd;
            Flight_Search = fs;
            Train_Search = ts;
            Hotel_Search = hs;
            Car_Search = cs;
            One_Way = ow;
            Round_Trip = rt;
            From_Text = ft;
            To_Text = tt;
            From_Airport = fa;
            To_Airport = ta;
            Hotel_Rooms = hr;
            Hotel_Occupancy = ho;
            CarPickup_Time = cpt;
            CarDropoff_Time = cdt;
            CarClass_DLL = ccddl;
            Car_Company = cc;
        }
    public DateTime search_date { get; set; }
    public bool Flight_Search { get; set; }
    public bool Train_Search { get; set; }
    public bool Hotel_Search { get; set; }
    public bool Car_Search { get; set; }
    public bool One_Way { get; set; }
    public bool Round_Trip { get; set; }
    public string From_Text { get; set; }
    public string To_Text { get; set; }
    public string From_Airport { get; set; }
    public string To_Airport { get; set; }
    public string Hotel_Rooms { get; set; }
    public string Hotel_Occupancy { get; set; }
    public string CarPickup_Time { get; set; }
    public string CarDropoff_Time { get; set; }
    public string CarClass_DLL { get; set; }
    public string Car_Company { get; set; }
}