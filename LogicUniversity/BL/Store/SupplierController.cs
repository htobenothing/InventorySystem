using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace BL.Store
{
   public class Suppliercontroller
    {
       InventorySysDBEntities en = new InventorySysDBEntities();
    
    //   string cs = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        public Supplier GetSupByid(string id)
        {           
            Supplier s = en.Suppliers.Single(u => u.Supplier_ID == id);
            return s;
        }

        public string GetSupByCode(string Code)
        {
            var q = from x in en.Suppliers where x.Supplier_ID == Code select x.Supplier_ID;
            return q.First();
        }

        //public string GetSupByEmail(string Code)
        //{
        //    var q = from x in en.Suppliers where x.Supplier_ID == Code select x.Email;
        //    return q.First();
        //}

        //public string GetSupByGST(string Code)
        //{
        //    var q = from x in en.Suppliers where x.Supplier_ID == Code select x.GST_No;
        //    return q.First();
        //}

        //public string GetSupByName(string Code)
        //{
        //    var q = from x in en.Suppliers where x.Supplier_ID == Code select x.Supplier_Name;
        //    return q.First();
        //}
        public List<Supplier> GetAllSupplier()
        {
            var q = from x in en.Suppliers where x.Supplier_ID != null select x;


            return q.ToList<Supplier>();
        }

       
       
    }
}
