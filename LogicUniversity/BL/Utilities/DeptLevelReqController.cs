using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
   public class DeptLevelReqController
    {
        string deptName;

        public string DeptName
        {
            get { return deptName; }
            set { deptName = value; }
        }
        Dictionary<string, List<ItemInfobyReqiIdModel>> deptReqi;

       

        public Dictionary<string, List<ItemInfobyReqiIdModel>> DeptReqi
        {
            get { return deptReqi; }
            set { deptReqi = value; }
        }
    }
}
