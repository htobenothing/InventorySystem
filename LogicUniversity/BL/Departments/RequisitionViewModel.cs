using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Departments
{
    public class RequisitionViewModel
    {
        public string categoryID { get; set; }
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public string UOM { get; set; }
        public int RequiredQty { get; set; }
    }
}
