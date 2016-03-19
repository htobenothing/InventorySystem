using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;
using BL;
using BL.Store;
using BL.Departments;

namespace LogicUniversity.Supervisor
{
    public partial class AuthorizeAdjVoucher : System.Web.UI.Page
    {
         ApproveAdjVoucherController approveAdjControl = new ApproveAdjVoucherController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                setInitialData();
                
            }
        }

    

        protected void setInitialData()
        {
           //voucherDetailView.Visible = true;
            List<Adjustment> adjList = new List<Adjustment>();
           // string staffId = "STF0013";
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("VoucherId", typeof(string)));
            dt.Columns.Add(new DataColumn("issuedate", typeof(string)));
            dt.Columns.Add(new DataColumn("staffname", typeof(string)));           
           // adjList = approveAdjControl.GetAllVoucherByStaff_Id(staffId);
            adjList = approveAdjControl.GetAllVoucherByStaff_Id();
            if (adjList.Count == 0)
            {
                LBStatus.Visible = true;
                VoucherSummaryView.Visible = false;
                voucherDetailView.Visible = false;
                PanelRemark.Visible = false;
            }
                
            else
            {
                foreach (Adjustment adj in adjList)
                {
                    string voucherId = adj.Voucher_ID;
                    DateTime issueDate = adj.Issue_Date;
                    string issueStaffId = adj.IssuedByStaff_ID;
                    string staffName = approveAdjControl.getNameByStaffId(issueStaffId);
                    DataRow dr = dt.NewRow();
                    dr["VoucherId"] = voucherId;
                    dr["issuedate"] = issueDate.ToShortDateString();
                    dr["staffname"] = staffName;
                    dt.Rows.Add(dr);
                }
                int y = dt.Rows.Count;
                dt.AcceptChanges();
                ViewState["CurrentTable"] = dt;
                VoucherSummaryView.DataSource = dt;
                VoucherSummaryView.DataBind();
                remarkLbl.Visible = false;
                remarkTxtArea.Visible = false;
                approveBtn.Visible = false;
                voucherDetailView.Visible = false;
            }
            
        }

       
        protected void VoucherSummaryView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            voucherDetailView.Visible = true;
             string voucherId="";
            List<string> itemCodeList = new List<string>();
            if (e.CommandName == "VoucherDetail") {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow selectedRow = VoucherSummaryView.Rows[index];
                voucherId=selectedRow.Cells[0].Text;
                remarkLbl.Visible = true;
                remarkTxtArea.Visible = true;
                approveBtn.Visible = true;
            }
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Category", typeof(string)));
            dt.Columns.Add(new DataColumn("Code", typeof(string)));
            dt.Columns.Add(new DataColumn("Description", typeof(string)));
            dt.Columns.Add(new DataColumn("Price", typeof(double)));
            dt.Columns.Add(new DataColumn("QtyAdj", typeof(Int32)));
            itemCodeList = approveAdjControl.GetAllItemCodeByVoucherId(voucherId);
            int a = itemCodeList.Count;
            foreach(string itemcode in itemCodeList) {
                DataRow dr = dt.NewRow();
                string category = approveAdjControl.GetItemcaegoryByCode(itemcode);
                string description = approveAdjControl.GetItemDescByCode(itemcode);
                double price = approveAdjControl.GetItemPrice(itemcode);
                int adjQty = approveAdjControl.GetAdjQty(voucherId, itemcode);               
                dr["Category"] = category;
                dr["Code"] = itemcode;
                dr["Description"] = description;
                dr["Price"] = price;
                dr["QtyAdj"] = adjQty;
                dt.Rows.Add(dr);
            }
            dt.AcceptChanges();
            int y = dt.Rows.Count;           
            ViewState["DetailTable"] = dt;            
            voucherDetailView.DataSource = dt;
            voucherDetailView.DataBind();          
            int x = voucherDetailView.Rows.Count;
            ViewState["voucherid"] = voucherId;
            string remark = approveAdjControl.GetRemarkByVoucherId(voucherId);
            remarkTxtArea.Value = remark;           
        }

        protected void voucherDetailView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Colouring GridView Row based on condition
                if (Convert.ToDouble(e.Row.Cells[3].Text) > 250)
                {
                    e.Row.BackColor = System.Drawing.Color.Red;
                    e.Row.ForeColor = System.Drawing.Color.White;
                }


            }
        }

        protected void approveBtn_Click(object sender, EventArgs e)
        {
            Staff supv = (Staff)Session["LoginStaff"];
            string superviserID = supv.Staff_ID; ;
            string voucherId = ViewState["voucherid"].ToString();
            DataTable dt = (DataTable)ViewState["DetailTable"];
            List<double> priceList = new List<double>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string code = dt.Rows[i]["Code"].ToString();
                
                string reason = "Adjustment "+"by reson: "+remarkTxtArea.Value;
                int qty = Convert.ToInt32(dt.Rows[i]["QtyAdj"].ToString());
                DateTime updateDate = DateTime.Now;               
                 approveAdjControl.AdjIntoItemTxn(voucherId, code, updateDate, reason, qty,superviserID);
                 double d =Convert.ToDouble(dt.Rows[i]["Price"].ToString());
                 priceList.Add(d);

            }
            
            
            
           // System.Windows.Forms.MessageBox.Show("Adjustment has been approved!!!");
            Page.ClientScript.RegisterStartupScript(this.GetType(),"script","<script>alert('Adjustment has been approved!')</script>");           
            setInitialData();//changes  
            foreach (double p in priceList)
            {
                if (p > 250)
                {
                    SendNotificationController.SendNotificaition(supv, voucherId);
                    break;
                }
            }
        }

    }
}