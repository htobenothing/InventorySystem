using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
using BL.Departments;
using BL.Store;


namespace LogicUniversity.View.Departments
{
    public partial class DelegateStaff : System.Web.UI.Page
    {
        DelegateAuthorityController dac = new DelegateAuthorityController();
        TimeConvertController TC = new TimeConvertController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginStaff"] != null)
            {
                if (!IsPostBack)
                {
                    PostBack();
                }
            }
            
        }

        protected void PostBack()
        {
            PanelDetail.Visible = false;
            //Staff sf = (Staff)Session["UserEntity"];
            //if (sf == null || sf.Role_ID != "manager")
            //{
            //    Response.Redirect("../NoStaffPage.aspx");
            //}
            Staff manager = (Staff)Session["LoginStaff"];
            List<Staff> AllStaff = dac.getAllStaffExpManager(manager.Dept_ID);
            Session["AllStaffs"] = AllStaff;

            GridView1.DataSource = dac.getAllStaffExpManager(manager.Dept_ID);
            GridView1.DataBind();



            for (int i = 0; i < AllStaff.Count; i++)
            {
                string role = dac.getSfRoleIdBySfId(GridView1.Rows[i].Cells[1].Text);

                if (role == "AuthStaff")
                {
                    Button btn = (Button)GridView1.Rows[i].Cells[2].FindControl("DelegateBtn");
                    btn.Text = "Undelegate";
                }
                else if (role == "Staff")
                {
                    Button btn = (Button)GridView1.Rows[i].Cells[2].FindControl("DelegateBtn");
                    btn.Text = "Delegate";

                }
            }
        }
        //grid view button event


        protected void DelegateConfirmBtn_Click(object sender, EventArgs e)
        {
            if (txtStartDate.Text == "" || txtEndDate.Text=="")
            {
                lblfrmDate.Text = "The Start Date and End Date can not be empty.";

            }
            else
            {
                lblfrmDate.Text = "";
                Staff manager = (Staff)Session["LoginStaff"];
                String sfid = (string)ViewState["delegateStaffID"];
                Staff sf = dac.getStaffBySfId(sfid);
                if (sf.Role_ID == "Staff")
                {

                    DateTime sd = TC.DateConvert(txtStartDate.Text);
                    DateTime ed = TC.DateConvert(txtEndDate.Text);
                    if (DateTime.Compare(sd, ed) > 0)
                    {
                        LBStatus.Text = "End date should be later than Start date";
                    }
                    else if (DateTime.Compare(sd, DateTime.Now) <= 0)
                        LBStatus.Text = "Start date should be later than today";
                    else
                    {
                        LBStatus.Text = "";
                        dac.createDelegateHistory(sfid, manager.Staff_ID, sd, ed);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Delegate success!')</script>");
                        PostBack();
                    }

                }
                else
                {

                    dac.updateDelegateHistory(sfid);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Undelegate success!')</script>");
                    PostBack();
                }
            }
            
            
        }

        protected void DelegateBtn_Click(object sender, EventArgs e)
        {
            GridViewRow r = (sender as Button).NamingContainer as GridViewRow;//Get the row which has changed
            string SfId = r.Cells[1].Text;
            ViewState["delegateStaffID"] = SfId;
            string role = dac.getSfRoleIdBySfId(SfId);
            PanelDetail.Visible = true;
            if (role == "AuthStaff")
            {
                string sfname = dac.getSfNameBySfId(SfId);

                lblStaffName.Text = sfname;

                DelegateHistory sh = dac.getDelegateHistory(SfId);
                txtStartDate.Text = sh.Start_Date.ToString("dd/MM/yyyy");
                txtEndDate.Text = sh.End_Date.ToString("dd/MM/yyyy");
                btnDelgtConfirm.Text = "Confirm Undelegate";
                
                        

            }
            else
            {
                string sfname = dac.getSfNameBySfId(SfId);

                lblStaffName.Text = sfname;
                txtStartDate.Text = "";
                txtEndDate.Text = "";
                btnDelgtConfirm.Text = "Confirm Delegate";
                
                        

            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            List<Staff> sfl = (List<Staff>)Session["AllStaffs"];
            GridView1.Visible = true;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataSource = sfl;
            GridView1.DataBind();
        }
    }
}