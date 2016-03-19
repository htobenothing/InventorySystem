using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
using BL.Departments;

namespace LogicUniversity.View.Departments
{
    public partial class UpdateCollectionPoint : System.Web.UI.Page
    {
        CollectionController UCDC = new CollectionController();
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Staff manager = (Staff)Session["LoginStaff"];
                string deptID = manager.Dept_ID;
                Staff Rep = UCDC.GetRepByDeptID(deptID);
                TBStaffID.Text = Rep.Staff_ID;
                LBStaffName.Text = Rep.Staff_Name;
                
                
                
                foreach (string desc in UCDC.GetAllCpDesc())
                {
                    DDLCP.Items.Add(desc);
                }
                DDLCP.SelectedValue = UCDC.getCPDesByDptID(deptID);
            }

        }

        protected void BtnChooseID_Click(object sender, EventArgs e)
        {
            Staff manager = (Staff)Session["LoginStaff"];
            string deptID = manager.Dept_ID;
            if (UCDC.GetStaffNameByStaffID(TBStaffID.Text, deptID) == null)
            {
                LBStaffName.Text = "No such staff in this department!";
                LBStaffName.ForeColor = System.Drawing.Color.Red;
            }
            else 
            {
                Staff stf =(Staff) UCDC.GetStaffNameByStaffID(TBStaffID.Text, deptID);
                if(stf.Role_ID == "Manager" || stf.Role_ID=="AuthStaff"){

                    LBStaffName.Text = " AuthStaff/Manager Can not be RepStaff!";
                    LBStaffName.ForeColor = System.Drawing.Color.Red;
                }
                else
                    LBStaffName.Text = UCDC.GetStaffNameByStaffID(TBStaffID.Text, deptID).Staff_Name;
            }
            
        }

        protected void BtnReverseStaff_Click(object sender, EventArgs e)
        {
            Staff manager = (Staff)Session["LoginStaff"];
            string deptID = manager.Dept_ID;
            
            Staff Rep = UCDC.GetRepByDeptID(deptID);
            TBStaffID.Text = Rep.Staff_ID;
            LBStaffName.Text = Rep.Staff_Name;
            LBStaffName.ForeColor = System.Drawing.Color.Black;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Staff manager = (Staff)Session["LoginStaff"];
            string deptID = manager.Dept_ID;
            
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                Staff Rep = UCDC.GetRepByDeptID(deptID);
                if (UCDC.GetStaffNameByStaffID(TBStaffID.Text, deptID) != null)
                {
                    bool flag = UCDC.ChangeRepToStaff(Rep.Dept_ID);
                    if (flag == true)
                    {
                        flag = UCDC.ChangeStaffToRep(TBStaffID.Text);
                        if (flag == true)
                        {
                            UCDC.UpdateCollectionPoint(deptID, DDLCP.SelectedValue);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Update success!')</script>");
                        }

                    }
                    else
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Department no rep! Please check the database.')</script>");
                }
                else
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('No such staff! Please check the staff ID!')</script>");



            }

        }
    }
}