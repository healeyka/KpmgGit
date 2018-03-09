using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class KPMG : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void NewBookingButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("KPMGFullSite.aspx");
    }
    protected void BookingsButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("BookingHistory.aspx");
    }
    protected void AccountButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("AccountPage.aspx");
    }
    protected void LogoutButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("KPMGLogin.aspx");
        Session.Clear();
        Session.Abandon();
        Session.RemoveAll();
    }
}
