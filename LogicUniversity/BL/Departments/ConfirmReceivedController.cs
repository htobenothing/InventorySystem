using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Utilities;
namespace BL.Departments
{
    public class ConfirmReceivedController
    {
        public static int checkDisbursmentID(string Dis_ID,  string RepID)
        {
            
            int isDisbursmentIDCorrect = 0;
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                Staff Rep = cntx.Staffs.Find(RepID);
                // whether disbursment exist
                Disbursement dis = cntx.Disbursements.Find(Dis_ID);
                if (dis != null)
                {
                    // Rep only can see its own disbursment
                    if( dis.Dept_ID == Rep.Dept_ID)
                    {
                        // only the submitted disbursment can be seen
                        switch (dis.Status)
                        {
                            case "Received":
                                isDisbursmentIDCorrect = 2;
                                break;
                            case "Submitted":
                                isDisbursmentIDCorrect = 1;
                                break;

                        }
                    }                                 
                }

                return isDisbursmentIDCorrect;
            }
        }
        public static List<DisbursmentItemModel_M> getDisbursmentItems(string Dis_ID)
        {

            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from ri in cntx.RequisitionItems
                        where ri.Requisition.Disbursement_ID == Dis_ID
                        group ri by ri.Item_ID into g
                        join i in cntx.Items on g.Key equals i.Item_ID
                        select new DisbursmentItemModel_M
                        {
                            
                            ItemCode = g.Key,
                            ItemDes = i.Item_Name,
                            NeedQty = g.Sum(m => m.Required_Qty),
                            UOM = i.UOM
                        };

                return q.ToList();

            }
        }
        public static Disbursement getDisbursmentByID(string id)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                
                var q = from f in cntx.Disbursements
                        where f.Disbursement_ID == id
                        select f;
                if (q.Count() > 0)
                {
                    return q.First();
                }
                return null;

            }
        }

        public static List<Requisition> getRequisitionsByDI(string DI)
        {
            using(InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from req in cntx.Requisitions
                        where req.Disbursement_ID == DI
                        select req;

                return q.ToList();
            }
        }
        public static void ConfirmRecivedItems(string DI, List<DisbursementDetail> ddlist, string deptID)
        {
            Staff rep = new Staff();
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                 rep  = StaffController.getRepByDepID(deptID);
                //update disbursmentdetail
                foreach (DisbursementDetail dd in ddlist)
                {
                    updateDisbursmentDetail(cntx,dd);
                    updateItemTransReceived(cntx, deptID, dd);
                }
                
                //update disbursment
                Disbursement disbursment = cntx.Disbursements.Find(DI);
                disbursment.Receive_Date = DateTime.Now;
                disbursment.Status = "Received";

                // update Requisition
                List<Requisition> reqList = getRequisitionsByDI(DI);
                foreach (Requisition req in reqList)
                {
                    Requisition reqData = cntx.Requisitions.Find(req.Requisition_ID);
                    reqData.Update_Date = DateTime.Now;
                    reqData.Status = "Received";
                }

                cntx.SaveChanges();
            }
            SendNotificationController.SendNotificaition(rep, DI);
        }

       

        public static void updateDisbursmentDetail(InventorySysDBEntities cntx, DisbursementDetail newDD)
        {
                // update disbursmentDetailRow
                string DI = newDD.Disbursement_ID;
                string itemID = newDD.Item_ID;
                DisbursementDetail oldDD = cntx.DisbursementDetails.Find(DI, itemID);
                oldDD.Quantity = newDD.Quantity;

        }

        public static void updateItemTransReceived(InventorySysDBEntities cntx, string deptID,DisbursementDetail d)
        {
            var q = from x in cntx.ItemTransactions where x.Item_ID == d.Item_ID select x;
            var p = from x in cntx.Items where x.Item_ID == d.Item_ID select x;
            List<ItemTransaction> ItList = q.ToList();
            ItemTransaction it = new ItemTransaction();
            Item i = p.First();
            it.Item_ID = d.Item_ID;
            it.Quantity = 0-d.Quantity;
            it.Balance = ItList.Last().Balance - d.Quantity;
            it.Reason = "Sent to " + deptID;
            it.Update_Date = DateTime.Now;
            cntx.ItemTransactions.Add(it);
            i.Inventory = ItList.Last().Balance - d.Quantity;
            cntx.SaveChanges();
        }
    }
}
