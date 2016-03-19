using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Store.TableView
{
    public partial class PurchaseOrderTableModel
    {
        
        public string Category { get; set; }
        public string Item_ID { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string UOM { get; set; }

        public PurchaseOrderTableModel() { }
    }
}
