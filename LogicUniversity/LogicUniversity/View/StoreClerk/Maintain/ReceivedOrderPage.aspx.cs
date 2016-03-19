using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
using BL.Store;
using BL.Store.TableView;

namespace LogicUniversity.StoreClerk.Maintain
{
   

    public partial class ReceivedOrderPage : System.Web.UI.Page
    {
        PurchaseOrderController POC = new PurchaseOrderController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void PostBack()
        {
            Staff clk = (Staff)Session["LoginStaff"];
            string clerkID = clk.Staff_ID;
            Session["UserID"] = clerkID;
            PanelOrderDetail.Visible = false;
            TBPOID.Text = "";
        }
        protected void BtnShow_Click(object sender, EventArgs e)
        {
            if (POC.isPurchaseOrderExist(TBPOID.Text) == 3)
            {
                PanelOrderDetail.Visible = true;
                GridView1.DataSource = POC.ReceivePurchaseOrderGrideView(TBPOID.Text);
                GridView1.DataBind();
                Session["PoNumber"] = TBPOID.Text;
            }
            else if (POC.isPurchaseOrderExist(TBPOID.Text) == 2)//if the order has been cancelled
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('This Purchase Order has been Cancelled!')</script>");
            }
            else if (POC.isPurchaseOrderExist(TBPOID.Text) == 1)//if the order has been received
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('This Purchase Order has been Received!')</script>");
            }
            else if (POC.isPurchaseOrderExist(TBPOID.Text) == 0)//if the order is not existed
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('This Purchase Order is not existed!')</script>");
            }
        }

        protected void BtnReceive_Click(object sender, EventArgs e)
        {
            string orderid = (string)Session["PoNumber"];
            Staff stf = (Staff)Session["LoginStaff"];
            List<int> RecNumberList = new List<int>();
            foreach (GridViewRow r in GridView1.Rows)
            {
                TextBox rectb = (TextBox)r.Cells[4].FindControl("TextboxReceiveQty");
                RecNumberList.Add(Convert.ToInt32(rectb.Text));
            }
            bool flag = POC.ReceivePurchaseOrder(orderid, RecNumberList, TBRemark.Text, stf.Staff_ID);
            if (flag == true)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Received success!')</script>");
                PostBack();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Received Failed!')</script>");
                PostBack();
            }
        }

    }
}