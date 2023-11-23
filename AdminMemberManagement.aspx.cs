using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace ElibraryManagement
{
    public partial class AdminMemberManagement : System.Web.UI.Page
    {

        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadRecord();
        }

        // Go button
        protected void btnMemberId_Click(object sender, EventArgs e)
        {
            GetMemberById();
        }

        //Active button
        protected void btnMemberStatusActive_Click(object sender, EventArgs e)
        {
            UpdateMemberStatusById("Active");
        }

        // Pending button
        protected void btnMemberStatusPending_Click(object sender, EventArgs e)
        {
            UpdateMemberStatusById("Pending");
        }

        // Deactive button
        protected void btnMemberStatusDeactive_Click(object sender, EventArgs e)
        {
            UpdateMemberStatusById("Deactive");
        }

        // Delete button
        protected void btnDeleteUserPermanently_Click(object sender, EventArgs e)
        {
            DeleteMember();
        }

        // User define functions 


        // Load Record 
        void LoadRecord()
        {

            SqlConnection con = new SqlConnection(strcon);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SELECT * FROM member_master_table ", con);
            SqlDataReader reader = cmd.ExecuteReader();
            GridViewMemberList.DataSource = reader;
            GridViewMemberList.DataBind();
            con.Close();
        }

        // Gtiing Member information by Id
        void GetMemberById()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("Select * from member_master_table WHERE member_id='" + TextBoxMemberId.Text.Trim() + "'", con);

                // Using Sql Data adapter

                /*SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    TextBoxFullName.Text = dt.Rows[0][0].ToString();
                    TextBoxAccountStatus.Text = dt.Rows[0][10].ToString();
                    TextBoxDateOfBirth.Text = dt.Rows[0][1].ToString();
                    TextBoxContactNo.Text = dt.Rows[0][2].ToString();
                    TextBoxEmailId.Text = dt.Rows[0][3].ToString();
                    TextBoxState.Text = dt.Rows[0][4].ToString();
                    TextBoxCity.Text = dt.Rows[0][5].ToString();
                    TextBoxPinCode.Text = dt.Rows[0][6].ToString();
                    TextBoxFullAddress.Text = dt.Rows[0][7].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Member with this ID Does not exist !');</script>");
                    ClearForm();
                }*/

                // Using Sql Data Reader

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        TextBoxFullName.Text = reader["full_name"].ToString();
                        TextBoxAccountStatus.Text = reader["account_status"].ToString();
                        TextBoxDateOfBirth.Text = reader["dob"].ToString();
                        TextBoxContactNo.Text = reader["contact_no"].ToString();
                        TextBoxEmailId.Text = reader["email"].ToString();
                        TextBoxState.Text = reader["state"].ToString();
                        TextBoxCity.Text = reader["city"].ToString();
                        TextBoxPinCode.Text = reader["pincode"].ToString();
                        TextBoxFullAddress.Text = reader["full_address"].ToString();
                    }
                }
                else
                {
                    Response.Write("<script>alert('Publisher with this ID Does not exist !');</script>");
                    ClearForm();
                }

                con.Close();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }

        }

        // Udate member status by Id 
        void UpdateMemberStatusById(string Status)
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("UPDATE member_master_table SET account_status= '" + Status + "' WHERE member_id = '" + TextBoxMemberId.Text.Trim() + "'", con);

                cmd.ExecuteNonQuery();
                con.Close();
                LoadRecord();
                Response.Write("<script>alert('Member Status Updated');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        //Delete member
        void DeleteMember()
        {
            if (CheckIfMemberExists())
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("DELETE from member_master_table WHERE member_id='" + TextBoxMemberId.Text.Trim() + "'", con);

                    cmd.ExecuteNonQuery();
                    con.Close();

                    Response.Write("<script>alert('Member Deleted successfully !')</script>");
                    ClearForm();
                    LoadRecord();

                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }


            }
            else
            {
                Response.Write("<script>alert('Member Does not exist !')</script>");
            }
        }

        // Check member exist or not
        bool CheckIfMemberExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from member_master_table where member_id ='" + TextBoxMemberId.Text.Trim() + "';", con);

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

        // clear form
        void ClearForm()
        {
            TextBoxFullName.Text = "";
            TextBoxAccountStatus.Text = "";
            TextBoxDateOfBirth.Text = "";
            TextBoxContactNo.Text = "";
            TextBoxEmailId.Text = "";
            TextBoxState.Text = "";
            TextBoxCity.Text = "";
            TextBoxPinCode.Text = "";
            TextBoxFullAddress.Text = "";
        }

    }
}

