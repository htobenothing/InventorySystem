using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.DataVisualization.Charting;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DataTable dt = GetData();
            LoadChartData(dt);
        }
    }

    private void LoadChartData(DataTable initialDataSource)
    {
        for (int i = 1; i < initialDataSource.Columns.Count; i++)
        {
            Series series = new Series();
            foreach (DataRow dr in initialDataSource.Rows)
            {
                int y = (int)dr[i];
                series.Points.AddXY(dr["Data"].ToString(), y);
            }
           // Chart1.Series.Add(series);
        }
    }

    private DataTable GetData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Data", Type.GetType("System.String"));
        dt.Columns.Add("Value1", Type.GetType("System.Int32"));
        dt.Columns.Add("Value2", Type.GetType("System.Int32"));
        dt.Columns.Add("Value3", Type.GetType("System.Int32"));
        DataRow dr1 = dt.NewRow();
        dr1["Data"] = "series1";
        dr1["Value1"] = 32;
        dr1["Value2"] = 45;
        dr1["Value3"] = 60;
        dt.Rows.Add(dr1);
        DataRow dr2 = dt.NewRow();
        dr2["Data"] = "series2";
        dr2["Value1"] = 62;
        dr2["Value2"] = 10;
        dr2["Value3"] = 89;
        dt.Rows.Add(dr2);
        DataRow dr3 = dt.NewRow();
        dr3["Data"] = "series3";
        dr3["Value1"] = 19;
        dr3["Value2"] = 23;
        dr3["Value3"] = 78;
        dt.Rows.Add(dr3);
        return dt;
    }
}