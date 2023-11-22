using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ElibraryManagement
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["role"].Equals(""))
                {
                    btnUserLogin.Visible = true;    // User Link Button
                    btnSignUp.Visible = true;       // SignUp Link button

                    btnLogout.Visible = false;       // Logout Link Button
                    btnHelloUser.Visible = false;    // Hello User link button 


                    btnAdminLogin.Visible = true;           // Admin Login link button
                    btnAuthorManagement.Visible = false;     // Author amnagement link button
                    btnPublisherManagement.Visible = false;  // Publisher Mnagement link button
                    btnBookInventory.Visible = false;        // Book Inventory link  button
                    btnBookIssuing.Visible = false;          // Book Issuing link button
                    btnMemberManagement.Visible = false;     // Member amnagement link button
                }

                else if (Session["role"].Equals("user"))
                {
                    btnUserLogin.Visible = false;    // User Link Button
                    btnSignUp.Visible = false;       // SignUp Link button

                    btnLogout.Visible = true;       // Logout Link Button
                    btnHelloUser.Visible = true;    // Hello User link button 
                    btnHelloUser.Text = "Hello " + Session["username"].ToString();


                    btnAdminLogin.Visible = true;           // Admin Login link button
                    btnAuthorManagement.Visible = false;     // Author amnagement link button
                    btnPublisherManagement.Visible = false;  // Publisher Mnagement link button
                    btnBookInventory.Visible = false;        // Book Inventory link  button
                    btnBookIssuing.Visible = false;          // Book Issuing link button
                }
                else if (Session["role"].Equals("admin"))
                {
                    btnUserLogin.Visible = false;    // User Link Button
                    btnSignUp.Visible = false;       // SignUp Link button

                    btnLogout.Visible = true;       // Logout Link Button
                    btnHelloUser.Visible = true;    // Hello User link button 
                    btnHelloUser.Text = "Hello Admin";


                    btnAdminLogin.Visible = false;           // Admin Login link button
                    btnAuthorManagement.Visible = true;     // Author amnagement link button
                    btnPublisherManagement.Visible = true;  // Publisher Mnagement link button
                    btnBookInventory.Visible = true;        // Book Inventory link  button
                    btnBookIssuing.Visible = true;          // Book Issuing link button
                }
                else
                {
                    Response.Write("<script>alert('Invalid Credential');</script>");
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnAdminLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminLogin.aspx");
        }

        protected void btnAuthorManagement_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminAuthorManagement.aspx");
        }

        protected void btnPublisherManagement_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminPublisherManagement.aspx");
        }

        protected void btnBookInventory_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminBookInventory.aspx");
        }

        protected void btnBookIssuing_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminBookIssuing.aspx");
        }

        protected void btnMemberManagement_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminMemberManagement.aspx");
        }

        protected void btnViewBook_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewBook.aspx");
        }

        protected void btnUserLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserLogIn.aspx");
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserSignUp.aspx");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["username"] = "";
            Session["fullname"] = "";
            Session["role"] = "";
            Session["status"] = "";


            btnUserLogin.Visible = true;    // User Link Button
            btnSignUp.Visible = true;       // SignUp Link button

            btnLogout.Visible = false;       // Logout Link Button
            btnHelloUser.Visible = false;    // Hello User link button 


            btnAdminLogin.Visible = true;           // Admin Login link button
            btnAuthorManagement.Visible = false;     // Author amnagement link button
            btnPublisherManagement.Visible = false;  // Publisher Mnagement link button
            btnBookInventory.Visible = false;        // Book Inventory link  button
            btnBookIssuing.Visible = false;          // Book Issuing link button
            btnMemberManagement.Visible = false;     // Member amnagement link button

            //Response.Redirect("Logout.aspx");
        }

        protected void btnHelloUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserProfile.aspx");
        }
    }
}