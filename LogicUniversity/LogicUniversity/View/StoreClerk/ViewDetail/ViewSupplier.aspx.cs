using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using BL.Store;
using BL;

namespace LogicUniversity.StoreClerk.View
{
    public partial class ViewSupplier : System.Web.UI.Page
    {
      
        Suppliercontroller sup = new Suppliercontroller();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ddlsupplier.DataTextField = "Supplier_ID";
                ddlsupplier.DataValueField = "Supplier_ID";
                ddlsupplier.DataSource = sup.GetAllSupplier();
                ddlsupplier.DataBind();
                ddlsupplier.Items.Insert(0, "-Select Supplier Code");

            }
        }

        protected void ShowBtn_Click(object sender, EventArgs e)
        {
            PanelDetail.Visible = false;
            if (ddlsupplier.SelectedIndex == 0)
            {
                mesglbl.Text = "Please Select supplier Code";
                gvSupplier.DataSource = null;
                gvSupplier.DataBind();

            }
            else
            {
                mesglbl.Text = "";

                String code = ddlsupplier.SelectedValue;

                Supplier SupIDList = sup.GetSupByid(code);

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("GST_Registration_No", typeof(string)));
                dt.Columns.Add(new DataColumn("Code", typeof(string)));
                dt.Columns.Add(new DataColumn("Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Email", typeof(string)));
                DataRow dr = dt.NewRow();
                dr["GST_Registration_No"] = SupIDList.GST_No;
                dr["Code"] = SupIDList.Supplier_ID;
                dr["Name"] = SupIDList.Supplier_Name;
                dr["Email"] = SupIDList.Email;
                dt.Rows.Add(dr);
                dt.AcceptChanges();
                ViewState["CurrentTable"] = dt;
                gvSupplier.DataSource = dt;
                gvSupplier.DataBind();
            }
            
          
        }

    

        protected void ShowAll_Click(object sender, EventArgs e)
        {
            ddlsupplier.SelectedIndex = 0;
            mesglbl.Text = "";
            PanelDetail.Visible = false;


            List<Supplier> List = new List<Supplier>();
            List = sup.GetAllSupplier();
        
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("GST_Registration_No", typeof(string)));
            dt.Columns.Add(new DataColumn("Code", typeof(string)));
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Email", typeof(string)));
            foreach (Supplier list in List)
            {
                DataRow dr = dt.NewRow();
                dr["GST_Registration_No"] = list.GST_No;
                dr["Code"] = list.Supplier_ID;

                dr["Name"] = list.Supplier_Name;
                dr["Email"] = list.Email;
                dt.Rows.Add(dr);
            }
            int y = dt.Rows.Count;
            dt.AcceptChanges();
            ViewState["CurrentTable"] = dt;
            gvSupplier.DataSource = dt;
            gvSupplier.DataBind();
        }

        //protected void ShowDetailBtn_Click(object sender, EventArgs e)
        //{
        //  // String SupCode="";
        //   Button btn = (Button)sender;           
        //   GridViewRow gvr = (GridViewRow)btn.NamingContainer;
        //   string supplierId = gvr.Cells[1].Text;
        //   Supplier supplier = sup.GetSupByid(supplierId);
        //   // SupIDList = sup.GetSupByid(SupCode);
        //   String Reg = supplier.GST_No;
        //   String Name = supplier.Supplier_Name;
        //   String ContName = supplier.Contact_Name;
        //   String PhoneN = supplier.PhoneNumber;
        //   String FaxN = supplier.FaxNumber;
        //   String Addr = supplier.Address;
        //   String Email = supplier.Email;


        //   Response.Redirect(String.Format("DetailSupplier.aspx?registrationNo={0}&SupCode={1}&SupName={2}&ContactName={3}&PhoneNo={4}&FaxNo={5}&Addr={6}&Email={7}", Reg, supplierId, Name, ContName,FaxN, PhoneN, Addr, Email));
        
        //}

        protected void gvSupplier_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ShowDetailBtn_Click")
            {
                PanelDetail.Visible = true;
                int index = Convert.ToInt32(e.CommandArgument);
               
                string supplierId = gvSupplier.Rows[index].Cells[1].Text;
                Supplier supplier = sup.GetSupByid(supplierId);
                LBGSTNo.Text = supplier.GST_No;
                LBSupCode.Text = supplier.Supplier_ID;
                LBSupName.Text = supplier.Supplier_Name;
                LBConName.Text = supplier.Contact_Name;
                LBPhNo.Text = supplier.PhoneNumber;
                LBFaxNo.Text = supplier.FaxNumber;
                LBAddress.Text = supplier.Address;
          
                LBEmail.Text = supplier.Email;
            }
        }

        

        

      

    }
}