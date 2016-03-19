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
    public class ViewAdjustmentVoucherController
    {
        InventorySysDBEntities context = new InventorySysDBEntities();
        List<Item> ItemList = new List<Item>();
        public List<Adjustment> GetVouchersByDateAndStatus(DateTime fromdate, DateTime todate, string status)
        {
            var q = from x in context.Adjustments where (x.Issue_Date >= fromdate && x.Issue_Date <= todate) && x.Status == status select x;
            return q.ToList<Adjustment>();
        }

        public List<Adjustment> GetVouchersByDate(DateTime fromdate, DateTime todate)
        {
            var q = from x in context.Adjustments where (x.Issue_Date >= fromdate && x.Issue_Date <= todate) select x;
            return q.ToList<Adjustment>();

        }
        public string getIssueByName(string staff_id)
        {
            var q = from x in context.Staffs where x.Staff_ID == staff_id select x;
            return q.First().Staff_Name;
        }
        public List<string> GetAllItemCodeByVoucherId(string voucherId)
        {
            var q = from x in context.AdjustmentDetails where x.Voucher_ID == voucherId select x.Item_ID;
            return q.ToList<string>();
        }
        public string GetItemcaegoryByCode(string itemCode)
        {
            var q = from x in context.Items where x.Item_ID == itemCode select x.Category_ID;
            return q.First();
        }
        public string GetItemDescByCode(string itemCode)
        {
            var q = from x in context.Items where x.Item_ID == itemCode select x.Item_Name;
            return q.First();
        }
        public double GetItemPrice(string itemCode)
        {
            var q = (from x in context.Items where x.Item_ID == itemCode select x.FirstSupplier_ID).Single();
            string s = q.ToString();
            var p = (from x in context.SupplierInventories where x.Supplier_ID == s && x.Item_ID == itemCode select x.Price).Single();
            string price = p.ToString();
            return Convert.ToDouble(p);
        }
        public int GetAdjQty(string voucherId, string itemCode)
        {
            var q = from x in context.AdjustmentDetails where x.Voucher_ID == voucherId && x.Item_ID == itemCode select x.Adj_Qty;
            return Convert.ToInt32(q.First());
        }
        public string GetRemarkByVoucherId(string voucherId)
        {
            var q = from x in context.Adjustments where x.Voucher_ID == voucherId select x.Remark;
            return q.First();
        }
    }
}
