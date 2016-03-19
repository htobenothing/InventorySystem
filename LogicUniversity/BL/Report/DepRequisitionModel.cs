using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Report
{
    public class MonthDepReportReq
    {
        public string Status { get; set; }
       public string DepID { get; set; }
       public string DepName { get; set; }
       public int Qty { get; set; }
    }
}
