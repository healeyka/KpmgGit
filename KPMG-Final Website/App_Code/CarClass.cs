using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

/// <summary>
/// Summary description for CarClass
/// </summary>
[Serializable]
public class CarClass
{
    public CarClass() { }

    public CarClass(string CR, string tra, string carcode, double topri, double dalrate, DateTime fulldtime, DateTime fullpicktime)
    {
        CarRentalCompany = CR;
        transmissiontype = tra;
        CarType = carcode;
        TotalPrice = topri; 
        DailyRate = dalrate;
        FullDropOffTime = fulldtime;
        FullPickUpDateTime = fullpicktime;
    }

    public string CarRentalCompany { get; set; }
    public string transmissiontype {get; set;}
    public String CarType { get; set; }
    public Double TotalPrice { get; set; }
    public Double DailyRate { get; set; }
    public DateTime FullDropOffTime { get; set; }
    public DateTime FullPickUpDateTime { get; set; }   
    
}