using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
using BL.User;
using BL.Store;

namespace LogicUniversity.View.Departments
{
    public partial class UpdateProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        
            if(Session["LoginStaff"] !=null)
            if (!IsPostBack)
            {
               
               Staff LoginStaff = (Staff)Session["LoginStaff"];
               lblStaffID.Text = LoginStaff.Staff_ID;
          

               StaffController staff = new StaffController();
               Staff staffdetail = staff.GetStaffByID(lblStaffID.Text);


               lblStaffName.Text = staffdetail.Staff_Name;
               lblemail.Text = staffdetail.Email;
               txtph.Text = staffdetail.PhoneNumber;
             
               

             
            }

        }



        protected void ChangePW_Click(object sender, EventArgs e)
        {
          
            Response.Redirect("ChangePassword.aspx");

        }

       

        protected void update_Click(object sender, EventArgs e)
        {
            
           
                StaffController staff = new StaffController();
                Staff staffdetail = staff.GetStaffByID(lblStaffID.Text);

                
                staffdetail.PhoneNumber = txtph.Text;
               
                staff.updateStaff(staffdetail);

                lbl_msg.Text = "Ph No. Successfully updated";


               
            }

          
        }
        }
    
