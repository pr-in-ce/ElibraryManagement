using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.Remoting.Messaging;

namespace ElibraryManagement
{
    public partial class AdminAuthorManagement : System.Web.UI.Page
    {

        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadRecord();
        }

        // Add button click
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (CheckIfAuthorExists())
            {
                Response.Write("<script>alert('Author with this ID already exist !');</script>");
            }
            else
            {
                AddNewAuthor();
                LoadRecord();
            }
        }

        // Update button click
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (CheckIfAuthorExists())
            {
                UpdateAuthor();
                LoadRecord();
            }
            else
            {
                Response.Write("<script>alert('Author with this ID Does not exist !');</script>");
            }
        }

        // Delete button click
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (CheckIfAuthorExists())
            {
                DeleteAuthor();
                LoadRecord();
            }
            else
            {
                Response.Write("<script>alert('Author with this ID Does not exist !');</script>");
            }
        }

        // Go button click
        protected void btnGo_Click(object sender, EventArgs e)
        {

            // Using Sql Data Adapter 

            GetAuthorById();

            // Using Sql Data Reader 

            /*if (CheckIfAuthorExists())
            {
                GetAuthorById();
                LoadRecord();   
            }
            else
            {
                Response.Write("<script>alert('Author with this ID Does not exist !');</script>");
                ClearForm();
            }*/
        }

        // User defined function for adding new author 

        void AddNewAuthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO author_master_tbl(author_id, author_name) values(@author_id, @author_name)", con);

                cmd.Parameters.AddWithValue("@author_id", TextBoxAuthorId.Text.Trim());
                cmd.Parameters.AddWithValue("@author_name", TextBoxAuthorName.Text.Trim());

                cmd.ExecuteNonQuery();
                con.Close();

                Response.Write("<script>alert('Author added successfully !')</script>");

                ClearForm();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }

        }


        // User defined function for Update new author 

        void UpdateAuthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("UPDATE author_master_tbl SET author_name=@author_name WHERE author_id='"+TextBoxAuthorId.Text.Trim()+"'", con);

                cmd.Parameters.AddWithValue("@author_name", TextBoxAuthorName.Text.Trim());

                cmd.ExecuteNonQuery();
                con.Close();

                Response.Write("<script>alert('Author Updated successfully !')</script>");

                ClearForm();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        // User defined function for Update new author 

        void DeleteAuthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("DELETE from author_master_tbl WHERE author_id='" + TextBoxAuthorId.Text.Trim() + "'", con);

                cmd.Parameters.AddWithValue("@author_name", TextBoxAuthorName.Text.Trim());

                cmd.ExecuteNonQuery();
                con.Close();

                Response.Write("<script>alert('Author Deleted successfully !')</script>");

                ClearForm();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        // User defined function Checking author is existing or not
        bool CheckIfAuthorExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from author_master_tbl where author_id ='" + TextBoxAuthorId.Text.Trim() + "';", con);

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

        void GetAuthorById()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("Select * from author_master_tbl WHERE author_id='" + TextBoxAuthorId.Text.Trim() + "'", con);

                // Using Sql Data adapter

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1) {
                    TextBoxAuthorName.Text = dt.Rows[0][1].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Author with this ID Does not exist !');</script>");
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

        // User Defined function for clear the form
        void ClearForm()
        {
            TextBoxAuthorId.Text = "";
            TextBoxAuthorName.Text = "";

        }


        // User defined function for Gridview Record 

        void LoadRecord()
        {

            SqlConnection con = new SqlConnection(strcon);
            if (con.State== ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd= new SqlCommand("SELECT * from author_master_tbl", con);
            SqlDataReader reader = cmd.ExecuteReader();
            GridViewAuthorList.DataSource = reader;
            GridViewAuthorList.DataBind();
            con.Close();
        }
    }
}