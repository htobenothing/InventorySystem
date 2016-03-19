using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Store.TableView
{
    public class ReorderReminderTableModel
    {
        public string Item_ID { get; set; }
        public string Category_ID { get; set; }
        public string Item_Name { get; set; }
        public int Reorder_Level { get; set; }
        public int Reorder_Qty { get; set; }
        public int Inventory { get; set; }
        public int InventoryAddOrder { get; set; }
    }
}
