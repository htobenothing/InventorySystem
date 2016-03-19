using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BL.Store.TableView;

namespace BL.Store
{
    public class ReorderController
    {
        InventorySysDBEntities context = new InventorySysDBEntities();

        public List<ReorderReminderTableModel> GetReorderItemList()
        {
            var q = from x in context.Items
                    where x.Item_ID != null && x.Inventory <= x.Reorder_Level
                    select x;
            List<Item> ItemList = q.ToList();
            List<int> addQty = new List<int>();
            foreach(Item i in ItemList)
            {
                var p = from x in context.OrderListDetails
                        where x.Item_ID == i.Item_ID && x.OrderList.Status == "Submitted"
                        select x.Needed_Qty;
                if(p.ToList().Count == 0)
                {
                    p.ToList().Add(0);
                    addQty.Add(0);
                }
                else
                    addQty.Add(p.Sum());
            }

            List<ReorderReminderTableModel> RRT = new List<ReorderReminderTableModel>();
            for(int i =0;i<ItemList.Count;i++)
            {
                ReorderReminderTableModel r = new ReorderReminderTableModel();
                r.Category_ID = ItemList[i].Category_ID;
                r.Inventory = ItemList[i].Inventory;
                r.InventoryAddOrder = ItemList[i].Inventory + addQty[i];
                r.Item_ID = ItemList[i].Item_ID;
                r.Item_Name = ItemList[i].Item_Name;
                r.Reorder_Level = ItemList[i].Reorder_Level;
                r.Reorder_Qty = ItemList[i].Reorder_Qty;
                RRT.Add(r);
            }
            return RRT;
        }
    }
}
