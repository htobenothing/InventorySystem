using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Utilities
{
    public class DeptLevelReqController_M
    {
        string deptName;

        public string DeptName
        {
            get { return deptName; }
            set { deptName = value; }
        }
        Dictionary<string, List<ItemInfobyReqiIdModel_M>> deptReqi;//reqID is key 

        internal Dictionary<string, List<ItemInfobyReqiIdModel_M>> DeptReqi
        {
            get { return deptReqi; }
            set { deptReqi = value; }
        }
    }
}
