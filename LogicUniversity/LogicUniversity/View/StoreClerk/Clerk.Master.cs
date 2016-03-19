using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
using System.Web.UI.HtmlControls;


namespace LogicUniversity
{
    public partial class Clerk : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginStaff"] == null)
            {
                Response.Redirect("/LogicUniversity/Login.aspx");
            }
            else
            {
                
                Staff loginStaff = (Staff)Session["LoginStaff"];
                lblUser.Text = loginStaff.Staff_Name;
                if (loginStaff.Dept_ID != "STAT")
                {
                    Response.Redirect("/LogicUniversity/Login.aspx");
                }
                else
                {
                   
                    Report.Visible = false;
                    ApproveAdj.Visible = false;
                    if (loginStaff.Role_ID == "SManager" || loginStaff.Role_ID == "Supervisor")
                    {
                        Report.Visible = true;
                        ApproveAdj.Visible = true;
                    }
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("/LogicUniversity/Login.aspx");
        }


    }
}