using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TrainClass
/// </summary>
[Serializable]
public class TrainClass
{
    public TrainClass() { }

    public TrainClass(double numb, string cr, string ct, string Time)
    {
        Carrier = cr;
        Cost = numb;
        cabintype = ct;
        Duration = Time;
    }   
    public string Carrier { get; set; }
    public double Cost { get; set; }
    public string cabintype { get; set; }
    public string Duration { get; set; }
}