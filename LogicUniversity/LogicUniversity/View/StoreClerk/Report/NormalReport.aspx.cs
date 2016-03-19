using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
using BL.Report;

namespace ADProject_Team1.View.StoreClerk.Report
{
    public partial class AnalysisReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initReport();
            }
            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            initReport();
            string basepath = "";
            string detailpath = "";
            string path = "";
            ReportDataSource rds = new ReportDataSource();
            DateTime nowdate = new DateTime();
            if (txtDate.Text != "")
            {
                
                
                

                string date = txtDate.Text;
                List<string> dateList = new List<string>();
                foreach (string i in date.Split('-'))
                {
                    dateList.Add(i);
                }
                int year = Convert.ToInt32(dateList[0]);
                int month = Convert.ToInt32(dateList[1]);
                nowdate = new DateTime(year, month, 1);

            }
                

                basepath = "/LogicUniversity/View/StoreClerk/Report/Normal/";
                switch (ddldepNor.SelectedValue)
                {
                    case "Department":
                        if (txtDate.Text != "")
                        {
                             detailpath = "requisition.rdlc";
                             path = basepath + detailpath;
                             rds = new ReportDataSource("DepReq", ReportController.getRequisitionByYear(nowdate));
                        }else{
                            ReportViewer.Visible = false;
                            lblError.Visible = true;
                            lblError.Text = "Pls Choose Date";
                        }
                       

                        break;
                    case "Store":
                        detailpath = "store.rdlc";
                        path = basepath + detailpath;
                        rds = new ReportDataSource("DataSet1", ReportController.getAllItem());
                        break;
                }


                path = basepath + detailpath;

                ReportViewer.Visible = true;
                ReportViewer.ProcessingMode = ProcessingMode.Local;
                ReportViewer.LocalReport.ReportPath = Server.MapPath(path);
                ReportViewer.LocalReport.DataSources.Add(rds);
            
            
            


        }
        protected void initReport()
        {
            lblError.Visible = false;
            ReportViewer.Visible = false;
            ReportViewer.LocalReport.DataSources.Clear();

        }

        protected void ddldepNor_SelectedIndexChanged(object sender, EventArgs e)
        {
            initReport();
           
        }

        
    }
}