using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace BL.Store
{
    public class DepartmentController
    {
        InventorySysDBEntities en = new InventorySysDBEntities();

        // string cs = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        public Department GetDeptByid(string id)
        {
            Department s = en.Departments.Single(u => u.Dept_ID == id);
            return s;
        }

        public List<string> GetAllDepName()
        {
            List<string> DepName = new List<string>();
            var q = from x in en.Departments where x.Dept_Name != "Stationary Store Dept" select x;
            foreach (Department d in q.ToList())
            {
                DepName.Add(d.Dept_Name);
            }
            return DepName;
        }

        public List<Department> GetCPByid(string id)
        {
           var s = from u in en.Departments where u.Collection_ID == id select u;
            return s.ToList();
        }


      
        public List<Department> GetDeptByName(string name)
        {
            var q = from x in en.Departments where x.Dept_Name == name select x;
            return q.ToList();
        }

        public List<Department> GetAllDept()
        {
            var q = from x in en.Departments where x.Dept_Name != "Stationary Store Dept"&&x.Dept_ID!="SUPP" select x;
            return q.ToList<Department>();
        }

        public List<CollectionPoint> GetAllCollectionPt()
        {
            var q = from x in en.CollectionPoints where x.Collection_ID != null select x;
            return q.ToList<CollectionPoint>();
        }

        public DepartmentStaffModel GetDeptByid_RepStaff(string id)
        {

            var query = (from x in en.Departments

                         join S in en.Staffs
                         on x.Dept_ID equals S.Dept_ID
                         where S.Dept_ID == id
                         join C in en.CollectionPoints
                         on x.Collection_ID equals C.Collection_ID
                        
                         select new DepartmentStaffModel
                         {
                             Representative_Staff = S.Staff_Name,
                             Dept_ID = S.Dept_ID,
                             Dept_Name = x.Dept_Name,
                             Contact_Name = x.Contact_Name,
                             Telephone_No = x.Telephone_No,
                             Fax_No = x.Fax_No,
                             Heads_Name = x.Heads_Name,
                             Collection_Desc = C.Collection_Desc,
                             Email = x.Email,


                         });

            return query.ToList().FirstOrDefault();
        }

        public DepartmentStaffModel GetDeptBySTAT_RepStaff(string id)
        {

            var query = (from x in en.Departments

                         join S in en.Staffs
                         on x.Dept_ID equals S.Dept_ID
                         where S.Role_ID != "RepStaff" && S.Dept_ID == id
                         join C in en.CollectionPoints
                         on x.Collection_ID equals C.Collection_ID

                         select new DepartmentStaffModel
                         {
                             Representative_Staff = null,
                             Dept_ID = S.Dept_ID,
                             Dept_Name = x.Dept_Name,
                             Contact_Name = x.Contact_Name,
                             Telephone_No = x.Telephone_No,
                             Fax_No = x.Fax_No,
                             Heads_Name = x.Heads_Name,
                             Collection_Desc = C.Collection_Desc,
                             Email = x.Email,


                         });

            return query.ToList().FirstOrDefault();
        }

    }
}
