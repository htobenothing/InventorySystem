using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BL;
using BL.Store;
using BL.Store.TableView;
using BL.Utilities;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ItemService" in code, svc and config file together.
public class ItemService : IItemService
{
    //protected CreatePurchaseOrderControl POC = new CreatePurchaseOrderControl();
    CreatePurchaseOrderControl cpoc = new CreatePurchaseOrderControl();
    SubmitPurchaseOrderLogic spol = new SubmitPurchaseOrderLogic();
    ReorderControl rc = new ReorderControl();
    List<WCF_Item> wcfIList = new List<WCF_Item>();
    List<int> qtyList = new List<int>();
    
    
    /*****************************************************************************************************************/
    /**************************Create Order List and Reorder**********************************************/
    /*****************************************************************************************************************/
    public List<WCF_Item> GetItemList()
    {
        return ConvertToWCFItemList(cpoc.GetAllItem());
    }

    public List<String> GetAllItemCategory()
    {
        return cpoc.GetAllItemCategory();
    }

    public List<WCF_ReorderReminderTable> GetReorderItemList()
    {
        return ConvertToWCFReorderReminderTable(rc.GetReorderItemList());
    }

    public Dictionary<string, List<ItemWithQty>> InsertOrderList(WCF_ItemWithQtyAndClerk wcfIWQACL)
    {
        //List<ItemWithQty> IWQList = spol.CreateItemWithQty(ConvertToItemList(wcfPurchase.wcfIList), wcfPurchase.qtyList);
        List<OrderDetail> ODList = spol.CreateOrderLists(ConvertToItemWithQtyList(wcfIWQACL.wcfIWQL));
        spol.DistributeToSuppliers(ODList);
        Dictionary<string, List<ItemWithQty>> dic = new Dictionary<string, List<ItemWithQty>>();
        dic = ConvertToDictionary(spol);

        //update databse
        cpoc.GenerateOrderListByMobile(dic, wcfIWQACL.clerkID);               
        return dic;
    }


    /*****************************************************************************************************************/
    /**************************Cancel Order List**********************************************/
    /*****************************************************************************************************************/
    public List<WCF_OrderList> GetOrderListByOrderClerk(String clerkID)
    {
        return ConvertToWCFOrderList(cpoc.GetOrderListByOrderClerk(clerkID));
    }

    public List<WCF_OrderListDetail> GetPurchaseOrderDetailByOrderID(String orderID)
    {
        return ConvertToWCFOrderDetailList(cpoc.GetPurchaseOrderDetailByOrderID(orderID));
    }


    public bool CancelPurchaseOrder(WCF_OrderList wcfOL)
    {
        return cpoc.CancelPurchaseOrder(wcfOL.Order_ID, wcfOL.Remark);
    }

    /*****************************************************************************************************************/
    /**************************Receive order items   **********************************************/
    /*****************************************************************************************************************/

    public int isPurchaseOrderExist(String poNumber)
    {
        return cpoc.isPurchaseOrderExist(poNumber);
    }

    public List<WCF_ReceiveOrderTable> GetReceivePurchaseOrder(String poNumber)
    {
        return ConvertToWCFReceiveOrderTableList(cpoc.ReceivePurchaseOrderGrideView(poNumber));
    }

    public bool ReceivePurchaseOrder(WCF_ReceivePurchaseOrder wcfRPO)
    {
        return cpoc.ReceivePurchaseOrder(wcfRPO.orderid, wcfRPO.recNumberList, wcfRPO.remark, wcfRPO.recStaff);

    }
    /*****************************************************************************************************************/
    /**************************Convert   **********************************************/
    /*****************************************************************************************************************/
    public List<Item> ConvertToItemList(List<WCF_Item> list)
    {
        List<Item> list_WCF = new List<Item>();
        foreach (WCF_Item i in list)
        {
            Item item = new Item();
            item.Item_ID = i.Item_ID;
            item.Category_ID = i.Category_ID;
            item.Item_Name = i.Item_Name;
            item.Reorder_Level = i.Reorder_Level;
            item.Reorder_Qty = i.Reorder_Qty;
            item.UOM = i.UOM;
            item.Bin_ID = i.Bin_ID;
            item.FirstSupplier_ID = i.FirstSupplier_ID;
            if (i.SecondSupplier_ID == "")
            {
                item.SecondSupplier_ID = null;
            }
            else
            {
                item.SecondSupplier_ID = i.SecondSupplier_ID;
            }
            if (i.ThirdSupplier_ID == "")
            {
                item.ThirdSupplier_ID = null;
            }
            else
            {
                item.ThirdSupplier_ID = i.ThirdSupplier_ID;
            }
            item.Inventory = i.Inventory;
            item.Status = i.Status;
            list_WCF.Add(item);

        }
        return list_WCF;

    }

    public List<ItemWithQty> ConvertToItemWithQtyList(List<WCF_ItemWithQty> list)
    {
        List<ItemWithQty> list1 = new List<ItemWithQty>();
        foreach (WCF_ItemWithQty i in list)
        {
            Item item = new Item();
            item = cpoc.GetItemByItem_ID(i.Item_ID);
            ItemWithQty it = new ItemWithQty();
            it.Item_ID = item.Item_ID;
            it.Category_ID = item.Category_ID;
            it.Item_Name = item.Item_Name;
            it.Reorder_Level = item.Reorder_Level;
            it.Reorder_Qty = item.Reorder_Qty;
            it.UOM = item.UOM;
            it.Bin_ID = item.Bin_ID;
            it.FirstSupplier_ID = item.FirstSupplier_ID;
            it.SecondSupplier_ID = item.SecondSupplier_ID;
            it.ThirdSupplier_ID = item.ThirdSupplier_ID;
            it.Inventory = item.Inventory;
            it.Status = item.Status;
            it.Qty = i.Qty;
            list1.Add(it);

        }
        return list1;

    }

    public Dictionary<string, List<ItemWithQty>> ConvertToDictionary(SubmitPurchaseOrderLogic spol)
    {

        Dictionary<string, List<ItemWithQty>> d = new Dictionary<string, List<ItemWithQty>>();
        d.Add("ALPA", spol.ALPAod);
        d.Add("BANE", spol.BANEod);
        d.Add("CHEP", spol.CHEPod);
        d.Add("OMEG", spol.OMEGod);
        return d;
    }

    public List<WCF_Item> ConvertToWCFItemList(List<Item> list)
    {
        List<WCF_Item> list_WCF = new List<WCF_Item>();
        foreach (Item i in list)
        {
            WCF_Item item = new WCF_Item();
            item.Item_ID = i.Item_ID;
            item.Category_ID = i.Category_ID;
            item.Item_Name = i.Item_Name;
            item.Reorder_Level = i.Reorder_Level;
            item.Reorder_Qty = i.Reorder_Qty;
            item.UOM = i.UOM;
            item.Bin_ID = i.Bin_ID;
            item.FirstSupplier_ID = i.FirstSupplier_ID;
            if (i.SecondSupplier_ID == null)
            {
                item.SecondSupplier_ID = "";
            }
            else
            {
                item.SecondSupplier_ID = i.SecondSupplier_ID;
            }
            if (i.ThirdSupplier_ID == null)
            {
                item.ThirdSupplier_ID = "";
            }
            else
            {
                item.ThirdSupplier_ID = i.ThirdSupplier_ID;
            }
            item.Inventory = i.Inventory;
            item.Status = i.Status;
            list_WCF.Add(item);

        }
        return list_WCF;

    }

    public List<WCF_ReorderReminderTable> ConvertToWCFReorderReminderTable(List<ReorderReminderTable> rrtl){
        List<WCF_ReorderReminderTable> wcfRRT = new List<WCF_ReorderReminderTable>();
        foreach (ReorderReminderTable rrt in rrtl)
        {
            WCF_ReorderReminderTable rrt1 = new WCF_ReorderReminderTable();
            rrt1.Category_ID = rrt.Category_ID;
            rrt1.Inventory = rrt.Inventory;
            rrt1.InventoryAddOrder = rrt.InventoryAddOrder;
            rrt1.Item_ID = rrt.Item_ID;
            rrt1.Item_Name = rrt.Item_Name;
            rrt1.Reorder_Level = rrt.Reorder_Level;
            rrt1.Reorder_Qty = rrt.Reorder_Qty;
            wcfRRT.Add(rrt1);
        }
        return wcfRRT;
    }

    public List<WCF_OrderList> ConvertToWCFOrderList(List<OrderList> orderList)
    {
        List<WCF_OrderList> wcfOrderListL = new List<WCF_OrderList>();
        foreach (OrderList ol in orderList)
        {
            WCF_OrderList wcfOrder = new WCF_OrderList();
            wcfOrder.Order_ID = ol.Order_ID;
            wcfOrder.Supplier_ID = ol.Supplier_ID;
            wcfOrder.Create_Date = ol.Create_Date.ToString("yyyy-MM-dd HH:mm:ss");
            if (ol.Finish_Date == null)
            {
                wcfOrder.Finish_Date = "";
            }
            else
            {
                wcfOrder.Finish_Date = ol.Finish_Date.ToString();
            }
            wcfOrder.OrdStaff_ID = ol.OrdStaff_ID;
            if (ol.RecStaff_ID == null)
            {
                wcfOrder.RecStaff_ID = "";
            }
            else
            {
                wcfOrder.RecStaff_ID = ol.RecStaff_ID;
            }
            wcfOrder.Status = ol.Status;
            if (wcfOrder.Remark == null)
            {
                wcfOrder.Remark = "";
            }
            else
            {
                wcfOrder.Remark = ol.Remark;
            }
            wcfOrderListL.Add(wcfOrder);
        }
        return wcfOrderListL;
    }

    public List<WCF_OrderListDetail> ConvertToWCFOrderDetailList(List<OrderListDetail> orderListDetail)
    {
        List<WCF_OrderListDetail> wcfOrderListL = new List<WCF_OrderListDetail>();
        foreach (OrderListDetail ol in orderListDetail)
        {
            WCF_OrderListDetail wcfOrder = new WCF_OrderListDetail();
            wcfOrder.Order_ID = ol.Order_ID;
            wcfOrder.Item_ID = ol.Item_ID;
            wcfOrder.Needed_Qty = ol.Needed_Qty;
            if (ol.Received_Qty == null)
            {
                wcfOrder.Received_Qty = 0;
            }
            else
            {
                wcfOrder.Received_Qty = (int)ol.Received_Qty;
            }
            
            wcfOrderListL.Add(wcfOrder);
        }
        return wcfOrderListL;
    }

    public List<WCF_ReceiveOrderTable> ConvertToWCFReceiveOrderTableList(List<ReceiveOrderTable> receiveOrderTable)
    {
        List<WCF_ReceiveOrderTable> wcfROL = new List<WCF_ReceiveOrderTable>();
        foreach (ReceiveOrderTable ro in receiveOrderTable)
        {
            int receivedQty;
            if (ro.Received_Qty == null)
            {
                receivedQty = 0;
            }
            else
            {
                receivedQty = (int)ro.Received_Qty;
            }
            WCF_ReceiveOrderTable wcfRO = WCF_ReceiveOrderTable.Make(ro.Item_ID, ro.Category_ID, ro.Item_Name, ro.Needed_Qty, receivedQty, ro.UOM);
            wcfROL.Add(wcfRO);
        }
        return wcfROL;
    }
}