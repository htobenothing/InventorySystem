using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using System.Diagnostics;
using BL.Store.TableView;
using BL.Utilities;
using System.Data.Entity.Core.Objects;

namespace BL.Store
{
    public class PurchaseOrderController
    {
        InventorySysDBEntities context = new InventorySysDBEntities();
        List<Item> ItemList = new List<Item>();

        //Part 1 Create purchase order
        //****************************************
        public List<Item> GetAllItem()
        {
            var q = from x in context.Items where x.Item_ID != null select x;
            ItemList = q.ToList<Item>();
            return ItemList;
        }

        public List<Item> GetItemByCategory(string cg)
        {
            var q = from x in context.Items where x.ItemCategory.Category_ID == cg select x;
            return q.ToList<Item>();
        }
        
        public Item GetItemByItem_ID(string id)
        {
            var q = from x in context.Items where x.Item_ID == id select x;
            return q.First();
        }

        public Item GetItemByDesc(string desc)
        {
            var q = from x in context.Items where x.Item_Name == desc select x;
            return q.First();
        }

        public List<string> GetAllItemCode()
        {
            var q = context.Items.Select(x => x.Item_ID).Distinct();
            return q.ToList<string>();
        }

        public List<string> GetAllItemCategory()
        {
            var q = context.Items.Select(x => x.ItemCategory.Category_ID).Distinct();
            return q.ToList<string>();
        }

        public List<string> GetAllItemDesc()
        {
            var q = context.Items.Select(x => x.Item_Name).Distinct();
            return q.ToList<string>();
        }

        public Item GetDefaultItem()
        {
            var q = from x in context.Items where x.Item_ID == "C001" select x;
            return q.First();
        }

        public int GetInventoryFromSupplier(string SupID,string ItemID)
        {
            var q = from x in context.SupplierInventories where x.Supplier_ID == SupID&&x.Item_ID ==ItemID select x;
            return q.First().Inventory_Qty;
        }

        public string CreateOrderID(string sup)
        {
           
            switch(sup)
            {
                case "ALPA":
                    string id1 ="Po01" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    var q1 = from x in context.OrderLists where x.Order_ID ==id1 select x;
                    if (q1.ToList().Count == 0)
                        return id1;
                    else
                        return "fail"; //to protect there is no duplicate order id;
                case "BANE":
                    string id2 = "Po02" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    var q2 = from x in context.OrderLists where x.Order_ID == id2 select x;
                    if (q2.ToList().Count == 0)
                        return id2;
                    else
                        return "fail"; //to protect there is no duplicate order id;
                case "CHEP":
                    string id3 = "Po03" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    var q3 = from x in context.OrderLists where x.Order_ID == id3 select x;
                    if (q3.ToList().Count == 0)
                        return id3;
                    else
                        return "fail"; //to protect there is no duplicate order id;
                case "OMEG":
                    string id4 = "Po04" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    var q4 = from x in context.OrderLists where x.Order_ID == id4 select x;
                    if (q4.ToList().Count == 0)
                        return id4;
                    else
                        return "fail"; //to protect there is no duplicate order id;
                default:
                    return "fail";
            }
        }

        public void NewPurchaseOrder(string id,string sup, string stf,string duedate)
        {
            try
            {
                OrderList ol = new OrderList();
                ol.Order_ID = id;
                ol.Supplier_ID = sup;
                ol.Create_Date = DateTime.Now;
                ol.OrdStaff_ID = stf;
                ol.Status = "Submitted";
                ol.Remark = "Please deliver before " + duedate;
                context.OrderLists.Add(ol);
                context.SaveChanges();

            }

            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
        }

        public void NewPurchaseOrder(string id, string sup, string stf)
        {
            try
            {
                OrderList ol = new OrderList();
                ol.Order_ID = id;
                ol.Supplier_ID = sup;
                ol.Create_Date = DateTime.Now;
                ol.OrdStaff_ID = stf;
                ol.Status = "Submitted";
                context.OrderLists.Add(ol);
                context.SaveChanges();

            }

            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
        }

        public void NewPurchaseOrderDetail(string orderid,string supid,List<ItemWithQtyModel> ItemList)
        {
            try
            {
                for (int i = 0; i < ItemList.Count; i++)
                {
                    OrderListDetail old = new OrderListDetail();
                    if(ItemList[i].Qty != 0)
                    {
                        string id = ItemList[i].Item_ID;
                        int qty = ItemList[i].Qty;
                        old.Order_ID = orderid;
                        old.Item_ID = id;
                        old.Needed_Qty = qty;
                        var q = from x in context.SupplierInventories where x.Item_ID == id && x.Supplier_ID == supid select x;
                        SupplierInventory si = q.First();
                        si.Inventory_Qty = si.Inventory_Qty - old.Needed_Qty;
                        context.OrderListDetails.Add(old);
                        context.SaveChanges();
                    }
                    
                }
                
            }
            catch(Exception e)
            {
                e.Source = "Create purchase order detail fail!";
                
            }
        }

        public List<PurchaseOrderTableModel> CreatePurchaseOrderGridView(List<ItemWithQtyModel> ItemList)
        {
            List<PurchaseOrderTableModel> table = new List<PurchaseOrderTableModel>();
            foreach(ItemWithQtyModel i in ItemList)
            {
                PurchaseOrderTableModel t = new PurchaseOrderTableModel();
                t.Category = i.Category_ID;
                t.Description = i.Item_Name;
                t.Item_ID = i.Item_ID;
                t.Quantity = i.Qty;
                t.UOM = i.UOM;
                table.Add(t);
            }
            return table;
        }
        //****************************************

        //Part2 Cancel purchase order
        //****************************************

        public List<OrderList> GetOrderListByOrderClerk(string userID)
        {
            var q = from x in context.OrderLists where x.OrdStaff_ID == userID&&x.Status =="Submitted" select x;
            return q.ToList<OrderList>();
        }

        public List<CancelOrderTableModel> CancelPurchaseOrderGridView(List<OrderList> ol)
        {
            List<CancelOrderTableModel> table = new List<CancelOrderTableModel>();
            foreach(OrderList o in ol)
            {
                CancelOrderTableModel c = new CancelOrderTableModel();
                c.Order_ID = o.Order_ID;
                c.Supplier = o.Supplier_ID;
                c.Create_Date = o.Create_Date.ToString("yyyy-MM-dd HH:mm:ss");
                table.Add(c);
            }
            return table;
        }

        public List<OrderListDetail> GetPurchaseOrderDetailByOrderID(string orderID)
        {
            var q = from x in context.OrderListDetails where x.Order_ID == orderID select x;
            return q.ToList();
        }

        public List<PurchaseOrderTableModel> CancelPurchaseOrderDetailGridView(List<OrderListDetail> DetailList)
        {
            List<PurchaseOrderTableModel> table = new List<PurchaseOrderTableModel>();
            foreach(OrderListDetail old in DetailList)
            {
                PurchaseOrderTableModel t = new PurchaseOrderTableModel();
                t.Category = old.Item.Category_ID;
                t.Item_ID = old.Item_ID;
                t.Description = old.Item.Item_Name;
                t.Quantity = old.Needed_Qty;
                t.UOM = old.Item.UOM;
                table.Add(t);
            }
            return table;
        }

        public bool CancelPurchaseOrder(string orderID,string remark)
        {
            try
            {
                var q = from x in context.OrderLists where x.Order_ID == orderID select x;
                OrderList ol = q.First();
                var p = from x in context.OrderListDetails where x.Order_ID == orderID select x;
                foreach (OrderListDetail od in p.ToList())
                {
                    var r = from x in context.SupplierInventories where x.Item_ID == od.Item_ID && x.Supplier_ID == ol.Supplier_ID select x;
                    SupplierInventory si = r.First();
                    si.Inventory_Qty = si.Inventory_Qty + od.Needed_Qty;
                }
                ol.Status = "Cancelled";
                ol.Finish_Date = DateTime.Now;
                if (remark != "")
                    ol.Remark = remark;
                else
                    ol.Remark = "Cancelled";
                context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
            
        }
        //*******************************************

        //Part3 Receive purchase order
        //****************************************

        public int isPurchaseOrderExist(string poNumber)
        {
            var q = from x in context.OrderLists where x.Order_ID == poNumber select x;
            if (q.ToList().Count == 0)
                return 0;
            else
                if (q.First().Status == "Received")
                    return 1;
                else
                    if (q.First().Status == "Cancelled")
                        return 2;
            return 3;
        }

        public List<ReceiveOrderTableModel> ReceivePurchaseOrderGrideView(string poNumber)
        {
            List<ReceiveOrderTableModel> rot = new List<ReceiveOrderTableModel>();
            var q = from x in context.OrderListDetails where x.Order_ID == poNumber&&x.OrderList.Status=="Submitted" select x;
            foreach(OrderListDetail old in q.ToList())
            {
                ReceiveOrderTableModel r = new ReceiveOrderTableModel();
                r.Item_ID = old.Item_ID;
                r.Item_Name = old.Item.Item_Name;
                r.Category_ID = old.Item.ItemCategory.Category_ID;
                r.Needed_Qty = old.Needed_Qty;
                r.UOM = old.Item.UOM;
                r.Received_Qty = 0;
                rot.Add(r);
            }
            return rot;
        }

        public bool ReceivePurchaseOrder(string orderid,List<int> recNumberList,string remark,string recStaff)
        {
            try
            {
                var q = from x in context.OrderLists where x.Order_ID == orderid select x;
                OrderList ol = q.First();
                var p = from x in context.OrderListDetails where x.Order_ID == orderid select x;
                List<OrderListDetail> old = p.ToList();
                for(int i=0;i<old.Count;i++)
                {
                    string ItemID = old[i].Item_ID;
                    old[i].Received_Qty = recNumberList[i];
                    ItemTransaction it = new ItemTransaction();
                    it.Item_ID = old[i].Item_ID;
                    it.Update_Date = DateTime.Now;
                    it.Reason = "Received from " + ol.Supplier_ID;
                    it.Quantity = recNumberList[i];

                    var r = from x in context.ItemTransactions where x.Item_ID == ItemID select x;

                    List<ItemTransaction> itList = r.ToList();
                    it.Balance = itList.Last().Balance;
                    it.Balance = it.Balance + recNumberList[i];
                    var o = from x in context.Items where x.Item_ID == ItemID select x;
                    o.First().Inventory = it.Balance;
                    context.ItemTransactions.Add(it);
                }
                ol.Status = "Received";
                if (remark != null)
                    ol.Remark = remark;
                ol.Finish_Date = DateTime.Now;
                ol.RecStaff_ID = recStaff;
                context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        //Mobile
        public void GenerateOrderListByMobile(Dictionary<string, List<ItemWithQtyModel>> SupItemMap, string clerkID)
        {
            Dictionary<string, string> SupOrderIDMap = new Dictionary<string, string>();

            foreach (string supID in SupItemMap.Keys)
            {
                if (SupItemMap[supID].Count != 0)
                {
                    SupOrderIDMap.Add(supID, CreateOrderID(supID));
                }
                else
                    SupOrderIDMap.Add(supID, "fail");
            }

            if (SupOrderIDMap.ContainsKey("ALPA"))
            {
                if (SupOrderIDMap["ALPA"] != "fail")
                {
                    List<ItemWithQtyModel> ALPAList = SupItemMap["ALPA"];
                    NewPurchaseOrder(SupOrderIDMap["ALPA"], "ALPA", clerkID);
                    NewPurchaseOrderDetail(SupOrderIDMap["ALPA"], "ALPA", ALPAList);
                    //PurchaseOrderPDF POPDF = new PurchaseOrderPDF();
                    //POPDF.MakePurchaseOrderPDF("ALPA", SupOrderIDMap["ALPA"],"", clerkID, ALPAList);
                    //PurchaseOrderMail POM = new PurchaseOrderMail();
                    //POM.email_send(POM.GetSupplierEmail("ALPA"), "C:\\Users\\Wangrz\\Downloads\\ADpo\\Logic University Purchase Order_" + SupOrderIDMap["ALPA"] + ".pdf");
                }
            }

            if (SupOrderIDMap.ContainsKey("BANE"))
            {
                if (SupOrderIDMap["BANE"] != "fail")
                {
                    List<ItemWithQtyModel> BANEList = SupItemMap["BANE"];
                    NewPurchaseOrder(SupOrderIDMap["BANE"], "BANE", clerkID);
                    NewPurchaseOrderDetail(SupOrderIDMap["BANE"], "BANE", BANEList);
                    //PurchaseOrderPDF POPDF = new PurchaseOrderPDF();
                    //POPDF.MakePurchaseOrderPDF("BANE", SupOrderIDMap["BANE"], "", clerkID, BANEList);
                    //PurchaseOrderMail POM = new PurchaseOrderMail();
                    //POM.email_send(POM.GetSupplierEmail("BANE"), "C:\\Users\\Wangrz\\Downloads\\ADpo\\Logic University Purchase Order_" + SupOrderIDMap["BANE"] + ".pdf");
                }
            }

            if (SupOrderIDMap.ContainsKey("CHEP"))
            {
                if (SupOrderIDMap["CHEP"] != "fail")
                {
                    List<ItemWithQtyModel> CHEPList = SupItemMap["CHEP"];
                    NewPurchaseOrder(SupOrderIDMap["CHEP"], "CHEP", clerkID);
                    NewPurchaseOrderDetail(SupOrderIDMap["CHEP"], "CHEP", CHEPList);
                    //PurchaseOrderPDF POPDF = new PurchaseOrderPDF();
                    //POPDF.MakePurchaseOrderPDF("CHEP", SupOrderIDMap["CHEP"], "", clerkID, CHEPList);
                    //PurchaseOrderMail POM = new PurchaseOrderMail();
                    // POM.email_send(POM.GetSupplierEmail("CHEP"), "C:\\Users\\Wangrz\\Downloads\\ADpo\\Logic University Purchase Order_" + SupOrderIDMap["CHEP"] + ".pdf");
                }
            }

            if (SupOrderIDMap.ContainsKey("OMEG"))
            {
                if (SupOrderIDMap["OMEG"] != "fail")
                {
                    List<ItemWithQtyModel> OMEGList = SupItemMap["OMEG"];
                    NewPurchaseOrder(SupOrderIDMap["OMEG"], "OMEG", clerkID);
                    NewPurchaseOrderDetail(SupOrderIDMap["OMEG"], "OMEG", OMEGList);
                    //PurchaseOrderPDF POPDF = new PurchaseOrderPDF();
                    //POPDF.MakePurchaseOrderPDF("OMEG", SupOrderIDMap["OMEG"], "", clerkID, OMEGList);
                    //PurchaseOrderMail POM = new PurchaseOrderMail();
                    //POM.email_send(POM.GetSupplierEmail("OMEG"), "C:\\Users\\Wangrz\\Downloads\\ADpo\\Logic University Purchase Order_" + SupOrderIDMap["OMEG"] + ".pdf");
                }
            }

        }



        public OrderListTableModel GetOrderByid(string id)
        {
            var query = (from i in context.OrderLists
                         where i.Order_ID == id
                         select new OrderListTableModel
                         {
                             Order_ID = i.Order_ID,
                             Supplier_Name = i.Supplier.Supplier_Name,
                             Order_Date = i.Create_Date,
                             Received_Date = i.Finish_Date,
                             Rec_Staff = i.RecStaff.Staff_Name,
                             Ord_Staff = i.OrdStaff.Staff_Name,
                             Status = i.Status


                         });

            return query.ToList().FirstOrDefault();
        }

        public List<OrderListTableModel> GetOrderBySupplier(string sup)
        {
            var query = (from i in context.OrderLists
                         where i.Supplier_ID == sup
                         select new OrderListTableModel
                         {
                             Order_ID = i.Order_ID,
                             Supplier_Name = i.Supplier.Supplier_Name,
                             Order_Date = i.Create_Date,
                             Received_Date = i.Finish_Date,
                             Rec_Staff = i.RecStaff.Staff_Name,
                             Ord_Staff = i.OrdStaff.Staff_Name,
                             Status = i.Status


                         });

            return query.ToList<OrderListTableModel>();
        }






        public List<OrderListTableModel> getRecStaffbyStatus(String status)
        {
            var q = (from i in context.OrderLists
                     where i.Status == status
                     select new OrderListTableModel
                     {
                         Order_ID = i.Order_ID,
                         Supplier_Name = i.Supplier.Supplier_Name,
                         Order_Date = i.Create_Date,
                         Received_Date = i.Finish_Date,
                         Ord_Staff = i.OrdStaff.Staff_Name,
                         Rec_Staff = i.RecStaff.Staff_Name,

                         Status = i.Status,
                         Rec_Staff_id = i.RecStaff_ID


                     });

            return q.ToList<OrderListTableModel>();
        }


        public List<OrderListTableModel> GetOrderBySupplierAndStatus(string sup, string status)
        {
            var q = (from i in context.OrderLists
                     where i.Supplier_ID == sup && i.Status == status
                     select new OrderListTableModel
                     {
                         Order_ID = i.Order_ID,
                         Supplier_Name = i.Supplier.Supplier_Name,
                         Order_Date = i.Create_Date,
                         Received_Date = i.Finish_Date,
                         Rec_Staff = i.RecStaff.Staff_Name,
                         Ord_Staff = i.OrdStaff.Staff_Name,
                         Status = i.Status


                     });



            return q.ToList<OrderListTableModel>();
        }

        public List<OrderListTableModel> GetOrderByOrdStaffAndStatusAndRecStaff(string ordStaff, string status, string recstaff)
        {
            var q = (from i in context.OrderLists
                     where i.OrdStaff_ID == ordStaff && i.Status == status && i.RecStaff_ID == recstaff
                     select new OrderListTableModel
                     {
                         Order_ID = i.Order_ID,
                         Supplier_Name = i.Supplier.Supplier_Name,
                         Order_Date = i.Create_Date,
                         Received_Date = i.Finish_Date,
                         Rec_Staff = i.RecStaff.Staff_Name,
                         Ord_Staff = i.OrdStaff.Staff_Name,
                         Status = i.Status


                     });



            return q.ToList<OrderListTableModel>();
        }
        public List<OrderListTableModel> GetOrderByOrdStaffAndStatus(string ordStaff, string status)
        {
            var q = (from i in context.OrderLists
                     where i.OrdStaff_ID == ordStaff && i.Status == status
                     select new OrderListTableModel
                     {
                         Order_ID = i.Order_ID,
                         Supplier_Name = i.Supplier.Supplier_Name,
                         Order_Date = i.Create_Date,
                         Received_Date = i.Finish_Date,
                         Rec_Staff_id = i.RecStaff_ID,
                         Rec_Staff = i.RecStaff.Staff_Name,
                         Ord_Staff = i.OrdStaff.Staff_Name,
                         Status = i.Status


                     });



            return q.ToList<OrderListTableModel>();
        }

        public List<string> GetStatusByOrdStaff(string ordStaff)
        {
            var q = from i in context.OrderLists
                    where i.OrdStaff_ID == ordStaff
                    select i.Status;

            return q.ToList<string>();
        }

        public List<string> GetStatusBySupplier(string Sup)
        {
            var q = from i in context.OrderLists
                    where i.Supplier_ID == Sup
                    select i.Status;

            return q.ToList<string>();
        }


        public OrderListTableModel GetOrderByStatusReceived(string status, string Rec)
        {
            var q = (from i in context.OrderLists
                     where i.Status == status && i.RecStaff_ID == Rec
                     select new OrderListTableModel
                     {
                         Order_ID = i.Order_ID,
                         Supplier_Name = i.Supplier.Supplier_Name,
                         Order_Date = i.Create_Date,
                         Received_Date = i.Finish_Date,
                         Rec_Staff = i.RecStaff.Staff_Name,
                         Ord_Staff = i.OrdStaff.Staff_Name,
                         Status = i.Status


                     });



            return q.ToList().FirstOrDefault(); ;
        }


        public List<OrderListTableModel> getAllOrderList()
        {
            var q = (from i in context.OrderLists
                     where i.Order_ID != null
                     select new OrderListTableModel
                     {
                         Order_ID = i.Order_ID,
                         Supplier_Name = i.Supplier.Supplier_Name,
                         Order_Date = i.Create_Date,
                         Received_Date = i.Finish_Date,
                         Ord_Staff = i.OrdStaff.Staff_Name,
                         Rec_Staff = i.RecStaff.Staff_Name,
                         Rec_Staff_id = i.RecStaff_ID,
                         Status = i.Status,
                         Ord_Staff_id = i.OrdStaff_ID


                     });

            return q.ToList<OrderListTableModel>();
        }
        public List<OrderListTableModel> GetOrderByDate(DateTime fromdate, DateTime todate)
        {
            var q = (from i in context.OrderLists
                     where (EntityFunctions.TruncateTime(i.Create_Date) >= fromdate.Date && EntityFunctions.TruncateTime(i.Create_Date) <= todate.Date)

                     select new OrderListTableModel
                     {
                         Order_ID = i.Order_ID,
                         Supplier_Name = i.Supplier.Supplier_Name,
                         Order_Date = i.Create_Date,
                         Received_Date = i.Finish_Date,
                         Rec_Staff = i.RecStaff.Staff_Name,
                         Ord_Staff = i.OrdStaff.Staff_Name,
                         Status = i.Status


                     });
            return q.ToList<OrderListTableModel>();

        }

        public List<OrderListTableModel> GetOrderByOrderStaff(string OrdStaff)
        {
            var query = (from i in context.OrderLists
                         where i.OrdStaff_ID == OrdStaff
                         select new OrderListTableModel
                         {
                             Order_ID = i.Order_ID,
                             Supplier_Name = i.Supplier.Supplier_Name,
                             Order_Date = i.Create_Date,
                             Received_Date = i.Finish_Date,
                             Rec_Staff = i.RecStaff.Staff_Name,
                             Ord_Staff = i.OrdStaff.Staff_Name,
                             Status = i.Status


                         });

            return query.ToList<OrderListTableModel>();
        }

       
    }
}
