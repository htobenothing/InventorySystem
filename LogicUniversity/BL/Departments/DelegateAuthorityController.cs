using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BL.Departments.Model;

namespace BL.Departments
{
    public class DelegateAuthorityController
    {
        InventorySysDBEntities ctx = new InventorySysDBEntities();
        public List<Staff> getAllStaffExpManager(string dpt_ID)
        {
            var t = (from x in ctx.Staffs where x.Dept_ID == dpt_ID && (x.Role_ID == "Staff" || x.Role_ID == "AuthStaff") select x).ToList();
            return t;

        }

        //get department id by user id 
        public Staff getStaffBySfId(string sfid)
        {
            var t = from x in ctx.Staffs where x.Staff_ID == sfid select x;
            Staff sf = t.First();
            return sf;

        }

        //get role id by staff id
        public string getSfRoleIdBySfId(string sfid)
        {
            var t = from x in ctx.Staffs where x.Staff_ID == sfid select x.Role_ID;
            string staffRole = t.First();
            return staffRole;

        }

        //get staff name by staff id
        public string getSfNameBySfId(string sfid)
        {
            var t = from x in ctx.Staffs where x.Staff_ID == sfid select x.Staff_Name;
            string staffName = t.First();
            return staffName;
        }

        //get delegate authority start date

        public string getStartDate(string sfid)
        {
            var t = from x in ctx.DelegateHistories where x.Staff_ID == sfid select x;
            DelegateHistory delegateHistory = t.First();
            DateTime startDay = delegateHistory.Start_Date;

            return startDay.ToString();

        }

        //get delegate authority end date

        public string getEndDate(string sfid)
        {
            var t = from x in ctx.DelegateHistories where x.Staff_ID == sfid select x;
            DelegateHistory delegateHistory = t.First();
            DateTime endDay = delegateHistory.End_Date;

            return endDay.ToString();

        }

        //cancle delegate
        public void cancleDelegateAuthority(string sfid)
        {
            Staff sf = getStaffBySfId(sfid);

            sf.Role_ID = "Staff";

            ctx.SaveChanges();


        }



        //get delegateHistory by staff id 
        public DelegateHistory getDelegateHistory(string sfid)
        {
            var t = from x in ctx.DelegateHistories where x.Staff_ID == sfid select x;
            DelegateHistory dh = t.First();
            return dh;


        }

        //create new delegate
        public void createDelegateHistory(string sfid, string managerId, DateTime startDate, DateTime endDate)
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

        //update  delegate
        public void updateDelegateHistory(string sfid)
        {
            var t = from x in ctx.DelegateHistories
                    where x.Staff_ID == sfid && x.Status == "Delegated"
                    select x;


            DelegateHistory dh = t.First();

            dh.Staff_ID = sfid;

            Staff sf = getStaffBySfId(sfid);
            sf.Role_ID = "Staff";

            dh.Status = "Undelegated";
            ctx.SaveChanges();

        }



    }
}
