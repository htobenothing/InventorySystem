using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class DelegateAuthorityController_M
    {
        static InventorySysDBEntities ctx = new InventorySysDBEntities();

        //get staff by staff id
        public static Staff getStaffBySfId(string sfid)
        {
            var t = from x in ctx.Staffs where x.Staff_ID == sfid select x;
            Staff sf = t.First();
            return sf;


        }
     
        //delegate
        //create new delegate
        public static void createDelegateHistory(string sfid, string managerId, DateTime startDate, DateTime endDate)
        {
            var t = from x in ctx.Staffs where x.Staff_ID == sfid select x;
            Staff sf = t.First();
            sf.Role_ID = "AuthStaff";

            DelegateHistory dh = new DelegateHistory();
            dh.Manager_ID = managerId;
            dh.Staff_ID = sf.Staff_ID;
            dh.Start_Date = Convert.ToDateTime(startDate);
            dh.End_Date = Convert.ToDateTime(endDate);
            dh.Status = "Delegated";
            ctx.DelegateHistories.Add(dh);
            ctx.SaveChanges();

        }



        //undelegate
        //update  delegate
        public static void updateDelegateHistory(string sfid)
        {
            var t = from x in ctx.DelegateHistories
                    where x.Staff_ID == sfid && x.Status == "Delegated"
                    select x;


            DelegateHistory dh = t.First();//error

            dh.Staff_ID = sfid;

            Staff sf = getStaffBySfId(sfid);
            sf.Role_ID = "Staff";

            dh.Status = "Undelegated";
            ctx.SaveChanges();

        }

        //staff List get by dept
        public static List<Staff> getStaffListByDepIDandroleID(string DepID, string roleID)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from x in cntx.Staffs
                        where x.Dept_ID == DepID
                        where x.Role_ID == roleID
                        select x;
                Console.Write(q.ToString());
                return q.ToList();
            }

        }
    }
}
