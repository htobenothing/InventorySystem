using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL.Store;
using BL.Store.TableView;
using BL;

namespace LogicUniversity.StoreClerk.View
{
    

    public partial class CollectionPointCheck : System.Web.UI.Page
    {
        ViewDpAndCpController DACC = new ViewDpAndCpController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<string> AllDep = DACC.GetAllDepName();
                List<string> AllCp = DACC.GetAllCpID();

                foreach (string depID in AllDep)
                {
                    DDLDP.Items.Add(new ListItem(depID, depID));
                }
                foreach (string cpID in AllCp)
                {
                    DDLCP.Items.Add(new ListItem(cpID, cpID));
                }
            }
        }

        protected void BtnView_Click(object sender, EventArgs e)
        {
            PanelTable.Visible = true;
            PanelDetail.Visible = false;
            if (RBCP.Checked == true)
            {
                List<CpAndDeptTableModel> table = new List<CpAndDeptTableModel>();
                table = DACC.CpAndDeptGridViewForCp(DDLCP.SelectedValue.ToString());
                if(table.Count == 0)
                {
                    LabelDesc.Text = "There is no data in search result.";
                    LabelDesc.ForeColor = System.Drawing.Color.Blue;
                }
                GridView1.DataSource = table;
                GridView1.DataBind();
            }
            if (RBDP.Checked == true)
            {
                List<CpAndDeptTableModel> table = new List<CpAndDeptTableModel>();
                table = DACC.CpAndDeptGridViewForDp(DDLDP.SelectedValue.ToString());
                GridView1.DataSource = table;
                GridView1.DataBind();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "showdetail")
            {
                PanelDetail.Visible = true;
                int index = Convert.ToInt32(e.CommandArgument);
                string deptID = GridView1.Rows[index].Cells[2].Text;
                DepartmentStaffModel d = DACC.GetDeptByid_RepStaff(deptID);
                LBDeptID.Text = d.Dept_ID;
                LBDeptName.Text = d.Dept_Name;
                LBRepName.Text = d.Representative_Staff;
                LBContName.Text = d.Contact_Name;
                LBPhNo.Text  = d.Telephone_No;
                LBHeadName.Text = d.Heads_Name;
                   LBEmail.Text = d.Heads_Name;
                   LBCD.Text = d.Collection_Desc;
                 LBEmail.Text = d.Email;
            }
        }

        protected void BtnViewall_Click(object sender, EventArgs e)
        {
            PanelTable.Visible = true;
            PanelDetail.Visible = false;
            List<CpAndDeptTableModel> table = new List<CpAndDeptTableModel>();
            table = DACC.CpAndDeptGridViewAll();
            GridView1.DataSource = table;
            GridView1.DataBind();
        }


    }
}