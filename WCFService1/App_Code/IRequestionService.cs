using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
//using Newtonsoft.Json;



// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRequestionService" in both code and config file together.
[ServiceContract]
public interface IRequestionService
{
    //for manager / auth staff
    //before reject or approve requistion form need to view
    [OperationContract]
    [WebGet(UriTemplate = "/RequisitionList/{StaffID}", ResponseFormat = WebMessageFormat.Json)]
    WCF_RequisitionItemList[] RequisitionList(string StaffID);

    //detail requistion form
    [OperationContract]
    [WebGet(UriTemplate = "/RequisitionListDetail/{ReqID}", ResponseFormat = WebMessageFormat.Json)]
    WCF_RequisitionItemListDetail[] RequisitionListDetail(string ReqID);


    //make requst form insert 
    [OperationContract]
    [WebInvoke(UriTemplate = "/insertRequisition", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
    bool insertRequisition(WCF_JSRequestDataInsertReq jsonData);

    //approve or reject requistion for manager
    [OperationContract]
    [WebInvoke(UriTemplate = "/updateRequisition", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
    bool updateRequisition(WCF_JSRequestDataUpdateReq jsonData);


    //requistion list
    //search history of list
    //need login user id? -> only show for particular user
      [OperationContract]
      [WebGet(UriTemplate = "/getReqByDate_Status/{staffID}/{status}/{fromDate}/{toDate}", ResponseFormat = WebMessageFormat.Json)]
    WCF_ReqList[] getReqByDate_Status(string staffID,string status, string fromDate = null, string toDate = null);

    
    //detail requistion list by req id
      [OperationContract]
      [WebGet(UriTemplate = "/getRequisitionVM/{reqID}", ResponseFormat = WebMessageFormat.Json)]
      WCF_RequisitionViewModel[] getRequisitionVM(string reqID);


    //Clerk
    [OperationContract]
    [WebGet(UriTemplate = "/RequisitionListByClerk/", ResponseFormat = WebMessageFormat.Json)]
    WCF_Requisition[] RequisitionListByClerk();


     [OperationContract]
     [WebInvoke(UriTemplate = "/createRetreivalList", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
    WCF_RequisitionItem[] createRetreivalList(List<WCF_ReqId> jsonData);

}

[DataContract]
public class WCF_RequisitionViewModel
{
    [DataMember]
    public string categoryID;
    [DataMember]
    public string ItemID;
    [DataMember]
    public string ItemName;
    [DataMember]
    public string UOM;
    [DataMember]
    public int RequiredQty;

    public WCF_RequisitionViewModel(string categoryID, string ItemID, string ItemName, string UOM, int RequiredQty)
    {
        this.categoryID = categoryID;
        this.ItemID = ItemID;
        this.ItemName = ItemName;
        this.UOM = UOM;
        this.RequiredQty = RequiredQty;
    }
    
}




[DataContract]
public class WCF_ReqList
{
    [DataMember]
    public string Requisition_ID;
    [DataMember]
    public string SubmissionStaff_ID;
    [DataMember]
    public String Create_Date;
    [DataMember]
    public string ApprovedByStaff_ID;
    [DataMember]
    public string RequisitonType;

    public WCF_ReqList(string Requisition_ID, string SubmissionStaff_ID, String Create_Date, string ApprovedByStaff_ID, string RequisitonType)
    {
        this.Requisition_ID = Requisition_ID;
        this.SubmissionStaff_ID = SubmissionStaff_ID;
        this.Create_Date = Create_Date;
        this.ApprovedByStaff_ID = ApprovedByStaff_ID;
        this.RequisitonType=RequisitonType;
    }
}

[DataContract]
public class WCF_ReqId
{
    [DataMember]
    public String Requisition_ID;
}

[DataContract]
public class WCF_Requisition
{
    [DataMember]
    public String Requisition_ID;

    [DataMember]
    public String Dept_Name;

    [DataMember]
    public String RequisitonType;

    public WCF_Requisition(string Requisition_ID, string Dept_Name, string RequisitonType)
    {
        this.Requisition_ID = Requisition_ID;
        this.Dept_Name = Dept_Name;
        this.RequisitonType = RequisitonType;

    }

}


[DataContract]
public class WCF_JSRequestDataUpdateReq
{
    [DataMember]
    public String Requisition_ID;

    [DataMember]
    public String ApprovedByStaff_ID;

    [DataMember]
    public String Status;

    [DataMember]

    public String Remark;

}

[DataContract]
public class WCF_JSRequestDataInsertReq
{
   [DataMember]
        public String type;

  [DataMember]
        public String status;

    [DataMember]
        public String submitStaffID;

    [DataMember]
       
        public List<WCF_RequisitionForm> items;

}
[DataContract]
public class WCF_RequisitionForm
{
    [DataMember]
    public string Requisition_ID;

    [DataMember]
    public int Required_Qty;
 
    [DataMember]
    public string Item_ID;
   
    

    public WCF_RequisitionForm(string Requisition_ID, string Item_ID, int Required_Qty)
    {
        this.Requisition_ID = Requisition_ID;
        this.Item_ID = Item_ID;
        this.Required_Qty = Required_Qty;

    }

}

[DataContract]
public class WCF_RequisitionItem
{
    [DataMember]
        public string Requisition_ID;
    [DataMember]
        public string UOM;
    [DataMember]
        public string Item_ID;
    [DataMember]
        public string Item_Name;
    [DataMember]
        public int Required_Qty;
    [DataMember]
        public int neededQty;
    [DataMember]
        public int providingQty;
    public WCF_RequisitionItem(string Item_ID, string Item_Name, int neededQty, int providingQty)
    {

        this.Item_ID = Item_ID;
        this.Item_Name = Item_Name;
        this.neededQty = neededQty;
        this.providingQty = providingQty;
     
    }
    public WCF_RequisitionItem(string Requisition_ID, string UOM, string Item_ID, string Item_Name,  int Required_Qty)
    {
        this.Requisition_ID = Requisition_ID;
        this.UOM = UOM;
        this.Item_ID = Item_ID;
        this.Item_Name = Item_Name;
        this.Required_Qty = Required_Qty;

    }

}

[DataContract]
public class WCF_RequisitionItemList
{
    
    [DataMember]
    public string RequisitionID;
    [DataMember]
    public string SubmissionStaffName;
    [DataMember]
    public String SubmitDate;
    [DataMember]
    public int TotalQty;

    public WCF_RequisitionItemList(string RequisitionID, string SubmissionStaffName, String SubmitDate, int TotalQty)
    {
        this.RequisitionID = RequisitionID;
        this.SubmissionStaffName = SubmissionStaffName;
        this.SubmitDate = SubmitDate;
        this.TotalQty = TotalQty; 

    }

}

[DataContract]
public class WCF_RequisitionItemListDetail
{
    [DataMember]
    public string ItemName;
    [DataMember]
    public string UOM;
    [DataMember]
    public int NeedQty;



    public WCF_RequisitionItemListDetail(string ItemName, string UOM,  int NeedQty)
    {
        this.ItemName = ItemName;
        this.UOM = UOM;
        this.NeedQty = NeedQty;
       

    }

}

