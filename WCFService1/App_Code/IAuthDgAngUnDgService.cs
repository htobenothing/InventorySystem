using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAuthDgAngUnDgService" in both code and config file together.
[ServiceContract]
public interface IAuthDgAngUnDgService
{
    //for auth
    //list of staff 
    [OperationContract]
    [WebGet(UriTemplate = "/getStaffListbyDeptAndStaffID/{DeptID}/{roleID}", ResponseFormat = WebMessageFormat.Json)]
    WCF_Staff[] getStaffListbyDeptAndStaffID(string DeptID, string roleID);


    //delegate
    [OperationContract]
    [WebInvoke(UriTemplate = "/DelegateAuthStaff", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
    bool DelegateAuthStaff(WCF_JSDataDgAndUnDg jsonData);

    //undelgate
    [OperationContract]
    [WebInvoke(UriTemplate = "/UnDelegateAuthStaff", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
    bool UnDelegateAuthStaff(WCF_JSDataDgAndUnDg jsonData);


}

[DataContract]
public class WCF_JSDataDgAndUnDg
{

    [DataMember]
    public String Staff_ID;

    [DataMember]
    public String Manager_ID;

    [DataMember]
    public String startDate;

    [DataMember]
    public String endDate;

    [DataMember]
    public String status;

}
