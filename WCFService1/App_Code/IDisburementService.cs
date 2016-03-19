using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDisburementService" in both code and config file together.
[ServiceContract]
public interface IDisburementService
{
    //confrim receive rep staff
    [OperationContract]
    [WebGet(UriTemplate = "/checkDisbursment/{DisID}/{ReqID}", ResponseFormat = WebMessageFormat.Json)]
    bool checkDisbursment(string DisID, string ReqID);

    [OperationContract]
    [WebGet(UriTemplate = "/getDisbursmentItemsDetail/{DisID}", ResponseFormat = WebMessageFormat.Json)]
    WCF_Disburement[] getDisbursmentItemsDetail(string DisID);

    [OperationContract]
    [WebInvoke(UriTemplate = "/confrimDisbursementList", Method = "POST",
       RequestFormat = WebMessageFormat.Json,
       ResponseFormat = WebMessageFormat.Json)]
    bool confrimDisbursementList(WCF_JSRDataConfrimDisb jsonData);

    //Clerk
    //disbursement list search history
    //need status? for search
    [OperationContract]
    [WebGet(UriTemplate = "/DisbursmentListByClerk/{status}/{ColID}/{fromDate}/{toDate}", ResponseFormat = WebMessageFormat.Json)]
    WCF_DisItemTotalQty[] DisbursmentListByClerk(string status, string ColID, string fromDate, string toDate);

   
    //disbursement list deail
    [OperationContract]
    [WebGet(UriTemplate = "/DisbItemDetailByDisIDByClerk/{DisID}", ResponseFormat = WebMessageFormat.Json)]
    WCF_DisItemTotalQty[] DisbItemDetailByDisIDByClerk(string DisID);

    //disbursement creating 
    //after changing reterival list
    //becoz error not actual data havent saved in db
    //give up function
   // [OperationContract]
   // [WebInvoke(UriTemplate = "/createDisbursementList", Method = "POST",
   //    RequestFormat = WebMessageFormat.Json,
   //    ResponseFormat = WebMessageFormat.Json)]
   //bool createDisbursementList(WCF_JSRDataCreateDisb jsonData);


}
[DataContract]
public class WCF_JSRDataConfrimDisb
{
    [DataMember]
    public string Disbursement_ID;

    [DataMember]
    public List<WCF_ConfrimItem> items;
}



[DataContract]
public class WCF_ConfrimItem
{

    [DataMember]
    public string Disbursement_ID;

    [DataMember]
    public string Item_ID;

    [DataMember]
    public string Item_Name;

    [DataMember]
    public int receivedQty;


}
//[DataContract]
//public class WCF_JSRDataCreateDisb
//{
//    [DataMember]
//    public List<WCF_ReqId> reqId;

//    [DataMember]
//    public List<WCF_CreateDisbItem> items;
//}

//[DataContract]
//public class WCF_CreateDisbItem
//{
//    [DataMember]
//    public string Item_Name;

//    [DataMember]
//    public string Item_ID;

//    [DataMember]
//    public int needQty;

//     [DataMember]
//    public int receivedQty;
//}



[DataContract]
public class WCF_DisItemTotalQty
{
    [DataMember]
    public string Dept_Name;

    [DataMember]
    public string Disbursement_ID;

    [DataMember]
    public int TotalQty;

    [DataMember]
    public string ItemCode;

    [DataMember]
    public string ItemDes;

    [DataMember]
    public string UOM;

    [DataMember]
    public int EachTotalQty;

    public WCF_DisItemTotalQty()
    {
        

    }
    public WCF_DisItemTotalQty(string Dept_Name, string Disbursement_ID, int TotalQty)
    {
        this.Dept_Name = Dept_Name;
        this.Disbursement_ID = Disbursement_ID;
        this.TotalQty = TotalQty;
      
    }
}

[DataContract]
public class WCF_Disburement
{
      [DataMember]
      public string ItemCode;
      [DataMember]
      public string ItemDes;
      [DataMember]
      public int NeedQty ;
      [DataMember]
      public int receivedQty;
      [DataMember]
      public string UOM;
      [DataMember]
      public string checkStatus;

      public WCF_Disburement()
      {
          this.ItemCode = "";
          this.ItemDes = "";
          this.NeedQty = 0;
          this.UOM = "";
      }
      public WCF_Disburement(string ItemCode, string ItemDes, int NeedQty, string UOM, int receivedQty)
    {
        this.ItemCode = ItemCode;
        this.ItemDes = ItemDes;
        this.NeedQty = NeedQty;
        this.UOM = UOM;
        this.receivedQty = receivedQty;

    }

}
