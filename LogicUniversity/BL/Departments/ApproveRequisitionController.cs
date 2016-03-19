using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BL.Departments.Model;

namespace BL.Departments
{

    public class ApproveRequisitionController
    {

        InventorySysDBEntities ctx = new InventorySysDBEntities();

        //


        //get all submitted requisition in department
        public List<Requisition> getAllDeptSubmitReq(string dpt_ID)
        {
            var q = from x in ctx.Requisitions where x.Requisition_ID != null select x;
            List<Requisition> AllRepList = q.ToList();

            var p = from x in ctx.Staffs where x.Dept_ID == dpt_ID select x.Staff_ID;
            List<string> DeptStaffIDList = p.ToList();

            List<Requisition> SubmitReqList = new List<Requisition>();

            foreach (Requisition r in AllRepList)
            {
                if (DeptStaffIDList.Contains(r.SubmissionStaff_ID) && r.Status == "Submitted")
                    SubmitReqList.Add(r);

            }

            return SubmitReqList;
        }

        //get staff name by staff id
        public string GetStaffNameByID(string id)
        {
            var q = from x in ctx.Staffs where x.Staff_ID == id select x.Staff_Name;
            return q.FirstOrDefault();
        }

        //show  submitted requisition table
        public List<SelectedReqModel> ShowReqTable(List<Requisition> ReqList)
        {
            List<SelectedReqModel> SList = new List<SelectedReqModel>();
            {
                foreach (Requisition r in ReqList)
                {
                    SelectedReqModel sr = new SelectedReqModel();
                    sr.Requisition_ID = r.Requisition_ID;
                    sr.Staff_Name = GetStaffNameByID(r.SubmissionStaff_ID);
                    sr.Submission_Date = r.Create_Date;
                    sr.Type = r.RequisitonType;
                    SList.Add(sr);
                }
            }
            return SList;
        }



        //get  SubmitItemDetails list
        public List<SubmitItemDetailsModel> GetReqItemByReqID(string ReqID)
        {
            var ReqList = (from x in ctx.RequisitionItems where x.Requisition_ID == ReqID select x).ToList();

            List<SubmitItemDetailsModel> sidsl = new List<SubmitItemDetailsModel>();
            //List<string> itemIDList = new List<string>();
            for (int i = 0; i < ReqList.Count; i++)
            {

                SubmitItemDetailsModel sids = new SubmitItemDetailsModel();
                int iq = ReqList[i].Required_Qty;
                string itemId = ReqList[i].Item_ID;

                var f = from x in ctx.Items where x.Item_ID == itemId select x;

                Item it = f.First();
                sids.Category_ID = it.Category_ID;
                sids.Item_Name = it.Item_Name;
                sids.Required_Qty = iq;
                sids.UOM = it.UOM;
                sidsl.Add(sids);

            }
            return sidsl;

        }

        //approve  requisitions
        public void ApproveRequisition(string reqId, string managerID)
        {
            Staff frmStaff = StaffController.getStaffById(managerID);
            var t = from x in ctx.Requisitions where x.Requisition_ID == reqId select x;
            Requisition r = t.First();
            r.Status = "Approved";
            r.ApprovedByStaff_ID = managerID;
            r.Update_Date = DateTime.Now;
            ctx.SaveChanges();
            SendNotificationController.SendNotificaition(frmStaff, r.Requisition_ID);
        }

        //reject requisitions
        public void RejectRequisition(string reqId, string managerID, string RejectReason)
        {
            Staff frmStaff = StaffController.getStaffById(managerID);
            var t = from x in ctx.Requisitions where x.Requisition_ID == reqId select x;
            Requisition r = t.First();
            r.Status = "Rejected";
            r.ApprovedByStaff_ID = managerID;
            r.Update_Date = DateTime.Now;
            r.Remark = RejectReason;
            ctx.SaveChanges();
            SendNotificationController.SendNotificaition(frmStaff, r.Requisition_ID);
        }






    }
}
