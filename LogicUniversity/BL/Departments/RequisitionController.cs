using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Store;

namespace BL.Departments
{
    public class RequisitionController
    {
        static TimeConvertController TC = new TimeConvertController();

        public List<string> getCategoryList()
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from c in cntx.ItemCategories select c.Category_ID;
                
                return q.ToList();
            }
        }

        public List<string> getAllItemIDList()
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from c in cntx.Items select c.Item_ID;
                return q.ToList();
            }
        }

        public List<string> getAllItemDesList()
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from c in cntx.Items select c.Item_Name;
                return q.ToList();
            }
        }

        public Dictionary<string, List<Item>> getAllItemDic()
        {
            List<Item> itemList = new List<Item>();
            Dictionary<string, Item> ItemDic = new Dictionary<string, Item>();

            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from c in cntx.Items
                        group c by c.Category_ID;
               return q.ToDictionary(g => g.Key, g=>g.ToList<Item>());
            }
             
             
        }

        public static List<RequisitionItemModel_M> getRequisitionItem_M(string reqID)
        {
            using(InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from i in cntx.RequisitionItems
                        where i.Requisition_ID == reqID
                        select new RequisitionItemModel_M
                        {
                            ItemName = i.Item.Item_Name,
                            UOM = i.Item.UOM,
                            NeedQty = i.Required_Qty
                        };

               return  q.ToList();
            }
            
            
        }

        public Item GetDefaultItem()
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from x in cntx.Items where x.Item_ID == "C001" select x;
                Item i = q.First<Item>();
                return i;
            }
            
        }

        public dynamic getItemDesIDByID(string catID)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from c in cntx.Items
                        where c.Category_ID == catID
                        select new
                        {
                            c.Item_ID,
                            c.Item_Name
                        };
                return q.ToList();
            }
        }
        public string getItemUOMByID(string id)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from c in cntx.Items
                        where c.Item_ID == id
                        select c.UOM;
                       
                return q.First();
            }
        }

        public Item getItemById(string id)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                Item item = cntx.Items.Find(id);
                return item;
            }
        }

        //public List<Requisition> getReqByID(string id)
        //{
        //    using (InventorySysDBEntities cntx = new InventorySysDBEntities())
        //    {
        //        var q = from x in cntx.RequisitionItems
        //                select new
        //                {
        //                    x.Item.Item_Name,
        //                    x.Item.Item_Name
        //                };
        //    }
        //}
        public static bool checkReqByStaffID(string ReqID,string StaffID ){

            bool isReqIDCorrect = false;
            
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {

               Requisition req = cntx.Requisitions.Find(ReqID); 
               if(req != null){
                   if(req.SubmissionStaff_ID == StaffID){
                       isReqIDCorrect = true;
                   }
               }
               return isReqIDCorrect;
            }
        }

        public static List<Requisition> getReqByDate_Status(string stfid,string fromDate, string toDate, string Status)
        {
            DateTime fromD = new DateTime();
            DateTime toD  = new DateTime();
            if(fromDate != "" && toDate !=""){

                fromD = TC.DateConvert(fromDate);
                toD = TC.DateConvert(toDate);
            }else{
                if(fromDate == "" && toDate == ""){
                    fromD = DateTime.MinValue;
                    toD = DateTime.MaxValue;
                }
                if(fromDate != "" && toDate ==""){
                    fromD = Convert.ToDateTime(fromDate);
                    toD = DateTime.MaxValue;
                }
                if(fromDate =="" && toDate!=""){
                    fromD = DateTime.MinValue;
                    toD = Convert.ToDateTime(toDate);
                }

            }
            

            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                
                if(Status == "All"){
                    var q = from x in cntx.Requisitions
                            where EntityFunctions.TruncateTime(x.Create_Date) >= fromD.Date
                            where EntityFunctions.TruncateTime(x.Create_Date) <= toD.Date
                            where x.SubmissionStaff_ID==stfid
                            select x;
                    return q.ToList();
                    
                }else
                {
                    var q = from x in cntx.Requisitions
                            where EntityFunctions.TruncateTime(x.Create_Date) >= fromD.Date
                            where EntityFunctions.TruncateTime(x.Create_Date) <= toD.Date 
                            where x.Status ==Status
                            where x.SubmissionStaff_ID == stfid
                            select x;
                    return q.ToList();
                }
                        
             }

                

            
        }

        public static Requisition getReqByID(string reqID){

            using (InventorySysDBEntities cntx =new InventorySysDBEntities())
            {

                return (Requisition)cntx.Requisitions.Find(reqID);
                        
            }

        }
        public static List<RequisitionViewModel> getRequisitionVM(string reqID)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                var q = from x in cntx.RequisitionItems
                        where x.Requisition_ID == reqID
                        select new RequisitionViewModel
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

       
        public static void updateRequisition(List<RequisitionItem> reqItemList, string reqID, string status)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                Requisition req = cntx.Requisitions.Find(reqID);
                req.Create_Date = DateTime.Now;
                
                req.Status = status;

                foreach (RequisitionItem reqi in reqItemList)
                {
                    if (reqi.Required_Qty == 0)
                    {
                        RequisitionItem reqItem =  cntx.RequisitionItems.Find(reqi.Requisition_ID, reqi.Item_ID);
                        cntx.RequisitionItems.Remove(reqItem);
                    }
                    else
                    {
                        RequisitionItem reqItem = cntx.RequisitionItems.Find(reqi.Requisition_ID, reqi.Item_ID);
                        reqItem.Required_Qty = reqi.Required_Qty;

                    }
                }
                cntx.SaveChanges();
            }
        }

        public static void createRequisition (List<RequisitionItem> reqItemList,string submitStaff_id, string type, string status)
        {
             
            Requisition newReq = new Requisition();
            Staff fromStaff = StaffController.getStaffById(submitStaff_id);
            // use sample data to test
            newReq.Requisition_ID = "REQ"+DateTime.Now.ToString("yyyyMMddHHmmss");

            newReq.SubmissionStaff_ID = submitStaff_id;
            // approve staff can be null , for test use sample data
            //newReq.ApprovedByStaff_ID = "STF0002";
            newReq.Create_Date = DateTime.Now;
            newReq.RequisitonType = type;
            newReq.Status = status;
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                cntx.Requisitions.Add(newReq);
                cntx.SaveChanges();

                
                foreach (RequisitionItem reqItem in reqItemList)
                {
                    reqItem.Requisition_ID = newReq.Requisition_ID;
                    cntx.RequisitionItems.Add(reqItem);
                    
                }
                cntx.SaveChanges();
            }
            if (type != "Special")
            {
                SendNotificationController.SendNotificaition(fromStaff, newReq.Requisition_ID);
            }
            
            
        }
        
    }
}
