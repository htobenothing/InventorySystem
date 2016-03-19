using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
using BL.Departments;

namespace LogicUniversity.Departments
{
   

    public partial class ConfirmReceived : System.Web.UI.Page
    {
        TextBox txtQty;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initPage();
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Staff stf = (Staff)Session["LoginStaff"];
            string id = txtDisbursmentID.Text;
            ViewState["DisID"] = id;
            if (ConfirmReceivedController.checkDisbursmentID(id, stf.Staff_ID) == 1)
            {
                LBStatus.Text = "";

                dgvDisbursmentItem.DataSource = ConfirmReceivedController.getDisbursmentItems(txtDisbursmentID.Text);
                dgvDisbursmentItem.DataBind();
                dgvDisbursmentItem.Visible = true;
                cbxReserved.Visible = true;
                btnConfirm.Visible = true;
            }
            else if (ConfirmReceivedController.checkDisbursmentID(id, stf.Staff_ID) == 2)
                LBStatus.Text = "This disbursement list has been received.";
            else if (ConfirmReceivedController.checkDisbursmentID(id, stf.Staff_ID) == 0)
                LBStatus.Text = "Sorry, the disbursement ID is wrong.";
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            Staff stf = (Staff)Session["LoginStaff"];
            string DisId = (string)ViewState["DisID"];
            List<DisbursementDetail> DDList = new List<DisbursementDetail>();
            List<ReceivedItemModel> recItemList = new List<ReceivedItemModel>();

            for (int i = 0; i < dgvDisbursmentItem.Rows.Count; i++)
            {
                string itemCode = dgvDisbursmentItem.Rows[i].Cells[0].Text;
                string itemName = dgvDisbursmentItem.Rows[i].Cells[1].Text;
                int needQty = Convert.ToInt32(dgvDisbursmentItem.Rows[i].Cells[2].Text);
                txtQty = (TextBox)dgvDisbursmentItem.Rows[i].Cells[3].FindControl("txtQty");
                int recQty = Convert.ToInt32(txtQty.Text);
                string uom = dgvDisbursmentItem.Rows[i].Cells[4].Text;

                DisbursementDetail dd = new DisbursementDetail();
                dd.Disbursement_ID = DisId;
                dd.Item_ID = itemCode;
                dd.Quantity = recQty;
                DDList.Add(dd);

                ReceivedItemModel recItem = new ReceivedItemModel();
                recItem.itemCode = itemCode;
                recItem.itemName = itemName;
                recItem.needQty = needQty;
                recItem.recQty = recQty;
                recItem.restQty = needQty - recQty;
                recItem.uom = uom;
                recItemList.Add(recItem);
            }


            if (cbxReserved.Checked)
            {
                lblReqtype.Visible = true;
                dgvSpeRequisition.Visible = true;
                btnCreate.Visible = true;
                btnCancel.Visible = true;


                List<ReceivedItemModel> restItemList = new List<ReceivedItemModel>();
                foreach (ReceivedItemModel recitem in recItemList)
                {
                    if (recitem.restQty > 0)
                    {
                        restItemList.Add(recitem);
                    }
                }
                Session["restItemList"] = restItemList;
                dgvSpeRequisition.DataSource = restItemList;
                dgvSpeRequisition.DataBind();

                try
                {
                    ConfirmReceivedController.ConfirmRecivedItems(DisId, DDList, stf.Dept_ID);
                }
                catch (Exception ex)
                {
                    btnConfirm.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Receive disbursement fail！')", true);
                    

                }
                btnConfirm.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Received success!')", true);
            }
            else
            {
                try
                {
                    ConfirmReceivedController.ConfirmRecivedItems(DisId, DDList, stf.Dept_ID);
                }
                catch (Exception ex)
                {
                    btnConfirm.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Receive disbursement fail！')", true);
                    initPage();

                }

                btnConfirm.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Received success!')", true);
                initPage();
            }
            
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            List<ReceivedItemModel> restItemList = (List<ReceivedItemModel>)Session["restItemList"];
            List<RequisitionItem> reqItemList = new List<RequisitionItem>();
            if (restItemList.Count > 0)
            {
                foreach (ReceivedItemModel restItem in restItemList)
                {
                    RequisitionItem reqItem = new RequisitionItem();
                    reqItem.Item_ID = restItem.itemCode;
                    reqItem.Required_Qty = restItem.restQty;
                    reqItemList.Add(reqItem);
                }
               
                Staff stf = (Staff)Session["LoginStaff"];
                string type = "Special";
                string status = "Approved";
                RequisitionController.createRequisition(reqItemList, stf.Staff_ID, type, status);
                Session["restItemList"] = "";

            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Create success!')", true);
            initPage();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            initPage();
        }

        protected void initPage()
        {
            Session["restItemList"] = "";
            lblReqtype.Visible = false;
            lblSpecial.Visible = false;
            btnCreate.Visible = false;
            btnCancel.Visible = false;
            dgvSpeRequisition.Visible = false;
            dgvDisbursmentItem.Visible = false;
            btnConfirm.Visible = false;
            cbxReserved.Visible = false;
        }



        protected void dgvSpeRequisition_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            List<ReceivedItemModel> restItemList = (List<ReceivedItemModel>)Session["restItemList"];
            if (e.CommandName == "DeleteItem")
            {
                if(dgvSpeRequisition.Rows.Count == 1)
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You should at least submit one itme.')", true);
                else
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    restItemList.RemoveAt(index);
                }             
            }
            Session["restItemList"] = restItemList;
            dgvSpeRequisition.DataSource = restItemList;
            dgvSpeRequisition.DataBind();
        }

    }
}