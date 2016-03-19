using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Store
{
   public class StaffController
    {
       InventorySysDBEntities en = new InventorySysDBEntities();
       public dynamic getAllStoreStaff()
       {
           var q = (from i in en.Staffs
                    where i.Staff_ID != null
                    where i.Dept_ID == "STAT"
                    select new
                    {
                        Staff_id = i.Staff_ID,
                        Staff_Name = i.Staff_Name
                        


                    });

           return q.ToList();
       }

       public Staff GetStaffByID(string id)
       {
           var q = from x in en.Staffs where x.Staff_ID == id select x;
           return q.First();
       }

       public void updateStaff(Staff sid)
       {
           Staff s = new Staff();
           Staff SS = en.Staffs.FirstOrDefault(m => m.Staff_ID == sid.Staff_ID);
           s.Staff_ID = sid.Staff_ID;
           //s.Staff_Name = sid.Staff_Name;
           s.PhoneNumber = sid.PhoneNumber;
           //s.Email = sid.Email;
           s.Password = sid.Password;
           
           en.SaveChanges();
       }



    }
}
