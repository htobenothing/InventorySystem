using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Store;
using BL.Utilities;
using System.Data.Entity.Core.Objects;

namespace BL.Store
{
    public class ClerkDisburementController_M
    {
          //Disburement Search History
        //listing
        public static string getDeptNameByDeptId(string deptID)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {

                var q = from x in cntx.Departments where x.Dept_ID == deptID select x.Dept_Name;
                return q.First();
            }
        }

        public static Dictionary<string, List<DisItemTotalQtyModel_M>> getDisburselistByCollectionPoint(string collectionID)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {

                Dictionary<string, List<DisItemTotalQtyModel_M>> DpNameAndDisMap = new Dictionary<string, List<DisItemTotalQtyModel_M>>();
                var q = from x in cntx.Departments where x.Collection_ID == collectionID select x.Dept_ID;
                List<string> deptIDList = q.ToList<string>();

                foreach (string deptId in deptIDList)
                {
                    Console.WriteLine(deptId);
                    var p = from x in cntx.DisbursementDetails
                            where x.Disbursement.Dept_ID == deptId && x.Disbursement.Status == "Submitted"
                            group x by x.Disbursement_ID into g
                            select new DisItemTotalQtyModel_M
                            {
                               
                                Disbursement_ID = g.Key,
                                TotalQty = g.Sum(i=> i.Quantity)

                            };

                    Console.WriteLine(p.ToString());
                    
                    DpNameAndDisMap.Add(getDeptNameByDeptId(deptId), p.ToList());
                }
                return DpNameAndDisMap;

               
            }
        }

        public static Dictionary<string, List<DisItemTotalQtyModel_M>> getDisburselistByCollectionPointAndDate(string status, string collectionID,string fromDate, string toDate)
        {

            DateTime fromD = new DateTime();
            DateTime toD  = new DateTime();
            if(fromDate != "" && toDate !=""){

                fromD = Convert.ToDateTime(fromDate);
                toD = Convert.ToDateTime(toDate);
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

                Dictionary<string, List<DisItemTotalQtyModel_M>> DpNameAndDisMap = new Dictionary<string, List<DisItemTotalQtyModel_M>>();
                var q = from x in cntx.Departments where x.Collection_ID == collectionID select x.Dept_ID;
                List<string> deptIDList = q.ToList<string>();
                if (status == "All")
                {
                    foreach (string deptId in deptIDList)
                    {
                        var p = from x in cntx.DisbursementDetails
                                where x.Disbursement.Dept_ID == deptId 
                                where EntityFunctions.TruncateTime(x.Disbursement.Create_Date) >= fromD.Date && EntityFunctions.TruncateTime(x.Disbursement.Create_Date) <= toD.Date
                                group x by x.Disbursement_ID into g
                                select new DisItemTotalQtyModel_M
                                {

                                    Disbursement_ID = g.Key,
                                    TotalQty = g.Sum(i => i.Quantity)

                                };
                        Console.WriteLine(p.ToString());

                        DpNameAndDisMap.Add(getDeptNameByDeptId(deptId), p.ToList());
                    }
                }
                else
                {
                    foreach (string deptId in deptIDList)
                    {
                        var p = from x in cntx.DisbursementDetails
                                where x.Disbursement.Dept_ID == deptId && x.Disbursement.Status == status
                                where EntityFunctions.TruncateTime(x.Disbursement.Create_Date) >= fromD.Date && EntityFunctions.TruncateTime(x.Disbursement.Create_Date) <= toD.Date
                                group x by x.Disbursement_ID into g
                                select new DisItemTotalQtyModel_M
                                {

                                    Disbursement_ID = g.Key,
                                    TotalQty = g.Sum(i => i.Quantity)

                                };
                        Console.WriteLine(p.ToString());

                        DpNameAndDisMap.Add(getDeptNameByDeptId(deptId), p.ToList());
                    }
                }
                
                return DpNameAndDisMap;

               
            }


            
        }
        //detail
        public static List<DisbursementDetailItemModel_M> getDisbursementDetailById(string disbursementId)
        {

            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {

                var q = from x in cntx.DisbursementDetails
                        where x.Disbursement_ID == disbursementId
                        select new DisbursementDetailItemModel_M
                        {
                            Item_ID = x.Item.Item_ID,
                            Item_Name = x.Item.Item_Name,
                            UOM = x.Item.UOM,
                            Quantity = x.Quantity,
                        };
                        
                return q.ToList<DisbursementDetailItemModel_M>();
            }
        }

        public static List<DisbDetailItemAndDeptNameModel_M> getDisbursementDetailAndDeptNameById(string disbursementId)
        {
            using (InventorySysDBEntities cntx = new InventorySysDBEntities())
            {
                List<DisbDetailItemAndDeptNameModel_M> ListM = new List<DisbDetailItemAndDeptNameModel_M>();
                var q = from x in cntx.DisbursementDetails
                        where x.Disbursement_ID == disbursementId
                        select x;
                foreach(DisbursementDetail dd in q.ToList())
                {
                    DisbDetailItemAndDeptNameModel_M dm = new DisbDetailItemAndDeptNameModel_M();
                    dm.Item_ID = dd.Item_ID;
                    dm.Item_Name= dd.Item.Item_Name;
                    dm.UOM = dd.Item.UOM;
                    dm.Quantity = dd.Quantity;
                    var p = from x in cntx.Disbursements where x.Disbursement_ID == dd.Disbursement_ID select x.Department.Dept_Name;
                    dm.deptName = p.First();
                    ListM.Add(dm);
                }


                return ListM;
                
            }
        }


        //createDisbursementList
        public void createDisbursementList(List<string> selectedReqidList, Dictionary<string, int> neededqty, Dictionary<string, int> UnderNeeded_qty)
          //  public List<DeptLevelReq> createDisbursementList(List<string> selectedReqidList, Dictionary<string, int> neededqty, Dictionary<string, int> UnderNeeded_qty)
        {

            Dictionary<string, DeptLevelReqController_M> mstrDic_Dept = new Dictionary<string, DeptLevelReqController_M>();
            //  Dictionary<string, int> underNeededItem_Dic = new Dictionary<string, int>();
            Dictionary<string, int> underNeededItem_Dic = UnderNeeded_qty;
            //  Dictionary<string, int> NeededItem_Dic = new Dictionary<string, int>();
            Dictionary<string, int> NeededItem_Dic = neededqty;
            Dictionary<string, List<ItemInfobyReqiIdModel_M>> deptReqDictionary = new Dictionary<string, List<ItemInfobyReqiIdModel_M>>();//pojo2 dictionary
            List<DeptLevelReqController_M> deptLevelReqList = new List<DeptLevelReqController_M>(); //Pojo 2 list
            Dictionary<string, Dictionary<string, int>> depConsolidatedDic = new Dictionary<string, Dictionary<string, int>>();
            Dictionary<string, List<ItemInfobyReqiIdModel_M>> depToItem = new Dictionary<string, List<ItemInfobyReqiIdModel_M>>();
            Dictionary<string, List<ItemInfobyReqiIdModel_M>> specialReqDic = new Dictionary<string, List<ItemInfobyReqiIdModel_M>>();
            Dictionary<string, List<ItemInfobyReqiIdModel_M>> urgentReqDic = new Dictionary<string, List<ItemInfobyReqiIdModel_M>>();
            Dictionary<string, List<ItemInfobyReqiIdModel_M>> normalReqDic = new Dictionary<string, List<ItemInfobyReqiIdModel_M>>();

            //Populating specialReqDic ,urgentReqDic ,normalReqDic 

            foreach (string reqiId in selectedReqidList)                           //1
            {
                List<RequisitionItem> reqiItemByReqId = GetreqItemListByReqId(reqiId);
                List<ItemInfobyReqiIdModel_M> ItemInfobyReqiIdList = new List<ItemInfobyReqiIdModel_M>();
                foreach (RequisitionItem reqItem in reqiItemByReqId)
                {
                    ItemInfobyReqiIdModel_M iteminfoByReq = new ItemInfobyReqiIdModel_M();
                    iteminfoByReq.RequisitionId1 = reqiId;
                    iteminfoByReq.ItemCode1 = reqItem.Item_ID;
                    iteminfoByReq.ItemQty = reqItem.Required_Qty;
                    ItemInfobyReqiIdList.Add(iteminfoByReq);
                }
                if (GetRequisitionTypeByReqId(reqiId) == "Special")
                {
                    specialReqDic[reqiId] = ItemInfobyReqiIdList;
                }
                if (GetRequisitionTypeByReqId(reqiId) == "Urgent")
                {
                    urgentReqDic[reqiId] = ItemInfobyReqiIdList;
                }

                if (GetRequisitionTypeByReqId(reqiId) == "Normal")
                {
                    normalReqDic.Add(reqiId, ItemInfobyReqiIdList);
                }
                deptReqDictionary.Add(reqiId, ItemInfobyReqiIdList);                                    //duplicacy

                Dictionary<string, List<ItemInfobyReqiIdModel_M>> deptReqDictionary_tmp2PopulateMstrListPojo2 = new Dictionary<string, List<ItemInfobyReqiIdModel_M>>();//pojo2 dictionary   
                deptReqDictionary_tmp2PopulateMstrListPojo2.Add(reqiId, ItemInfobyReqiIdList);
                string dept_Name = GetDepName(reqiId);
                DeptLevelReqController_M deptLevelReq = new DeptLevelReqController_M();
                deptLevelReq.DeptName = dept_Name;
                deptLevelReq.DeptReqi = deptReqDictionary_tmp2PopulateMstrListPojo2;                          // populate Pojo2 ???
                deptLevelReqList.Add(deptLevelReq);                                 //pojo 2 lis                

            }


            for (int index = 0; index < specialReqDic.Count; index++)
            {
                int y = specialReqDic.Count;
                var item = specialReqDic.ElementAt(index);
                string reqId = item.Key;
                List<ItemInfobyReqiIdModel_M> reqIdItemList = item.Value;
                string deptName = GetDepName(reqId);

                foreach (DeptLevelReqController_M depLevelReq in deptLevelReqList)                //deplevel final map
                {
                    //deptReqDictionary=Session["deptReqDictionary"];
                    if (depLevelReq.DeptName == deptName)
                    {
                        List<ItemInfobyReqiIdModel_M> reqIdItemInfo = deptReqDictionary[reqId];
                        foreach (ItemInfobyReqiIdModel_M itemInfoByreq in reqIdItemInfo)
                        {

                            string itemCode = itemInfoByreq.ItemCode1;
                            int assign = itemInfoByreq.ItemQty;

                            //start  allocating quantity
                            if (underNeededItem_Dic.ContainsKey(itemCode))
                            {
                                if (itemInfoByreq.ItemQty < underNeededItem_Dic[itemCode])
                                {
                                    itemInfoByreq.Item_assigned1 = itemInfoByreq.ItemQty;
                                    underNeededItem_Dic[itemCode] = underNeededItem_Dic[itemCode] - itemInfoByreq.Item_assigned1;
                                }
                                else
                                {
                                    itemInfoByreq.Item_assigned1 = underNeededItem_Dic[itemCode];
                                }
                            }
                            else
                            {
                                itemInfoByreq.Item_assigned1 = itemInfoByreq.ItemQty;
                                NeededItem_Dic[itemCode] = NeededItem_Dic[itemCode] - itemInfoByreq.Item_assigned1;
                            }

                            ///finish allocation
                        }


                    }

                }
            }

            for (int index = 0; index < urgentReqDic.Count; index++)
            {
                int y = urgentReqDic.Count;
                var item = urgentReqDic.ElementAt(index);
                string reqId = item.Key;
                List<ItemInfobyReqiIdModel_M> reqIdItemList = item.Value;
                string deptName = GetDepName(reqId);

                foreach (DeptLevelReqController_M depLevelReq in deptLevelReqList)
                {
                    if (depLevelReq.DeptName == deptName)
                    {
                        List<ItemInfobyReqiIdModel_M> reqIdItemInfo = deptReqDictionary[reqId];
                        foreach (ItemInfobyReqiIdModel_M itemInfoByreq in reqIdItemInfo)
                        {

                            string itemCode = itemInfoByreq.ItemCode1;
                            int assign = itemInfoByreq.ItemQty;

                            //start  allocating quantity
                            if (underNeededItem_Dic.ContainsKey(itemCode))
                            {
                                if (itemInfoByreq.ItemQty < underNeededItem_Dic[itemCode])
                                {
                                    itemInfoByreq.Item_assigned1 = itemInfoByreq.ItemQty;
                                    underNeededItem_Dic[itemCode] = underNeededItem_Dic[itemCode] - itemInfoByreq.Item_assigned1;
                                }
                                else
                                {
                                    itemInfoByreq.Item_assigned1 = underNeededItem_Dic[itemCode];
                                }
                            }
                            else
                            {
                                itemInfoByreq.Item_assigned1 = itemInfoByreq.ItemQty;
                                NeededItem_Dic[itemCode] = NeededItem_Dic[itemCode] - itemInfoByreq.Item_assigned1;
                            }

                            ///finish allocation
                        }


                    }

                }
            }



            // selection from screen --- mstr map : req-item          
            for (int index = 0; index < normalReqDic.Count; index++)
            {
                int y = normalReqDic.Count;
                var item = normalReqDic.ElementAt(index);
                string reqId = item.Key;
                List<ItemInfobyReqiIdModel_M> reqIdItemList = item.Value;
                string deptName = GetDepName(reqId);

                foreach (DeptLevelReqController_M depLevelReq in deptLevelReqList)
                {
                    if (depLevelReq.DeptName == deptName)
                    {
                        List<ItemInfobyReqiIdModel_M> reqIdItemInfo = deptReqDictionary[reqId];
                        foreach (ItemInfobyReqiIdModel_M itemInfoByreq in reqIdItemInfo)
                        {

                            string itemCode = itemInfoByreq.ItemCode1;
                            int assign = itemInfoByreq.ItemQty;

                            //start  allocating quantity
                            if (underNeededItem_Dic.ContainsKey(itemCode))
                            {
                                if (itemInfoByreq.ItemQty < underNeededItem_Dic[itemCode])
                                {
                                    itemInfoByreq.Item_assigned1 = itemInfoByreq.ItemQty;
                                    underNeededItem_Dic[itemCode] = underNeededItem_Dic[itemCode] - itemInfoByreq.Item_assigned1;
                                }
                                else
                                {
                                    itemInfoByreq.Item_assigned1 = underNeededItem_Dic[itemCode];
                                }
                            }
                            else
                            {                                                /////////new
                                itemInfoByreq.Item_assigned1 = itemInfoByreq.ItemQty;
                                NeededItem_Dic[itemCode] = NeededItem_Dic[itemCode] - itemInfoByreq.Item_assigned1;
                            }

                            ///finish allocation
                        }

                    }

                }
            }



            ////for showing on screen populating dictionary depToItem[deptnmae(key),listOfItem(value)]
            // List<Dictionary<string, List<ItemInfobyReqiId>>> deptLevelDisbursementList = new List<Dictionary<string, List<ItemInfobyReqiId>>>();
         //  List<DeptLevelReq> deptLevelDisbursementList = new List<DeptLevelReq>();
            foreach (DeptLevelReqController_M deptLevelReq in deptLevelReqList)
            {
                if (depToItem.ContainsKey(deptLevelReq.DeptName))
                {
                    List<ItemInfobyReqiIdModel_M> finalAggregatedItemsLis = addNewItemListIntoExistingItemList(depToItem[deptLevelReq.DeptName], getAggregatedItems(deptLevelReq));
                    depToItem.Remove(deptLevelReq.DeptName);
                    depToItem.Add(deptLevelReq.DeptName, finalAggregatedItemsLis);
                }
                else
                {
                    depToItem.Add(deptLevelReq.DeptName, getAggregatedItems(deptLevelReq));
                }
                //DeptLevelReq deptLevel = new DeptLevelReq();
                //deptLevel.DeptName = deptLevelReq.DeptName;
                //deptLevel.DeptReqi = depToItem;
                //deptLevelDisbursementList.Add(deptLevel);
            }
            string s = "";
            for (int index = 0; index < depToItem.Count; index++)
            {
                Dictionary<string, int> itemCodeDic = new Dictionary<string, int>();
                var item = depToItem.ElementAt(index);
                string deptName = item.Key;
                List<ItemInfobyReqiIdModel_M> consolidateItemList = item.Value;
                string disbursementId = "D0" + getDisbursementId();
                if (disbursementId == s) {
                    disbursementId = "D0" + (Convert.ToInt64(disbursementId.Substring(2)) + 1).ToString();
                
                }
               
                foreach (ItemInfobyReqiIdModel_M itemList in consolidateItemList)
                {
                    string itemcode = itemList.ItemCode1;
                    int quantity = itemList.Item_assigned1;
                    itemCodeDic.Add(itemcode, quantity);
                }
                List<string> reqIdByDept = new List<string>();
                //selectedReqidList = (List<string>)Session["selectedReqIdsList"];
                foreach (string reqId in selectedReqidList)
                {

                    string depname = GetDepName(reqId);
                    if (depname == deptName)
                    {
                        reqIdByDept.Add(reqId);
                    }
                }
                Disbursement disbursement = new Disbursement();
                disbursement.Disbursement_ID = disbursementId;
                disbursement.Create_Date = DateTime.Now;
                disbursement.Receive_Date = null;
                disbursement.Dept_ID = getDeptIdByDeptName(deptName);
                disbursement.RepStaff_ID = getRepStaffId(disbursement.Dept_ID);
                disbursement.Status = "Submitted";
                saveDisburseList(reqIdByDept, itemCodeDic, disbursement);
                s = disbursementId;
                //DeptLevelReq deptlevelReq = new DeptLevelReq();
                //dept

            }
           // return deptLevelDisbursementList;

        }

        public List<ItemInfobyReqiIdModel_M> getAggregatedItems(DeptLevelReqController_M deptLevelReq)
        {


            Dictionary<string, ItemInfobyReqiIdModel_M> temp_Dic_AggregatedItems = new Dictionary<string, ItemInfobyReqiIdModel_M>();


            Dictionary<string, List<ItemInfobyReqiIdModel_M>> temp_DeptReqi = deptLevelReq.DeptReqi;


            foreach (var key in temp_DeptReqi.Keys)
            {
                ItemInfobyReqiIdModel_M reqItemInfo = new ItemInfobyReqiIdModel_M();
                foreach (ItemInfobyReqiIdModel_M itemInfoReq in temp_DeptReqi[key])
                {

                    if (temp_Dic_AggregatedItems.ContainsKey(itemInfoReq.ItemCode1))
                    {
                        temp_Dic_AggregatedItems[itemInfoReq.ItemCode1].ItemQty = temp_Dic_AggregatedItems[itemInfoReq.ItemCode1].ItemQty + itemInfoReq.ItemQty;
                    }
                    else
                    {
                        temp_Dic_AggregatedItems.Add(itemInfoReq.ItemCode1, itemInfoReq);
                    }
                }

            }


            // make list of items values
            List<ItemInfobyReqiIdModel_M> temp_Dic_AggregatedItems_List = new List<ItemInfobyReqiIdModel_M>();
            foreach (var key in temp_Dic_AggregatedItems.Keys)
            {
                temp_Dic_AggregatedItems_List.Add(temp_Dic_AggregatedItems[key]);
            }

            return temp_Dic_AggregatedItems_List;
        }

        public List<ItemInfobyReqiIdModel_M> addNewItemListIntoExistingItemList(List<ItemInfobyReqiIdModel_M> existingAggregatedItemsList, List<ItemInfobyReqiIdModel_M> NewAggregatedItemsList)
        {

            Boolean matchFound = false;
            int indexForMatchFound = 0;

            List<ItemInfobyReqiIdModel_M> finalAggregatedItemsList = existingAggregatedItemsList;// new List<ItemInfobyReqiId>();

            foreach (ItemInfobyReqiIdModel_M newItemInfobyReqiId in NewAggregatedItemsList)
            {
                matchFound = false;
                indexForMatchFound = 0;
                foreach (ItemInfobyReqiIdModel_M existingItemInfobyReqiId in existingAggregatedItemsList)
                {

                    if (existingItemInfobyReqiId.ItemCode1 == newItemInfobyReqiId.ItemCode1)
                    {
                        matchFound = true;
                        finalAggregatedItemsList[indexForMatchFound].Item_assigned1 = newItemInfobyReqiId.Item_assigned1 + existingItemInfobyReqiId.Item_assigned1;
                    }
                    indexForMatchFound++;
                }
                if (!matchFound)
                {
                    finalAggregatedItemsList.Add(newItemInfobyReqiId);
                }


            }
            return finalAggregatedItemsList;
        }
        private String getDisbursementId()
        {
            String timeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            return timeStamp;
        }


        //calling from create disburement method
        //mobile
        public string GetRequisitionTypeByReqId(string requisitionId)
        {
            using (InventorySysDBEntities context = new InventorySysDBEntities())
            {
                var q = from x in context.Requisitions where x.Requisition_ID == requisitionId select x.RequisitonType;
                return q.First();
            }

        }

        //mobile
        public List<RequisitionItem> GetreqItemListByReqId(string requisitionId)
        {
            using (InventorySysDBEntities context = new InventorySysDBEntities())
            {
                var q = from x in context.RequisitionItems where x.Requisition_ID == requisitionId select x;
                return q.ToList<RequisitionItem>();
            }
        }

        //mobile
        public string getRepStaffId(string deptId)
        {
            using (InventorySysDBEntities context = new InventorySysDBEntities())
            {
                var q = from x in context.Staffs where x.Dept_ID == deptId && x.Role_ID == "RepStaff" select x.Staff_ID;
                return q.First();
            }
        }

        //mobile
       

        //mobile
        public string getDeptIdByDeptName(string deptName)
        {
            using (InventorySysDBEntities context = new InventorySysDBEntities())
            {
                var q = from x in context.Departments where x.Dept_Name == deptName select x.Dept_ID;
                return q.First();
            }
        }

        //mobile
        public string GetDepName(string ReqId)
        {

            using (InventorySysDBEntities context = new InventorySysDBEntities())
            {
                var q = from x in context.Requisitions where x.Requisition_ID == ReqId select x.SubmissionStaff_ID;
                string staffId = q.First();
                var p = from x in context.Staffs where x.Staff_ID == staffId select x.Dept_ID;
                string deptId = p.First();
                var r = from x in context.Departments where x.Dept_ID == deptId select x.Dept_Name;
                return r.First();
            }
        }
        //methods for departmentwise consolidation
       

        //moblie
        public void saveDisburseList(List<string> reqIdByDeptList, Dictionary<string, int> itemDic, Disbursement NewDisbursement)
        {
            using (InventorySysDBEntities context = new InventorySysDBEntities())
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
}
