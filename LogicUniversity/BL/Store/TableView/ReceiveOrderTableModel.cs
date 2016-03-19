using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Store.TableView
{
   public class ReceiveOrderTableModel
    {
        public string Item_ID { get; set; }
        public string Category_ID { get; set; }
        public string Item_Name { get; set; }
        public int Needed_Qty { get; set; }
        public Nullable<int> Received_Qty { get; set; }
        public string UOM { get; set; }
    }
}
