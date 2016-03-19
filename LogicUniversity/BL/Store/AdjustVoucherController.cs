using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace BL.Store
{
    public class AdjustVoucherController
    {
        InventorySysDBEntities context = new InventorySysDBEntities();
        List<Item> ItemList = new List<Item>();

        string cs = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        public List<Item> GetAllItem()
        {
            var q = from x in context.Items where x.Item_ID != null select x;
            ItemList = q.ToList<Item>();
            return ItemList;
        }

        public List<Item> GetItemByCategory(string cg)
        {
            var q = from x in context.Items where x.ItemCategory.Category_ID == cg select x;
            return q.ToList<Item>();
        }

        public Item GetItemByItem_ID(string id)
        {
            var q = from x in context.Items where x.Item_ID == id select x;
            return q.First();
        }

        public Item GetItemByName(string name)
        {
            var q = from x in context.Items where x.Item_Name == name select x;
            return q.First();
        }

        public List<string> GetAllItemCode()
        {
            var q = context.Items.Select(x => x.Item_ID).Distinct();
            return q.ToList<string>();
        }
        public List<string> GetAllItemCodeByctgry(string ctgry)
        {
            var q = from x in context.Items where x.ItemCategory.Category_Desc == ctgry select x.Item_ID;
            return q.ToList<string>();
        }
        public List<string> GetAllItemNameByctgry(string ctgry)
        {
            var q = from x in context.Items where x.ItemCategory.Category_Desc == ctgry select x.Item_Name;
            return q.ToList<string>();
        }

        public List<string> GetAllItemCategory()
        {
            var q = context.Items.Select(x => x.ItemCategory.Category_ID).Distinct();
            return q.ToList<string>();
        }

        public double GetItemPrice(string itemCode)
        {
            var q = (from x in context.Items where x.Item_ID == itemCode select x.FirstSupplier_ID).Single();
            string s = q.ToString();
            var p = (from x in context.SupplierInventories where x.Supplier_ID == s && x.Item_ID == itemCode select x.Price).Single();
            string price = p.ToString();
            return Convert.ToDouble(p);
        }

        public List<string> GetAllItemDesc()
        {
            var q = context.Items.Select(x => x.Item_Name).Distinct();
            return q.ToList<string>();
        }

       
        public void setAdjustment(Adjustment adj)
        {
            
            context.Adjustments.Add(adj);
            context.SaveChanges();
        }

        public void setAdjustmentDetail(AdjustmentDetail adjDetail)
        {
            context.AdjustmentDetails.Add(adjDetail);
            context.SaveChanges();
        }
    }
}
