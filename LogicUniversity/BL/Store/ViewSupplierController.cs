using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BL.Store
{
    public class ViewSupplierController
    {
        InventorySysDBEntities context = new InventorySysDBEntities();
        public List<Supplier> GetAllSupplier()
        {
            var q = from x in context.Suppliers where x.Supplier_ID != null select x;
            return q.ToList();
        }
        
        public Supplier GetSupplierBySupplierID(string id)
        {
            var q = from x in context.Suppliers where x.Supplier_ID == id select x;
            return q.First();
        }
    }
}
