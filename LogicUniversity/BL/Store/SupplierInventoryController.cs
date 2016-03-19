using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Store.TableView;

namespace BL.Store
{
    public class SupplierInventoryController
    {
        InventorySysDBEntities context = new InventorySysDBEntities();
        public List<SupplierInventoryTableModel> GetSupplierInventory(string supID)
        {
            List<SupplierInventoryTableModel> table = new List<SupplierInventoryTableModel>();
            var q = from x in context.SupplierInventories where x.Supplier_ID == supID select x;
            foreach(SupplierInventory si in q.ToList())
            {
                SupplierInventoryTableModel sit = new SupplierInventoryTableModel();
                sit.Item_Name = si.Item.Item_Name;
                sit.Inventory_Qty = si.Inventory_Qty;
                table.Add(sit);
            }
            return table;
        }

        public bool UpdateSupplierInventroy(string supId,List<string> Qty)
        {
            try
            {
                var q = from x in context.SupplierInventories where x.Supplier_ID == supId select x;
                List<SupplierInventory> SIList = q.ToList();
                for (int i = 0; i < SIList.Count;i++ )
                {
                    SIList[i].Inventory_Qty = Convert.ToInt32(Qty[i]);
                    context.SaveChanges();
                }
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
