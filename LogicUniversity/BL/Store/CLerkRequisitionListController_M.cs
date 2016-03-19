using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Utilities;

namespace BL.Store
{
    public class CLerkRequisitionListController_M
    {
        List<RequisitionItem> reqItemListByReqId = new List<RequisitionItem>();
        Dictionary<string, List<RequisitionItem>> RequisitionItemDictionaryByReqiId = new Dictionary<string, List<RequisitionItem>>();
        Dictionary<string, int> itemsDictionary = new Dictionary<string, int>();
        List<RequisitionItem> AllreqItemList = new List<RequisitionItem>();

        //Requistion list for clerk
        public static List<RequisitionModel_M> GetRequistionObjects()
        {

            using (InventorySysDBEntities context = new InventorySysDBEntities())
            {
                var q = from x in context.Requisitions where x.Status == "Approved" orderby x.Update_Date select x;

                List<Requisition> requiList = q.ToList<Requisition>();

                List<RequisitionModel_M> requisitionDetailList = new List<RequisitionModel_M>();

                foreach (Requisition requi in requiList)
                {

                    var p = from x in context.Requisitions where x.Requisition_ID == requi.Requisition_ID select x.SubmissionStaff_ID;

                    string staffId = p.First();

                    var r = from x in context.Staffs where x.Staff_ID == staffId select x.Department.Dept_Name;

                    string deptName = r.First();

                    RequisitionModel_M singleRequsition = new RequisitionModel_M();

                    singleRequsition.ReuisitionId1 = requi.Requisition_ID;

                    singleRequsition.DepartmentName1 = deptName;

                    singleRequsition.RequisitionType1 = requi.RequisitonType;

                    requisitionDetailList.Add(singleRequsition);

                }

                return requisitionDetailList;
            }

        }

        
          
          //mobile
        public void UpdateReqIdStatus(List<string> reqList)
        {

            using (InventorySysDBEntities context = new InventorySysDBEntities())
            {

                foreach (string reqId in reqList)
                {
                    var q = from x in context.Requisitions where x.Requisition_ID == reqId select x;
                    Requisition r = q.First();
                    r.Status = "Dealing";

                }
                context.SaveChanges();
            }
        }

        //mobile
        public List<RequisitionItem> GetSelectedreqItemList(List<string> reqIdList)
        {
            using (InventorySysDBEntities context = new InventorySysDBEntities())
            {

                var q = from x in context.RequisitionItems where reqIdList.Contains(x.Requisition_ID) select x;
                return q.ToList<RequisitionItem>();
            }
        }

        //mobile
        public Item GetItemObjectById(string itemId)
        {
            using (InventorySysDBEntities context = new InventorySysDBEntities())
            {
                Item itemOb = context.Items.Single(u => u.Item_ID == itemId);
                return itemOb;
            }
        }

        //create retreival list
        public  List<RetreivedItemModel_M> createRetreivalList(List<string> selectedReqIdsList){
            ClerkDisburementController_M disbListClerk = new ClerkDisburementController_M();
            List<RetreivedItemModel_M> retreivedItemList =new List<RetreivedItemModel_M>();
            foreach(string SelectedRequisitionId in selectedReqIdsList)
            {
                reqItemListByReqId = disbListClerk.GetreqItemListByReqId(SelectedRequisitionId);
                RequisitionItemDictionaryByReqiId.Add(SelectedRequisitionId, reqItemListByReqId);
            }

            //UpdateReqIdStatus(selectedReqIdsList);

            AllreqItemList = GetSelectedreqItemList(selectedReqIdsList);

            foreach (RequisitionItem reqItem in AllreqItemList)
            {
                string itemId = reqItem.Item_ID;
                int qty = reqItem.Required_Qty;
                if (!itemsDictionary.ContainsKey(itemId))
                {
                    itemsDictionary.Add(itemId, qty);
                }
                else
                {
                    itemsDictionary[itemId] = itemsDictionary[itemId] + qty;
                }

            }


            foreach (var ReqItem in itemsDictionary)
            {
                string itemcode = ReqItem.Key;
                int qty = ReqItem.Value;
                Item item = GetItemObjectById(itemcode); 	
                RetreivedItemModel_M retreivedItem = new RetreivedItemModel_M();
                retreivedItem.ItemCode = ReqItem.Key;
                retreivedItem.Description = item.Item_Name;
                retreivedItem.NeededQty =  ReqItem.Value;
                retreivedItem.ProvidingQty =  ReqItem.Value;
                retreivedItemList .Add(retreivedItem);
            }

             return retreivedItemList ;
        }

        ////createDisbursementList
        //public List<DeptLevelReq> createDisbursementList(List<string> selectedReqidList, Dictionary<string, int> neededqty, Dictionary<string, int> UnderNeeded_qty)
        //{

        //    Dictionary<string, DeptLevelReq> mstrDic_Dept = new Dictionary<string, DeptLevelReq>();
        //  //  Dictionary<string, int> underNeededItem_Dic = new Dictionary<string, int>();
        //    Dictionary<string, int> underNeededItem_Dic = UnderNeeded_qty;
        //  //  Dictionary<string, int> NeededItem_Dic = new Dictionary<string, int>();
        //    Dictionary<string, int> NeededItem_Dic = neededqty;
        //    Dictionary<string, List<ItemInfobyReqiId>> deptReqDictionary = new Dictionary<string, List<ItemInfobyReqiId>>();//pojo2 dictionary
        //    List<DeptLevelReq> deptLevelReqList = new List<DeptLevelReq>(); //Pojo 2 list
        //    Dictionary<string, Dictionary<string, int>> depConsolidatedDic = new Dictionary<string, Dictionary<string, int>>();
        //    Dictionary<string, List<ItemInfobyReqiId>> depToItem = new Dictionary<string, List<ItemInfobyReqiId>>();
        //    Dictionary<string, List<ItemInfobyReqiId>> specialReqDic = new Dictionary<string, List<ItemInfobyReqiId>>();
        //    Dictionary<string, List<ItemInfobyReqiId>> urgentReqDic = new Dictionary<string, List<ItemInfobyReqiId>>();
        //    Dictionary<string, List<ItemInfobyReqiId>> normalReqDic = new Dictionary<string, List<ItemInfobyReqiId>>();

        //    //Populating specialReqDic ,urgentReqDic ,normalReqDic 

        //    foreach (string reqiId in selectedReqidList)                           //1
        //    {
        //        List<RequisitionItem> reqiItemByReqId = GetreqItemListByReqId(reqiId);
        //        List<ItemInfobyReqiId> ItemInfobyReqiIdList = new List<ItemInfobyReqiId>();
        //        foreach (RequisitionItem reqItem in reqiItemByReqId)
        //        {
        //            ItemInfobyReqiId iteminfoByReq = new ItemInfobyReqiId();
        //            iteminfoByReq.RequisitionId1 = reqiId;
        //            iteminfoByReq.ItemCode1 = reqItem.Item_ID;
        //            iteminfoByReq.ItemQty = reqItem.Required_Qty;
        //            ItemInfobyReqiIdList.Add(iteminfoByReq);
        //        }
        //        if (GetRequisitionTypeByReqId(reqiId) == "Special")
        //        {
        //            specialReqDic[reqiId] = ItemInfobyReqiIdList;
        //        }
        //        if (GetRequisitionTypeByReqId(reqiId) == "Urgent")
        //        {
        //            urgentReqDic[reqiId] = ItemInfobyReqiIdList;
        //        }

        //        if (GetRequisitionTypeByReqId(reqiId) == "Normal")
        //        {
        //            normalReqDic.Add(reqiId, ItemInfobyReqiIdList);
        //        }
        //        deptReqDictionary.Add(reqiId, ItemInfobyReqiIdList);                                    //duplicacy

        //        Dictionary<string, List<ItemInfobyReqiId>> deptReqDictionary_tmp2PopulateMstrListPojo2 = new Dictionary<string, List<ItemInfobyReqiId>>();//pojo2 dictionary
        //        deptReqDictionary_tmp2PopulateMstrListPojo2.Add(reqiId, ItemInfobyReqiIdList);
        //        string dept_Name = GetDepName(reqiId);
        //        DeptLevelReq deptLevelReq = new DeptLevelReq();
        //        deptLevelReq.DeptName = dept_Name;
        //        deptLevelReq.DeptReqi = deptReqDictionary_tmp2PopulateMstrListPojo2;                          // populate Pojo2 ???
        //        deptLevelReqList.Add(deptLevelReq);                                 //pojo 2 lis                

        //    }


        //    for (int index = 0; index < specialReqDic.Count; index++)
        //    {
        //        int y = specialReqDic.Count;
        //        var item = specialReqDic.ElementAt(index);
        //        string reqId = item.Key;
        //        List<ItemInfobyReqiId> reqIdItemList = item.Value;
        //        string deptName = GetDepName(reqId);

        //        foreach (DeptLevelReq depLevelReq in deptLevelReqList)                //deplevel final map
        //        {
        //            //deptReqDictionary=Session["deptReqDictionary"];
        //            if (depLevelReq.DeptName == deptName)
        //            {
        //                List<ItemInfobyReqiId> reqIdItemInfo = deptReqDictionary[reqId];
        //                foreach (ItemInfobyReqiId itemInfoByreq in reqIdItemInfo)
        //                {

        //                    string itemCode = itemInfoByreq.ItemCode1;
        //                    int assign = itemInfoByreq.ItemQty;

        //                    //start  allocating quantity
        //                    if (underNeededItem_Dic.ContainsKey(itemCode))
        //                    {
        //                        if (itemInfoByreq.ItemQty < underNeededItem_Dic[itemCode])
        //                        {
        //                            itemInfoByreq.Item_assigned1 = itemInfoByreq.ItemQty;
        //                            underNeededItem_Dic[itemCode] = underNeededItem_Dic[itemCode] - itemInfoByreq.Item_assigned1;
        //                        }
        //                        else
        //                        {
        //                            itemInfoByreq.Item_assigned1 = underNeededItem_Dic[itemCode];
        //                        }
        //                    }
        //                    else
        //                    {
        //                        itemInfoByreq.Item_assigned1 = itemInfoByreq.ItemQty;
        //                        NeededItem_Dic[itemCode] = NeededItem_Dic[itemCode] - itemInfoByreq.Item_assigned1;
        //                    }

        //                    ///finish allocation
        //                }


        //            }

        //        }
        //    }

        //    for (int index = 0; index < urgentReqDic.Count; index++)
        //    {
        //        int y = urgentReqDic.Count;
        //        var item = urgentReqDic.ElementAt(index);
        //        string reqId = item.Key;
        //        List<ItemInfobyReqiId> reqIdItemList = item.Value;
        //        string deptName = GetDepName(reqId);

        //        foreach (DeptLevelReq depLevelReq in deptLevelReqList)
        //        {
        //            if (depLevelReq.DeptName == deptName)
        //            {
        //                List<ItemInfobyReqiId> reqIdItemInfo = deptReqDictionary[reqId];
        //                foreach (ItemInfobyReqiId itemInfoByreq in reqIdItemInfo)
        //                {

        //                    string itemCode = itemInfoByreq.ItemCode1;
        //                    int assign = itemInfoByreq.ItemQty;

        //                    //start  allocating quantity
        //                    if (underNeededItem_Dic.ContainsKey(itemCode))
        //                    {
        //                        if (itemInfoByreq.ItemQty < underNeededItem_Dic[itemCode])
        //                        {
        //                            itemInfoByreq.Item_assigned1 = itemInfoByreq.ItemQty;
        //                            underNeededItem_Dic[itemCode] = underNeededItem_Dic[itemCode] - itemInfoByreq.Item_assigned1;
        //                        }
        //                        else
        //                        {
        //                            itemInfoByreq.Item_assigned1 = underNeededItem_Dic[itemCode];
        //                        }
        //                    }
        //                    else
        //                    {
        //                        itemInfoByreq.Item_assigned1 = itemInfoByreq.ItemQty;
        //                        NeededItem_Dic[itemCode] = NeededItem_Dic[itemCode] - itemInfoByreq.Item_assigned1;
        //                    }

        //                    ///finish allocation
        //                }


        //            }

        //        }
        //    }



        //    // selection from screen --- mstr map : req-item          
        //    for (int index = 0; index < normalReqDic.Count; index++)
        //    {
        //        int y = normalReqDic.Count;
        //        var item = normalReqDic.ElementAt(index);
        //        string reqId = item.Key;
        //        List<ItemInfobyReqiId> reqIdItemList = item.Value;
        //        string deptName = GetDepName(reqId);

        //        foreach (DeptLevelReq depLevelReq in deptLevelReqList)
        //        {
        //            if (depLevelReq.DeptName == deptName)
        //            {
        //                List<ItemInfobyReqiId> reqIdItemInfo = deptReqDictionary[reqId];
        //                foreach (ItemInfobyReqiId itemInfoByreq in reqIdItemInfo)
        //                {

        //                    string itemCode = itemInfoByreq.ItemCode1;
        //                    int assign = itemInfoByreq.ItemQty;

        //                    //start  allocating quantity
        //                    if (underNeededItem_Dic.ContainsKey(itemCode))
        //                    {
        //                        if (itemInfoByreq.ItemQty < underNeededItem_Dic[itemCode])
        //                        {
        //                            itemInfoByreq.Item_assigned1 = itemInfoByreq.ItemQty;
        //                            underNeededItem_Dic[itemCode] = underNeededItem_Dic[itemCode] - itemInfoByreq.Item_assigned1;
        //                        }
        //                        else
        //                        {
        //                            itemInfoByreq.Item_assigned1 = underNeededItem_Dic[itemCode];
        //                        }
        //                    }
        //                    else
        //                    {                                                /////////new
        //                        itemInfoByreq.Item_assigned1 = itemInfoByreq.ItemQty;
        //                        NeededItem_Dic[itemCode] = NeededItem_Dic[itemCode] - itemInfoByreq.Item_assigned1;
        //                    }

        //                    ///finish allocation
        //                }

        //            }

        //        }
        //    }



        //    ////for showing on screen populating dictionary depToItem[deptnmae(key),listOfItem(value)]
        //   // List<Dictionary<string, List<ItemInfobyReqiId>>> deptLevelDisbursementList = new List<Dictionary<string, List<ItemInfobyReqiId>>>();
        //    List<DeptLevelReq> deptLevelDisbursementList = new List<DeptLevelReq>();
        //    foreach (DeptLevelReq deptLevelReq in deptLevelReqList)
        //    {
        //        if (depToItem.ContainsKey(deptLevelReq.DeptName))
        //        {
        //            List<ItemInfobyReqiId> finalAggregatedItemsLis = addNewItemListIntoExistingItemList(depToItem[deptLevelReq.DeptName], getAggregatedItems(deptLevelReq));
        //            depToItem.Remove(deptLevelReq.DeptName);
        //            depToItem.Add(deptLevelReq.DeptName, finalAggregatedItemsLis);
        //        }
        //        else
        //        {
        //            depToItem.Add(deptLevelReq.DeptName, getAggregatedItems(deptLevelReq));
        //        } 
        //        DeptLevelReq deptLevel=new DeptLevelReq();
        //        deptLevel.DeptName = deptLevelReq.DeptName;
        //        deptLevel.DeptReqi = depToItem;
        //        deptLevelDisbursementList.Add(deptLevel);
        //    }
          
        //    for (int index = 0; index < depToItem.Count; index++)
        //    {
        //        Dictionary<string, int> itemCodeDic = new Dictionary<string, int>();
        //        var item = depToItem.ElementAt(index);
        //        string deptName = item.Key;
        //        List<ItemInfobyReqiId> consolidateItemList = item.Value;
        //        string disbursementId = "D0" + getDisbursementId();
        //        List<string> reqIdByDept = new List<string>();
        //        foreach (ItemInfobyReqiId itemList in consolidateItemList)
        //           {

        //               string itemcode = itemList.ItemCode1;
        //               int quantity = itemList.Item_assigned1;

        //               itemCodeDic.Add(itemcode, quantity);
                
        //        }
        //        //selectedReqidList = (List<string>)Session["selectedReqIdsList"];
        //        foreach (string reqId in selectedReqidList)
        //        {

        //            string depname = GetDepName(reqId);
        //            if (depname == deptName)
        //            {
        //                reqIdByDept.Add(reqId);
        //            }
        //        }
        //        Disbursement disbursement = new Disbursement();
        //        disbursement.Disbursement_ID = disbursementId;
        //        disbursement.Create_Date = DateTime.Now;
        //        disbursement.Receive_Date = null;
        //        disbursement.Dept_ID = getDeptIdByDeptName(deptName);
        //        disbursement.RepStaff_ID = getRepStaffId(disbursement.Dept_ID);
        //        disbursement.Status = "Submitted";
        //        CreateDisburseList(reqIdByDept, itemCodeDic, disbursement);
        //        //DeptLevelReq deptlevelReq = new DeptLevelReq();
        //        //dept
                
        //    }
        //    return deptLevelDisbursementList;

        //}

       

    }
}
