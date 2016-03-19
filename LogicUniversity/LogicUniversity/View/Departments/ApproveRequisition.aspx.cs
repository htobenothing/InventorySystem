using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

using BL.Departments;
using BL.Departments.Model;

namespace LogicUniversity.View.Departments
{
    public partial class ApproveRequisition : System.Web.UI.Page
    {
        ApproveRequisitionController ARC = new ApproveRequisitionController();
        


        //string deptID = "COMM";
        //string staffName = "Adam";
        //string ManagerID = "STF0003";

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
            Staff Manager = (Staff)Session["LoginStaff"];
            string deptID = Manager.Dept_ID;
            string staffName = Manager.Staff_Name;
            Panel1.Visible = false;

            List<Requisition> requisitionList = ARC.getAllDeptSubmitReq(deptID);
            Session["SubmittedRequisition"] = requisitionList;
            if (requisitionList.Count == 0)
            {
                lblCheckRequisition.Text = "There is no Requisition to be approved.";
                btnApproveAll.Visible = false;
                GridView1.Visible = false;
            }
            else
            {
                GridView1.DataSource = ARC.ShowReqTable(ARC.getAllDeptSubmitReq(deptID));
                GridView1.DataBind();
            }            
        }

        protected void btnApproveAll_Click(object sender, EventArgs e)
        {
            Staff Manager = (Staff)Session["LoginStaff"];
            string deptID = Manager.Dept_ID;
            string staffName = Manager.Staff_Name;
            string ManagerID = Manager.Staff_ID;

            List<Requisition> ReqList = new List<Requisition>();
            ReqList = ARC.getAllDeptSubmitReq(deptID);

            for (int i = 0; i < ReqList.Count; i++)
            {

                string rq = ReqList[i].Requisition_ID;

                ARC.ApproveRequisition(rq, ManagerID);

            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Approve all successs!')</script>");
            PostBack();

            //Response.Redirect("~/View/Departments/ApproveRequisition.aspx");
        
        }

       

        public void btnReject_Click1(object sender, EventArgs e)
        {
            Staff Manager = (Staff)Session["LoginStaff"];

            string ManagerID = Manager.Staff_ID;

            String rId = lblRequisitionID.Text;
            String RejectReason = txtRejectReason.Text;
            ARC.RejectRequisition(rId, ManagerID, RejectReason);//ManagerId=login user id
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script1", "<script>alert('Reject successsful!')</script>");
            //Response.Redirect("~/View/Departments/ApproveRequisition.aspx");
            PostBack();

        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            Staff Manager = (Staff)Session["LoginStaff"];

            string ManagerID = Manager.Staff_ID;

            String rId = lblRequisitionID.Text;
            ARC.ApproveRequisition(rId, ManagerID);//ManagerId=login user id//ManagerId=login user id
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script1", "<script>alert('Approve successsful!')</script>");
            PostBack();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "viewdetail")
            {
                Panel1.Visible = true;
                int index = Convert.ToInt32(e.CommandArgument);
                string ReqId = GridView1.Rows[index].Cells[0].Text;
                lblRequisitionID.Text = ReqId;
                List<SubmitItemDetailsModel> sids = ARC.GetReqItemByReqID(ReqId);
                txtRejectReason.Text = RequisitionController.getReqByID(ReqId).Remark;
                Session["SubmittedItemDetails"] = sids;
                GridView2.DataSource = ARC.GetReqItemByReqID(ReqId);
                
                GridView2.DataBind();

            }

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Staff Manager = (Staff)Session["LoginStaff"];
            string deptID = Manager.Dept_ID;
            //List<Requisition> srl = (List<Requisition>)Session["SubmittedRequisition"];
            GridView1.Visible = true;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataSource = ARC.ShowReqTable(ARC.getAllDeptSubmitReq(deptID));
            GridView1.DataBind();
        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            List<SubmitItemDetailsModel> sids = (List<SubmitItemDetailsModel>)Session["SubmittedItemDetails"];
            GridView1.Visible = true;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataSource = sids;
            GridView1.DataBind();
        }

       
    }
}