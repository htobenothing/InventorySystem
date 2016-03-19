using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Utilities;

namespace BL.Store
{
    public class SubmitPurchaseOrderController_M
    {
        int status;
        int[] qtyCanSuppList = new int[3];
        string[] supplierList= new string[3];
        string insufficientInfo="";
        string[] emptySup = { "", "", "" };
        int[] emptyNum = {0,0,0};
       
        public List<ItemWithQtyModel_M> ALPAod = new List<ItemWithQtyModel_M>();
        public List<ItemWithQtyModel_M> BANEod = new List<ItemWithQtyModel_M>();
        public List<ItemWithQtyModel_M> CHEPod = new List<ItemWithQtyModel_M>();
        public List<ItemWithQtyModel_M> OMEGod = new List<ItemWithQtyModel_M>();

        public string InsufficientInfo
        {
            get { return insufficientInfo; }
            set { insufficientInfo = value; }
        }
        public int[] QtyCanSuppList
        {
            get { return qtyCanSuppList; }
            set { qtyCanSuppList = value; }
        }       
        public string[] SupplierList
        {
            get { return supplierList; }
            set { supplierList = value; }
        }      
        public int Flag
        {
            get { return status; }
            set { status = value; }
        }

        
        public List<ItemWithQtyModel_M> CreateItemWithQty(List<Item> ItemList,List<int> qty)
        {
            List<ItemWithQtyModel_M> ItemWithQtyList= new List<ItemWithQtyModel_M>();
            for(int i=0;i<ItemList.Count;i++)
            {
                if(i >= 1 && ItemList.GetRange(0,i).Contains(ItemList[i]))
                    //for the list has add if it contain same item, should sum the qty
                {
                    for(int j =0;j<ItemWithQtyList.Count;j++)
                        if(ItemWithQtyList[j].Item_ID == ItemList[i].Item_ID)
                            ItemWithQtyList[j].Qty += qty[i];
                }
                else
                {
                    ItemWithQtyModel_M i1 = new ItemWithQtyModel_M();
                    i1.Item_ID = ItemList[i].Item_ID;
                    i1.Item_Name = ItemList[i].Item_Name;
                    i1.Category_ID = ItemList[i].ItemCategory.Category_ID;
                    i1.UOM = ItemList[i].UOM;
                    i1.Qty = qty[i];
                    i1.FirstSupplier_ID = ItemList[i].FirstSupplier_ID;
                    i1.SecondSupplier_ID = ItemList[i].SecondSupplier_ID;
                    i1.ThirdSupplier_ID = ItemList[i].ThirdSupplier_ID;
                    ItemWithQtyList.Add(i1);
                }
                
            }
            return ItemWithQtyList;
        }


        public bool AskOneSupplier(ItemWithQtyModel_M itemQty)
        {
            PurchaseOrderController_M CPOC = new PurchaseOrderController_M();

            int inventory = CPOC.GetInventoryFromSupplier(itemQty.FirstSupplier_ID, itemQty.Item_ID);
            if(inventory>=itemQty.Qty)
            {
                qtyCanSuppList[0] = itemQty.Qty;
                supplierList[0] = itemQty.FirstSupplier_ID;
                status = 1;
                return true;
            }
                 //mean supplier 1 can fulfill the order
            else
            {
                inventory = CPOC.GetInventoryFromSupplier(itemQty.SecondSupplier_ID, itemQty.Item_ID);
                if (inventory >= itemQty.Qty)
                {
                    qtyCanSuppList[0]= itemQty.Qty;
                    supplierList[0] = itemQty.SecondSupplier_ID;
                    status = 2;
                    return true;
                }
                  //mean supplier 2 can fulfill the order
                else
                {
                    inventory = CPOC.GetInventoryFromSupplier(itemQty.ThirdSupplier_ID, itemQty.Item_ID);
                    if (inventory >= itemQty.Qty)
                    {
                        qtyCanSuppList[0] = itemQty.Qty;
                        supplierList[0] = itemQty.ThirdSupplier_ID;
                        status = 3;
                        return true;
                    }
                        //mean supplier 3 can fulfill the order
                    else
                    {
                        status = -1;
                        return false;
                    }
                       //no one supplier can fulfill
                }
            }
        }

        public bool AskTwoSupplier(ItemWithQtyModel_M itemQty)
        {

            PurchaseOrderController_M CPOC = new PurchaseOrderController_M();
            int inventory1 = CPOC.GetInventoryFromSupplier(itemQty.FirstSupplier_ID, itemQty.Item_ID);
            int inventory2 = CPOC.GetInventoryFromSupplier(itemQty.SecondSupplier_ID, itemQty.Item_ID);
            if (inventory1 + inventory2 >= itemQty.Qty)
            {
                qtyCanSuppList[0] = inventory1;
                qtyCanSuppList[1] = itemQty.Qty - inventory1;
                supplierList[0] = itemQty.FirstSupplier_ID;
                supplierList[1] = itemQty.SecondSupplier_ID;
                status = 12;
                return true;
            }
                //means supplier 1 and supplier 2 can fulfill the order together
            else
            {
                inventory1 = CPOC.GetInventoryFromSupplier(itemQty.FirstSupplier_ID, itemQty.Item_ID);
                inventory2 = CPOC.GetInventoryFromSupplier(itemQty.ThirdSupplier_ID, itemQty.Item_ID);
                if (inventory1 + inventory2 >= itemQty.Qty)
                {
                    qtyCanSuppList[0] = inventory1;
                    qtyCanSuppList[1] = itemQty.Qty-inventory1;
                    supplierList[0] = itemQty.FirstSupplier_ID;
                    supplierList[1] = itemQty.ThirdSupplier_ID;
                    status = 13;
                    return true;
                }
                    //means supplier 1 and supplier 3 can fulfill the order together
                else
                {
                    inventory1 = CPOC.GetInventoryFromSupplier(itemQty.SecondSupplier_ID, itemQty.Item_ID);
                    inventory2 = CPOC.GetInventoryFromSupplier(itemQty.ThirdSupplier_ID, itemQty.Item_ID);
                    if (inventory1 + inventory2 >= itemQty.Qty)
                    {
                       qtyCanSuppList[0] = inventory1;
                       qtyCanSuppList[1] = itemQty.Qty - inventory1;
                       supplierList[0] = itemQty.SecondSupplier_ID;
                       supplierList[1] = itemQty.ThirdSupplier_ID;
                        status = 23;
                        return true;
                    }
                        //means supplier 2 and supplier 3 can fulfill the order together
                    else
                    {
                        status = -2;
                        return false;
                    }
                        //no two supplier can fulfill
                }
            }
        }

        public bool AskThreeSupplier(ItemWithQtyModel_M itemQty)
        {

            PurchaseOrderController_M CPOC = new PurchaseOrderController_M();
            int inventory1 = CPOC.GetInventoryFromSupplier(itemQty.FirstSupplier_ID, itemQty.Item_ID);
            int inventory2 = CPOC.GetInventoryFromSupplier(itemQty.SecondSupplier_ID, itemQty.Item_ID);
            int inventory3 = CPOC.GetInventoryFromSupplier(itemQty.ThirdSupplier_ID, itemQty.Item_ID);
            if (inventory1 + inventory2 + inventory3 >= itemQty.Qty)
            {
                qtyCanSuppList[0] = inventory1;
                qtyCanSuppList[1] = inventory2;
                qtyCanSuppList[2] = itemQty.Qty - inventory1-inventory2;
                supplierList[0] = itemQty.FirstSupplier_ID;
                supplierList[1] = itemQty.SecondSupplier_ID;
                supplierList[2] = itemQty.ThirdSupplier_ID;
                status = 123;
                return true;
            }
                
            else
            {
                qtyCanSuppList[0] = inventory1;
                qtyCanSuppList[1] = inventory2;
                qtyCanSuppList[2] = inventory3;
                supplierList[0] = itemQty.FirstSupplier_ID;
                supplierList[1] = itemQty.SecondSupplier_ID;
                supplierList[2] = itemQty.ThirdSupplier_ID;
                status = -3;
                return false;
            }
        }

        public List<OrderDetailController_M> CreateOrderLists(List<ItemWithQtyModel_M> ItemList)
        {
            List<OrderDetailController_M> OrderDetailList = new List<OrderDetailController_M>();
            if (ItemList.Count == 0)
                return OrderDetailList;
            for(int i = 0;i<ItemList.Count;i++)
            {
                bool flag;
                ItemWithQtyModel_M iwq = ItemList[i];
                emptyNum.CopyTo(qtyCanSuppList, 0);//Initialize two arrays
                emptySup.CopyTo(supplierList, 0);
                flag = AskOneSupplier(iwq);
                if(flag == true)
                {
                    OrderDetailController_M order = new OrderDetailController_M(iwq.Item_ID, supplierList, qtyCanSuppList);
                    OrderDetailList.Add(order);
                }
                else
                {
                    flag = AskTwoSupplier(iwq);
                    if(flag == true)
                    {
                        OrderDetailController_M order = new OrderDetailController_M(iwq.Item_ID, supplierList, qtyCanSuppList);
                        OrderDetailList.Add(order);
                    }
                    else
                    {
                        flag = AskThreeSupplier(iwq);
                        if(flag == true)
                        {
                            OrderDetailController_M order = new OrderDetailController_M(iwq.Item_ID, supplierList, qtyCanSuppList);
                            OrderDetailList.Add(order);
                        }
                        else
                        {
                            if(flag == false&&status==-3)
                            {
                                OrderDetailController_M order = new OrderDetailController_M(iwq.Item_ID, supplierList, qtyCanSuppList);
                                OrderDetailList.Add(order);
                                insufficientInfo = ShowNotEnoughInfo(iwq.Qty,iwq.Item_Name, iwq.UOM, supplierList, qtyCanSuppList,insufficientInfo);
                            }
                            /*else
                            {
                                problem happen!
                            }*/
                        }
                    }
                }
                
            }
            return OrderDetailList;
        }

        public string ShowNotEnoughInfo(int needqty,string desc,string UOM,string[] supList,int[] qtyList,string insufficient_info)
        {
            string info = String.Format("Item: {0} cannot get sufficient amount.<br />", desc) +
                String.Format("{0} can supply {1} {2}.<br />", supList[0], qtyList[0], UOM) +
                String.Format("{0} can supply {1} {2}.<br />", supList[1], qtyList[1], UOM) +
                String.Format("{0} can supply {1} {2}.<br />", supList[2], qtyList[2], UOM) +
                String.Format("Total supply amount is {0} and {1} can not be fulfilled.<br /><br />", qtyList.Sum(),needqty-qtyList.Sum());
                
            return insufficient_info+info;
        }

        public void DistributeToSuppliers(List<OrderDetailController_M> odList)
        {
            PurchaseOrderController_M CPOC = new PurchaseOrderController_M();
            foreach(OrderDetailController_M od in odList)
            {
                for(int i =0; i<od.supplierList.Length;i++)
                {
                    Item item = CPOC.GetItemByItem_ID(od.Item_ID);
                    switch(od.supplierList[i])
                    {
                        case "ALPA":
                            ItemWithQtyModel_M iwq0 = new ItemWithQtyModel_M();
                            iwq0.Item_ID=item.Item_ID;
                            iwq0.Qty=od.qtyCanSuppList[i];
                            iwq0.Category_ID = item.ItemCategory.Category_ID;
                            iwq0.Item_Name = item.Item_Name;
                            iwq0.UOM = item.UOM;
                            ALPAod.Add(iwq0);
                            break;
                        case "BANE":
                            ItemWithQtyModel_M iwq1 = new ItemWithQtyModel_M();
                            iwq1.Item_ID=item.Item_ID;
                            iwq1.Qty=od.qtyCanSuppList[i];
                            iwq1.Category_ID = item.ItemCategory.Category_ID;
                            iwq1.Item_Name = item.Item_Name;
                            iwq1.UOM = item.UOM;
                            
                            BANEod.Add(iwq1);
                            break;
                        case "CHEP":
                            ItemWithQtyModel_M iwq2 = new ItemWithQtyModel_M();
                            iwq2.Item_ID=item.Item_ID;
                            iwq2.Qty=od.qtyCanSuppList[i];
                            iwq2.Category_ID = item.ItemCategory.Category_ID;
                            iwq2.Item_Name = item.Item_Name;
                            iwq2.UOM = item.UOM;
                            CHEPod.Add(iwq2);
                            break;
                        case"OMEG":
                            ItemWithQtyModel_M iwq3 = new ItemWithQtyModel_M();
                            iwq3.Item_ID=item.Item_ID;
                            iwq3.Qty=od.qtyCanSuppList[i];
                            iwq3.Category_ID = item.ItemCategory.Category_ID;
                            iwq3.Item_Name = item.Item_Name;
                            iwq3.UOM = item.UOM;                            
                            OMEGod.Add(iwq3);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public bool AskOneSupplier(List<ItemWithQtyModel_M> IWQ)
        {
            throw new NotImplementedException();
        }
    }
}
