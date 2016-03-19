using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using BL.Store;
using BL;
using System;

namespace LogicUniversity.StoreClerk.View
{
    public partial class ViewDepartment : System.Web.UI.Page
    {
        DepartmentController dept = new DepartmentController();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                ddlDept.DataTextField = "Dept_Name";
                ddlDept.DataValueField = "Dept_ID";
                ddlDept.DataSource = dept.GetAllDept();
                ddlDept.DataBind();
                ddlDept.Items.Insert(0, "Select Dept Name");


                //ddlCP.DataTextField = "Collection_ID";
                //ddlCP.DataValueField = "Collection_ID";
                //ddlCP.DataSource = dept.GetAllCollectionPt();
                //ddlCP.DataBind();
                //ddlCP.Items.Insert(0, "Select CollectionPoint");

            }
        }

        protected void ShowBtn_Click(object sender, EventArgs e)
        {
            PanelDetail.Visible = false;

            if (ddlDept.SelectedIndex == 0)
            {
                mesglbl.Text = "Please Select Data";
                gvDept.DataSource = null;
                gvDept.DataBind();

            }

            else
            {
                mesglbl.Text = "";
                String code = ddlDept.SelectedValue;

                Department DeptIDList = dept.GetDeptByid(code);

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("Dept_ID", typeof(string)));

                dt.Columns.Add(new DataColumn("Dept_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("CollectionPoint", typeof(string)));

                DataRow dr = dt.NewRow();
                dr["Dept_ID"] = DeptIDList.Dept_ID;


                dr["Dept_Name"] = DeptIDList.Dept_Name;
                dr["CollectionPoint"] = DeptIDList.Collection_ID;
                dt.Rows.Add(dr);

                dt.AcceptChanges();
                ViewState["CurrentTable"] = dt;
                gvDept.DataSource = dt;
                gvDept.DataBind();
            }
            //else
            //{
            //    List<Department> DeptIDList = new List<Department>();

            //    String code = ddlCP.SelectedValue;


            //    DeptIDList = dept.GetCPByid(code);

            //    DataTable dt = new DataTable();
            //    dt.Columns.Add(new DataColumn("Dept_ID", typeof(string)));

            //    dt.Columns.Add(new DataColumn("Name", typeof(string)));
            //    dt.Columns.Add(new DataColumn("ColllectionPoint", typeof(string)));

            //    foreach (Department list in DeptIDList)
            //    {
            //        DataRow dr = dt.NewRow();
            //        dr["Dept_ID"] = list.Dept_ID;
            //        dr["Name"] = list.Dept_Name;

            //        dr["ColllectionPoint"] = list.Collection_ID;

            //        dt.Rows.Add(dr);
            //    }

            //    dt.AcceptChanges();
            //    ViewState["CurrentTable"] = dt;
            //    gvDept.DataSource = dt;
            //    gvDept.DataBind();

            //}
            
            
            
            
           
        }



        protected void ShowAll_Click(object sender, EventArgs e)
        {
            mesglbl.Text = "";
            ddlDept.SelectedIndex = 0;
            PanelDetail.Visible = false;

                List<Department> List = new List<Department>();
                List = dept.GetAllDept();

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("Dept_ID", typeof(string)));

                dt.Columns.Add(new DataColumn("Dept_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("CollectionPoint", typeof(string)));
                foreach (Department list in List)
                {
                    DataRow dr = dt.NewRow();
                    dr["Dept_ID"] = list.Dept_ID;


                    dr["Dept_Name"] = list.Dept_Name;
                    dr["CollectionPoint"] = list.Collection_ID;
                    dt.Rows.Add(dr);
                }
                int y = dt.Rows.Count;
                dt.AcceptChanges();
                ViewState["CurrentTable"] = dt;
                gvDept.DataSource = dt;
                gvDept.DataBind();
            
        }

      

        //protected void ShowDetailBtn_Click(object sender, EventArgs e)
        //{
        //   //// List<Department> department = new List<Department>();
        //   // Button btn = (Button)sender;
        //   // GridViewRow gvr = (GridViewRow)btn.NamingContainer;
        //   // string DeptId = gvr.Cells[0].Text;
        //   // DepartmentStaff department;


        //   // //if (DeptId == "STAT")
        //   // //{
        //   // //     department = dept.GetDeptBySTAT_RepStaff(DeptId);
           
            
        //   // //}
        //   // //else

        //   //  department = dept.GetDeptByid_RepStaff(DeptId);

           



        //   // String Code = department.Dept_ID;

        //   // String Name = department.Dept_Name;

        //   // String ContName = department.Contact_Name;

        //   // String PhoneN = department.Telephone_No;

        //   // String FaxN = department.Fax_No;

        //   // String HeadName = department.Heads_Name;

        //   // String CollectionPt = department.Collection_Desc;

        //   // String Rep_Name = department.Representative_Staff;

        //   // String Email = department.Email;

        //   // Response.Redirect(String.Format("DetailDepartment.aspx?D_Code={0}&Name={1}&ContName={2}&PhoneN={3}&FaxN={4}&HeadName={5}&CollectionPt={6}&Email={7}&Rep_Name={8}", Code, Name, ContName, PhoneN, FaxN, HeadName, CollectionPt, Email, Rep_Name));


           
        //    PanelDetail.Visible = false;
        //    List<Department> table = new List<Department>();
        //    table = dept.GetAllDept();
        //    gvDept.DataSource = table;
        //    gvDept.DataBind();
        //}

      

        protected void gvDept_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ShowDetailBtn_Click")
            {
                mesglbl.Text = "";
                PanelDetail.Visible = true;
                int index = Convert.ToInt32(e.CommandArgument);
                string deptID = gvDept.Rows[index].Cells[0].Text;
                DepartmentStaffModel d = dept.GetDeptByid_RepStaff(deptID);
                LBDeptID.Text = d.Dept_ID;
                LBDeptName.Text = d.Dept_Name;
                LBRepName.Text = d.Representative_Staff;
                LBContName.Text = d.Contact_Name;
                LBPhNo.Text = d.Telephone_No;
                LBHeadName.Text = d.Heads_Name;
                LBEmail.Text = d.Heads_Name;
                LBCD.Text = d.Collection_Desc;
                LBEmail.Text = d.Email;
            }
        }

       
    }
}