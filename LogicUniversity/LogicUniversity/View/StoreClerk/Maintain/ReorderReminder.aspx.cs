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
  

    public partial class ReorderReminder : System.Web.UI.Page
    {
        ReorderController RC = new ReorderController();
        PurchaseOrderController POC = new PurchaseOrderController();
        SubmitPurchaseOrderController SPOL = new SubmitPurchaseOrderController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PostBack();
            }
        }
        
        protected void PostBack()
        {
            LBTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if(RC.GetReorderItemList().Count == 0)
            {
                LBDesc.Text = "There is no item need to be re-ordered now.";
                LBDesc.ForeColor = System.Drawing.Color.Blue;
                PanelTable.Visible = false;
            }
            else
            {
                GridViewROtable.DataSource = RC.GetReorderItemList();
                GridViewROtable.DataBind();
                foreach (GridViewRow r in GridViewROtable.Rows)
                {
                    if (Convert.ToInt32(r.Cells[5].Text) >= Convert.ToInt32(r.Cells[6].Text))
                        r.BackColor = System.Drawing.Color.Yellow;
                    else
                    {
                        r.BackColor = System.Drawing.Color.Crimson;
                        r.ForeColor = System.Drawing.Color.White;
                    }

                }
            }
           
        }
        protected void CheckBoxReorderAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow r in GridViewROtable.Rows)
            {
                CheckBox cb = (CheckBox)r.Cells[0].FindControl("CheckBoxReorder");
                if (CheckBoxReorderAll.Checked == true)
                    cb.Checked = true;
                else
                    cb.Checked = false;
            }
            if (CheckBoxReorderAll.Checked == true)
                CBReorderAllRed.Checked = true;
            else
                CBReorderAllRed.Checked = false;
        }

        protected void CBReorderAllRed_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow r in GridViewROtable.Rows)
            {
                if (r.BackColor == System.Drawing.Color.Crimson)
                {
                    CheckBox cb = (CheckBox)r.Cells[0].FindControl("CheckBoxReorder");
                    if (CBReorderAllRed.Checked == true)
                        cb.Checked = true;
                    else
                        cb.Checked = false;
                }
                
            }
        }

        protected void BtnCreateOrder_Click(object sender, EventArgs e)
        {
            List<int> QtyList = new List<int>();
            List<Item> ItemList = new List<Item>();
            ItemList.Clear();
            QtyList.Clear();
            bool flag = false;
            Dictionary<string, string> orderIDMap = new Dictionary<string, string>();
            foreach (GridViewRow r in GridViewROtable.Rows)
            {
                CheckBox cb = (CheckBox)r.Cells[0].FindControl("CheckBoxReorder");
                if (cb.Checked == true)
                {
                    ItemList.Add(POC.GetItemByItem_ID(r.Cells[2].Text));
                    QtyList.Add(Convert.ToInt32(r.Cells[7].Text));
                    flag = true;
                }
            }
            if(flag == true)
            {
                List<ItemWithQtyModel> IWQList = SPOL.CreateItemWithQty(ItemList, QtyList);
                List<OrderDetailController> ODList = SPOL.CreateOrderLists(IWQList);
                SPOL.DistributeToSuppliers(ODList);

                Session["ALPAList"] = SPOL.ALPAod;
                Session["BANEList"] = SPOL.BANEod;
                Session["CHEPList"] = SPOL.CHEPod;
                Session["OMEGList"] = SPOL.OMEGod;

                GridViewALPH.DataSource = POC.CreatePurchaseOrderGridView(SPOL.ALPAod);
                GridViewALPH.DataBind();

                GridViewBANE.DataSource = POC.CreatePurchaseOrderGridView(SPOL.BANEod);
                GridViewBANE.DataBind();

                GridViewCHEP.DataSource = POC.CreatePurchaseOrderGridView(SPOL.CHEPod);
                GridViewCHEP.DataBind();

                GridViewOMEG.DataSource = POC.CreatePurchaseOrderGridView(SPOL.OMEGod);
                GridViewOMEG.DataBind();


                if (SPOL.ALPAod.Count > 0)
                {
                    Panel1.Visible = true;
                    string orderid = POC.CreateOrderID("ALPA");
                    orderIDMap.Add("ALPA", orderid);
                    LbOrderID1.Text = "Order ID: " + orderid;


                }

                if (SPOL.BANEod.Count > 0)
                {
                    Panel2.Visible = true;
                    string orderid = POC.CreateOrderID("BANE");
                    orderIDMap.Add("BANE", orderid);
                    LbOrderID2.Text = "Order ID: " + orderid;


                }

                if (SPOL.CHEPod.Count > 0)
                {
                    Panel3.Visible = true;
                    string orderid = POC.CreateOrderID("CHEP");
                    orderIDMap.Add("CHEP", orderid);
                    LbOrderID3.Text = "Order ID: " + orderid;

                }

                if (SPOL.OMEGod.Count > 0)
                {
                    Panel4.Visible = true;
                    string orderid = POC.CreateOrderID("OMEG");
                    orderIDMap.Add("OMEG", orderid);
                    LbOrderID4.Text = "Order ID: " + orderid;


                }
                Session["SupMap"] = orderIDMap;
                Panelcontrol.Visible = true;
                PanelMain.Visible = false;
                LBNotEnough.Text = SPOL.InsufficientInfo;
            }
            else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Please choose at least one item! ')</script>");
        }


        protected void BtnConfirm_Click(object sender, EventArgs e)
        {
            Staff clk = (Staff)Session["LoginStaff"];
            Dictionary<string, string> orderIDMap = new Dictionary<string, string>();
            if (Session["SupMap"] != null)
                orderIDMap = (Dictionary<string, string>)Session["SupMap"];

            string clerkID = clk.Staff_ID;
            if (orderIDMap.ContainsKey("ALPA"))
            {
                if (orderIDMap["ALPA"] != "fail")
                {
                    List<ItemWithQtyModel> ALPAList = (List<ItemWithQtyModel>)Session["ALPAList"];
                    POC.NewPurchaseOrder(orderIDMap["ALPA"], "ALPA", clerkID);
                    POC.NewPurchaseOrderDetail(orderIDMap["ALPA"], "ALPA", ALPAList);
                    PurchaseOrderPDFController POPDF = new PurchaseOrderPDFController();
                    POPDF.MakePurchaseOrderPDF("ALPA", orderIDMap["ALPA"], DateTime.Today.AddDays(7).ToString("yyyy-MM-dd"), clerkID, ALPAList);
                    PurchaseOrderMailController POM = new PurchaseOrderMailController();
                    POM.email_send(POM.GetSupplierEmail("ALPA"), "C:\\inetpub\\wwwroot\\LogicUniversity\\PDF\\Logic University Purchase Order_" + orderIDMap["ALPA"] + ".pdf");
                }
            }

            if (orderIDMap.ContainsKey("BANE"))
            {
                if (orderIDMap["BANE"] != "fail")
                {
                    List<ItemWithQtyModel> BANEList = (List<ItemWithQtyModel>)Session["BANEList"];
                    POC.NewPurchaseOrder(orderIDMap["BANE"], "BANE", clerkID);
                    POC.NewPurchaseOrderDetail(orderIDMap["BANE"], "BANE", BANEList);
                    PurchaseOrderPDFController POPDF = new PurchaseOrderPDFController();
                    POPDF.MakePurchaseOrderPDF("BANE", orderIDMap["BANE"], DateTime.Today.AddDays(7).ToString("yyyy-MM-dd"), clerkID, BANEList);
                    PurchaseOrderMailController POM = new PurchaseOrderMailController();
                    POM.email_send(POM.GetSupplierEmail("BANE"), "C:\\inetpub\\wwwroot\\LogicUniversity\\PDF\\Logic University Purchase Order_" + orderIDMap["BANE"] + ".pdf");
                }
            }

            if (orderIDMap.ContainsKey("CHEP"))
            {
                if (orderIDMap["CHEP"] != "fail")
                {
                    List<ItemWithQtyModel> CHEPList = (List<ItemWithQtyModel>)Session["CHEPList"];
                    POC.NewPurchaseOrder(orderIDMap["CHEP"], "CHEP", clerkID);
                    POC.NewPurchaseOrderDetail(orderIDMap["CHEP"], "CHEP", CHEPList);
                    PurchaseOrderPDFController POPDF = new PurchaseOrderPDFController();
                    POPDF.MakePurchaseOrderPDF("CHEP", orderIDMap["CHEP"], DateTime.Today.AddDays(7).ToString("yyyy-MM-dd"), clerkID, CHEPList);
                    PurchaseOrderMailController POM = new PurchaseOrderMailController();
                    POM.email_send(POM.GetSupplierEmail("CHEP"), "C:\\inetpub\\wwwroot\\LogicUniversity\\PDF\\Logic University Purchase Order_" + orderIDMap["CHEP"] + ".pdf");
                }
            }

            if (orderIDMap.ContainsKey("OMEG"))
            {
                if (orderIDMap["OMEG"] != "fail")
                {
                    List<ItemWithQtyModel> OMEGList = (List<ItemWithQtyModel>)Session["OMEGList"];
                    POC.NewPurchaseOrder(orderIDMap["OMEG"], "OMEG", clerkID);
                    POC.NewPurchaseOrderDetail(orderIDMap["OMEG"], "OMEG", OMEGList);
                    PurchaseOrderPDFController POPDF = new PurchaseOrderPDFController();
                    POPDF.MakePurchaseOrderPDF("OMEG", orderIDMap["OMEG"], DateTime.Today.AddDays(7).ToString("yyyy-MM-dd"), clerkID, OMEGList);
                    PurchaseOrderMailController POM = new PurchaseOrderMailController();
                    POM.email_send(POM.GetSupplierEmail("OMEG"), "C:\\inetpub\\wwwroot\\LogicUniversity\\PDF\\Logic University Purchase Order_" + orderIDMap["OMEG"] + ".pdf");
                }
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Orders submitted success! ')</script>");
           
            Panelcontrol.Visible = false;
            PanelMain.Visible = true;
            Panel1.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = false;

            PostBack();
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Panelcontrol.Visible = false;
            PanelMain.Visible = true;
            Panel1.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = false;
        }

       




    }
}