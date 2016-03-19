using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using BL.Utilities;


namespace BL.Store
{
    public class CreateDisbursementListController
    {
        InventorySysDBEntities context = new InventorySysDBEntities();
        string cs = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        public List<string> GetAllReuisitionId()
        {
            var q = from x in context.Requisitions where x.Status == "Approved" orderby x.Update_Date, x.Staff1.Dept_ID, x.RequisitonType ascending select x.Requisition_ID;
            return q.ToList<string>();
        }

        public string GetSubmissionStaff_ID(string requisitionId)
        {
            var q = from x in context.Requisitions where x.Requisition_ID == requisitionId select x.SubmissionStaff_ID;
            return q.First();
        }

        public string GetDepartmentIdByStaffId(string staff_ID)
        {
            var q = from x in context.Staffs where x.Staff_ID == staff_ID select x.Dept_ID;
            return q.First();
        }

        public string GetDepartmentNameById(string departmentId)
        {
            var q = from x in context.Departments where x.Dept_ID == departmentId select x.Dept_Name;
            return q.First();
        }
        public string GetRequisitionTypeByReqId(string requisitionId)
        {
            var q = from x in context.Requisitions where x.Requisition_ID == requisitionId select x.RequisitonType;
            return q.First();

        }

       /* public List<RequisitionClass> GetRequistionObjects() {
            var q = from x in context.Requisitions where x.Status == "Approved" orderby x.Update_Date select x;
            List<Requisition> requiList = q.ToList<Requisition>();
            List<RequisitionClass> requisitionDetailList = new List<RequisitionClass>();
            foreach(Requisition requi in requiList)
            {
                var p = from x in context.Requisitions where x.Requisition_ID == requi.Requisition_ID select x.SubmissionStaff_ID;
                string staffId=p.First();
                var r = from x in context.Staffs where x.Staff_ID == staffId select x.Department.Dept_Name;
                string deptName=r.First();
                RequisitionClass singleRequsition = new RequisitionClass();
                singleRequsition.ReuisitionId = requi.Requisition_ID;
                singleRequsition.DepartmentName = deptName;
                singleRequsition.RequisitionType = requi.RequisitonType;
                requisitionDetailList.Add(singleRequsition);
            }
            return requisitionDetailList;
        }*/    //Mobile part

        public string GetItemDescByitemId(string itemCode)
        {
            var q = from x in context.Items where x.Item_ID == itemCode select x.Item_Name;
            return q.First();
        }

        public string GetItemCategoryByitemId(string itemCode)
        {
            var q = from x in context.Items where x.Item_ID == itemCode select x.Category_ID;
            return q.First();

        }
        public string GetItemUomByitemId(string itemCode)
        {
            var q = from x in context.Items where x.Item_ID == itemCode select x.UOM;
            return q.First();

        }

        //mobile
        public List<RequisitionItem> GetSelectedreqItemList(List<string> reqIdList)
        {
            var q = from x in context.RequisitionItems where reqIdList.Contains(x.Requisition_ID) select x;
            return q.ToList<RequisitionItem>();
        }


        //mobile
        public List<RequisitionItem> GetreqItemListByReqId(string requisitionId)
        {
            var q = from x in context.RequisitionItems where x.Requisition_ID == requisitionId select x;
            return q.ToList<RequisitionItem>();
        }

        //mobile
        public Item GetItemObjectById(string itemId)
        {
            Item itemOb = context.Items.Single(u => u.Item_ID == itemId);
            return itemOb;
        }

        public List<string> GetReqItemCodeByReqId(string requisitionId)
        {
            var q = from x in context.RequisitionItems where x.Requisition_ID == requisitionId select x.Item_ID;
            return q.ToList<string>();
        }
        public int GetRequiredQty(string reqId, string itemCode)
        {
            var q = from x in context.RequisitionItems where x.Requisition_ID == reqId && x.Item_ID == itemCode select x.Required_Qty;
            return Convert.ToInt32(q.First());
        }

        public string GetStaffIdByReqId(string reqId)
        {
            var q = from x in context.Requisitions where x.Requisition_ID == reqId select x.SubmissionStaff_ID;
            return q.First();
        }


        public List<string> GetDeptList(List<string> ReqIdList)
        {
            var q = from x in context.Requisitions where ReqIdList.Contains(x.Requisition_ID) select x.SubmissionStaff_ID;
            List<string> staffIdList = q.ToList<string>();
            var p = from x in context.Staffs where staffIdList.Contains(x.Staff_ID) select x.Dept_ID;
            List<string> deptIdList = p.ToList<string>();
            var r = from x in context.Departments where deptIdList.Contains(x.Dept_ID) select x.Dept_Name;
            return r.ToList<string>();

        }

        //mobile
        public string GetDepName(string ReqId)
        {

            var q = from x in context.Requisitions where x.Requisition_ID == ReqId select x.SubmissionStaff_ID;
            string staffId = q.First();
            var p = from x in context.Staffs where x.Staff_ID == staffId select x.Dept_ID;
            string deptId = p.First();
            var r = from x in context.Departments where x.Dept_ID == deptId select x.Dept_Name;
            return r.First();
        }

        
        //mobile
        public void UpdateReqIdStatus(List<string> reqList)
        {

            foreach (string reqId in reqList)
            {
                var q = from x in context.Requisitions where x.Requisition_ID == reqId select x;
                Requisition r = q.First();
                r.Status = "Dealing";

            }           
            context.SaveChanges();
        }

        public string getApproveStaffIdByDepName(string deptName)
        {
            var q = from x in context.Departments where x.Dept_Name == deptName select x.Dept_ID;
            string dept_Id = q.First();
            var p = from x in context.Staffs where x.Dept_ID == dept_Id && x.Role_ID == "Manager" select x.Staff_ID;
            return p.First();

        }

        //mobile
        public string getDeptIdByDeptName(string deptName)
        {
            var q = from x in context.Departments where x.Dept_Name == deptName select x.Dept_ID;
            return q.First();          
        }

        //mobile
        public string getRepStaffId(string deptId)
        {
            var q = from x in context.Staffs where x.Dept_ID == deptId && x.Role_ID == "RepStaff" select x.Staff_ID;
            return q.First();
        }



        //moblie
        public void CreateDisburseList(List<string> reqIdByDeptList, Dictionary<string, int> itemDic, Disbursement NewDisbursement)
        {
            context.Disbursements.Add(NewDisbursement);
            foreach (string reqId in reqIdByDeptList)
            {
                var q = from x in context.Requisitions where x.Requisition_ID == reqId select x;
                Requisition r = q.First();
                r.Disbursement_ID = NewDisbursement.Disbursement_ID;
            }

            foreach (string itemID in itemDic.Keys)
            {
                DisbursementDetail disburementDetail = new DisbursementDetail();
                disburementDetail.Disbursement_ID = NewDisbursement.Disbursement_ID;
                disburementDetail.Item_ID = itemID;
                disburementDetail.Quantity = itemDic[itemID];
                context.DisbursementDetails.Add(disburementDetail);
            }
            context.SaveChanges();
        }

    }
  }

