using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICollectionService" in both code and config file together.
[ServiceContract]
public interface ICollectionService
{
    //update collection
    //get collection point for drop down
    [OperationContract]
    [WebGet(UriTemplate = "/CollectionList/", ResponseFormat = WebMessageFormat.Json)]
    WCF_CollectionPoint[] CollectionList();

    //get collection name for display 
    [OperationContract]
    [WebGet(UriTemplate = "/getCollectionListbyDept/{DeptID}", ResponseFormat = WebMessageFormat.Json)]
    WCF_CollectionPoint getCollectionListbyDept(string DeptID);

    //get staff list for drop down
    [OperationContract]
    [WebGet(UriTemplate = "/getStaffListbyDept/{DeptID}", ResponseFormat = WebMessageFormat.Json)]
    WCF_Staff[] getStaffListbyDept(string DeptID);

    //[OperationContract]
    //[WebGet(UriTemplate = "/getStaffListbyDeptAndStaffID/{DeptID}/{StaffID}", ResponseFormat = WebMessageFormat.Json)]
    //WCF_Staff[] getStaffListbyDeptAndStaffID(string DeptID, string StaffID);

    //update collection point
    [OperationContract]
    [WebInvoke(UriTemplate = "/updateCollectionPoint", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
    bool updateCollectionPoint(WCF_JSDataUpdateCollection jsonData);
}


[DataContract]
public class WCF_JSDataUpdateCollection
{
    [DataMember]
    public String Dept_ID;

    [DataMember]
    public String Staff_ID;

    [DataMember]
    public String Collection_ID;

 

}


[DataContract]
public class WCF_CollectionPoint
{
   [DataMember]
        public String Collection_ID;

  [DataMember]
        public String Collection_Desc;

  public WCF_CollectionPoint(string Collection_ID, string Collection_Desc)
    {
        this.Collection_ID = Collection_ID;
        this.Collection_Desc = Collection_Desc;
       

    }

}


//[DataContract]
//public class WCF_Staff
//{
//    [DataMember]
//        public string Staff_ID;
//    [DataMember]
//        public string Staff_Name;
//    [DataMember]
//        public string Role_ID;
//    [DataMember]
//        public string Password;
//    [DataMember]
//        public string PhoneNumber;
//    [DataMember]
//        public string Email;
//    [DataMember]
//        public string Dept_ID;
//    [DataMember]
//        public string Status;


//    public WCF_Staff(string Staff_ID, string Staff_Name, string Role_ID, string Password, string PhoneNumber, string Email, string Dept_ID, string Status)
//    {
//        this.Staff_ID = Staff_ID;
//        this.Staff_Name = Staff_Name;
//        this.Role_ID = Role_ID;
//        this.Password = Password;
//        this.PhoneNumber = PhoneNumber;
//        this.Email = Email;
//        this.Dept_ID = Dept_ID;
//        this.Status = Status;

//    }

//}

 