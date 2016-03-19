using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Store
{
    public partial class ItemWithQtyModel
    {
        public string Item_ID { get; set; }
        public string Category_ID { get; set; }
        public string Item_Name { get; set; }
        public int Reorder_Level { get; set; }
        public int Reorder_Qty { get; set; }
        public string UOM { get; set; }
        public string Bin_ID { get; set; }
        public string FirstSupplier_ID { get; set; }
        public string SecondSupplier_ID { get; set; }
        public string ThirdSupplier_ID { get; set; }
        public int Inventory { get; set; }
        public string Status { get; set; }
        public int Qty { get; set; }
        
        public ItemWithQtyModel() { }
        
              
    }
}
