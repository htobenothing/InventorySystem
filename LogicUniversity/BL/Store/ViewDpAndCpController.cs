using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Store.TableView;

namespace BL.Store
{
    public class ViewDpAndCpController
    {
        InventorySysDBEntities context = new InventorySysDBEntities();
        public List<string> GetAllDepName()
        {
            List<string> DepName = new List<string>();
            var q = from x in context.Departments where x.Dept_Name != "Stationary Store Dept" select x;
            foreach(Department d in q.ToList())
            {
                DepName.Add(d.Dept_Name);
            }
            return DepName;
        }

        public List<string> GetAllCpID()
        {
            List<string> CpID = new List<string>();
            var q = from x in context.CollectionPoints where x.Collection_ID!= null  select x;
            foreach (CollectionPoint c in q.ToList())
            {
                CpID.Add(c.Collection_ID);
            }
            return CpID;
        }


        public DepartmentStaffModel GetDeptByid_RepStaff(string id)
        {

            var query = (from x in context.Departments

                         join S in context.Staffs
                         on x.Dept_ID equals S.Dept_ID
                         where  S.Dept_ID == id
                         join C in context.CollectionPoints
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



        public Department GetDeptByDeptID(string deptID)
        {
            var q = from x in context.Departments where x.Dept_ID == deptID select x;
            return q.First();
        }

        public List<Department> GetAllDept()
        {
            var q = from x in context.Departments where x.Dept_ID != null select x;
            return q.ToList();
        }

        public string GetCollectionDescByID(String cpID)
        {
            var q = from x in context.CollectionPoints where x.Collection_ID == cpID select x.Collection_Desc;
            return q.First();
        }
        public List<CpAndDeptTableModel> CpAndDeptGridViewAll()
        {
            List<CpAndDeptTableModel> cadtList = new List<CpAndDeptTableModel>();
            var q = from x in context.Departments where x.Dept_ID != "STAT"&&x.Dept_ID!="SUPP" select x;
            foreach(Department d in q.ToList())
            {
                CpAndDeptTableModel cadt = new CpAndDeptTableModel();
                cadt.Dept_ID = d.Dept_ID;
                cadt.Collection_ID = d.Collection_ID;
                cadt.Collection_Desc = GetCollectionDescByID(d.Collection_ID);
                cadtList.Add(cadt);
            }
            return cadtList;
        }

        public List<CpAndDeptTableModel> CpAndDeptGridViewForDp(string deptName)
        {
            List<CpAndDeptTableModel> cadtList = new List<CpAndDeptTableModel>();
            var q = from x in context.Departments where x.Dept_Name == deptName select x;
            Department d = q.First();    
            CpAndDeptTableModel cadt = new CpAndDeptTableModel();
            cadt.Dept_ID = d.Dept_ID;
            cadt.Collection_ID = d.Collection_ID;
            cadt.Collection_Desc = d.CollectionPoint.Collection_Desc;
            cadtList.Add(cadt);
            
            return cadtList;
        }

        public List<CpAndDeptTableModel> CpAndDeptGridViewForCp(string cpID)
        {
            List<CpAndDeptTableModel> cadtList = new List<CpAndDeptTableModel>();
            var q = from x in context.Departments where x.Collection_ID == cpID select x;
            foreach (Department d in q.ToList())
            {
                CpAndDeptTableModel cadt = new CpAndDeptTableModel();
                cadt.Dept_ID = d.Dept_ID;
                cadt.Collection_ID = d.Collection_ID;
                cadt.Collection_Desc = d.CollectionPoint.Collection_Desc;
                cadtList.Add(cadt);
            }
            return cadtList;
        }
    }
}
