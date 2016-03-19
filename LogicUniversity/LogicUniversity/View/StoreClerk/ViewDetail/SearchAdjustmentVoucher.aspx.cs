using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections.Specialized;
using BL;
using BL.Store;

namespace LogicUniversity.StoreClerk.View
{
    public partial class AdjustmentVoucher : System.Web.UI.Page
    {
        TimeConvertController TC = new TimeConvertController();
        List<string> statusList = new List<string>();
        ViewAdjustmentVoucherController adjControl = new ViewAdjustmentVoucherController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {

                statusList.Add("Pending");
                statusList.Add("Approved");
                statusDropdownlist.DataSource = statusList;
                statusDropdownlist.DataBind();
                statusDropdownlist.Items.Insert(0, "--select Status--");
                remarkArea.Visible = false;
                detailLbl.Visible = false;
                remarkLbl.Visible = false;

            }
        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {
            List<Adjustment> adjList = new List<Adjustment>();
            DateTime fromdate = TC.DateConvert(fromDate.Text);
            DateTime todate = TC.DateConvert(toDate.Text);
            if (DateTime.Compare(fromdate, todate) > 0)
                LBDateAlert.Text = "To date should be later than From date!";
            else
            {
                LBDateAlert.Text = "";
                string status = statusDropdownlist.SelectedItem.Text;
                if (statusDropdownlist.SelectedIndex > 0)
                {
                    adjList = adjControl.GetVouchersByDateAndStatus(fromdate, todate, status);
                }
                else
                {
                    adjList = adjControl.GetVouchersByDate(fromdate, todate);
                }


                // adjList = adjControl.GetVouchersByDateAndStatus(fromdate, todate, status);
                int x = adjList.Count;
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("voucher_Id", typeof(string)));
                dt.Columns.Add(new DataColumn("IssueDate", typeof(DateTime)));
                dt.Columns.Add(new DataColumn("IssueBy", typeof(string)));
                dt.Columns.Add(new DataColumn("status", typeof(string)));
                foreach (Adjustment list in adjList)
                {
                    DataRow dr = dt.NewRow();
                    dr["voucher_Id"] = list.Voucher_ID;
                    
                    dr["IssueDate"] = list.Issue_Date;
                    string issueBy = adjControl.getIssueByName(list.IssuedByStaff_ID);
                    dr["IssueBy"] = issueBy;
                    dr["status"] = list.Status;
                    dt.Rows.Add(dr);
                }
                int y = dt.Rows.Count;
                dt.AcceptChanges();
                Session["CurrentTable"] = dt;
                if (y != 0)
                {
                    LBStatus.Visible = false;
                    vouchersGridView.DataSource = dt;
                    vouchersGridView.DataBind();
                }
                else
                    LBStatus.Visible = true;
                remarkArea.Visible = false;
                detailLbl.Visible = false;
                remarkLbl.Visible = false;

            }
           
        }

        protected void vouchersGridView_RowCommand(object sender, GridViewCommandEventArgs e) {         
         
            if (e.CommandName == "ShowDetail")
            {
                string voucherId = "";
                List<string> itemCodeList = new List<string>();

                string s=(string)e.CommandArgument;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow selectedRow = vouchersGridView.Rows[index];
                voucherId = selectedRow.Cells[0].Text;

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("Category", typeof(string)));
                dt.Columns.Add(new DataColumn("Code", typeof(string)));
                dt.Columns.Add(new DataColumn("Description", typeof(string)));
                dt.Columns.Add(new DataColumn("Price", typeof(double)));
                dt.Columns.Add(new DataColumn("QtyAdj", typeof(Int32)));
                itemCodeList = adjControl.GetAllItemCodeByVoucherId(voucherId);
                int a = itemCodeList.Count;

                foreach (string itemcode in itemCodeList)
                {
                    DataRow dr = dt.NewRow();
                    string category = adjControl.GetItemcaegoryByCode(itemcode);
                    string description = adjControl.GetItemDescByCode(itemcode);
                    double price = adjControl.GetItemPrice(itemcode);
                    int adjQty = adjControl.GetAdjQty(voucherId, itemcode);
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
                VoucherDetailGridView.DataSource = dt;
                VoucherDetailGridView.DataBind();
                int x = VoucherDetailGridView.Rows.Count;
                ViewState["voucherid"] = voucherId;
                string remark = adjControl.GetRemarkByVoucherId(voucherId);
                detailLbl.Visible = true;
                remarkLbl.Visible = true;
                remarkArea.Visible = true;
                remarkArea.Value = remark;
            }
            
        }

        protected void vouchersGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = (DataTable) Session["CurrentTable"];
            //vouchersGridView.Visible = true;
            
            vouchersGridView.DataSource = dt;
            vouchersGridView.PageIndex = e.NewPageIndex;
            vouchersGridView.DataBind();
            
        }        
    }
             
}
    
