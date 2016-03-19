using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BL;
using BL.Store;
using BL.Utilities;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IItemService" in both code and config file together.
[ServiceContract]
public interface IItemService
{
    //ALL ARE ORDER PART FOR Clerk
    //henry
    [OperationContract]
    [WebGet(UriTemplate = "/GetItemList", ResponseFormat = WebMessageFormat.Json)]
    List<WCF_Item> GetItemList();

    [OperationContract]
    [WebGet(UriTemplate = "/GetAllItemCategory", ResponseFormat = WebMessageFormat.Json)]
    List<String> GetAllItemCategory();

    
    [OperationContract]
    [WebInvoke(UriTemplate = "/InsertOrderList", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
    Dictionary<string, List<ItemWithQty>> InsertOrderList(WCF_ItemWithQtyAndClerk wcfIWQACL);

    [OperationContract]
    [WebGet(UriTemplate = "/GetReorderItemList", ResponseFormat = WebMessageFormat.Json)]
    List<WCF_ReorderReminderTable> GetReorderItemList();

    [OperationContract]
    [WebGet(UriTemplate = "/GetOrderListByOrderClerk/{clerkID}", ResponseFormat = WebMessageFormat.Json)]
    List<WCF_OrderList> GetOrderListByOrderClerk(String clerkID);

    [OperationContract]
    [WebGet(UriTemplate = "/GetPurchaseOrderDetailByOrderID/{orderID}", ResponseFormat = WebMessageFormat.Json)]
    List<WCF_OrderListDetail> GetPurchaseOrderDetailByOrderID(String orderID);

    [OperationContract]
    [WebInvoke(UriTemplate = "/CancelPurchaseOrder", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
    bool CancelPurchaseOrder(WCF_OrderList wcfOL);

    [OperationContract]
    [WebGet(UriTemplate = "/isPurchaseOrderExist/{poNumber}", ResponseFormat = WebMessageFormat.Json)]
    int isPurchaseOrderExist(String poNumber);

    [OperationContract]
    [WebGet(UriTemplate = "/GetReceivePurchaseOrder/{poNumber}", ResponseFormat = WebMessageFormat.Json)]
    List<WCF_ReceiveOrderTable> GetReceivePurchaseOrder(String poNumber);

    [OperationContract]
    [WebInvoke(UriTemplate = "/ReceivePurchaseOrder", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
    bool ReceivePurchaseOrder(WCF_ReceivePurchaseOrder wcfRPO);
}

[DataContract]
public class WCF_ReceivePurchaseOrder
{
    [DataMember]
    public string orderid { get; set; }
    [DataMember]
    public List<int> recNumberList { get; set; }
    [DataMember]
    public string remark { get; set; }
    [DataMember]
    public string recStaff { get; set; }

    public static WCF_ReceivePurchaseOrder Make(string orderid, List<int> recNumberList, string remark, string recStaff)
    {
        WCF_ReceivePurchaseOrder wcfRO = new WCF_ReceivePurchaseOrder();
        wcfRO.orderid = orderid;
        wcfRO.recNumberList = recNumberList;
        wcfRO.remark = remark;
        wcfRO.recStaff = recStaff;
        return wcfRO;
    }
}


[DataContract]
public class WCF_ReceiveOrderTable
{
    [DataMember]
    public string Item_ID { get; set; }
    [DataMember]
    public string Category_ID { get; set; }
    [DataMember]
    public string Item_Name { get; set; }
    [DataMember]
    public int Needed_Qty { get; set; }
    [DataMember]
    public int Received_Qty { get; set; }
    [DataMember]
    public string UOM { get; set; }

    public static WCF_ReceiveOrderTable Make(string Item_ID, string Category_ID, string Item_Name, int Needed_Qty, int Received_Qty, string UOM)
    {
        WCF_ReceiveOrderTable wcfRO = new WCF_ReceiveOrderTable();
        wcfRO.Item_ID = Item_ID;
        wcfRO.Category_ID = Category_ID;
        wcfRO.Item_Name = Item_Name;
        wcfRO.Needed_Qty = Needed_Qty;
        wcfRO.Received_Qty = Received_Qty;
        wcfRO.UOM = UOM;
        return wcfRO;
    }
}

[DataContract]
public class WCF_OrderListDetail
{
    [DataMember]
    public string Order_ID { get; set; }
    [DataMember]
    public string Item_ID { get; set; }
    [DataMember]
    public int Needed_Qty { get; set; }
    [DataMember]
    public int Received_Qty { get; set; }

    public static WCF_OrderListDetail Make(string Order_ID, string Item_ID, int Needed_Qty, int Received_Qty)
    {
        WCF_OrderListDetail wcfOL = new WCF_OrderListDetail();
        wcfOL.Order_ID = Order_ID;
        wcfOL.Item_ID = Item_ID;
        wcfOL.Needed_Qty = Needed_Qty;
        wcfOL.Received_Qty = Received_Qty;
        return wcfOL;
    }
}

[DataContract]
public class WCF_OrderList
{
    [DataMember]
    public string Order_ID { get; set; }
    [DataMember]
    public string Supplier_ID { get; set; }
    [DataMember]
    public string Create_Date { get; set; }
    [DataMember]
    public string Finish_Date { get; set; }
    [DataMember]
    public string OrdStaff_ID { get; set; }
    [DataMember]
    public string RecStaff_ID { get; set; }
    [DataMember]
    public string Status { get; set; }
    [DataMember]
    public string Remark { get; set; }

    public static WCF_OrderList Make(string Order_ID, string Supplier_ID, string Create_Date,
        string Finish_Date, string OrdStaff_ID, string RecStaff_ID, string Status, string Remark)
    {
        WCF_OrderList wcfOL = new WCF_OrderList();
        wcfOL.Order_ID = Order_ID;
        wcfOL.Supplier_ID = Supplier_ID;
        wcfOL.Create_Date = Create_Date;
        wcfOL.Finish_Date = Finish_Date;
        wcfOL.OrdStaff_ID = OrdStaff_ID;
        wcfOL.RecStaff_ID = RecStaff_ID;
        wcfOL.Status = Status;
        wcfOL.Remark = Remark;
        return wcfOL;
    }
}


[DataContract]
public class WCF_ItemWithQtyAndClerk
{
    [DataMember]
    public List<WCF_ItemWithQty> wcfIWQL { get; set; }
    [DataMember]
    public string clerkID { get; set; }

    public static WCF_ItemWithQtyAndClerk Make(string clerkID, List<WCF_ItemWithQty> wcfIWQL)
    {
        WCF_ItemWithQtyAndClerk iwqac = new WCF_ItemWithQtyAndClerk();
        iwqac.wcfIWQL = wcfIWQL;
        iwqac.clerkID = clerkID;
        return iwqac;
    }
}

[DataContract]
public class WCF_ItemWithQty
{
    [DataMember]
    public string Item_ID { get; set; }
    [DataMember]
    public string Category_ID { get; set; }
    [DataMember]
    public string Item_Name { get; set; }
    [DataMember]
    public int Reorder_Level { get; set; }
    [DataMember]
    public int Reorder_Qty { get; set; }
    [DataMember]
    public string UOM { get; set; }
    [DataMember]
    public string Bin_ID { get; set; }
    [DataMember]
    public string FirstSupplier_ID { get; set; }
    [DataMember]
    public string SecondSupplier_ID { get; set; }
    [DataMember]
    public string ThirdSupplier_ID { get; set; }
    [DataMember]
    public int Inventory { get; set; }
    [DataMember]
    public string Status { get; set; }
    [DataMember]
    public int Qty { get; set; }

    public static WCF_ItemWithQty Make(string Item_ID, string Category_ID, string Item_Name, int Reorder_Level,
        int Reorder_Qty, string UOM, string Bin_ID, string FirstSupplier_ID, string SecondSupplier_ID,
        string ThirdSupplier_ID, int Inventory, string Status, int Qty)
    {
        WCF_ItemWithQty it = new WCF_ItemWithQty();
        it.Item_ID = Item_ID;
        it.Category_ID = Category_ID;
        it.Item_Name = Item_Name;
        it.Reorder_Level = Reorder_Level;
        it.Reorder_Qty = Reorder_Qty;
        it.UOM = UOM;
        it.Bin_ID = Bin_ID;
        it.FirstSupplier_ID = FirstSupplier_ID;
        it.SecondSupplier_ID = SecondSupplier_ID;
        it.ThirdSupplier_ID = ThirdSupplier_ID;
        it.Inventory = Inventory;
        it.Status = Status;
        it.Qty = Qty;
        return it;
    }


}

[DataContract]
public class WCF_Item
{
    [DataMember]
    public string Item_ID { get; set; }
    [DataMember]
    public string Category_ID { get; set; }
    [DataMember]
    public string Item_Name { get; set; }
    [DataMember]
    public int Reorder_Level { get; set; }
    [DataMember]
    public int Reorder_Qty { get; set; }
    [DataMember]
    public string UOM { get; set; }
    [DataMember]
    public string Bin_ID { get; set; }
    [DataMember]
    public string FirstSupplier_ID { get; set; }
    [DataMember]
    public string SecondSupplier_ID { get; set; }
    [DataMember]
    public string ThirdSupplier_ID { get; set; }
    [DataMember]
    public int Inventory { get; set; }
    [DataMember]
    public string Status { get; set; }

    public static WCF_Item Make(string Item_ID, string Category_ID, string Item_Name, int Reorder_Level, 
        int Reorder_Qty, string UOM, string Bin_ID, string FirstSupplier_ID, string SecondSupplier_ID,
        string ThirdSupplier_ID, int Inventory, string Status)
    {
        WCF_Item it = new WCF_Item();
        it.Item_ID = Item_ID;
        it.Category_ID = Category_ID;
        it.Item_Name = Item_Name;
        it.Reorder_Level = Reorder_Level;
        it.Reorder_Qty = Reorder_Qty;
        it.UOM = UOM;
        it.Bin_ID = Bin_ID;
        it.FirstSupplier_ID = FirstSupplier_ID;
        it.SecondSupplier_ID = SecondSupplier_ID;
        it.ThirdSupplier_ID = ThirdSupplier_ID;
        it.Inventory = Inventory;
        it.Status = Status;
        return it;
    }
}

[DataContract]
public class WCF_ReorderReminderTable
{
    [DataMember]
    public string Item_ID { get; set; }
    [DataMember]
    public string Category_ID { get; set; }
    [DataMember]
    public string Item_Name { get; set; }
    [DataMember]
    public int Reorder_Level { get; set; }
    [DataMember]
    public int Reorder_Qty { get; set; }
    [DataMember]
    public int Inventory { get; set; }
    [DataMember]
    public int InventoryAddOrder { get; set; }

    public static WCF_ReorderReminderTable Make(string Item_ID, string Category_ID, string Item_Name, int Reorder_Level,
        int Reorder_Qty, int Inventory, int InventoryAddOrder)
    {
        WCF_ReorderReminderTable rr = new WCF_ReorderReminderTable();
        rr.Item_ID = Item_ID;
        rr.Category_ID = Category_ID;
        rr.Item_Name = Item_Name;
        rr.Reorder_Level = Reorder_Level;
        rr.Reorder_Qty = Reorder_Qty;
        rr.Inventory = Inventory;
        rr.InventoryAddOrder = InventoryAddOrder;
        return rr;
    }
}