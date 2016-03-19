using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Utilities
{
    public class ReceivedItemModel_M
    {
        public string itemCode { get; set; }
        public string itemName { get; set; }
        public int needQty { get; set; }
        public int recQty { get; set; }
        public int restQty { get; set; }
        public string uom { get; set; }
    }
}
