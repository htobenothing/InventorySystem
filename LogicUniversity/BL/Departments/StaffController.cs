using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Departments
{
    public class StaffController
    {
        public static List<Staff> getStaffListById(string id){
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from x in cntx.Staffs
                        where x.Staff_ID == id
                        select x;
                return q.ToList();
            }
        }
        public static List<Staff> getAllClerk()
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from x in cntx.Staffs
                        where x.Role_ID == "StoreClerk"
                        select x;
                return q.ToList();
            }
        }
        public static Staff getRepByDepID(string depid)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from x in cntx.Staffs
                        where x.Dept_ID == depid && x.Role_ID == "RepStaff"
                        select x;
                return q.First();
            }
        }
        public static Staff getStaffById(string id)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from x in cntx.Staffs
                        where x.Staff_ID == id
                        select x;
                return q.First();
            }
        }

        public static List<Staff> getStaffListByDepID(string DepID)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from x in cntx.Staffs
                        where x.Dept_ID == DepID
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

        public static List<Staff> getSManagerByid(Staff stf)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
               
                var q = from x in cntx.Staffs
                        where x.Dept_ID == stf.Dept_ID && x.Role_ID == "SManager"
                        select x;
                return q.ToList();
            }
        }
    }
}
