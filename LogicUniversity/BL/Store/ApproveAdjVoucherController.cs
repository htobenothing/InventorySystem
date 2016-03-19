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
    public class ApproveAdjVoucherController
    {
        InventorySysDBEntities context = new InventorySysDBEntities();

        string cs = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

       // public List<Adjustment> GetAllVoucherByStaff_Id(string staff_Id)
        public List<Adjustment> GetAllVoucherByStaff_Id()
        {
           // var q = from x in context.Adjustments where x.IssuedByStaff_ID == staff_Id && x.Status=="Pending" select x ;
            var q = from x in context.Adjustments where x.Status == "Pending" select x; 
            return q.ToList<Adjustment>();
        }

        public string getNameByStaffId(string staffId) {
            var q = from x in context.Staffs where x.Staff_ID == staffId select x.Staff_Name;
            return q.First();
        }

        public List<string> GetAllItemCodeByVoucherId(string voucherId) {
            var q = from x in context.AdjustmentDetails where x.Voucher_ID == voucherId  select x.Item_ID;
            return q.ToList<string>();
        }

        public string GetItemcaegoryByCode(string itemCode) {

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

        public int GetAdjQty(string voucherId,string itemCode) {
            var q = from x in context.AdjustmentDetails where x.Voucher_ID == voucherId && x.Item_ID == itemCode select x.Adj_Qty;
            return Convert.ToInt32(q.First());
        }

        public string GetRemarkByVoucherId(string voucherId) {
            var q = from x in context.Adjustments where x.Voucher_ID == voucherId select x.Remark;
            return q.First();
        }
        public static List<ItemWithPriceModel> getItemPriceByid(List<string> itemlist)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                List<ItemWithPriceModel> ipList = new List<ItemWithPriceModel>();
                foreach (string id in itemlist)
                {
                    var q = from x in cntx.SupplierInventories
                        group x by x.Item_ID into g
                        where g.Key == id
                        select new ItemWithPriceModel
                        {
                            itemID = g.Key,
                            Price = g.Average(i => i.Price)
                        };
                    ipList.Add(q.First<ItemWithPriceModel>());
                }
                return ipList.ToList();
            }
            
        }
        public void AdjIntoItemTxn(string voucherId, string itemCode, DateTime updateDate, string reason, int qty, string superviserID)
        {
            DateTime updatedate = updateDate;
            //string format = "MM/dd/yyyy";
            //string date = updatedate.ToString(format);
            //var q = from x in context.Items where x.Item_ID == itemCode select x.Inventory;
            //int inventory = Convert.ToInt32(q.First());      
            try{
                var q = from x in context.Adjustments where x.Voucher_ID == voucherId select x;
                Adjustment adj = q.First();
                adj.Approved_Date = updatedate;
                adj.ApprovedByStaff_ID  = superviserID;
                adj.Remark = reason;
                adj.Status = "Approved";
                
                var p = from x in context.ItemTransactions where x.Item_ID == itemCode select x;
                List<ItemTransaction> ItemTranHis = p.ToList();
                ItemTransaction it = new ItemTransaction();
                it.Item_ID = itemCode;
                it.Update_Date = updatedate;
                it.Quantity = qty;
                it.Balance = ItemTranHis.Last().Balance + qty;
                it.Reason= reason;
                context.ItemTransactions.Add(it);
                context.SaveChanges();
            }
               
            //using (SqlConnection con = new SqlConnection(cs))
            //{
            //    con.Open();
            //    SqlTransaction transaction;
            //    transaction = con.BeginTransaction("SampleTransaction");
            //    SqlCommand command = con.CreateCommand();
            //    command.Connection = con;
            //    command.Transaction = transaction;
            //    try
            //    {
            //        command.CommandText = "insert into ItemTransaction values('" + itemCode + "','" + date + "','" + reason + "'," + qty + "," + (inventory + (qty)) + ");";
            //        int x = command.ExecuteNonQuery();
            //        command.CommandText = "update Item set Inventory=Inventory+" + (qty) + " where Item_ID='" + itemCode + "';";                    
            //        int y = command.ExecuteNonQuery();
            //        command.CommandText = "update Adjustment set Status='Approved', Approved_Date='" + date + "',ApprovedByStaff_ID='" + superviserID + "' where Voucher_ID='" + voucherId + "';";                   
            //        int z = command.ExecuteNonQuery();
            //        transaction.Commit();
            //        Console.WriteLine("three records are written to database.");
            //    }
                catch (Exception ex)
                {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);
                    // Attempt to roll back the transaction.
                    //try
                    //{
                    //    transaction.Rollback();
                    //}
                    //catch (Exception ex2)
                    //{
                    //    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                    //    Console.WriteLine("  Message: {0}", ex2.Message);
                    //}
                }
              
            }

        }

    }

