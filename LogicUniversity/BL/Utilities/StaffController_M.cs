using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Utilities
{
    public class StaffController_M
    {
        public static List<Staff> getStaffListById(string id)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from x in cntx.Staffs
                        where x.Staff_ID == id
                        select x;
                return q.ToList();
            }
        }

        // approve staff may be many, AuthStaff and Manager
        public static List<Staff> getApprovStaffListByDepId(string DepId)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from x in cntx.Staffs
                        where x.Dept_ID == DepId && (x.Role_ID == "Manager" || x.Role_ID == "AuthStaff")
                        select x;
                return q.ToList();
            }
        }
    }
}
