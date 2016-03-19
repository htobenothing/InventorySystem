using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Report
{
    public class ReportController
    {
        

        public static List<YearReportReuisitionModel> getMonthTrendByReq(DateTime date)
        {

            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from x in cntx.Requisitions
                        where  x.Create_Date.Year == date.Year
                        group x by new {x.Create_Date.Month, x.RequisitonType } into g
                        select new YearReportReuisitionModel
                        {
                           Type = g.Key.RequisitonType,
                           month = g.Key.Month,
                           qty = g.Count()
                        };
                return q.ToList();
            }
        }

        public static List<YearSupplierDurationModel> getMonthTrendSupplyDur(DateTime frmdate )
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                

                var q = from x in cntx.OrderLists
                        where x.Create_Date.Year == frmdate.Year 
                        where x.Finish_Date != null

                        group x by new { x.Create_Date.Month, x.Supplier.Supplier_ID, dur = DbFunctions.DiffDays(x.Create_Date, x.Finish_Date).Value } into g
                        select new YearSupplierDurationModel
                        {
                            Month = g.Key.Month,
                            SupplierName = g.Key.Supplier_ID,
                            Duration = g.Key.dur
                        };
                return q.ToList();

            }
        }


        // get unfullfilled PONo
        public static List<YearSupplierUnfufilledModel> getMonthTrendUnFullfilledPO(DateTime frmdate)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                
                var q = from x in cntx.OrderLists
                        where x.Create_Date.Year ==frmdate.Year
                        join ordi in cntx.OrderListDetails on x.Order_ID equals ordi.Order_ID
                        where ordi.Needed_Qty > ordi.Received_Qty
                        group x by new { x.Supplier_ID, x.Create_Date.Month } into g
                        join s in cntx.Suppliers on g.Key.Supplier_ID equals s.Supplier_ID

                        select new YearSupplierUnfufilledModel
                        {
                            Month = g.Key.Month,
                            SupplierName = s.Supplier_ID,
                            Number = g.Count()

                        };
                return q.ToList();
            }
        }

        public static List<YearLossCostModel> getMonthTrendLoss(DateTime date)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from si in cntx.SupplierInventories
                        join x in cntx.Items on si.Item_ID equals x.Item_ID
                        join a in cntx.AdjustmentDetails on si.Item_ID equals a.Item_ID
                        where a.Adjustment.Issue_Date.Year == date.Year && a.Adj_Qty < 0
                        group new { si, a } by new { si.Item_ID, a.Adjustment.Issue_Date.Month } into g
                        select new YearLossCostModel
                        {
                            Month = g.Key.Month,
                            LossCost = g.Sum(i => i.a.Adj_Qty) * g.Average(i => i.si.Price)*(-1)
                        };
                return q.ToList();
            }
        }

        public static List<MonthDepReportReq> getReqByDep(DateTime date)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {

                var q = from x in cntx.Requisitions
                        where x.Create_Date.Year == date.Year && x.Create_Date.Month == date.Month
                        group x by new { x.Staff1.Dept_ID, x.RequisitonType} into g
                        join dep in cntx.Departments on g.Key.Dept_ID equals dep.Dept_ID
                        where dep.Dept_ID !="STAT" && dep.Dept_ID !="SUPP"
                        select new MonthDepReportReq
                        {
                            Status = g.Key.RequisitonType,
                            DepID = g.Key.Dept_ID,
                            DepName = dep.Dept_Name,
                            Qty = g.Count()
                        };
                return q.ToList();
            }
        }




        // get the no of requisition for each disbursment
        //public dynamic getReqByDisBy(DateTime date)
        //{
        //    using (InventorySysDBEntities cntx = new InventorySysDBEntities())
        //    {
        //        var q = from x in cntx.Requisitions
        //                where x.Create_Date.Year == date.Year && x.Create_Date.Month == date.Month

        //                group x by new {x.Create_Date.Month, }

        //                select new
        //                {
                            
        //                    Date = g.Average(i => i.Create_Date.Month),
        //                    DisID = g.Key,
        //                    ReqNo = g.Count()
        //                };
        //        return q.ToList();
        //    }
        //}



        // get Requisition Item by Category by dept dy date(latest 3 month)
        public static List<MonthReqItemModel> getTop5ReqItemByDate_Category_Dep(DateTime frmdate, string cate, string DepID)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                int premonth = frmdate.Month - 3;
                var q = from x in cntx.RequisitionItems
                        where x.Requisition.Create_Date.Year == frmdate.Year 
                        && x.Requisition.Create_Date.Month <= frmdate.Month 
                        && x.Requisition.Create_Date.Month >=premonth
                        && x.Requisition.Staff1.Dept_ID == DepID
                                && x.Item.Category_ID == cate
                        group x by x.Item_ID into g 
                        join i in cntx.Items on g.Key equals i.Item_ID
                        orderby g.Sum(t => t.Required_Qty) descending
                        select new MonthReqItemModel
                        {
                            ItemName = i.Item_Name,
                            NeedQty = g.Sum(t => t.Required_Qty)

                        };
                return q.Take(5).ToList();
            }
        }

        // get Supplier supply item duration by Month
        public static List<MonthSupplierDurationModel> getSupplyDurByMonth(DateTime frmdate /*int i*/)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                //DateTime toDate = frmdate.AddMonths(i);
                
                var q = from x in cntx.OrderLists
                        where x.Create_Date.Year == frmdate.Year && x.Create_Date.Month == frmdate.Month
                        where x.Finish_Date != null
                        group x by new { x.Supplier.Supplier_ID, x.Create_Date.Month ,DbFunctions.DiffDays(x.Create_Date, x.Finish_Date).Value } into g
                        select new MonthSupplierDurationModel
                        {
                            SupplierName = g.Key.Supplier_ID,
                            Duration = g.Average(t => DbFunctions.DiffDays(t.Create_Date, t.Finish_Date).Value)
                        };
                return q.ToList();

            }
        }


       




        //public List<AverageItem> getLossCost(DateTime date)
        //{
        //    using (InventorySysDBEntities cntx = new InventorySysDBEntities())
        //    {
        //        var q = from x in cntx.Adjustments
        //                where x.Issue_Date.Month == date.Month
        //                join ad in cntx.AdjustmentDetails on x.Voucher_ID equals ad.Voucher_ID


        //                select new AverageItem
        //                {
        //                    Item_ID= ad.Item_ID,
        //                    qty =  ad.Adj_Qty,

        //                };
        //        return q.ToList();
        //    }
        //}


        public static double getLossCost(DateTime date)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from si in cntx.SupplierInventories
                        group si by si.Item_ID into g
                        join x in cntx.Items on g.Key equals x.Item_ID
                        join a in cntx.AdjustmentDetails on g.Key equals a.Item_ID
                        where a.Adjustment.Issue_Date.Year == date.Year && a.Adjustment.Issue_Date.Month == date.Month && a.Adj_Qty < 0
                        select a.Adj_Qty * g.Average(t => t.Price);

                return q.First();
            }
        }

        
        public static List<Item> getAllItem()
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from x in cntx.Items select x;
                return q.ToList();
            }
            
        }
        public static List<Department> getAllDepartment()
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from x in cntx.Departments 
                        where x.Dept_ID != "STAT" && x.Dept_ID!="SUPP"
                        select x;
                return q.ToList();
            }
        }

        public static List<DepRequisitionDetailModel> getRequisitionByYear(DateTime date)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities()){
                var q = from x in cntx.Requisitions
                        where x.Create_Date.Year == date.Year
                        where x.Create_Date.Month == date.Month
                        select new DepRequisitionDetailModel
                        {
                            RequisitionID = x.Requisition_ID,
                            CreateDate = x.Create_Date,
                            CreateStaff = x.Staff1.Staff_Name,
                            DepartmentName = x.Staff1.Department.Dept_Name,
                            Status = x.Status,

                        };
                return q.ToList();
            }
        }

        public static List<ItemCategory> getAllCategory()
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from x in cntx.ItemCategories
                        select x;
                return q.ToList();
            }
        }
        
    }
}
