using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BL.Store
{
    public class ViewItemController
    {
        InventorySysDBEntities context = new InventorySysDBEntities();
        public List<string> GetAllItemCode()
        {
            var q = context.Items.Select(x => x.Item_ID).Distinct();
            return q.ToList<string>();
        }

        public List<string> GetAllItemCategory()
        {
            var q = context.Items.Select(x => x.ItemCategory.Category_ID).Distinct();
            return q.ToList<string>();
        }

        public List<string> GetAllItemDesc()
        {
            var q = context.Items.Select(x => x.Item_Name).Distinct();
            return q.ToList<string>();
        }

        public List<string> GetItemIDByCategory(string category)
        {
            var q = from x in context.Items where x.ItemCategory.Category_ID == category select x.Item_ID;
            return q.ToList<string>();
        }

        public List<string> GetDescByCategory(string category)
        {
            var q = from x in context.Items where x.ItemCategory.Category_ID == category select x.Item_Name;
            return q.ToList<string>();
        }

        public string GetCategoryByItemID(string id)
        {
            var q = from x in context.Items where x.Item_ID == id select x.Category_ID;
            return q.First<string>();
        }

        public string GetCategoryByDesc(string desc)
        {
            var q = from x in context.Items where x.Item_Name == desc select x.Category_ID;
            return q.First<string>();
        }

        public Item GetItemByItemID(string id)
        {
            var q = from x in context.Items where x.Item_ID == id select x;
            return q.First();
        }

        public Item GetItemByDesc(string desc)
        {
            var q = from x in context.Items where x.Item_Name == desc select x;
            return q.First();
        }

        public List<Item> GetItemByCategory(string category)
        {
            var q = from x in context.Items where x.ItemCategory.Category_ID == category select x;
            return q.ToList();
        }

        public List<ItemTransaction> GetItemTransactionByItemID(string id)
        {
            var q = from x in context.ItemTransactions where x.Item_ID == id select x;
            return q.ToList();
        }

        public List<Item> GetAllItem()
        {
            var q = from x in context.Items where x.Item_ID != null select x;
            return q.ToList();
        }
    }
}
