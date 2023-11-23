using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ElibraryManagement
{
    public partial class AdminPublisherManagement : System.Web.UI.Page
    {

        // Connection string configuration 
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        // Go button click
        protected void btnGo_Click(object sender, EventArgs e)
        {

            // Using Sql Data Adapter 

            GetPublisherById();

            // Using Sql Data Reader 

            /*if (CheckIfPublisherExists())
            {
                GetPublisherById();
                LoadRecord();
            }
            else
            {
                Response.Write("<script>alert('Author with this ID Does not exist !');</script>");
                ClearForm();
            }*/
        }

        // Add button click
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (CheckIfPublisherExists())
            {
                Response.Write("<script>alert('Publisher with this ID already exist !');</script>");
            }
            else
            {
                AddNewPublisher();
                LoadRecord();
                ClearForm();
            }
        }

        // Update button click
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (CheckIfPublisherExists())
            {
                UpdatePublisher();
                LoadRecord();
                ClearForm();
            }
            else
            {
                Response.Write("<script>alert('Publisher with this ID Does not exist !');</script>");
            }
        }

        // Delete button click
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (CheckIfPublisherExists())
            {
                DeletePublisher();
                LoadRecord();
            }
            else
            {
                Response.Write("<script>alert('Publisher with this ID Does not exist !');</script>");
            }
        }




        // User defined function for adding new author 
        void AddNewPublisher()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO publisher_master_tbl(publisher_id, publisher_name) values(@publisher_id, @publisher_name)", con);

                cmd.Parameters.AddWithValue("@publisher_id", TextBoxPublisherId.Text.Trim());
                cmd.Parameters.AddWithValue("@publisher_name", TextBoxPublisherName.Text.Trim());

                cmd.ExecuteNonQuery();
                con.Close();

                Response.Write("<script>alert('Publisher added successfully !')</script>");

                ClearForm();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }

        }

        // User defined function for Update new author 
        void UpdatePublisher()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("UPDATE publisher_master_tbl SET publisher_name=@publisher_name WHERE publisher_id='" + TextBoxPublisherId.Text.Trim() + "'", con);

                cmd.Parameters.AddWithValue("@publisher_name", TextBoxPublisherName.Text.Trim());

                cmd.ExecuteNonQuery();
                con.Close();

                Response.Write("<script>alert('Publisher Updated successfully !')</script>");

                ClearForm();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        // User defined function for Update new author 
        void DeletePublisher()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("DELETE from publisher_master_tbl WHERE publisher_id='" + TextBoxPublisherId.Text.Trim() + "'", con);

                cmd.Parameters.AddWithValue("@publisher_name", TextBoxPublisherName.Text.Trim());

                cmd.ExecuteNonQuery();
                con.Close();

                Response.Write("<script>alert('Publisher Deleted successfully !')</script>");

                ClearForm();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        // User defined function Checking author is existing or not
        bool CheckIfPublisherExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from publisher_master_tbl where publisher_id ='" + TextBoxPublisherId.Text.Trim() + "';", con);

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

        // User defined function for GO
        void GetPublisherById()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("Select * from publisher_master_tbl WHERE publisher_id='" + TextBoxPublisherId.Text.Trim() + "'", con);

                // Using Sql Data adapter

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    TextBoxPublisherName.Text = dt.Rows[0][1].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Publisher with this ID Does not exist !');</script>");
                }

                // Using Sql Data Reader

                /*SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                TextBoxAuthorName.Text = reader["author_name"].ToString();
                GridViewAuthorList.DataSource = reader;
                GridViewAuthorList.DataBind();*/

                con.Close();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }

        }

        // User defined function for Gridview Record 
        void LoadRecord()
        {

            SqlConnection con = new SqlConnection(strcon);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SELECT * from publisher_master_tbl", con);
            SqlDataReader reader = cmd.ExecuteReader();
            GridViewPublisherList.DataSource = reader;
            GridViewPublisherList.DataBind();
            con.Close();
        }

        // User Defined function for clear the form
        void ClearForm()
        {
            TextBoxPublisherId.Text = "";
            TextBoxPublisherName.Text = "";

        }

    }
}