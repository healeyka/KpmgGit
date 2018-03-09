using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AccountPage : System.Web.UI.Page
{
    public System.Data.SqlClient.SqlDataReader reader;
    public System.Data.SqlClient.SqlDataReader readerast;
    List<string> strlis = new List<string>();
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (((string[])Session["UserIdAndAcctType"])[1].Equals("S"))
        {
            ((Button)Master.FindControl("NewBookingButton")).Visible = false;
            ((Button)Master.FindControl("BookingsButton")).Visible = false;
            ((Button)Master.FindControl("AccountButton")).Visible = false;
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["HotelNamePreference"] = DDLHotel.SelectedValue;
        Session["AirlineCabinPreference"] = DDLPlaneCabin.SelectedValue;
        Session["AirlineNamePreference"] = DDLAirline.SelectedValue;
        Session["CarTransmissionPreference"] = DDLCarTrans.SelectedValue;
        Session["TrainCabinPreference"] = DDLTrainCabin.SelectedValue;
        if (!IsPostBack)
        {
            if (((string[])Session["UserIdAndAcctType"])[1].Equals("B"))
            {
                lblAstDropDown.Visible = true;
            }

            try
            {
                System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                sc.ConnectionString = @"Data Source=pkyqlbhc9z.database.windows.net;Initial Catalog=KPMGTravel;Persist Security Info=True;User ID=episcopd;Password=Showker93;";

                sc.Open();
                cmd.Connection = sc;
                cmd.CommandText = @"select isnull(Passport, ' '), isnull(TSAKTNNumber, ' '), isnull(TSAPrecheck, ' ') from TSATable where UserID = @UserID";
                cmd.Parameters.AddWithValue("@UserID", ((string[])Session["UserIdAndAcctType"])[0]);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtPassportNumber.Text = reader.GetString(0);
                    txtTSAKN.Text = reader.GetString(1);
                    txtPre.Text = reader.GetString(2);
                }
                reader.Close();

                cmd.CommandText = "";
                cmd.CommandText = @"select isnull(CardHolder, ' '), isnull(CardNumber, ' '), isnull(CardType, ' '), isnull(CardExpiration, ' ') from CreditCard where UserID = @UserID1";
                cmd.Parameters.AddWithValue("@UserID1", ((string[])Session["UserIdAndAcctType"])[0]);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtCCName.Text = reader.GetString(0);
                    txtCCNumber.Text = reader.GetString(1);
                    txtCCType.Text = reader.GetString(2);
                    txtCCExp.Text = reader.GetString(3);
                }
                reader.Close();

                if (((string[])Session["UserIdAndAcctType"])[1].Equals("B"))
                {
                    AssistantDropDown.Visible = true;
                    cmd.CommandText = @"Select UserID from SystemUser Where AssistantID = @accty";
                    cmd.Parameters.AddWithValue("@accty", ((string[])Session["UserIdAndAcctType"])[0]);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string tempstr = reader.GetString(0);
                        strlis.Add(tempstr);
                    }
                    Session["strlis"] = strlis;
                    AssistantDropDown.DataSource = (List<string>)Session["strlis"];
                    AssistantDropDown.DataBind();
                    reader.Close();
                }

                //int user = Convert.ToInt32(Session["UserID"]);
                cmd.CommandText = "";
                cmd.CommandText = @"SELECT UserID, isnull(FirstName, ' ') As FirstName, isnull(MiddleName, ' ') As MiddleName, isnull(LastName, ' ') As LastName, isnull(AddressLineOne, ' '), isnull(AddressLineTwo, ' '), 
                                    isnull(City, ' '), isnull(State, ' '), isnull(Zip_Code, ' '), isnull(Telephone, ' '), isnull(Email_Address, ' ') From [SystemUser] WHERE [SystemUser].[UserID] = @UserID2";
                cmd.Parameters.AddWithValue("@UserID2", ((string[])Session["UserIdAndAcctType"])[0]);

                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtUserID.Text = reader.GetString(0);
                    txtFirstName.Text = reader.GetString(1);
                    txtMiddleName.Text = reader.GetString(2);
                    txtLastName.Text = reader.GetString(3);
                    txtAddress.Text = reader.GetString(4);
                    txtAddress2.Text = reader.GetString(5);
                    txtCity.Text = reader.GetString(6);
                    txtState.Text = Convert.ToString(reader.GetValue(7));
                    txtZipCode.Text = Convert.ToString(reader.GetValue(8));
                    txtTelephone.Text = reader.GetString(9);
                    txtEmail.Text = reader.GetString(10);
                }
                reader.Close();
            }
            catch (Exception)
            {
                //Display Error for not being able to connect to database
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error Connecting to Database.');", true);
            }
        }
    }

    protected void Booking_Click(object sender, EventArgs e)
    {
        Response.Redirect("KPMGFullSite.aspx");
    }
    protected void BookingHistory_Click(object sender, EventArgs e)
    {
        Response.Redirect("BookingHistory.aspx");
    }
    protected void SaveRewardButton9_Click(object sender, EventArgs e)
    {
        Page.Validate();
        if (Page.IsValid)
        {
            String FirstName = txtFirstName.Text;
            String LastName = txtLastName.Text;
            String MiddleName = txtMiddleName.Text;
            String Address = txtAddress.Text;
            String Address2 = txtAddress2.Text;
            String City = txtCity.Text;
            String State = txtState.Text;
            String Telephone = txtTelephone.Text;
            String Email = txtEmail.Text;
            try
            {
                System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                sc.ConnectionString = @"Data Source=pkyqlbhc9z.database.windows.net;Initial Catalog=KPMGTravel;Persist Security Info=True;User ID=episcopd;Password=Showker93;";

                //Open Db Connection
                sc.Open();
                cmd.Connection = sc;

//                cmd.CommandText = @"UPDATE [Person].[Person] SET FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName
//                        WHERE [Person].[Person].[BusinessEntityID] = @Eid";

//                cmd.Parameters.AddWithValue("@FirstName", FirstName);
//                cmd.Parameters.AddWithValue("@MiddleName", MiddleName);
//                cmd.Parameters.AddWithValue("@LastName", LastName);
//                cmd.Parameters.AddWithValue("@Eid", Eid);

//                cmd.ExecuteNonQuery();

//                //Update the form with my new variables
//                txtFullName.Text = LastName + ", " + FirstName + " " + MiddleName;
//                txtSearch.Text = "";
//                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Record updated successfully.');", true);
            }
            catch (Exception)
            {
                //Display Error for not being able to connect to database
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error Connecting to Database.');", true);
            }
        }
    }
    protected void AssistantDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string[] strar = new string[2];
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
            sc.ConnectionString = @"Data Source=pkyqlbhc9z.database.windows.net;Initial Catalog=KPMGTravel;Persist Security Info=True;User ID=episcopd;Password=Showker93;";
            sc.Open();
            cmd.Connection = sc;
            cmd.CommandText = @"Select UserID, AccountType from SystemUser Where UserID = @seled";
            cmd.Parameters.AddWithValue("@seled", AssistantDropDown.SelectedValue);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                strar[0] = reader.GetString(0);
                strar[1] = reader.GetString(1);
            }
            Session["ActiveUserIdAndAcctType"] = strar;
        }
        catch (Exception ex)
        {

        }
    }
    protected void SaveNewPasswordButton1_Click(object sender, EventArgs e)
    {
        try
        {
            if ((txtConfirmNewPassword0.Text.Equals(txtNewPassword.Text)))
            {
                string hashpass = SimpleHash.ComputeHash(txtNewPassword.Text, "MD5", null);
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
                sc.ConnectionString = @"Data Source=pkyqlbhc9z.database.windows.net;Initial Catalog=KPMGTravel;Persist Security Info=True;User ID=episcopd;Password=Showker93;";
                sc.Open();
                cmd.Connection = sc;
                cmd.CommandText = @"Update SystemUser Set passwordHash = @hashpas Where UserID = @UserID";
                cmd.Parameters.AddWithValue("@hashpas", hashpass);
                cmd.Parameters.AddWithValue("@UserID", ((string[])Session["UserIdAndAcctType"])[0]);
                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void DDLHotel_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["HotelNamePreference"] = DDLHotel.SelectedValue;
    }
    protected void DDLPlaneCabin_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["AirlineCabinPreference"] = DDLPlaneCabin.SelectedValue;
    }
    protected void DDLAirline_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["AirlineNamePreference"] = DDLAirline.SelectedValue;
    }
    protected void DDLCarTrans_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["CarTransmissionPreference"] = DDLCarTrans.SelectedValue;
    }
    protected void DDLTrainCabin_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["TrainCabinPreference"] = DDLTrainCabin.SelectedValue;
    }
    protected void TSASave(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
        System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
        sc.ConnectionString = @"Data Source=pkyqlbhc9z.database.windows.net;Initial Catalog=KPMGTravel;Persist Security Info=True;User ID=episcopd;Password=Showker93;";
        sc.Open();
        cmd.Connection = sc;
        cmd.CommandText = @"Update TSATable Set Passport = @Passport, TSAKTNNumber = @TSAKN, TSAPrecheck = @Pre Where UserID = @UserID";
        cmd.Parameters.AddWithValue("@Passport", txtPassportNumber.Text);
        cmd.Parameters.AddWithValue("@TSAKN", txtTSAKN.Text);
        cmd.Parameters.AddWithValue("@Pre", txtPre.Text);
        cmd.Parameters.AddWithValue("@UserID", ((string[])Session["UserIdAndAcctType"])[0]);
        cmd.ExecuteNonQuery();
    }
    protected void SaveCCButton0_Click(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
        System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
        sc.ConnectionString = @"Data Source=pkyqlbhc9z.database.windows.net;Initial Catalog=KPMGTravel;Persist Security Info=True;User ID=episcopd;Password=Showker93;";
        sc.Open();
        cmd.Connection = sc;
        cmd.CommandText = @"Update CreditCard Set CardHolder = @CH, CardNumber = @CN, CardType = @CT, CardExpiration = @CE Where UserID = @UserID";
        cmd.Parameters.AddWithValue("@CH", txtCCName.Text);
        cmd.Parameters.AddWithValue("@CN", txtCCNumber.Text);
        cmd.Parameters.AddWithValue("@CT", txtCCType.Text);
        cmd.Parameters.AddWithValue("@CE", txtCCExp.Text);
        cmd.Parameters.AddWithValue("@UserID", ((string[])Session["UserIdAndAcctType"])[0]);
        cmd.ExecuteNonQuery();
    }
}