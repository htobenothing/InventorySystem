using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections.Specialized;
using System.Windows;
using BL;
using BL.Store;

namespace ADProject_Team1.View.Store
{
    public partial class CeateAdjVoucherConrtol : System.Web.UI.Page
    {        
        protected AdjustVoucherController adjVoucher = new AdjustVoucherController();
        DropDownList itemCtgry =new DropDownList();
        DropDownList itemCode=new DropDownList();
        DropDownList itemName=new DropDownList();
     
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                          
                SetInitialRow();               
            }         
        }

        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;          
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dt.Columns.Add(new DataColumn("Column2", typeof(string))); 
            dt.Columns.Add(new DataColumn("Column3", typeof(string)));
            dt.Columns.Add(new DataColumn("Column4", typeof(string))); 
            dt.Columns.Add(new DataColumn("Column5", typeof(string)));
            dr = dt.NewRow();         
            dr["Column5"] = string.Empty;           
            dt.Rows.Add(dr);
            //Store the DataTable in ViewState for future reference   
            ViewState["CurrentTable"] = dt;
            //Bind the Gridview   
            AdjVouGridview.DataSource = dt;
            AdjVouGridview.DataBind();
            //After binding the gridview, we can then extract and fill the DropDownList with Data   
            DropDownList ddl1 = (DropDownList)AdjVouGridview.Rows[0].Cells[0].FindControl("DropDownListCategory");
            TodayDateLbl.Text = DateTime.Today.ToShortDateString();
        }  
        protected void AdjVouGridview_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                List<string> categoryddl = adjVoucher.GetAllItemCategory();
                itemCtgry = (DropDownList)e.Row.FindControl("DropDownListCategory");
                itemCode = (DropDownList)e.Row.FindControl("DropDownListItemCode");
                itemName = (DropDownList)e.Row.FindControl("DropDownListItemName");
                TextBox priceTxt = (TextBox)e.Row.FindControl("priceTxt"); 
                if (itemCtgry != null)
                {
                                     
                    foreach (string category in categoryddl)
                    {
                        itemCtgry.Items.Add(new ListItem(category, category));
                    }
                }
                itemCode.Items.Insert(0, "C001");
                itemName.Items.Insert(0, "Clips Double 1\"");
                priceTxt.Text = "1.44";

                
            }
        }

        protected void DropDownListCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
                       
            GridViewRow r = (sender as DropDownList).NamingContainer as GridViewRow;
            itemCtgry = (DropDownList)r.Cells[0].FindControl("DropDownListCategory");
            itemCode = (DropDownList)r.Cells[1].FindControl("DropDownListItemCode");
            itemName = (DropDownList)r.Cells[2].FindControl("DropDownListItemName");
            TextBox priceTxt = (TextBox)r.Cells[4].FindControl("priceTxt");
            string category = itemCtgry.SelectedValue.ToString();
            List<string> itemCodelist = new List<string>();
            itemCodelist = adjVoucher.GetAllItemCodeByctgry(category);
            List<string> itemNamelist = new List<string>();
            itemNamelist = adjVoucher.GetAllItemNameByctgry(category);
            itemCode.Items.Clear();
            itemName.Items.Clear();
           
            foreach (string itemcode in itemCodelist)
            {
                itemCode.Items.Add(new ListItem(itemcode, itemcode));
            }
            foreach (string name in itemNamelist)
            {
                itemName.Items.Add(new ListItem(name, name));
            }
            string firstItemCode = itemCode.SelectedValue;
            priceTxt.Text = adjVoucher.GetItemPrice(firstItemCode).ToString();
        }
       
        protected void DropDownListItemCode_SelectedIndexChanged(object sender, EventArgs e) {
            GridViewRow r = (sender as DropDownList).NamingContainer as GridViewRow;
            itemCode = (DropDownList)r.Cells[1].FindControl("DropDownListItemCode");
            itemName = (DropDownList)r.Cells[2].FindControl("DropDownListItemName");
            TextBox priceTxt = (TextBox)r.Cells[4].FindControl("priceTxt");
            string Code = itemCode.SelectedValue.ToString();
            string name = adjVoucher.GetItemByItem_ID(Code).Item_Name;
            itemCode.Items.Clear();
            itemName.Items.Clear();
            itemCode.Items.Add(new ListItem(Code, Code));
            itemName.Items.Add(new ListItem(name, name));
            priceTxt.Text = adjVoucher.GetItemPrice(Code).ToString();                      
        }
        protected void DropDownListItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            GridViewRow r = (sender as DropDownList).NamingContainer as GridViewRow;
            itemCode = (DropDownList)r.Cells[1].FindControl("DropDownListItemCode");
            itemName = (DropDownList)r.Cells[2].FindControl("DropDownListItemName");
            TextBox priceTxt = (TextBox)r.Cells[4].FindControl("priceTxt");          
            string name = itemName.SelectedValue.ToString();          
            string Code = adjVoucher.GetItemByName(name).Item_ID;
            itemCode.Items.Clear();
            itemName.Items.Clear();
            itemCode.Items.Add(new ListItem(Code, Code));
            itemName.Items.Add(new ListItem(name, name));
            priceTxt.Text = adjVoucher.GetItemPrice(Code).ToString();
        }



        protected void AdjVouGridview_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if(dt.Rows.Count >1 )
            {
                dt.Rows.RemoveAt(e.RowIndex);
                Session["LastQty"] = dt.Rows[dt.Rows.Count - 1]["Column5"];
                List<string> PriceList = new List<string>();
                foreach (GridViewRow r in AdjVouGridview.Rows)
                {
                    TextBox tbPrice = (TextBox)r.Cells[3].FindControl("priceTxt");
                    PriceList.Add(tbPrice.Text);
                }
                Session["PriceList"] = PriceList;
                // ViewState["CurrentTable"] = dt;
                AdjVouGridview.DataSource = dt;
                AdjVouGridview.DataBind();
                SetPreviousData();

                TextBox tbQty = (TextBox)AdjVouGridview.Rows[dt.Rows.Count - 1].Cells[4].FindControl("qtyTxt");
                tbQty.Text = (string)Session["LastQty"];
                for (int i = 0; i < AdjVouGridview.Rows.Count; i++)
                {
                    TextBox tbPrice = (TextBox)AdjVouGridview.Rows[i].Cells[3].FindControl("priceTxt");
                    PriceList = (List<string>)Session["priceList"];
                    tbPrice.Text = PriceList[i];
                }
            }
            else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('You should choose at least one item!')</script>");
            
        }
       
        protected void NewBtn_Click(object sender, EventArgs e)
        {                             
                    AddNewRowToGrid();                                                
            
        }
        private void AddNewRowToGrid()
        {

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    drCurrentRow = dtCurrentTable.NewRow();
                    //add new row to DataTable   
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    //Store the current data to ViewState for future reference   
                    ViewState["CurrentTable"] = dtCurrentTable;
                    for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                    { 
                        TextBox box1 = (TextBox)AdjVouGridview.Rows[i].Cells[1].FindControl("priceTxt");
                        TextBox box2 = (TextBox)AdjVouGridview.Rows[i].Cells[2].FindControl("qtyTxt");
                        dtCurrentTable.Rows[i]["Column4"] = box1.Text;
                        dtCurrentTable.Rows[i]["Column5"] = box2.Text;
                        
                       DropDownList ddl1 = (DropDownList)AdjVouGridview.Rows[i].Cells[0].FindControl("DropDownListCategory");
                       DropDownList ddl2 = (DropDownList)AdjVouGridview.Rows[i].Cells[1].FindControl("DropDownListItemCode");
                       DropDownList ddl3 = (DropDownList)AdjVouGridview.Rows[i].Cells[2].FindControl("DropDownListItemName");
                        
                        dtCurrentTable.Rows[i]["Column1"] = ddl1.SelectedItem.Text;
                        dtCurrentTable.Rows[i]["Column2"] = ddl2.SelectedItem.Text;
                        dtCurrentTable.Rows[i]["Column3"] = ddl2.SelectedItem.Text;
                    }
                    //Rebind the Grid with the current data to reflect changes   
                    AdjVouGridview.DataSource = dtCurrentTable;
                    AdjVouGridview.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            //Set Previous Data on Postbacks   
            SetPreviousData();
        }
        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox box1 = (TextBox)AdjVouGridview.Rows[i].Cells[1].FindControl("priceTxt");
                        TextBox box2 = (TextBox)AdjVouGridview.Rows[i].Cells[2].FindControl("qtyTxt");
                        DropDownList ddl1 = (DropDownList)AdjVouGridview.Rows[i].Cells[0].FindControl("DropDownListCategory");
                        DropDownList ddl2 = (DropDownList)AdjVouGridview.Rows[i].Cells[1].FindControl("DropDownListItemCode");
                        DropDownList ddl3 = (DropDownList)AdjVouGridview.Rows[i].Cells[2].FindControl("DropDownListItemName");
                        ddl1.Text = dt.Rows[i]["Column1"].ToString();
                        string category = ddl1.Text;
                        List<string> itemCodelist = new List<string>();
                        itemCodelist = adjVoucher.GetAllItemCodeByctgry(category);
                        List<string> itemNamelist = new List<string>();
                        itemNamelist = adjVoucher.GetAllItemNameByctgry(category);
                        ddl2.Items.Clear();
                        ddl3.Items.Clear();
                      
                        foreach (string itemcode in itemCodelist)
                        {
                            ddl2.Items.Add(new ListItem(itemcode, itemcode));
                        }
                        foreach (string name in itemNamelist)
                        {
                            ddl3.Items.Add(new ListItem(name, name));
                        }
                        if (i < dt.Rows.Count - 1)
                        {
                            //Assign the value from DataTable to the TextBox   
                            box1.Text = dt.Rows[i]["Column4"].ToString();
                            box2.Text = dt.Rows[i]["Column5"].ToString();
                            //Set the Previous Selected Items on Each DropDownList  on Postbacks                               
                            ddl1.Text = dt.Rows[i]["Column1"].ToString();                         
                            ddl1.Items.FindByText(dt.Rows[i]["Column1"].ToString()).Selected = true;
                            ddl2.Text = dt.Rows[i]["Column2"].ToString();                          
                            ddl3.Text = dt.Rows[i]["Column3"].ToString();
                            box1.Enabled = false;
                            box2.Enabled = false;
                            ddl1.Enabled = false;
                            ddl2.Enabled = false;
                            ddl3.Enabled = false;                          
                        }
                        rowIndex++;
                    }
                }
            }
        }

        protected void submitBtn_Click(object sender, EventArgs e)
        {
            Staff stf = (Staff)Session["LoginStaff"];
            string voucherId = getvoucherId();
            Adjustment adj = new Adjustment();
            adj.Voucher_ID = "V0" + voucherId;
            //adj.Issue_Date = DateTime.Today;
            adj.Issue_Date = DateTime.Now;
            adj.Approved_Date = null;
            adj.IssuedByStaff_ID = stf.Staff_ID;
            adj.ApprovedByStaff_ID = null;
            adj.Status = "Pending";
            adj.Remark = remarkTxt.Value;
            
            adjVoucher.setAdjustment(adj);
            List<AdjustmentDetail> adjList = new List<AdjustmentDetail>();
            if (AdjVouGridview.Rows.Count > 0) {
                for (int i = 0; i < AdjVouGridview.Rows.Count; i++) {
                    DropDownList itemCodeList = (DropDownList)AdjVouGridview.Rows[i].Cells[1].FindControl("DropDownListItemCode");
                    string itemCode = itemCodeList.SelectedItem.Text;
                    TextBox quantity = (TextBox)AdjVouGridview.Rows[i].Cells[4].FindControl("qtyTxt");
                    int adjQty = Convert.ToInt32(quantity.Text);
                    AdjustmentDetail adjDetail = new AdjustmentDetail();
                    adjDetail.Voucher_ID = adj.Voucher_ID;
                    adjDetail.Item_ID = itemCode;
                    adjDetail.Adj_Qty = adjQty;
                    adjList.Add(adjDetail);
                    //adjVoucher.setAdjustmentDetail(adjDetail);
                }

                adjList = adjList.GroupBy(x => new { x.Item_ID, x.Voucher_ID })
                                        .Select(y => new AdjustmentDetail { Voucher_ID= y.Key.Voucher_ID, Item_ID = y.Key.Item_ID, Adj_Qty = y.Sum(t => t.Adj_Qty) })
                                        .ToList();
                foreach (AdjustmentDetail ad in adjList)
                {
                    adjVoucher.setAdjustmentDetail(ad);
                }
                       
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Adjustment has been Done!')</script>");
            SetInitialRow();
            submitBtn.Enabled = true;
            NewBtn.Enabled = true;
            remarkTxt.Value = string.Empty;

        }
        private String getvoucherId() {
            String timeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            return timeStamp;
        }

    

    }
}