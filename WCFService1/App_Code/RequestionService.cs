using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BL;
using BL.Departments;
using BL.Utilities;
using BL.Store;
//using Newtonsoft.Json;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RequestionService" in code, svc and config file together.
public class RequestionService : IRequestionService
{
    public WCF_RequisitionItemList[] RequisitionList(string StaffID)
    {
        List<WCF_RequisitionItemList> wf = new List<WCF_RequisitionItemList>();
        foreach (RequisitionItemList_M req in RequisitionItemManager_M.getAllRequisitionListByDepartment(StaffID))
        {
            wf.Add(new WCF_RequisitionItemList(req.RequisitionID,req.SubmissionStaffName,req.SubmitDate.ToShortDateString(),req.TotalQty));
        }
        

        return wf.ToArray<WCF_RequisitionItemList>();

    }
    public WCF_RequisitionItemListDetail[] RequisitionListDetail(string ReqID)
    {
        List<WCF_RequisitionItemListDetail> wf = new List<WCF_RequisitionItemListDetail>();
        foreach (RequisitionItemDetail_M reqDetail in RequisitionItemManager_M.getRequisitionItem_Detail(ReqID))
        {
            wf.Add(new WCF_RequisitionItemListDetail(reqDetail.ItemName, reqDetail.UOM,  reqDetail.NeedQty));

        }
        return wf.ToArray<WCF_RequisitionItemListDetail>();
    }

    public bool insertRequisition(WCF_JSRequestDataInsertReq jsonData)
    {

       // WCF_JsonRequestData res = JsonConvert.DeserializeObject<WCF_JsonRequestData>("items");

        bool isInsertSuccesful = false;
        List<RequisitionItem> resultList = new List<RequisitionItem>();
        foreach (WCF_RequisitionForm result in jsonData.items)
        {

            RequisitionItem req = new RequisitionItem();
            req.Requisition_ID = result.Requisition_ID;
            req.Item_ID = result.Item_ID;
            req.Required_Qty = result.Required_Qty;
            resultList.Add(req);
        }
        try
        {
            RequisitionItemManager_M.createRequisition(resultList, jsonData.submitStaffID, jsonData.type, jsonData.status);
             isInsertSuccesful = true;
        }
        catch (Exception ex)
        {
            isInsertSuccesful = false;
        }

        return isInsertSuccesful;

    }

    public bool updateRequisition(WCF_JSRequestDataUpdateReq jsonData)
    {
        bool isInsertSuccesful = false;
       string status=jsonData.Status.ToString().Trim();
      
        try
        {
            if (string.Compare(status, "Approved") == 0)
            {
                RequisitionItemManager_M.ApproveRequisition(jsonData.Requisition_ID, jsonData.ApprovedByStaff_ID);
            }
            else if(string.Compare(status, "Rejected") == 0)
            {
                RequisitionItemManager_M.RejectRequisition(jsonData.Requisition_ID, jsonData.ApprovedByStaff_ID,jsonData.Remark);
            }
            isInsertSuccesful = true;
        }
        catch (Exception ex)
        {
            
        }

        return isInsertSuccesful;
    }


    //requsition list for staff
    //public WCF_ReqList[] getReqByDate_Status(string status)
    //{
    //    List<WCF_ReqList> wf = new List<WCF_ReqList>();
    //    foreach (Requisition d in RequisitionItemManager_M.getReqByDate_Status( null,  null, status))
    //    {

    //        wf.Add(new WCF_ReqList(d.Requisition_ID,d.SubmissionStaff_ID, d.Create_Date, d.ApprovedByStaff_ID, d.RequisitonType));

    //    }
    //    return wf.ToArray<WCF_ReqList>();
    //}
    //public WCF_ReqList[] getReqByDate_Status(string status, string fromDate = "", string toDate = "", string staffID)
    //{
    //    List<WCF_ReqList> wf = new List<WCF_ReqList>();
    //    foreach (Requisition d in RequisitionItemManager_M.getReqByDate_Status(fromDate, toDate, status, staffID))
    //    {
    //        wf.Add(new WCF_ReqList(d.Requisition_ID,d.SubmissionStaff_ID, d.Create_Date, d.ApprovedByStaff_ID, d.RequisitonType));

    //    }
    //    return wf.ToArray<WCF_ReqList>();
    //}
    public WCF_ReqList[] getReqByDate_Status(string staffID, string status, string fromDate = null, string toDate = null)
    {
        List<WCF_ReqList> wf = new List<WCF_ReqList>();
        foreach (Requisition d in RequisitionItemManager_M.getReqByDate_Status(staffID,fromDate, toDate, status))
        {
            wf.Add(new WCF_ReqList(d.Requisition_ID, d.SubmissionStaff_ID, d.Create_Date.ToShortDateString(), d.ApprovedByStaff_ID, d.RequisitonType));

        }
        return wf.ToArray<WCF_ReqList>();
    }
   
    //detail

    public WCF_RequisitionViewModel[] getRequisitionVM(string reqID)
    {
        List<WCF_RequisitionViewModel> wf = new List<WCF_RequisitionViewModel>();
        foreach (RequisitionViewModel_M d in RequisitionItemManager_M.getRequisitionVM(reqID))
        {

            wf.Add(new WCF_RequisitionViewModel(d.categoryID, d.ItemID, d.ItemName, d.UOM, d.RequiredQty));

        }
        return wf.ToArray<WCF_RequisitionViewModel>();
        
    }


    //Clerk
    public WCF_Requisition[] RequisitionListByClerk()
    {

        List<WCF_Requisition> wf = new List<WCF_Requisition>();
        foreach (Requisition_M d in RequistionListManagerClerk_M.GetRequistionObjects())
        {
            wf.Add(new WCF_Requisition(d.ReuisitionId1, d.DepartmentName1, d.RequisitionType1));

        }
        return wf.ToArray<WCF_Requisition>();
  
    }

    public WCF_RequisitionItem[] createRetreivalList(List<WCF_ReqId> jsonData)
    {
        RequistionListManagerClerk_M res = new RequistionListManagerClerk_M();

        List<String> reqId = new List<string>();
        foreach (WCF_ReqId reqid in jsonData)
        {
            reqId.Add(reqid.Requisition_ID);
        }

        List<WCF_RequisitionItem> wf = new List<WCF_RequisitionItem>();
        foreach (RetreivedItem_M temp in res.createRetreivalList(reqId))
        {
            WCF_RequisitionItem reqItem = new WCF_RequisitionItem(temp.ItemCode, temp.Description, temp.NeededQty, temp.ProvidingQty);
            wf.Add(reqItem);
        }
        return wf.ToArray<WCF_RequisitionItem>();
    }
}
