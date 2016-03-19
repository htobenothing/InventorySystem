using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Utilities;
using BL.Departments;

namespace BL
{
    public class ConfirmReceivedController_M
    {
        //check disbursement
        public static bool checkDisbursmentID(string Dis_ID, string RepID)
        {

            bool isDisbursmentIDCorrect = false;
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                Staff Rep = cntx.Staffs.Find(RepID);
                // whether disbursment exist
                Disbursement dis = cntx.Disbursements.Find(Dis_ID);
                if (dis != null)
                {
                    // Rep only can see its own disbursment
                    if (dis.RepStaff_ID == Rep.Staff_ID)
                    {
                        // only the submitted disbursment can be seen
                        switch (dis.Status)
                        {
                            case "Received":
                                break;
                            case "Submitted":
                                isDisbursmentIDCorrect = true;
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

        //confirm received
        public static List<Requisition> getRequisitionsByDI(string DI)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from req in cntx.Requisitions
                        where req.Disbursement_ID == DI
                        select req;

                return q.ToList();
            }
        }
        public static void ConfirmRecivedItems(string DI, List<DisbursementDetail> ddlist)
        {
            Staff rep = new Staff();

            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
               
                //update disbursmentdetail
                foreach (DisbursementDetail dd in ddlist)
                {
                    updateDisbursmentDetail(cntx, dd);
                }

                //update disbursment
                Disbursement disbursment = cntx.Disbursements.Find(DI);
                rep = StaffController.getRepByDepID(disbursment.Dept_ID);

                disbursment.Receive_Date = DateTime.Now;
                disbursment.Status = "Recieved";

                // update Requisition
                List<Requisition> reqList = getRequisitionsByDI(DI);
                foreach (Requisition req in reqList)
                {
                    Requisition reqData = cntx.Requisitions.Find(req.Requisition_ID);
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
      
        

    }
}
