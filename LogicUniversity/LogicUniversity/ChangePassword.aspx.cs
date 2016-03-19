using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
using BL.Store;

namespace LogicUniversity
{
   

    public partial class ChangePassword2 : System.Web.UI.Page
    {

        public string userid = null;
        public String password = null;

              protected void Page_Load(object sender, EventArgs e)
                  {
          
                        String UserID = Convert.ToString(Session["UserID"]);
                        userid = UserID;

                        StaffController staff = new StaffController();
                        Staff staffdetail = staff.GetStaffByID(userid);
                        password = staffdetail.Password;
                        txt_cpassword.Attributes["type"] = "password";
                        txt_npassword.Attributes["type"] = "password";

                        
                     }


                protected void btn_update_Click(object sender, EventArgs e)
                {
                    StaffController staff = new StaffController();
                    Staff staffdetail = staff.GetStaffByID(userid);
                    password = staffdetail.Password;



                    staffdetail.Staff_ID = staffdetail.Staff_ID;
                    staffdetail.Staff_Name = staffdetail.Staff_Name;
                    staffdetail.Email = staffdetail.Email;
                    staffdetail.PhoneNumber = staffdetail.PhoneNumber;
                    staffdetail.Password = txt_ccpassword.Text;

                    staff.updateStaff(staffdetail);

                    txt_cpassword.Text = null;
                    txt_npassword.Text = null;



                    lblUpadate.Text = "Successfully updated";
                }

                protected void Back_Click(object sender, EventArgs e)
                {
                    Response.Redirect("UpdateProdile.aspx?UserID=" + Session["UserID"]);
                }

                protected void txt_cpassword_TextChanged(object sender, EventArgs e)
                {
                    txt_cpassword.Visible = true;
                    if (password != txt_cpassword.Text)
                    {
                        lblCP.Text = "Password do not match!";
                        txt_npassword.ReadOnly = true;
                        txt_ccpassword.ReadOnly=true;

                    }
                    else
                    {
                        txt_cpassword.Text = password;
                        lblCP.Text = "";
                        txt_npassword.ReadOnly = false;
                        txt_ccpassword.ReadOnly = false;
                    }
                }

                protected void txt_npassword_TextChanged(object sender, EventArgs e)
                {
                    StaffController staff = new StaffController();
                    Staff staffdetail = staff.GetStaffByID(userid);
                    password = staffdetail.Password;


                    if (txt_npassword.Text == password)
                    {
                        lblSame.Text = "should not be same as current Password!";
                        txt_ccpassword.ReadOnly = true;


                    }

                    else
                    {
                        lblSame.Text = "";
                        txt_ccpassword.ReadOnly = false;
                    }

                }

               
                
    }
}