using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
using BL.Store;
using BL.Store.TableView;

namespace LogicUniversity.View.Public
{
 

    public partial class UpdateSupInventory : System.Web.UI.Page
    {
        SupplierInventoryController SIC = new SupplierInventoryController();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["LoginStaff"] != null )
            {
                Staff stf = (Staff)Session["LoginStaff"];
                
                if (stf.Dept_ID == "SUPP")
                {
                    if (!IsPostBack)
                    {
                        
                         
                        //HttpCookie ck = Request.Cookies["login"];
                        //string clerkID = ck.Value.ToString();
                        string supplierID = stf.Staff_ID;
                        GridView1.DataSource = SIC.GetSupplierInventory(supplierID);
                        GridView1.DataBind();
                        Session["ItemList"] = SIC.GetSupplierInventory(supplierID);
                        foreach (GridViewRow r in GridView1.Rows)
                        {
                            TextBox tbQty = (TextBox)r.Cells[2].FindControl("TextboxQty");
                            tbQty.Text = r.Cells[1].Text;
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }

            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
            
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataSource = Session["ItemList"];
            GridView1.DataBind();
            foreach (GridViewRow r in GridView1.Rows)
            {
                TextBox tbQty = (TextBox)r.Cells[2].FindControl("TextboxQty");
                tbQty.Text = r.Cells[1].Text;
            }
        }

        protected void BtnConfrim_Click(object sender, EventArgs e)
        {
            //HttpCookie ck = Request.Cookies["login"];
            //string clerkID = ck.Value.ToString();
            Staff stf = (Staff)Session["LoginStaff"];
            
            string supplierID = stf.Staff_ID;
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                List<string> Qty = new List<string>();

                for (int i = 0; i < GridView1.PageCount; i++)
                    foreach (GridViewRow r in GridView1.Rows)
                    {
                        TextBox tbQty = (TextBox)r.Cells[2].FindControl("TextboxQty");
                        Qty.Add(tbQty.Text);
                    }


                bool flag = SIC.UpdateSupplierInventroy(supplierID, Qty);
                if (flag == true)
                {
                    LBStatus.Text = "Update Success!";
                    Response.Redirect("~/View/Supplier/UpdateSupInventory.aspx");
                }
                else
                    LBStatus.Text = "Update Fail!";

            }
        }

        protected void BtnRevert_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView1.PageCount; i++)
                foreach (GridViewRow r in GridView1.Rows)
                {
                    TextBox tbQty = (TextBox)r.Cells[2].FindControl("TextboxQty");
                    tbQty.Text = r.Cells[1].Text;
                }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/Login.aspx");
        }
    }
}