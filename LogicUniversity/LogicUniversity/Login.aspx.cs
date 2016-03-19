using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
using BL.User;

namespace LogicUniversity
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        public string UserID
        {
            get
            {
                if (string.IsNullOrEmpty(Request.QueryString["UserID"]))
                    return string.Empty;
                return Request.QueryString["UserID"];
            }

        }



        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageTitle();
        }

        private void SetPageTitle()
        {
            this.Title = "User Login";
        }



        protected void butSignIn_Click(object sender, EventArgs e)
        {
            
            Staff Info = UserController.SelectByLogIn(txtemail.Text, txtPassword.Text);

            if (Info == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('User Name and Password do not match!')</script>");
            }

            else
            {

                string path = "";
                Session["LoginStaff"] = Info;

                switch (Info.Dept_ID)
                {
                    case "STAT":
                        path = "~/View/StoreClerk/Create/CreateDisbursement.aspx";
                        break;
                    case "SUPP":
                        path = "~/View/Supplier/UpdateSupInventory.aspx";       
                        break;
                    default:
                        path = "~/View/Departments/CreateRequisition.aspx";
                        break;


                }

                Response.Redirect(path);

            }

        }


    }

  }




