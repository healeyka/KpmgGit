using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AccountPage : System.Web.UI.Page
{
    public System.Data.SqlClient.SqlDataReader reader;

    protected void Page_Load(object sender, EventArgs e)
    {
   

   
    }

    

    
    protected void Button1_Click(object sender, EventArgs e)
    {
        string user = txtUserID.Text;
        string pass= txtPass.Text;
        string type= dropAcctType.SelectedValue.ToString();
        DateTime dob = Convert.ToDateTime(txtDOB0.Text);
        string first = txtFirstName.Text;
        string middle = txtMiddleName.Text;
        string last = txtLastName.Text;
        string email = txtEmail.Text;
        string add1 = txtAddress.Text;
        string add2 = txtAddress2.Text;
        string city = txtCity.Text;
        string state = txtState.Text;
        int zip;
         if (!string.IsNullOrEmpty(txtZipCode.Text))
                {
                     zip = Convert.ToInt32(txtZipCode.Text);
                }
          zip = 0; 

        string phone = txtTelephone.Text; 


        string hashpass = SimpleHash.ComputeHash(pass, "MD5", null);

        System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
        sc.ConnectionString = @"Data Source=pkyqlbhc9z.database.windows.net;Initial Catalog=KPMGTravel;Persist Security Info=True;User ID=episcopd;Password=Showker93;";
        sc.Open();
        cmd.Connection = sc;
        Status.Text = "Connection Success!";
        // int UserID = 2;
        cmd.CommandText = @"insert into SystemUser(UserID, PasswordHash, AccountType, DOB, FirstName,MiddleName,LastName, Email_Address, AddressLineOne,AddressLineTwo,City,State,Zip_Code,Telephone) 
        values (@user,@pass,@type,@DOB, @first, @middle, @last, @email, @add1, @add2, @city, @state, @zip,@phone)";


        cmd.Parameters.AddWithValue("@user", user);
        cmd.Parameters.AddWithValue("@pass", hashpass);
        cmd.Parameters.AddWithValue("@type", type);
        cmd.Parameters.AddWithValue("@DOB", dob);
        cmd.Parameters.AddWithValue("@first", first);
        cmd.Parameters.AddWithValue("@middle", middle);
        cmd.Parameters.AddWithValue("@last", last);
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@add1", add1);
        cmd.Parameters.AddWithValue("@add2", add2);
        cmd.Parameters.AddWithValue("@city", city);
        cmd.Parameters.AddWithValue("@state", state);
        cmd.Parameters.AddWithValue("@zip", zip);
        cmd.Parameters.AddWithValue("@phone", phone);
        cmd.ExecuteNonQuery();
        Status.Text = "Added" + user+"User!";

    }
}