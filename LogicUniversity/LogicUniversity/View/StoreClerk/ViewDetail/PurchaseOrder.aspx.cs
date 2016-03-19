using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
using BL.Store;
using System.Data;
using BL.Store.TableView;

namespace LogicUniversity.StoreClerk.View
{
    public partial class PurchaseOrder : System.Web.UI.Page
    {
        List<string> statusList = new List<string>();

        StaffController staffCon = new StaffController();

        PurchaseOrderController orderCon = new PurchaseOrderController();
        Suppliercontroller supCon = new Suppliercontroller();
        PurchaseOrderController CPO = new PurchaseOrderController();
        TimeConvertController TC = new TimeConvertController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ddlSup.DataTextField = "Supplier_ID";
                ddlSup.DataValueField = "Supplier_ID";
                ddlSup.DataSource = supCon.GetAllSupplier();
                ddlSup.DataBind();
                ddlSup.Items.Insert(0, "-Select-");

                
                

                //statusList.Add("Pending");
                //statusList.Add("Received");
              //  ddlStatus.DataSource = statusList;
                ddlStatus.DataBind();




                statusList.Add("Submitted");
                statusList.Add("Received");
                statusList.Add("Cancelled");
                foreach (String t in statusList)
                {
                    ddl_Status.Items.Add(new ListItem(t, t));


                }
             
                ddl_Status.Items.Insert(0, "-Select Status-");
               //// ddl_Status.SelectedItem.Enabled = false;


                ddlorderBy.DataTextField = "Staff_Name";
                ddlorderBy.DataValueField = "Staff_id";
                ddlorderBy.DataSource = staffCon.getAllStoreStaff();
                ddlorderBy.DataBind();
                ddlorderBy.Items.Insert(0, "-Select-");


                //ddlRecBy.DataTextField = "Rec_Staff";
                //ddlRecBy.DataValueField = "Rec_Staff_id";
                //ddlRecBy.DataSource = orderCon.getAllOrderList();
                //ddlRecBy.DataBind();
                //ddlRecBy.Items.Insert(0, "");
              



            }
        }


        protected void Btn_Search(object sender, EventArgs e)
        {
            DateTime fromdate;
            DateTime todate;

           

            String poNo = POtxt.Text;

            if (POtxt.Text == "" && fromDate.Text == "" && toDate.Text == "")
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Please enter data!')</script>");
                gvPO.Visible = false;
                lbltoDate.Text = "";
                lblFDate.Text = "";
            }

            //else if (POtxt.Text == "")//if the order is not existed
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('This Purchase Order is not existed!')</script>");
            //}


            else if (poNo != "")
            {

                OrderListTableModel OL = orderCon.GetOrderByid(poNo);

                if (OL == null)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('This PO number does not exist!')</script>");
                    gvPO.Visible = false;
                    lbltoDate.Text = "";
                    lblFDate.Text = "";
                }
                else
                {
                    gvPO.Visible = true;
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Order_ID", typeof(string)));
                    dt.Columns.Add(new DataColumn("Supplier_Name", typeof(string)));
                    dt.Columns.Add(new DataColumn("Order_Date", typeof(DateTime)));
                    dt.Columns.Add(new DataColumn("Received_Date", typeof(DateTime)));
                    dt.Columns.Add(new DataColumn("Ord_Staff", typeof(string)));
                    dt.Columns.Add(new DataColumn("Rec_Staff", typeof(string)));
                    dt.Columns.Add(new DataColumn("Status", typeof(string)));
                    DataRow dr = dt.NewRow();
                    dr["Order_ID"] = OL.Order_ID;
                    dr["Supplier_Name"] = OL.Supplier_Name;
                    dr["Order_Date"] = OL.Order_Date;
                    if (OL.Received_Date == null)
                    {

                        dr["Received_Date"] = DBNull.Value;
                        dr["Ord_Staff"] = OL.Ord_Staff;
                        dr["Rec_Staff"] = OL.Rec_Staff;
                            dr["Status"] = OL.Status;
                            dt.Rows.Add(dr);
                            dt.AcceptChanges();
                            Session["CurrentTable"] = dt;
                            gvPO.DataSource = dt;
                            gvPO.DataBind();
                        
                    }
                    else
                    {
                        dr["Received_Date"] = OL.Received_Date;
                        dr["Ord_Staff"] = OL.Ord_Staff;
                        dr["Rec_Staff"] = OL.Rec_Staff;
                        dr["Status"] = OL.Status;
                        dt.Rows.Add(dr);
                        dt.AcceptChanges();
                        Session["CurrentTable"] = dt;
                        gvPO.DataSource = dt;
                        gvPO.DataBind();
                    }
                }

                lblmesg.Text = "";

            }

            else
            {
                if (fromDate.Text == "")
                {
                    lblFDate.Text = "Should not be blank here!!";
                    gvPO.Visible = false;
                    lbltoDate.Text = "";
                }

              

                else if(toDate.Text == "")
                {
                    lbltoDate.Text = "Should not be blank here!!";
                    gvPO.Visible = false;
                    lblFDate.Text = "";
                }
             

                else if ( fromDate.Text != "" && toDate.Text != "")
                {
                    fromdate = TC.DateConvert(fromDate.Text);
                    lbltoDate.Text = "";
                    lblFDate.Text = "";
                    todate = TC.DateConvert(toDate.Text);
                    List<OrderListTableModel> List = new List<OrderListTableModel>();
                    List = orderCon.GetOrderByDate(fromdate, todate);


                    if (fromdate > todate)
                    {
                        gvPO.Visible = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('FromDate should not be greater than ToDate!')</script>");
                        
                        }
                    else if (List.Count == 0)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Not Found data!')</script>");
                        gvPO.Visible = false;

                    }

                        
                    else
                    {
                        gvPO.Visible = true;
                        DataTable dt = new DataTable();
                        dt.Columns.Add(new DataColumn("Order_ID", typeof(string)));
                        dt.Columns.Add(new DataColumn("Supplier_Name", typeof(string)));
                        dt.Columns.Add(new DataColumn("Order_Date", typeof(DateTime)));
                        dt.Columns.Add(new DataColumn("Received_Date", typeof(DateTime)));
                        dt.Columns.Add(new DataColumn("Ord_Staff", typeof(string)));
                        dt.Columns.Add(new DataColumn("Rec_Staff", typeof(string)));
                        dt.Columns.Add(new DataColumn("Status", typeof(string)));
                        foreach (OrderListTableModel list in List)
                        {
                            DataRow dr = dt.NewRow();
                            dr["Order_ID"] = list.Order_ID;
                            dr["Supplier_Name"] = list.Supplier_Name;
                            dr["Order_Date"] = list.Order_Date;

                            if (list.Received_Date == null)
                            {
                                dr["Received_Date"] = DBNull.Value;
                                dr["Ord_Staff"] = list.Ord_Staff;
                                dr["Rec_Staff"] = list.Rec_Staff;
                                dr["Status"] = list.Status;
                                dt.Rows.Add(dr);
                            }
                            else
                            {
                                dr["Received_Date"] = list.Received_Date;
                                dr["Ord_Staff"] = list.Ord_Staff;
                                dr["Rec_Staff"] = list.Rec_Staff;
                                dr["Status"] = list.Status;
                                dt.Rows.Add(dr);
                            }
                        }
                        int y = dt.Rows.Count;
                        dt.AcceptChanges();
                        Session["CurrentTable"] = dt;
                        gvPO.DataSource = dt;
                        gvPO.DataBind();

                    }
                }
                

                
               



            }
        }
        protected void btn_Search2(object sender, EventArgs e)
        {
            String supName = ddlSup.SelectedValue;
            String Status = ddlStatus.SelectedValue;


            if (ddlStatus.SelectedValue == "-Select Status-" && ddlorderBy.SelectedIndex == 0)
            {
                lblerror.Text = "";
                List<OrderListTableModel> List = new List<OrderListTableModel>();
                List = orderCon.GetOrderBySupplier(supName);
                if (List.Count == 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Not Found data!')</script>");
                    lblerror.Text = "Not Found Data";
                    gvPO2.Visible = false;

                }
                else
                {
                 
                    gvPO2.Visible = true;
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Order_ID", typeof(string)));
                    dt.Columns.Add(new DataColumn("Supplier_Name", typeof(string)));
                    dt.Columns.Add(new DataColumn("Order_Date", typeof(DateTime)));
                    dt.Columns.Add(new DataColumn("Received_Date", typeof(DateTime)));
                    dt.Columns.Add(new DataColumn("Ord_Staff", typeof(string)));
                    dt.Columns.Add(new DataColumn("Rec_Staff", typeof(string)));
                    dt.Columns.Add(new DataColumn("Status", typeof(string)));
                    foreach (OrderListTableModel list in List)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Order_ID"] = list.Order_ID;
                        dr["Supplier_Name"] = list.Supplier_Name;
                        dr["Order_Date"] = list.Order_Date;

                        if (list.Received_Date == null)
                        {
                            dr["Received_Date"] = DBNull.Value;
                            dr["Ord_Staff"] = list.Ord_Staff;
                            dr["Rec_Staff"] = list.Rec_Staff;
                            dr["Status"] = list.Status;
                            dt.Rows.Add(dr);
                        }
                        else
                        {
                            dr["Received_Date"] = list.Received_Date;
                            dr["Ord_Staff"] = list.Ord_Staff;
                            dr["Rec_Staff"] = list.Rec_Staff;
                            dr["Status"] = list.Status;
                            dt.Rows.Add(dr);
                        }
                    }
                    dt.AcceptChanges();
                    Session["CurrentTable"] = dt;
                    gvPO2.DataSource = dt;
                    gvPO2.DataBind();
                }
            }
            else if (ddlSup.SelectedValue != "" && ddlorderBy.SelectedIndex == 0)
            {
                lblerror.Text = "";
                List<OrderListTableModel> List = new List<OrderListTableModel>();
                List = orderCon.GetOrderBySupplierAndStatus(supName, Status);
                if (List.Count == 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Not Found data!')</script>");
                    lblerror.Text = "Not Found Data";
                    gvPO2.Visible = false;

                }
                else
                {
                  
                    gvPO2.Visible = true;
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Order_ID", typeof(string)));
                    dt.Columns.Add(new DataColumn("Supplier_Name", typeof(string)));
                    dt.Columns.Add(new DataColumn("Order_Date", typeof(DateTime)));
                    dt.Columns.Add(new DataColumn("Received_Date", typeof(DateTime)));
                    dt.Columns.Add(new DataColumn("Ord_Staff", typeof(string)));
                    dt.Columns.Add(new DataColumn("Rec_Staff", typeof(string)));
                    dt.Columns.Add(new DataColumn("Status", typeof(string)));
                    foreach (OrderListTableModel list in List)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Order_ID"] = list.Order_ID;
                        dr["Supplier_Name"] = list.Supplier_Name;
                        dr["Order_Date"] = list.Order_Date;

                        if (list.Received_Date == null)
                        {
                            dr["Received_Date"] = DBNull.Value;
                            dr["Ord_Staff"] = list.Ord_Staff;
                            dr["Rec_Staff"] = list.Rec_Staff;
                            dr["Status"] = list.Status;
                            dt.Rows.Add(dr);
                        }
                        else
                        {
                            dr["Received_Date"] = list.Received_Date;
                            dr["Ord_Staff"] = list.Ord_Staff;
                            dr["Rec_Staff"] = list.Rec_Staff;
                            dr["Status"] = list.Status;
                            dt.Rows.Add(dr);
                        }
                    }
                    dt.AcceptChanges();
                    Session["CurrentTable"] = dt;
                    gvPO2.DataSource = dt;
                    gvPO2.DataBind();
                }
                
                }

            else  if ( ddlSup.SelectedIndex== 0 && ddlRecBy.SelectedValue == "" && ddl_Status.SelectedIndex == 1)
            {
                lblerror.Text = "";
                List<OrderListTableModel> List = new List<OrderListTableModel>();
                String OrdStaff = ddlorderBy.SelectedValue;
               String Status2 = ddl_Status.SelectedValue;
               List = orderCon.GetOrderByOrdStaffAndStatus(OrdStaff, Status2);
               if (List.Count == 0)
               {
                   Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Not Found data!')</script>");
                   lblerror.Text = "Not Found Data";
                   gvPO2.Visible = false;

               }

               else
               {
                   
                   gvPO2.Visible = true;
                   DataTable dt = new DataTable();
                   dt.Columns.Add(new DataColumn("Order_ID", typeof(string)));
                   dt.Columns.Add(new DataColumn("Supplier_Name", typeof(string)));
                   dt.Columns.Add(new DataColumn("Order_Date", typeof(DateTime)));
                   dt.Columns.Add(new DataColumn("Received_Date", typeof(DateTime)));
                   dt.Columns.Add(new DataColumn("Ord_Staff", typeof(string)));
                   dt.Columns.Add(new DataColumn("Rec_Staff", typeof(string)));
                   dt.Columns.Add(new DataColumn("Status", typeof(string)));
                   foreach (OrderListTableModel list in List)
                   {
                       DataRow dr = dt.NewRow();
                       dr["Order_ID"] = list.Order_ID;
                       dr["Supplier_Name"] = list.Supplier_Name;
                       dr["Order_Date"] = list.Order_Date;

                       if (list.Received_Date == null)
                       {
                           dr["Received_Date"] = DBNull.Value;
                           dr["Ord_Staff"] = list.Ord_Staff;
                           dr["Rec_Staff"] = list.Rec_Staff;
                           dr["Status"] = list.Status;
                           dt.Rows.Add(dr);
                       }
                       else
                       {
                           dr["Received_Date"] = list.Received_Date;
                           dr["Ord_Staff"] = list.Ord_Staff;
                           dr["Rec_Staff"] = list.Rec_Staff;
                           dr["Status"] = list.Status;
                           dt.Rows.Add(dr);
                       }
                   }
                   dt.AcceptChanges();
                   Session["CurrentTable"] = dt;
                   gvPO2.DataSource = dt;
                   gvPO2.DataBind();
               }

                ddlRecBy.SelectedIndex = -1;

            }


            //else if ( ddlorderBy.SelectedIndex == 0 && ddl_Status.SelectedIndex == 0)
            //{
                
            //fromdate = Convert.ToDateTime(fromDate.Text);
            //todate = Convert.ToDateTime(toDate.Text);



            //    dynamic OrderL = orderCon.GetOrderByDate(fromdate, todate);
            //    gvPO2.DataSource = OrderL;
            //    gvPO2.DataBind();

              

          //  }


            else if (ddlSup.SelectedIndex==0 && ddl_Status.SelectedIndex == 0)
            {
                lblerror.Text = "";
                List<OrderListTableModel> List = new List<OrderListTableModel>();
                String OrderStaff = ddlorderBy.SelectedValue;
                List = orderCon.GetOrderByOrderStaff(OrderStaff);

                if (List.Count == 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Not Found data!')</script>");
                    lblerror.Text = "Not Found Data";
                    gvPO2.Visible = false;

                }

                else
                {
                    
                    gvPO2.Visible = true;
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Order_ID", typeof(string)));
                    dt.Columns.Add(new DataColumn("Supplier_Name", typeof(string)));
                    dt.Columns.Add(new DataColumn("Order_Date", typeof(DateTime)));
                    dt.Columns.Add(new DataColumn("Received_Date", typeof(DateTime)));
                    dt.Columns.Add(new DataColumn("Ord_Staff", typeof(string)));
                    dt.Columns.Add(new DataColumn("Rec_Staff", typeof(string)));
                    dt.Columns.Add(new DataColumn("Status", typeof(string)));
                    foreach (OrderListTableModel list in List)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Order_ID"] = list.Order_ID;
                        dr["Supplier_Name"] = list.Supplier_Name;
                        dr["Order_Date"] = list.Order_Date;

                        if (list.Received_Date == null)
                        {
                            dr["Received_Date"] = DBNull.Value;
                            dr["Ord_Staff"] = list.Ord_Staff;
                            dr["Rec_Staff"] = list.Rec_Staff;
                            dr["Status"] = list.Status;
                            dt.Rows.Add(dr);
                        }
                        else
                        {
                            dr["Received_Date"] = list.Received_Date;
                            dr["Ord_Staff"] = list.Ord_Staff;
                            dr["Rec_Staff"] = list.Rec_Staff;
                            dr["Status"] = list.Status;
                            dt.Rows.Add(dr);
                        }

                        dt.AcceptChanges();
                        Session["CurrentTable"] = dt;
                        gvPO2.DataSource = dt;
                        gvPO2.DataBind();
                    }
                }
             }

            
           else 
            {
                lblerror.Text = "";
                String OrdStaff = ddlorderBy.SelectedValue;
               String RecStaff = ddlRecBy.SelectedValue;
               String Status2 = ddl_Status.SelectedValue;



               List<OrderListTableModel> List = new List<OrderListTableModel>();
               List = orderCon.GetOrderByOrdStaffAndStatusAndRecStaff(OrdStaff, Status2, RecStaff);

               if (List.Count == 0)
               {
                   //Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Not Found data!')</script>");
                   lblerror.Text = "Not Found Data";
                   gvPO2.Visible = false;

               }
               else
               {
                   
                   gvPO2.Visible = true;
                   DataTable dt = new DataTable();
                   dt.Columns.Add(new DataColumn("Order_ID", typeof(string)));
                   dt.Columns.Add(new DataColumn("Supplier_Name", typeof(string)));
                   dt.Columns.Add(new DataColumn("Order_Date", typeof(DateTime)));
                   dt.Columns.Add(new DataColumn("Received_Date", typeof(DateTime)));
                   dt.Columns.Add(new DataColumn("Ord_Staff", typeof(string)));
                   dt.Columns.Add(new DataColumn("Rec_Staff", typeof(string)));
                   dt.Columns.Add(new DataColumn("Status", typeof(string)));
                   foreach (OrderListTableModel list in List)
                   {
                       DataRow dr = dt.NewRow();
                       dr["Order_ID"] = list.Order_ID;
                       dr["Supplier_Name"] = list.Supplier_Name;
                       dr["Order_Date"] = list.Order_Date;

                       if (list.Received_Date == null)
                       {
                           dr["Received_Date"] = DBNull.Value;
                           dr["Ord_Staff"] = list.Ord_Staff;
                           dr["Rec_Staff"] = list.Rec_Staff;
                           dr["Status"] = list.Status;
                           dt.Rows.Add(dr);
                       }
                       else
                       {
                           dr["Received_Date"] = list.Received_Date;
                           dr["Ord_Staff"] = list.Ord_Staff;
                           dr["Rec_Staff"] = list.Rec_Staff;
                           dr["Status"] = list.Status;
                           dt.Rows.Add(dr);
                       }
                   }
                   dt.AcceptChanges();
                   Session["CurrentTable"] = dt;
                   gvPO2.DataSource = dt;
                   gvPO2.DataBind();
               }
               
            }




        }

        protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlorderBy.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            ddl_Status.SelectedIndex = 0;
            ddlRecBy.Items.Clear();
            
        }

       

        protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSup.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;

            if (ddlorderBy.SelectedIndex == 0)
            { 
                ddl_Status.SelectedIndex = 0;
               
            }

            if (ddlorderBy.SelectedValue != null)
            {
                String OrdStaff = ddlorderBy.SelectedValue;

              //  String Status = ddl_Status.SelectedValue;

                List<string> statusList = new List<string>();
                statusList.Add("-Select-");
                statusList.Add("Submitted");
                statusList.Add("Received");
                ddl_Status.DataSource = statusList;
            //    ddl_Status.DataSource = orderCon.GetStatusByOrdStaff(OrdStaff);
                ddl_Status.DataBind();

                if (ddl_Status.SelectedValue == "Received")
                {

                    ddl_Status.Items[1].Attributes.Add("Received", "Received");
                    // ddl_Status.Items[2].Attributes.Add("disabled", "disabled");
                    ddlRecBy.DataTextField = "Rec_Staff";
                    ddlRecBy.DataValueField = "Rec_Staff_id";
                    ddlRecBy.DataSource = orderCon.GetOrderByOrdStaffAndStatus(OrdStaff, "Received");
                    ddlRecBy.DataBind();



                }
                else

                    ddlRecBy.Items.Clear();
            }
            

                
           
        }


        protected void ddlRecBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void fromDate_TextChanged(object sender, EventArgs e)
        {
            POtxt.Text = "";
            
          
        }

        protected void toDate_TextChanged(object sender, EventArgs e)
        {
            POtxt.Text = "";
        }

        protected void POtxt_TextChanged(object sender, EventArgs e)
        {

            fromDate.Text = "";
            toDate.Text = "";
                
        }

        protected void ddl_Status_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSup.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;

            if (ddl_Status.SelectedValue != null)
            {
                String status = ddl_Status.SelectedValue;
                ddlRecBy.DataTextField = "Rec_Staff";
                ddlRecBy.DataValueField = "Rec_Staff_id";
                ddlRecBy.DataSource = orderCon.getRecStaffbyStatus(status);
                ddlRecBy.DataBind();

                if (ddl_Status.SelectedIndex == 1)
                {
                    ddlRecBy.Enabled = false;

                }
                else
                    ddlRecBy.Enabled = true;

            }

           
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlorderBy.SelectedIndex = 0;
           
            ddl_Status.SelectedIndex = 0;
            ddlRecBy.Items.Clear();
        }

        protected void gvPO_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt =(DataTable) Session["CurrentTable"];
            gvPO.Visible = true;
            gvPO.PageIndex = e.NewPageIndex;
            gvPO.DataSource = dt;
            gvPO.DataBind();
        }

        protected void gvPO2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = (DataTable)Session["CurrentTable"];
            gvPO2.Visible = true;
            gvPO2.PageIndex = e.NewPageIndex;
            gvPO2.DataSource = dt; 
            gvPO2.DataBind();
        }

        

    }
}