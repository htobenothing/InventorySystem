using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Utilities
{
    public class RequisitionViewModel_M
    {
        public string categoryID { get; set; }
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public string UOM { get; set; }
        public int RequiredQty { get; set; }
    }
}
