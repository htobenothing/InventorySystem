using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BL
{
    public class CollectionController_M
    {
       
        //collection List
        public static List<CollectionPoint> getAllCollection()
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from x in cntx.CollectionPoints
                        select x;
                return q.ToList();
            }
        }

        //collection get by dept
        public static CollectionPoint getCollectionInfoByDepID(string DepId)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from d in cntx.Departments
                        where d.Dept_ID == DepId
                        select d.CollectionPoint;

                return q.ToList().First();
            }
        }

        //staff List get by dept
        public static List<Staff> getStaffListByDepID(string DepID)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from x in cntx.Staffs
                        where x.Dept_ID == DepID && (x.Role_ID=="Staff" || x.Role_ID=="RepStaff")
                        select x;
                Console.Write(q.ToString());
                return q.ToList();
            }

        }


        ////staff List get by dept
        //public static List<Staff> getStaffListByDepIDandroleID(string DepID, string roleID)
        //{
        //    using (InventorySysDBEntities cntx = new InventorySysDBEntities())
        //    {
        //        var q = from x in cntx.Staffs
        //                where x.Dept_ID == DepID
        //                where x.Role_ID == roleID
        //                select x;
        //        Console.Write(q.ToString());
        //        return q.ToList();
        //    }

        //}

       //update collection point according to staff and collection point

        public static Staff GetRepByDeptID(string deptID)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from x in cntx.Staffs where x.Dept_ID == deptID && x.Role_ID == "RepStaff" select x;
                return q.First();
            }
        }

        //public static bool ChangeRepToStaff(string deptID)
        //{
        //    using (InventorySysDBEntities cntx = new InventorySysDBEntities())
        //    {

        //        Staff stf = GetRepByDeptID(deptID);
        //        Staff repStaff = cntx.Staffs.Find(stf.Staff_ID);
        //        if (repStaff != null)
        //        {
        //            repStaff.Role_ID = "Staff";
        //            cntx.SaveChanges();
        //            return true;
        //        }
        //        else
        //            return false;
        //    }
        //}

        //public static bool ChangeStaffToRep(string staffID)
        //{
        //    using (InventorySysDBEntities cntx = new InventorySysDBEntities())
        //    {
        //        var q = from x in cntx.Staffs where x.Staff_ID == staffID select x;
        //        if (q.ToList().Count > 0)
        //        {
        //            q.First().Role_ID = "RepStaff";
        //            cntx.SaveChanges();
        //            return true;
        //        }
        //        else
        //            return false;
        //    }
        //}

        public static void UpdateCollectionPoint(string DeptID, string ColID)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                
                var q = from x in cntx.Departments where x.Dept_ID == DeptID select x;
                Department d = q.First();

                d.Collection_ID = ColID;
                cntx.SaveChanges();
            }
        }


        public static bool UpdateCollectionPoint(string DeptID, string staffID,string ColID)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {

                using (var dbContextTransaction = cntx.Database.BeginTransaction())
                {
                    try
                    {
                        //make here the changes

                            Staff stf = GetRepByDeptID(DeptID);
                            Staff repStaff = cntx.Staffs.Find(stf.Staff_ID);
                            repStaff.Role_ID = "Staff";
                               
                           

                            var changeStaffTRep = from x in cntx.Staffs where x.Staff_ID == staffID select x;
                            changeStaffTRep.First().Role_ID = "RepStaff";
                            

                            var updateCollectionPoint = from x in cntx.Departments where x.Dept_ID == DeptID select x;
                            Department d = updateCollectionPoint.First();
                            d.Collection_ID = ColID;


                            cntx.SaveChanges();
                            dbContextTransaction.Commit();
                        
                        return true;
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        return false;
                    }
                }

            }

        }
  
    }
}
