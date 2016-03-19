using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Utilities;
using System.Data.Entity.Core.Objects;

namespace BL.Departments
{
    public class RequisitionItemController_M
    {

        public static List<RequisitionItemList_M> getAllRequisitionListByDepartment(string staffID)
        {

            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                Staff managerStaff = cntx.Staffs.Find(staffID);
               

                var q = from ri in cntx.RequisitionItems
                        where ri.Requisition.Staff1.Dept_ID == managerStaff.Dept_ID
                        group ri by ri.Requisition_ID into g
                        join req in cntx.Requisitions on g.Key equals req.Requisition_ID
                        where req.Status == "Submitted"
                        select new RequisitionItemList_M
                        {
                            RequisitionID = g.Key,
                            SubmissionStaffName = req.Staff1.Staff_Name,
                            SubmitDate = req.Create_Date,
                            TotalQty = g.Sum(m => m.Required_Qty),
                        };
                return q.ToList();
                
            }
                   
        }
        public static List<RequisitionItemDetail_M> getRequisitionItem_Detail(string reqID)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from i in cntx.RequisitionItems
                        where i.Requisition_ID == reqID
                        select new RequisitionItemDetail_M
                        {
                            ItemName = i.Item.Item_Name,
                            UOM = i.Item.UOM,
                            NeedQty = i.Required_Qty
                        };

                return q.ToList();
            }


        }
        public static void createRequisition(List<RequisitionItem> reqItemList, string submitStaff_id, string type, string status)
        {


            Requisition newReq = new Requisition();
            Staff fromStaff = StaffController.getStaffById(submitStaff_id);
            // use sample data to test
            newReq.Requisition_ID = "REQ" + DateTime.Now.ToString("yyyyMMddHHmmss");
           // newReq.Requisition_ID = "asdqwer";
            newReq.SubmissionStaff_ID = submitStaff_id;
            // approve staff can be null , for test use sample data
            newReq.ApprovedByStaff_ID = "STF0002";
            newReq.Create_Date = DateTime.Now;
            newReq.RequisitonType = type;
            newReq.Status = status;
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                cntx.Requisitions.Add(newReq);
                cntx.SaveChanges();


                for (int i = 0; i < reqItemList.Count; i++)
                {
                    reqItemList[i].Requisition_ID = newReq.Requisition_ID;
                    cntx.RequisitionItems.Add(reqItemList[i]);
                }
                    
                cntx.SaveChanges();

            }
            if (type != "Special")
            {
                SendNotificationController.SendNotificaition(fromStaff, newReq.Requisition_ID);
            }

        }

        //approve  requisitions
        public static void ApproveRequisition(string reqId, string managerID)
        {
            Staff frmStaff = StaffController.getStaffById(managerID);
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var t = from x in cntx.Requisitions where x.Requisition_ID == reqId select x;
                Requisition r = t.First();
                r.Status = "Approved";
                r.ApprovedByStaff_ID = managerID;
                r.Update_Date = DateTime.Now;
                cntx.SaveChanges();
                SendNotificationController.SendNotificaition(frmStaff, r.Requisition_ID);
            }
         

        }

        //reject requisitions
        public static void RejectRequisition(string reqId, string managerID, string RejectReason)
        {
            Staff frmStaff = StaffController.getStaffById(managerID);
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {

                var t = from x in cntx.Requisitions where x.Requisition_ID == reqId select x;
                Requisition r = t.First();
                r.Status = "Rejected";
                r.ApprovedByStaff_ID = managerID;
                r.Update_Date = DateTime.Now;
                r.Remark = RejectReason;
                cntx.SaveChanges();
                SendNotificationController.SendNotificaition(frmStaff, r.Requisition_ID);
            }
            

        }


        //search listing requisition for staff
        public static List<Requisition> getReqByDate_Status(string stfid, string fromDate, string toDate, string Status)
        {
            DateTime fromD = new DateTime();
            DateTime toD = new DateTime();
            if (fromDate != "" && toDate != "")
            {

                fromD = Convert.ToDateTime(fromDate);
                toD = Convert.ToDateTime(toDate);
            }
            else
            {
                if (fromDate == "" && toDate == "")
                {
                    fromD = DateTime.MinValue;
                    toD = DateTime.MaxValue;
                }
                if (fromDate != "" && toDate == "")
                {
                    fromD = Convert.ToDateTime(fromDate);
                    toD = DateTime.MaxValue;
                }
                if (fromDate == "" && toDate != "")
                {
                    fromD = DateTime.MinValue;
                    toD = Convert.ToDateTime(toDate);
                }

            }


            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {

                if (Status == "All")
                {
                    var q = from x in cntx.Requisitions
                            where EntityFunctions.TruncateTime(x.Create_Date) >= fromD.Date
                            where EntityFunctions.TruncateTime(x.Create_Date) <= toD.Date
                            where x.SubmissionStaff_ID == stfid
                            select x;
                            
                    return q.ToList();

                }
                else
                {
                    var q = from x in cntx.Requisitions
                            where EntityFunctions.TruncateTime(x.Create_Date) >= fromD.Date
                            where EntityFunctions.TruncateTime(x.Create_Date) <= toD.Date
                            where x.Status == Status
                            where x.SubmissionStaff_ID == stfid
                            select x;
                            
                    return q.ToList();
                }

            }




        }

        //detail requisition detail after searching for staff
        public static List<RequisitionViewModel_M> getRequisitionVM(string reqID)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from x in cntx.RequisitionItems
                        where x.Requisition_ID == reqID
                        select new RequisitionViewModel_M
                        {
                            ItemID = x.Item_ID,
                            ItemName = x.Item.Item_Name,
                            UOM = x.Item.UOM,
                            categoryID = x.Item.Category_ID,
                            RequiredQty = x.Required_Qty
                        };
                return q.ToList();
            }
        }


        
    }
}
/**
 * 
Declare @p__linq__0 nvarchar;

Set @p__linq__0 = 'COMM'
SELECT
    [Extent1].[Requisition_ID] AS [Requisition_ID],
    [Extent1].[Item_ID] AS [Item_ID],
    [Extent1].[Required_Qty] AS [Required_Qty]
    FROM   [dbo].[RequisitionItem] AS [Extent1]
    INNER JOIN [dbo].[Requisition] AS [Extent2] ON [Extent1].[Requisition_ID] =
[Extent2].[Requisition_ID]
    INNER JOIN [dbo].[Staff] AS [Extent3] ON [Extent2].[ApprovedByStaff_ID] = [Extent3].[Staff_ID]
    WHERE [Extent3].[Dept_ID] = @p__linq__0
 */
