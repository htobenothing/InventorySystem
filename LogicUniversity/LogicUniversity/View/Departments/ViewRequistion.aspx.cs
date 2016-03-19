using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL.Departments;
using BL;
using BL.Store;

namespace LogicUniversity.View.Departments
{
    

    public partial class ViewRequistion : System.Web.UI.Page
    {
        TimeConvertController TC = new TimeConvertController();
        protected void Page_Load(object sender, EventArgs e)
        {
            initPage();
        }

        private void initPage()
        {
            detailPanel.Visible = false;
            lblRequisition.Visible = false;
            dgvDetail.Visible = false;
            dgvfix.Visible = false;
            dgvList.Visible = false;
            btnResubmit.Visible = false;
            lblReq.Visible = false;
           
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Staff loginStaff = (Staff)Session["loginStaff"];
            lblReqResult.Text = "";
            string reqID = txtID.Text;
            if (reqID != "")
            {
                if (RequisitionController.checkReqByStaffID(reqID, loginStaff.Staff_ID))
                {

                    setdgvDetailData(reqID);

                }
                else
                {
                    lblReqResult.Text = "Requisition invalid, Please check!";
                }


            }
            else
            {
                string fromDate = txtFromDate.Text;
                string toDate = txtToDate.Text;
                if (fromDate == "" || toDate == "")
                {
                    LBStatus.Text = "Please select both From date and To date.";
                }
                else if (DateTime.Compare(TC.DateConvert(fromDate), TC.DateConvert(toDate)) > 0)
                    LBStatus.Text = "From date should be later than To date";
                else
                {
                    Staff stf = (Staff)Session["LoginStaff"];
                    LBStatus.Text = "";
                    string status = dplStatus.SelectedValue;
                    List<Requisition> reqList = RequisitionController.getReqByDate_Status(stf.Staff_ID,fromDate, toDate, status);
                    Session["reqList"] = reqList;

                    initPage();
                    dgvList.Visible = true;
                    dgvList.DataSource = reqList;
                    dgvList.DataBind();
                }
            }
        }

        protected void dgvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetail")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string reqid = dgvList.Rows[index].Cells[0].Text;

                dgvList.Visible = true;
                setdgvDetailData(reqid);

            }
        }
        public void setdgvDetailData(string reqID)
        {

            lblRequisition.Visible = true;
            lblReq.Visible = true;
            lblReq.Text = reqID;
            detailPanel.Visible = true;
            Requisition req = RequisitionController.getReqByID(reqID);
            txtReason.Text = req.Remark;
            if (req.Status == "Rejected")
            {
                dgvDetail.Visible = true;
                
                List<RequisitionViewModel> reqVMList = RequisitionController.getRequisitionVM(reqID);
                lblReq.Text = reqID;
                dgvDetail.DataSource = reqVMList;
                dgvDetail.DataBind();
                for (int i = 0; i < dgvDetail.Rows.Count; i++)
                {
                    TextBox txtQty = (TextBox)dgvDetail.Rows[i].Cells[3].FindControl("txtQty");
                    //dgvDetail.Rows[i].Cells[0].Text = reqVMList[i].categoryID;
                    //dgvDetail.Rows[i].Cells[1].Text = reqVMList[i].ItemName;
                    //dgvDetail.Rows[i].Cells[2].Text = reqVMList[i].RequiredQty.ToString();
                    txtQty.Text = reqVMList[i].RequiredQty.ToString();
                }
                btnResubmit.Visible = true;
            }
            else
            {

                dgvfix.Visible = true;
                List<RequisitionViewModel> reqVMList = RequisitionController.getRequisitionVM(reqID);
                dgvfix.DataSource = reqVMList;
                dgvfix.DataBind();
            }


        }

        protected void dgvList_PageIndexChanged(object sender, EventArgs e)
        {

            List<Requisition> reqList = (List<Requisition>)Session["reqList"];
            dgvList.DataSource = reqList;
            dgvList.DataBind();

        }
        public void clearPage()
        {
            Session["reqList"] = "";
        }

        protected void btnResubmit_Click(object sender, EventArgs e)
        {
            List<RequisitionItem> reqList = new List<RequisitionItem>();
            string reqid = lblReq.Text;
            string status = "Submitted";
            for (int i = 0; i < dgvDetail.Rows.Count; i++)
            {
                TextBox txtQty = (TextBox)dgvDetail.Rows[i].Cells[3].FindControl("txtQty");


                string category = dgvDetail.Rows[i].Cells[0].Text;
                string ItemID = dgvDetail.Rows[i].Cells[1].Text;
                string ItemName = dgvDetail.Rows[i].Cells[2].Text;
                string UOM = dgvDetail.Rows[i].Cells[3].Text;
                int Qty = Convert.ToInt32(txtQty.Text);



                RequisitionItem reqitem = new RequisitionItem();
                reqitem.Item_ID = ItemID;
                reqitem.Required_Qty = Qty;
                reqitem.Requisition_ID = reqid;

                reqList.Add(reqitem);
            }
            try
            {
                RequisitionController.updateRequisition(reqList, reqid, status);
                clearPage();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Requisition Submit Successfully')", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Requisition Submit Failed')", true);
            }

        }

        protected void dgvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            List<Requisition> reqList = (List<Requisition>)Session["reqList"];
            dgvList.Visible = true;
            dgvList.PageIndex = e.NewPageIndex;
            dgvList.DataSource = reqList;
            dgvList.DataBind();
        }

        protected void txtID_TextChanged(object sender, EventArgs e)
        {
            lblReqResult.Text = "";
            txtFromDate.Text = "";
            txtToDate.Text = "";
            dplStatus.SelectedIndex = 0;
        }

        protected void txtFromDate_TextChanged(object sender, EventArgs e)
        {
            lblReqResult.Text = "";
            txtID.Text = "";
        }

        protected void txtToDate_TextChanged(object sender, EventArgs e)
        {
            lblReqResult.Text = "";
            txtID.Text = "";
        }

    }
}