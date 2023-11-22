using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ElibraryManagement
{
    public partial class UserSignUp : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            if (CheckMemberExists())
            {
                Response.Write("<script>alert('Member alredy exist in our database')</script>");
            }
            else
            {
                SignUpNewMember();
            }
        }

        bool CheckMemberExists()
        {

            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from member_master_table where member_id ='"+TextBoxUserId.Text.Trim()+"';", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    return true; 
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false; 
            }
        }

        void SignUpNewMember()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO member_master_table(full_name,dob,contact_no,email,state,city,pincode,full_address,member_id,password,account_status) values(@full_name,@dob,@contact_no,@email,@state,@city,@pincode,@full_address,@member_id,@password,@account_status)", con);

                cmd.Parameters.AddWithValue("@full_name", TextBoxFullName.Text.Trim());
                cmd.Parameters.AddWithValue("@dob", TextBoxDateOfBirth.Text.Trim());
                cmd.Parameters.AddWithValue("@contact_no", TextBoxContactNumber.Text.Trim());
                cmd.Parameters.AddWithValue("@email", TextBoxEmailId.Text.Trim());
                cmd.Parameters.AddWithValue("@state", DropDownListState.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@city", TextBoxCity.Text.Trim());
                cmd.Parameters.AddWithValue("@pincode", TextBoxPincode.Text.Trim());
                cmd.Parameters.AddWithValue("@full_address", TextBoxFullAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@member_id", TextBoxUserId.Text.Trim());
                cmd.Parameters.AddWithValue("@password", TextBoxPassword.Text.Trim());
                cmd.Parameters.AddWithValue("@account_status", "pending");

                cmd.ExecuteNonQuery();
                con.Close();

                Response.Write("<script>alert('Sign Up successful. Go to User Login to Login')</script>");

                ClearForm();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void ClearForm()
        {
            TextBoxFullName.Text = "";
            TextBoxDateOfBirth.Text = "";
            TextBoxContactNumber.Text = "";
            TextBoxEmailId.Text = "";
            DropDownListState.Text = "";
            TextBoxCity.Text = "";
            TextBoxPincode.Text = "";
            TextBoxFullAddress.Text = "";
            TextBoxUserId.Text = "";
            TextBoxPassword.Text = "";
        }

    }
}