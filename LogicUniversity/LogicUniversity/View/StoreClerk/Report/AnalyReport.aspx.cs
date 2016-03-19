using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
using BL.Report;
using Microsoft.Reporting.WebForms;

namespace ADProject_Team1.View.StoreClerk.Report
{
    public partial class AnalyReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initPage();
            }
            
        }

        private void initPage()
        {


            
            lblStrategy.Visible = false;
            pnMonthDep.Visible = false;
            lblMessage.Visible = false;
            ddlStrategy.Visible = false;
        }


        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            initPage();
            initReport();
            txtDate.Text = "";
            if (ddlType.SelectedValue == "Month Report")
            {
                ddlStrategy.Visible = true;
                ddlStrategy.SelectedIndex = 0;
                lblStrategy.Visible = true;
                btnSearch.Visible = false;
            }
            else
            {
                if (ddlType.SelectedValue == "Year Report")
                {
                    lblDate.Visible = true;
                    ddlType.Visible = true;
                    btnSearch.Visible = true;
                }
            }
        }

        protected void ddlStrategy_SelectedIndexChanged(object sender, EventArgs e)
        {
            initPage();
            initReport();
            btnSearch.Visible = true;
            if (ddlStrategy.SelectedValue == "Store")
            {

                lblDate.Visible = true;
                txtDate.Visible = true;
                lbltype.Visible = true;
                ddlType.Visible = true;
                lblStrategy.Visible = true;
                ddlStrategy.Visible = true;
            }
            else
            {
                if (ddlStrategy.SelectedValue == "Department")
                {
                    lbltype.Visible = true;
                    ddlType.Visible = true;
                    lblStrategy.Visible = true;
                    ddlStrategy.Visible = true;
                    pnMonthDep.Visible = true;


                    ddlCategory.DataSource = ReportController.getAllCategory();
                    ddlCategory.DataTextField = "Category_ID";
                    ddlCategory.DataTextField = "Category_ID";
                    ddlCategory.DataBind();


                    List<Department> depList = ReportController.getAllDepartment();
                    ddlDepName.DataSource = depList;
                    ddlDepName.DataTextField = "Dept_Name";
                    ddlDepName.DataValueField = "Dept_ID";
                    ddlDepName.DataBind();




                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            initReport();
            if (txtDate.Text != "")
            {
                ReportViewer.Visible = true;
                ReportDataSource rds = new ReportDataSource();
                ReportDataSource rds1 = new ReportDataSource();
                ReportDataSource rds2 = new ReportDataSource();
                string date = txtDate.Text;
                List<string> dateList = new List<string>();
                foreach (string i in date.Split('-'))
                {
                    dateList.Add(i);
                }
                int year = Convert.ToInt32(dateList[0]);
                int month = 1;
                if (dateList.Count >1)
                {
                    month = Convert.ToInt32(dateList[1]);
                }
              
                DateTime nowdate = new DateTime(year, month, 1);
                ReportViewer.ProcessingMode = ProcessingMode.Local;

                string basepath = "/LogicUniversity/View/StoreClerk/Report/";
                string detailpath = "";
                string path = "";
                string cate = ddlCategory.SelectedValue;
                string depid = ddlDepName.SelectedValue;

                switch (ddlType.SelectedValue)
                {
                    case "Month Report":
                        basepath = basepath + "MonthReport/";
                        if (ddlStrategy.SelectedValue == "Department")
                        {
                            detailpath = "department.rdlc";
                            rds = new ReportDataSource("Top5Item", ReportController.getTop5ReqItemByDate_Category_Dep(nowdate, cate, depid));
                        }
                        else
                        {
                            if (ddlStrategy.SelectedValue == "Store")
                            {
                                detailpath = "store.rdlc";
                                rds = new ReportDataSource("DepReq", ReportController.getReqByDep(nowdate));
                                rds1 = new ReportDataSource("SupplierDur", ReportController.getSupplyDurByMonth(nowdate));
                                ReportViewer.LocalReport.DataSources.Clear();
                                ReportViewer.LocalReport.DataSources.Add(rds1);
                                
                            }
                        }

                        break;

                    case "Year Report":
                        
                        basepath = basepath + "YearRep/";
                        detailpath = "YearReport.rdlc";

                        rds = new ReportDataSource("DepRep", ReportController.getMonthTrendByReq(nowdate));
                        rds1 = new ReportDataSource("SupplierDuration", ReportController.getMonthTrendSupplyDur(nowdate));
                        rds2 = new ReportDataSource("LossCost", ReportController.getMonthTrendLoss(nowdate));
                        ReportViewer.LocalReport.DataSources.Clear();
                        ReportViewer.LocalReport.DataSources.Add(rds1);
                        ReportViewer.LocalReport.DataSources.Add(rds2);
                        break;
                }

                path = basepath + detailpath;

                ReportViewer.Visible = true;

                ReportViewer.LocalReport.ReportPath = Server.MapPath(path);

                ReportViewer.LocalReport.DataSources.Add(rds);
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Pls Input Date";
            }

           
        }

        protected void initReport()
        {
            ReportViewer.LocalReport.DataSources.Clear();
            ReportViewer.Visible = false;
        }
        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            

            btnSearch.Visible = true;
            initReport();
            
        }
       
    }
}
