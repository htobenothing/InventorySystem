using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
using BL.Store.TableView;
using BL.Store;


namespace LogicUniversity.StoreClerk.Maintain
{
   

    public partial class PurchaseOrderCancel : System.Web.UI.Page
    {
        PurchaseOrderController POC = new PurchaseOrderController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginStaff"] != null)
            {
                if (!IsPostBack)
                {
                    PostBack();
                }
            }
            //HttpCookie ck = Request.Cookies["login"];
            //string clerkID = ck.Value.ToString();
           
        }

        protected void PostBack()
        {
            Staff clk = (Staff)Session["LoginStaff"];
            string clerkID = clk.Staff_ID;
            List<OrderList> OrderList = POC.GetOrderListByOrderClerk(clerkID);
            if (OrderList.Count == 0)
            {
                LBDesc1.Text = "You have not any submitted orders";
                LBDesc1.ForeColor = System.Drawing.Color.Blue;
            }              
            else
                LBDesc1.Text = "Orders you have made: ";
            Session["OrderList"] = OrderList;
            GridViewOrder.DataSource = POC.CancelPurchaseOrderGridView(OrderList);
            GridViewOrder.DataBind();
        }
        protected void GridViewOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "viewdetail")
            {
                PanelOrderDetail.Visible = true;
                int index = Convert.ToInt32(e.CommandArgument);
                string orderid = GridViewOrder.Rows[index].Cells[0].Text;
                List<OrderListDetail> OrderListDetail = POC.GetPurchaseOrderDetailByOrderID(orderid);
                Session["OrderListDetail"] = OrderListDetail;
                GridViewOrderDetail.DataSource = POC.CancelPurchaseOrderDetailGridView(OrderListDetail);
                GridViewOrderDetail.DataBind();
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                List<OrderListDetail> OrderListDetail = (List<OrderListDetail>)Session["OrderListDetail"];
                string orderid = OrderListDetail[0].Order_ID;
                bool flag = POC.CancelPurchaseOrder(orderid, txtRemark.Text);
                if (flag == true)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Cancel Success!')</script>");
                    PanelOrderDetail.Visible = false;
                    PostBack();
                }
                else
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Cancel failed!')</script>");
                //Add pop window to show success

            }


        }

    }
}