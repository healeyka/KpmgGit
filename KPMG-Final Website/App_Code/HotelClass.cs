using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for HotelClass
/// </summary>
[Serializable]
public class HotelClass
{
    public HotelClass() { }

    public HotelClass(string hn, double TP, string amen, string CheckIn, string CheckOut, double AvgPrice)
    {
        HotelName = hn;
        TotalPrice = TP;
        Amenities = amen;
        CheckInDate = CheckIn;
        CheckOutDate = CheckOut;
        AveragePricePerNight = AvgPrice;
    }
    public string HotelName { get; set; }
    public Double TotalPrice { get; set; }
    public string Amenities { get; set; }
    public String CheckInDate { get; set; }
    public String CheckOutDate { get; set; }
    public Double AveragePricePerNight { get; set; }
}