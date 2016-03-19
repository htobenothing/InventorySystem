using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BL;
using BL.Departments;
using BL.Store;
using BL.Utilities;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DisburementService" in code, svc and config file together.
public class DisburementService : IDisburementService
{
    public bool checkDisbursment(string DisID, string ReqID)
    {
        return DisburementListManager_M.checkDisbursmentID(DisID, ReqID);
       // return false;

    }
    public WCF_Disburement[] getDisbursmentItemsDetail(string DisID)
    {

        List<WCF_Disburement> wf = new List<WCF_Disburement>();
        foreach (DisbursmentItem_M q in DisburementListManager_M.getDisbursmentItems(DisID))
        {
            WCF_Disburement wcfDis = new WCF_Disburement();
            
            wcfDis.ItemDes = q.ItemDes;
            wcfDis.ItemCode = q.ItemCode;
            wcfDis.NeedQty = q.NeedQty;
            wcfDis.UOM = q.UOM;
            wcfDis.receivedQty = 0;
            wf.Add(wcfDis);
        }

        return wf.ToArray<WCF_Disburement>();

    }


    public bool confrimDisbursementList(WCF_JSRDataConfrimDisb jsonData)
    {
        bool isInsertSuccesful = false;
        List<DisbursementDetail> resultList = new List<DisbursementDetail>();
        foreach (WCF_ConfrimItem result in jsonData.items)
        {

            DisbursementDetail req = new DisbursementDetail();
           
            req.Item_ID = result.Item_ID;
            req.Quantity = result.receivedQty;
            //req.Disbursement_ID = result.Disbursement_ID;
            req.Disbursement_ID = jsonData.Disbursement_ID;
            
            resultList.Add(req);

        }
       
        try
        {
            DisburementListManager_M.ConfirmRecivedItems(jsonData.Disbursement_ID, resultList);
           
            isInsertSuccesful = true;
        }
        catch (Exception ex)
        {
            isInsertSuccesful = false;
        }

        return isInsertSuccesful;
         
    }
   



    //Clerk search disbursement
    //listing
    public WCF_DisItemTotalQty[] DisbursmentListByClerk(string ColID)
    {

        List<WCF_DisItemTotalQty> wf = new List<WCF_DisItemTotalQty>();
        foreach (KeyValuePair<string, List<DisItemTotalQty_M>> temp in DisburementListManagerClerk_M.getDisburselistByCollectionPoint(ColID))
        {
          
          //  Console.Write(temp.Key.ToString());
            foreach (DisItemTotalQty_M d in temp.Value)
            {
                WCF_DisItemTotalQty res = new WCF_DisItemTotalQty();
                res.Dept_Name = temp.Key.ToString();
                res.Disbursement_ID = d.Disbursement_ID.Trim();
                res.TotalQty = d.TotalQty;
                wf.Add(res);
              
            }
        }
        return wf.ToArray<WCF_DisItemTotalQty>();
    }

    public WCF_DisItemTotalQty[] DisbursmentListByClerk(string status,string ColID, string fromDate, string toDate)
    {
        List<WCF_DisItemTotalQty> wf = new List<WCF_DisItemTotalQty>();
        foreach (KeyValuePair<string, List<DisItemTotalQty_M>> temp in DisburementListManagerClerk_M.getDisburselistByCollectionPointAndDate(status, ColID, fromDate, toDate))
        {

            //  Console.Write(temp.Key.ToString());
            foreach (DisItemTotalQty_M d in temp.Value)
            {
                WCF_DisItemTotalQty res = new WCF_DisItemTotalQty();
                res.Dept_Name = temp.Key.ToString();
                res.Disbursement_ID = d.Disbursement_ID.Trim();
                res.TotalQty = d.TotalQty;
                wf.Add(res);

            }
        }
        return wf.ToArray<WCF_DisItemTotalQty>();
    }

    //detail
    public WCF_DisItemTotalQty[] DisbItemDetailByDisIDByClerk(string DisID)
    {
        List<WCF_DisItemTotalQty> wf = new List<WCF_DisItemTotalQty>();
        
        foreach (DisbursementDetailItem_M d in DisburementListManagerClerk_M.getDisbursementDetailById(DisID))
        {
            WCF_DisItemTotalQty wcfDis = new WCF_DisItemTotalQty();

            wcfDis.ItemCode = d.Item_ID;
            wcfDis.UOM = d.UOM;
            wcfDis.EachTotalQty=d.Quantity;
            wcfDis.ItemDes = d.Item_Name;
            wf.Add(wcfDis);
        }

        return wf.ToArray<WCF_DisItemTotalQty>();
    }
    //public bool createDisbursementList(WCF_JSRDataCreateDisb jsonData)
    //{

    //    bool isInsertSuccesful = false;
    //    DisburementListManagerClerk_M res = new DisburementListManagerClerk_M();

    //    List<string> reqId = new List<string>();

    //    Dictionary<string, int> neededqty = new Dictionary<string, int>();
    //    Dictionary<string, int> UnderNeeded_qty = new Dictionary<string, int>();

    //    foreach (WCF_ReqId req in jsonData.reqId)
    //        reqId.Add(req.Requisition_ID);


    //    foreach (WCF_CreateDisbItem reqItem in jsonData.items)
    //        neededqty.Add(reqItem.Item_ID, reqItem.needQty);



    //    foreach (WCF_CreateDisbItem reqItem in jsonData.items)
    //        UnderNeeded_qty.Add(reqItem.Item_ID, reqItem.receivedQty);
           
       
    //    try
    //    {
    //        res.createDisbursementList(reqId, neededqty, UnderNeeded_qty);
           
    //        isInsertSuccesful = true;
    //    }
    //    catch (Exception ex)
    //    {
    //        isInsertSuccesful = false;
    //    }

    //    return isInsertSuccesful;
       
    //}
}
