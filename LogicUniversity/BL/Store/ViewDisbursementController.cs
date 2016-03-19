using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace BL.Store
{
    public class ViewDisbursementController
    {
        InventorySysDBEntities context = new InventorySysDBEntities();

        public string getDeptID(string deptName)
        {
            var q = from x in context.Departments where x.Dept_Name == deptName select x.Dept_ID;
            return q.First();
        }
        public List<Disbursement> GetDisbursementByDateDeptAndStatus(DateTime fromDate, DateTime todate, string deptId, string status) {
            var q = from x in context.Disbursements where EntityFunctions.TruncateTime(x.Create_Date) >= fromDate && EntityFunctions.TruncateTime(x.Create_Date) <= todate && x.Status == status && x.Dept_ID==deptId select x;          
            return q.ToList<Disbursement>();       
        
        }

        public List<Disbursement> GetDisbursementByDateAndStatus(DateTime fromDate, DateTime todate, string status) 
        {
            var q = from x in context.Disbursements where EntityFunctions.TruncateTime(x.Create_Date) >= fromDate && EntityFunctions.TruncateTime(x.Create_Date) <= todate && x.Status == status select x;
            return q.ToList<Disbursement>();       
        }
        public List<Disbursement> GetDisbursementByDate(DateTime fromDate, DateTime todate)
        { 
            var q = from x in context.Disbursements  where EntityFunctions.TruncateTime(x.Create_Date) >= fromDate.Date && EntityFunctions.TruncateTime(x.Create_Date) <= todate.Date select x;           
            return q.ToList<Disbursement>(); 
        }
        public string GetCollectionDesc(string deptId)
        {
            var q = from x in context.Departments where x.Dept_ID == deptId select x.CollectionPoint.Collection_Desc;
            return q.First();
        }

        public List<Disbursement> GetDisbursementByDateAndDept(DateTime fromDate, DateTime todate, string deptId)
        {
            var q = from x in context.Disbursements where (EntityFunctions.TruncateTime(x.Create_Date) >= fromDate.Date && EntityFunctions.TruncateTime(x.Create_Date) <= todate.Date && x.Dept_ID == deptId) select x;
            return q.ToList<Disbursement>();
        }
        public List<DisbursementDetail> getDisbursementDetailById(string disbursementId)
        {
            var q = from x in context.DisbursementDetails where x.Disbursement_ID == disbursementId select x;
            return q.ToList<DisbursementDetail>();
        }
        public string getItemDescById(string itemId)
        {
            var q = from x in context.Items where x.Item_ID == itemId select x.Item_Name;
            return q.First();
        }

        public string getDeptNameByDisId(string disbursementId) 
        {
            var q = from x in context.Disbursements where x.Disbursement_ID == disbursementId select x.Department.Dept_Name;
            return q.First();
        }

        public string getDeptNameByDeptId(string deptID)
        {
            var q = from x in context.Departments where x.Dept_ID == deptID select x.Dept_Name;
            return q.First();
        }
        public Dictionary<string,List<Disbursement>> getDisburselistByCollectionPoint(string collection) {
            Dictionary<string, List<Disbursement>> DpNameAndDisMap = new Dictionary<string, List<Disbursement>>();
            var q = from x in context.Departments where x.Collection_ID == collection select x.Dept_ID;
            List<string> deptIDList = q.ToList<string>();

            foreach(string deptId in deptIDList)
            {
                var p = from x in context.Disbursements where x.Dept_ID == deptId && x.Status == "Submitted"  select x;
                DpNameAndDisMap.Add(getDeptNameByDeptId(deptId), p.ToList());
            }
            return DpNameAndDisMap;
        }
    }
}
