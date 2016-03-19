using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BL.User
{
 
    public class UserController
    {
       
        public UserController()
        {
           
           // string cs = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }
       

      
       public static Staff SelectByLogIn(string email, string password)
        {
            InventorySysDBEntities en = new InventorySysDBEntities();
           
          

            var query = from c in en.Staffs
                        where c.Email == email && c.Password == password
                        select c;

           if( query == null )
           {
                return  null;
           }
           else
            return query.ToList().FirstOrDefault();
        }


     
    }
}
