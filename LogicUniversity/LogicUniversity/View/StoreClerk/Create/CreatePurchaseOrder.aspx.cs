using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
using BL.Store;
using BL.Store.TableView;

namespace LogicUniversity.StoreClerk.Create
{
   
    public partial class CreatePurchaseOrder : System.Web.UI.Page
    {
        protected PurchaseOrderController POC = new PurchaseOrderController();
        protected TimeConvertController TC = new TimeConvertController();
        DropDownList drp1;
        DropDownList drp2;
        DropDownList drp3;
        //public static List<Item> ItemList = new List<Item>();
        //static List<ItemWithQty> ALPAList = new List<ItemWithQty>();
        //static List<ItemWithQty> BANEList = new List<ItemWithQty>();
        //static List<ItemWithQty> CHEPList = new List<ItemWithQty>();
        //static List<ItemWithQty> OMEGList = new List<ItemWithQty>();
        //static Dictionary<string,string> orderIDMap = new Dictionary<string,string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PostBack();
            }
            
           
        }

        protected void PostBack()
        {
            Item firstItem = new Item();
            firstItem = POC.GetDefaultItem();
            List<Item> ItemList = new List<Item>();
            ItemList.Add(firstItem);
            Session["ItemList"] = ItemList;
            GridViewCreateOrder.DataSource = ItemList;
            GridViewCreateOrder.DataBind();
        }

        protected void GridViewCreateOrder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                List<string> itemcodeddl = POC.GetAllItemCode();
                List<string> categoryddl = POC.GetAllItemCategory();
                List<string> descddl = POC.GetAllItemDesc();
                TextBox tbUOM = new TextBox();
                drp1 = (DropDownList)e.Row.FindControl("DropDownListCategory");
                drp2 = (DropDownList)e.Row.FindControl("DropDownListItemCode");
                drp3 = (DropDownList)e.Row.FindControl("DropDownListDesc");
                tbUOM = (TextBox)e.Row.FindControl("TextboxUOM");
                if (drp1 != null)
                {
                    // populate drpnop items
                    foreach (string category in categoryddl)
                    {
                        drp1.Items.Add(new ListItem(category, category));
                    }
                    foreach (string itemcode in itemcodeddl)
                    {
                        drp2.Items.Add(new ListItem(itemcode, itemcode));
                    }
                    foreach (string desc in descddl)
                    {
                        drp3.Items.Add(new ListItem(desc, desc));
                    }
                    tbUOM.Text = POC.GetDefaultItem().UOM;
                }

            }
        }

        protected void DropDownListCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList drpCate = new DropDownList();
            DropDownList drpItem = new DropDownList();
            DropDownList drpDesc = new DropDownList();
            TextBox tbUOM = new TextBox();

            GridViewRow r = (sender as DropDownList).NamingContainer as GridViewRow;//Get the row which has changed

            drpCate = (DropDownList)r.Cells[0].FindControl("DropDownListCategory");
            drpItem = (DropDownList)r.Cells[1].FindControl("DropDownListItemCode");
            drpDesc = (DropDownList)r.Cells[2].FindControl("DropDownListDesc");
            tbUOM = (TextBox)r.Cells[4].FindControl("TextboxUOM");

            string category = drpCate.SelectedValue;

            List<Item> itemList = new List<Item>();
            List<string> itemCodeList = new List<string>();
            List<string> itemDescList = new List<string>();

            itemList = POC.GetItemByCategory(category);   //get items by category

            for (int i = 0; i < itemList.Count; i++)
            {
                itemCodeList.Add(itemList[i].Item_ID);
                itemDescList.Add(itemList[i].Item_Name);
                if (i == 0)  //get the UOM of the first one
                {
                    tbUOM.Text = itemList[i].UOM;
                }
            }
            drpItem.Items.Clear();
            drpDesc.Items.Clear();
            foreach (string itemcode in itemCodeList)
            {
                drpItem.Items.Add(new ListItem(itemcode, itemcode));
            }

            foreach (string desc in itemDescList)
            {
                drpDesc.Items.Add(new ListItem(desc, desc));
            }
        }

        protected void DropDownListItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList drpCate = new DropDownList();
            DropDownList drpItem = new DropDownList();
            DropDownList drpDesc = new DropDownList();
            TextBox tbUOM = new TextBox();

            GridViewRow r = (sender as DropDownList).NamingContainer as GridViewRow;//Get the row which has changed

            drpCate = (DropDownList)r.Cells[0].FindControl("DropDownListCategory");
            drpItem = (DropDownList)r.Cells[1].FindControl("DropDownListItemCode");
            drpDesc = (DropDownList)r.Cells[2].FindControl("DropDownListDesc");
            tbUOM = (TextBox)r.Cells[4].FindControl("TextboxUOM");

            string IC = drpItem.SelectedValue;
            Item item = new Item();
            item = POC.GetItemByItem_ID(IC);

            string category = item.ItemCategory.Category_ID;
            List<Item> itemList = new List<Item>();
            List<string> itemDescList = new List<string>();
            itemList = POC.GetItemByCategory(category);   //get items by category

            for (int i = 0; i < itemList.Count; i++)
                itemDescList.Add(itemList[i].Item_Name);
            drpCate.SelectedValue = category;
            drpDesc.Items.Clear();
            foreach (string desc in itemDescList)
            {
                drpDesc.Items.Add(new ListItem(desc, desc));
            }
            drpDesc.SelectedValue = item.Item_Name;
            tbUOM.Text = item.UOM;
        }

        protected void DropDownListDesc_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList drpCate = new DropDownList();
            DropDownList drpItem = new DropDownList();
            DropDownList drpDesc = new DropDownList();
            TextBox tbUOM = new TextBox();

            GridViewRow r = (sender as DropDownList).NamingContainer as GridViewRow;//Get the row which has changed

            drpCate = (DropDownList)r.Cells[0].FindControl("DropDownListCategory");
            drpItem = (DropDownList)r.Cells[1].FindControl("DropDownListItemCode");
            drpDesc = (DropDownList)r.Cells[2].FindControl("DropDownListDesc");
            tbUOM = (TextBox)r.Cells[4].FindControl("TextboxUOM");

            string DESC = drpDesc.SelectedValue;
            Item item = new Item();
            item = POC.GetItemByDesc(DESC);

            string category = item.ItemCategory.Category_ID;
            List<Item> itemList = new List<Item>();
            List<string> itemCodeList = new List<string>();

            itemList = POC.GetItemByCategory(category);   //get items by category
            for (int i = 0; i < itemList.Count; i++)
                itemCodeList.Add(itemList[i].Item_ID);

            drpCate.SelectedValue = category;
            drpItem.Items.Clear();
            foreach (string itemcode in itemCodeList)
            {
                drpItem.Items.Add(new ListItem(itemcode, itemcode));
            }
            drpItem.SelectedValue = item.Item_ID;
            tbUOM.Text = item.UOM;
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            TextBox tbQty = new TextBox();
            TextBox tbUOM = new TextBox();
            DropDownList drpItem = new DropDownList();
            DropDownList drpDesc = new DropDownList();
            DropDownList drpCate = new DropDownList();
            List<string> QtyList = new List<string>();
            List<Item> ItemList = new List<Item>();
            Item newItem = new Item();
            
            ItemList.Clear();
            QtyList.Clear();
            for (int i = 0; i < GridViewCreateOrder.Rows.Count; i++)
            {
                drpItem = (DropDownList)GridViewCreateOrder.Rows[i].Cells[1].FindControl("DropDownListItemCode");
                Item additem = POC.GetItemByItem_ID(drpItem.SelectedValue);
                ItemList.Add(additem);
                tbQty = (TextBox)GridViewCreateOrder.Rows[i].Cells[3].FindControl("TextboxQty");
                QtyList.Add(tbQty.Text);
            }
            newItem = POC.GetDefaultItem();
            ItemList.Add(newItem);
            Session["ItemList"] = ItemList;
            GridViewCreateOrder.DataSource = ItemList;
            GridViewCreateOrder.DataBind();

            for (int i = 0; i < QtyList.Count; i++)
            {
                tbQty = (TextBox)GridViewCreateOrder.Rows[i].Cells[3].FindControl("TextboxQty");
                tbQty.Text = QtyList[i];
            }
            for (int i = 0; i < GridViewCreateOrder.Rows.Count; i++)
            {
                drpItem = (DropDownList)GridViewCreateOrder.Rows[i].Cells[1].FindControl("DropDownListItemCode");
                drpDesc = (DropDownList)GridViewCreateOrder.Rows[i].Cells[2].FindControl("DropDownListDesc");
                drpCate = (DropDownList)GridViewCreateOrder.Rows[i].Cells[0].FindControl("DropDownListCategory");
                tbUOM = (TextBox)GridViewCreateOrder.Rows[i].Cells[4].FindControl("TextboxUOM");
                drpItem.SelectedValue = ItemList[i].Item_ID;
                drpCate.SelectedValue = ItemList[i].ItemCategory.Category_ID;
                drpDesc.SelectedValue = ItemList[i].Item_Name;
                tbUOM.Text = ItemList[i].UOM;
            }
        }

        protected void GridViewCreateOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteItem")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                TextBox tbQty = new TextBox();
                TextBox tbUOM = new TextBox();
                DropDownList drpItem = new DropDownList();
                DropDownList drpDesc = new DropDownList();
                DropDownList drpCate = new DropDownList();
                List<string> QtyList = new List<string>();
                List<Item> ItemList = new List<Item>();
                ItemList.Clear();
                QtyList.Clear();
                if (GridViewCreateOrder.Rows.Count == 1)
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Order should has at least one item! ')</script>");
                else
                {
                    for (int i = 0; i < GridViewCreateOrder.Rows.Count; i++)
                    {
                        drpItem = (DropDownList)GridViewCreateOrder.Rows[i].Cells[1].FindControl("DropDownListItemCode");
                        Item additem = POC.GetItemByItem_ID(drpItem.SelectedValue);
                        ItemList.Add(additem);
                        tbQty = (TextBox)GridViewCreateOrder.Rows[i].Cells[3].FindControl("TextboxQty");
                        QtyList.Add(tbQty.Text);
                    }
                    ItemList.RemoveAt(index);
                    QtyList.RemoveAt(index);
                    Session["ItemList"] = ItemList;
                    GridViewCreateOrder.DataSource = ItemList;
                    GridViewCreateOrder.DataBind();
                    for (int i = 0; i < QtyList.Count; i++)
                    {
                        tbQty = (TextBox)GridViewCreateOrder.Rows[i].Cells[3].FindControl("TextboxQty");
                        tbQty.Text = QtyList[i];
                    }
                    for (int i = 0; i < GridViewCreateOrder.Rows.Count; i++)
                    {
                        drpItem = (DropDownList)GridViewCreateOrder.Rows[i].Cells[1].FindControl("DropDownListItemCode");
                        drpDesc = (DropDownList)GridViewCreateOrder.Rows[i].Cells[2].FindControl("DropDownListDesc");
                        drpCate = (DropDownList)GridViewCreateOrder.Rows[i].Cells[0].FindControl("DropDownListCategory");
                        tbUOM = (TextBox)GridViewCreateOrder.Rows[i].Cells[4].FindControl("TextboxUOM");
                        drpItem.SelectedValue = ItemList[i].Item_ID;
                        drpCate.SelectedValue = ItemList[i].ItemCategory.Category_ID;
                        drpDesc.SelectedValue = ItemList[i].Item_Name;
                        tbUOM.Text = ItemList[i].UOM;
                    }
                }

            }
        }

        protected void BtnCreate_Click(object sender, EventArgs e)
        {

            TextBox tbQty = new TextBox();
            DropDownList drpItem = new DropDownList();
            SubmitPurchaseOrderController SPOL = new SubmitPurchaseOrderController();
            List<int> QtyList = new List<int>();
            List<Item> IList = new List<Item>();
            Dictionary<string, string> orderIDMap = new Dictionary<string, string>();
            foreach (GridViewRow r in GridViewCreateOrder.Rows)
            {
                tbQty = (TextBox)r.Cells[3].FindControl("TextboxQty");
                if (tbQty.Text == "")
                    LBforValidation.Text = "Please fill all Qty fields";
                else
                    LBforValidation.Text = "";
            }
            if (TbEDD.Text == "")
                LBEDDValidate.Text = "Please choose the Expected Delivery Date.";
            else
            {
                DateTime EED = TC.DateConvert(TbEDD.Text);
                if (EED < DateTime.Today.Date) 
                    LBEDDValidate.Text = "The expected delivery date can not be before today.";
                else
                {
                    LBEDDValidate.Text = "";
                    if (LBforValidation.Text != "Please fill all Qty fields")
                    {
                        for (int i = 0; i < GridViewCreateOrder.Rows.Count; i++)
                        {
                            drpItem = (DropDownList)GridViewCreateOrder.Rows[i].Cells[1].FindControl("DropDownListItemCode");
                            Item additem = POC.GetItemByItem_ID(drpItem.SelectedValue);
                            IList.Add(additem);
                            tbQty = (TextBox)GridViewCreateOrder.Rows[i].Cells[3].FindControl("TextboxQty");
                            QtyList.Add(Convert.ToInt32(tbQty.Text));
                        }

                        List<ItemWithQtyModel> IWQList = SPOL.CreateItemWithQty(IList, QtyList);
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
                            PanelSup1.Visible = true;
                            string orderid = POC.CreateOrderID("ALPA");
                            orderIDMap.Add("ALPA", orderid);
                            LbOrderID1.Text = "Order ID: " + orderid;
                            LbSupEDD1.Text = "Expected Dellivery Date: " + TbEDD.Text;
                            //PurchaseOrderPDF POPDF = new PurchaseOrderPDF();
                            //POPDF.MakePurchaseOrderPDF("ALPA",orderid,LbSupEDD1.Text, "Tony", SPOL.ALPAod);

                        }

                        if (SPOL.BANEod.Count > 0)
                        {
                            PanelSup2.Visible = true;
                            string orderid = POC.CreateOrderID("BANE");
                            orderIDMap.Add("BANE", orderid);
                            LbOrderID2.Text = "Order ID: " + orderid;
                            LbSupEDD2.Text = "Expected Dellivery Date: " + TbEDD.Text;
                            //PurchaseOrderPDF POPDF = new PurchaseOrderPDF();
                            // POPDF.MakePurchaseOrderPDF("BANE", "002", LbSupEDD1.Text, "Tony", SPOL.BANEod);
                        }

                        if (SPOL.CHEPod.Count > 0)
                        {
                            PanelSup3.Visible = true;
                            string orderid = POC.CreateOrderID("CHEP");
                            orderIDMap.Add("CHEP", orderid);
                            LbOrderID3.Text = "Order ID: " + orderid;
                            LbSupEDD3.Text = "Expected Dellivery Date: " + TbEDD.Text;
                            //PurchaseOrderPDF POPDF = new PurchaseOrderPDF();
                            //POPDF.MakePurchaseOrderPDF("CHEP", "003", LbSupEDD1.Text, "Tony", SPOL.CHEPod);
                        }

                        if (SPOL.OMEGod.Count > 0)
                        {
                            PanelSup4.Visible = true;
                            string orderid = POC.CreateOrderID("OMEG");
                            orderIDMap.Add("OMEG", orderid);
                            LbOrderID4.Text = "Order ID: " + orderid;
                            LbSupEDD4.Text = "Expected Dellivery Date: " + TbEDD.Text;

                        }
                        Session["SupMap"] = orderIDMap;
                        PanelContorl.Visible = true;
                        PanelCreateOrder.Visible = false;

                        LBNotEnough.Text = SPOL.InsufficientInfo;

                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Please fill all Qty fields')</script>");
                    }
                }
                
            }


        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            PanelContorl.Visible = false;
            PanelCreateOrder.Visible = true;
            PanelSup1.Visible = false;
            PanelSup2.Visible = false;
            PanelSup3.Visible = false;
            PanelSup4.Visible = false;

        }

        //need change
        //protected void IBCalendar_Click(object sender, ImageClickEventArgs e)
        //{
        //    if (Calendar1.Visible == false)
        //        Calendar1.Visible = true;
        //    else
        //        Calendar1.Visible = false;
        //}

        ////need change
        //protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        //{
        //    TbEDD.Text = Calendar1.SelectedDate.ToString("yyyy-MM-dd");
        //    Calendar1.Visible = false;
        //}

        protected void BtnConfirm_Click(object sender, EventArgs e)
        {
            Staff clerk = (Staff)Session["LoginStaff"];
            Dictionary<string, string> orderIDMap = new Dictionary<string, string>();
            if (Session["SupMap"] != null)
                orderIDMap = (Dictionary<string, string>)Session["SupMap"];

            string clerkID = clerk.Staff_ID;
            if (orderIDMap.ContainsKey("ALPA"))
            {
                if (orderIDMap["ALPA"] != "fail")
                {
                    List<ItemWithQtyModel> ALPAList = (List<ItemWithQtyModel>)Session["ALPAList"];
                    POC.NewPurchaseOrder(orderIDMap["ALPA"], "ALPA", clerkID, TbEDD.Text);
                    POC.NewPurchaseOrderDetail(orderIDMap["ALPA"], "ALPA", ALPAList);
                    PurchaseOrderPDFController POPDF = new PurchaseOrderPDFController();
                    POPDF.MakePurchaseOrderPDF("ALPA", orderIDMap["ALPA"], TbEDD.Text, clerkID, ALPAList);
                    PurchaseOrderMailController POM = new PurchaseOrderMailController();
                    POM.email_send(POM.GetSupplierEmail("ALPA"), "C:\\inetpub\\wwwroot\\LogicUniversity\\PDF\\Logic University Purchase Order_" + orderIDMap["ALPA"] + ".pdf");
                }
            }

            if (orderIDMap.ContainsKey("BANE"))
            {
                if (orderIDMap["BANE"] != "fail")
                {
                    List<ItemWithQtyModel> BANEList = (List<ItemWithQtyModel>)Session["BANEList"];
                    POC.NewPurchaseOrder(orderIDMap["BANE"], "BANE", clerkID, TbEDD.Text);
                    POC.NewPurchaseOrderDetail(orderIDMap["BANE"], "BANE", BANEList);
                    PurchaseOrderPDFController POPDF = new PurchaseOrderPDFController();
                    POPDF.MakePurchaseOrderPDF("BANE", orderIDMap["BANE"], TbEDD.Text, clerkID, BANEList);
                    PurchaseOrderMailController POM = new PurchaseOrderMailController();
                    POM.email_send(POM.GetSupplierEmail("BANE"), "C:\\inetpub\\wwwroot\\LogicUniversity\\PDF\\Logic University Purchase Order_" + orderIDMap["BANE"] + ".pdf");
                }
            }

            if (orderIDMap.ContainsKey("CHEP"))
            {
                if (orderIDMap["CHEP"] != "fail")
                {
                    List<ItemWithQtyModel> CHEPList = (List<ItemWithQtyModel>)Session["CHEPList"];
                    POC.NewPurchaseOrder(orderIDMap["CHEP"], "CHEP", clerkID, TbEDD.Text);
                    POC.NewPurchaseOrderDetail(orderIDMap["CHEP"], "CHEP", CHEPList);
                    PurchaseOrderPDFController POPDF = new PurchaseOrderPDFController();
                    POPDF.MakePurchaseOrderPDF("CHEP", orderIDMap["CHEP"], TbEDD.Text, clerkID, CHEPList);
                    PurchaseOrderMailController POM = new PurchaseOrderMailController();
                    POM.email_send(POM.GetSupplierEmail("CHEP"), "C:\\inetpub\\wwwroot\\LogicUniversity\\PDF\\Logic University Purchase Order_" + orderIDMap["CHEP"] + ".pdf");
                }
            }

            if (orderIDMap.ContainsKey("OMEG"))
            {
                if (orderIDMap["OMEG"] != "fail")
                {
                    List<ItemWithQtyModel> OMEGList = (List<ItemWithQtyModel>)Session["OMEGList"];
                    POC.NewPurchaseOrder(orderIDMap["OMEG"], "OMEG", clerkID, TbEDD.Text);
                    POC.NewPurchaseOrderDetail(orderIDMap["OMEG"], "OMEG", OMEGList);
                    PurchaseOrderPDFController POPDF = new PurchaseOrderPDFController();
                    POPDF.MakePurchaseOrderPDF("OMEG", orderIDMap["OMEG"], TbEDD.Text, clerkID, OMEGList);
                    PurchaseOrderMailController POM = new PurchaseOrderMailController();
                    POM.email_send(POM.GetSupplierEmail("OMEG"), "C:\\Users\\hnin\\Documents\\ADPDF\\Logic University Purchase Order_" + orderIDMap["OMEG"] + ".pdf");
                }
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Orders submitted success! ')</script>");
            
            PanelContorl.Visible = false;
            PanelCreateOrder.Visible = true;
            PanelSup1.Visible = false;
            PanelSup2.Visible = false;
            PanelSup3.Visible = false;
            PanelSup4.Visible = false;
            PostBack();
            //need add email sending code
        }

        protected void TbEDD_TextChanged(object sender, EventArgs e)
        {
            ViewState["EED"] = TbEDD.Text;
        }


    }
}