using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace LogicUniversity.Departments
{
    public partial class Dept : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StaffLogin();
            if (Session["LoginStaff"] == null)
            {
                Response.Redirect("/LogicUniversity/Login.aspx");
            }
            else
            {
                
                Staff loginStaff = (Staff)Session["LoginStaff"];
                lblUser.Text = loginStaff.Staff_Name;
                if (loginStaff.Dept_ID == "STAT" || loginStaff.Dept_ID == "SUPP")
                {
                    Response.Redirect("/LogicUniversity/Login.aspx");
                }
                else
                {

                    switch (loginStaff.Role_ID)
                    {
                        case "RepStaff":
                            RecLogin();
                            break;
                        case "AuthStaff":
                            AuthLogin();
                            break;
                        case "Manager":
                            ManagerLogin();
                            break;

                    }
                }
            }

        }
        protected void StaffLogin()
        {
            liApprove.Visible = false;
            liCollection.Visible = false;
            liConfirmRec.Visible = false;
            liDelegate.Visible = false;
        }
        protected void AuthLogin()
        {
            liApprove.Visible = true;

            liCollection.Visible = true;
        }

        protected void ManagerLogin()
        {
            liApprove.Visible = true;
            liCollection.Visible = true;

            liDelegate.Visible = true;
        }

        protected void RecLogin()
        {
            liConfirmRec.Visible = true;
        }


        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("/LogicUniversity/Login.aspx");
        }
    }
}