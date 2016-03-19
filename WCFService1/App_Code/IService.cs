using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BL;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
public interface IService
{
    //item list for make request form
    [OperationContract]
    [WebGet(UriTemplate = "/itemList", ResponseFormat = WebMessageFormat.Json)]
    WCF_Item[] itemList();


    //[OperationContract]
    //[WebGet(UriTemplate = "/List", ResponseFormat = WebMessageFormat.Json)]
    //string[] List();
}

//[DataContract]
//public class WCF_Item
//{
    
//    [DataMember]
//        public string Item_ID;
//    [DataMember]
//        public string Category_ID ;
//    [DataMember]
//        public string Item_Name ;
//    [DataMember]
//        public int Reorder_Level ;
//    [DataMember]
//        public int Reorder_Qty ;
//    [DataMember]
//        public string UOM ;
//    [DataMember]
//        public string Bin_ID;
//    [DataMember]
//        public string FirstSupplier_ID ;
//    [DataMember]
//        public string SecondSupplier_ID ;
//    [DataMember]
//        public string ThirdSupplier_ID ;
//    [DataMember]
//        public int Inventory ;
//    [DataMember]
//        public string Status ;



//    public WCF_Item(string Item_ID, string Category_ID, string Item_Name, int Reorder_Level, int Reorder_Qty, string UOM, string Bin_ID, string FirstSupplier_ID, string SecondSupplier_ID, string ThirdSupplier_ID, int Inventory, string Status)
//    {
//        this.Item_ID = Item_ID;
//        this.Category_ID = Category_ID;
//        this.Item_Name = Item_Name;
//        this.Reorder_Level = Reorder_Level;
//        this.Reorder_Qty = Reorder_Qty;
//        this.UOM = UOM;
//        this.FirstSupplier_ID = FirstSupplier_ID;
//        this.SecondSupplier_ID = SecondSupplier_ID;
//        this.ThirdSupplier_ID = ThirdSupplier_ID;
//        this.Inventory = Inventory;
//        this.Status = Status;
        
//    }

//}

