using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Security.Cryptography;

public partial class KPMGLogin : System.Web.UI.Page
{
    public System.Data.SqlClient.SqlDataReader reader;
    public string hashed;
    bool valid = false;
    //Declare variables


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        //public System.Data.SqlClient.SqlDataReader reader;

        //Page.ClientScript.RegisterStartupScript(this.GetType(),
        //    "alert", "alert('Welcome to our site. Enjoy your stay!');", true);

        //Response.Write("<script type='text/javascript'> window.open('flight.aspx'); </script>");

        Page.Validate();
        if (Page.IsValid)
        {
            String UserNameInput = UserName.Text;
            String PasswordInput = Password.Text;            

            try
            {
                string passwordHashMD5 = SimpleHash.ComputeHash(PasswordInput, "MD5", null);

                System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                sc.ConnectionString = @"Data Source=pkyqlbhc9z.database.windows.net;Initial Catalog=KPMGTravel;Persist Security Info=True;User ID=episcopd;Password=Showker93;";
                sc.Open();
                cmd.Connection = sc;
                Label1.Text = "Connection Success!";

                // int UserID = 2;

                string user = UserName.Text.ToString();

                cmd.CommandText = @"Select PasswordHash, UserID, AccountType from SystemUser where UserID = @user";

                cmd.Parameters.AddWithValue("@user", user);
                reader = cmd.ExecuteReader();
                Label1.Text = "reader is working";
                if (reader.Read())
                {
                    hashed = reader.GetValue(0).ToString();
                    Label1.Text = "login success!";

                    valid = SimpleHash.VerifyHash(PasswordInput, "MD5", hashed);
                    Label1.Text = "login success!";

                    if (valid == true)
                    {
                        String User = Convert.ToString(reader.GetValue(1));
                        String AccountT = Convert.ToString(reader.GetValue(2));
                        Label1.Text = "login success!";
                        Session["UserIdAndAcctType"] = new String[2] { User, AccountT };
                        Session["ActiveUserIdAndAcctType"] = new String[2] { User, AccountT };
                        if (AccountT == "S")
                            Response.Redirect("KPMGFullSite.aspx");
                        Response.Redirect("AccountPage.aspx");
                    }
                    else
                    {                   
                        Label1.Text = "wrong password";
                    }
                }
                else
                {
                    Label1.Text = "No Record";
                }
            }
            catch (Exception)
            {
                //Diplay array max reached message
                //Page.ClientScript.RegisterStartupScript(this.GetType(),
                //    "alert", "alert('HELLO THERE.');", true);
            }
        }
    }
    protected void btnNewUser_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateUser.aspx");
    }
}