using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Utilities
{
    public class RequisitionItemList_M
    {
        public RequisitionItemList_M()
        {
          
        }
    
        public string RequisitionID { get; set; }
        public string SubmissionStaffName { get; set; }
        public System.DateTime SubmitDate { get; set; }
        public int TotalQty { get; set; }
      
    }
}
