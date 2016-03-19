using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Report
{
    public class DepRequisitionDetailModel
    {
        public string RequisitionID {get;set;}
        public DateTime CreateDate {get;set;}
        public string CreateStaff {get;set;}
        public string DepartmentName {get;set;}
        public string Status { get; set; }
    }
}
