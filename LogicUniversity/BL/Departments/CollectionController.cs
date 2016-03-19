using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Departments
{
    public class CollectionController
    {
        InventorySysDBEntities context = new InventorySysDBEntities();

        //GET collection point des  by department id
        public string  getCPDesByDptID(string dptID)

        { 
            var f= from x in context.Departments where x.Dept_ID==dptID select x ;
            Department dpt=f.First();
            var t = from x in context.CollectionPoints where x.Collection_ID == dpt.Collection_ID select x.Collection_Desc;
            return t.First();
        }

        public Staff GetRepByDeptID(string deptID)
        {
            var q = from x in context.Staffs where x.Dept_ID == deptID && x.Role_ID == "RepStaff" select x;
            return q.First();
        }

        public Staff GetStaffNameByStaffID(string staffId, string deptID)
        {
            var q = from x in context.Staffs where x.Staff_ID == staffId && x.Dept_ID == deptID select x;
            if (q.ToList().Count == 0)
            {
                return null; // No such staff in this dept.
            }
            else
                return q.First();
        }

        public List<string> GetAllCpDesc()
        {
            var q = from x in context.CollectionPoints where x.Collection_ID != null select x.Collection_Desc;
            return q.ToList();
        }

        public bool ChangeRepToStaff(string deptID)
        {
            Staff s = GetRepByDeptID(deptID);
            if (s != null)
            {
                s.Role_ID = "Staff";
                context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool ChangeStaffToRep(string staffID)
        {
            var q = from x in context.Staffs where x.Staff_ID == staffID select x;
            if (q.ToList().Count > 0)
            {
                Staff s = q.First();
                s.Role_ID = "RepStaff";
                context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public void UpdateCollectionPoint(string DeptID, string CpDesc)
        {
            var q = from x in context.Departments where x.Dept_ID == DeptID select x;
            Department d = q.First();
            var p = from x in context.CollectionPoints where x.Collection_Desc == CpDesc select x;
            CollectionPoint c = p.First();
            d.Collection_ID = c.Collection_ID;
            context.SaveChanges();
        }
    }
}
