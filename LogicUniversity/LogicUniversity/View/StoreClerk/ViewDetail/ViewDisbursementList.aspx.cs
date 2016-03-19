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
    public partial class ViewDisbursementList : System.Web.UI.Page
    {
        ViewDisbursementController control = new ViewDisbursementController();
        TimeConvertController TC = new TimeConvertController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                disbursementDetailView.DataSource = null;
                disbursementDetailView.DataBind();
                deptNameLbl.Visible = false;
                deptNameValLbl.Visible = false;
            }
            
        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {
            
            List<Disbursement> disbusementList = new List<Disbursement>(); 
            DateTime fromdate = TC.DateConvert(fromDate.Text);
            DateTime todate = TC.DateConvert(toDate.Text);
            if (DateTime.Compare(fromdate, todate) > 0)
                LBDateAlert.Text = "To date should be later than From date!";
            else
            {
                LBDateAlert.Text = "";
                string status = statusdropdownalist.SelectedItem.Text;
                string deptName = departmentDropDownList.SelectedItem.Text;
                if (departmentDropDownList.SelectedIndex > 0 && statusdropdownalist.SelectedIndex > 0)
                {

                    string deptId = control.getDeptID(deptName);
                    disbusementList = control.GetDisbursementByDateDeptAndStatus(fromdate, todate, deptId, status);

                }
                else
                {
                    if (statusdropdownalist.SelectedIndex > 0)
                    {
                        disbusementList = control.GetDisbursementByDateAndStatus(fromdate, todate, status);
                    }
                    else
                    {
                        if (departmentDropDownList.SelectedIndex > 0)
                        {
                            string deptId = control.getDeptID(deptName);
                            disbusementList = control.GetDisbursementByDateAndDept(fromdate, todate, deptId);
                        }
                        else
                        {
                            disbusementList = control.GetDisbursementByDate(fromdate, todate);
                        }
                    }
                }
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("discriptionId", typeof(string)));
                dt.Columns.Add(new DataColumn("collectionDesc", typeof(string)));
                dt.Columns.Add(new DataColumn("CreateDate", typeof(DateTime)));
                dt.Columns.Add(new DataColumn("ReceiveDate", typeof(DateTime)));
                dt.Columns.Add(new DataColumn("status", typeof(string)));
                foreach (Disbursement list in disbusementList)
                {
                    
                    DataRow dr = dt.NewRow();
                    dr["discriptionId"] = list.Disbursement_ID;
                    string deptID = list.Dept_ID;
                    dr["collectionDesc"] = control.GetCollectionDesc(deptID);
                    dr["CreateDate"] = list.Create_Date;
                    
                    dr["status"] = list.Status;
                    if (list.Receive_Date == null)
                    {
                        dr["ReceiveDate"] = DBNull.Value;
                    }
                    else
                    {
                        dr["ReceiveDate"] = list.Receive_Date;
                    }
                    dt.Rows.Add(dr);
                }
                int y = dt.Rows.Count;
                dt.AcceptChanges();
                Session["CurrentTable"] = dt;
                if (y > 0)
                {
                    LBStatus.Visible = false;
                    disbursementListView.DataSource = dt;
                    disbursementListView.DataBind();
                }
                else
                    LBStatus.Visible = true;
                
            }
            
        }
        protected void DisbursementListGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
            
            if (e.CommandName == "ShowDetail")
            {
                deptNameLbl.Visible = true;
                string disbursementId = "";

                string s = (string)e.CommandArgument;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow selectedRow = disbursementListView.Rows[index];
                disbursementId = selectedRow.Cells[0].Text;


                List<DisbursementDetail> disbursementDetailList = new List<DisbursementDetail>();
                disbursementDetailList = control.getDisbursementDetailById(disbursementId);
                string deptName = control.getDeptNameByDisId(disbursementId);
                deptNameValLbl.Text = deptName;
                deptNameValLbl.Visible = true;
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("itemId", typeof(string)));
                dt.Columns.Add(new DataColumn("itemDesc", typeof(string)));
                dt.Columns.Add(new DataColumn("qty", typeof(Int32)));
                foreach (DisbursementDetail disbursementDetail in disbursementDetailList)
                {
                    DataRow dr = dt.NewRow();
                    dr["itemId"] = disbursementDetail.Item_ID;
                    dr["itemDesc"] = control.getItemDescById(disbursementDetail.Item_ID);
                    dr["qty"] = disbursementDetail.Quantity;
                    dt.Rows.Add(dr);

                }
                Session["DetailTable"] = dt;
                disbursementDetailView.DataSource = dt;
                disbursementDetailView.DataBind();
                dt.Clear(); 

            }

                     
        }

        protected void disbursementListView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = (DataTable)Session["CurrentTable"];
            //disbursementListView.Visible = true;
            
            disbursementListView.DataSource = dt;
            disbursementListView.PageIndex = e.NewPageIndex;
            disbursementListView.DataBind();
        }

      }
  }
