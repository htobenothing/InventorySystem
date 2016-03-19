using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Utilities
{
    class RequisitionClass
    {
        string reuisitionId;

        public string ReuisitionId
        {
            get { return reuisitionId; }
            set { reuisitionId = value; }
        }
        string departmentName;

        public string DepartmentName
        {
            get { return departmentName; }
            set { departmentName = value; }
        }
        string requisitionType;

        public string RequisitionType
        {
            get { return requisitionType; }
            set { requisitionType = value; }
        }
    }
}
