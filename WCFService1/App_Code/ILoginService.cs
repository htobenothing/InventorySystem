using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILoginService" in both code and config file together.
[ServiceContract]
public interface ILoginService
{
    //Login for mobile
    [OperationContract]
    [WebGet(UriTemplate = "/Login/{email}/{password}", ResponseFormat = WebMessageFormat.Json)]
    WCF_Staff Login(string email, string password);
	
}

[DataContract]
public class WCF_Staff
{
    [DataMember]
    public string Staff_ID;
    [DataMember]
    public string Staff_Name;
    [DataMember]
    public string Role_ID;
    [DataMember]
    public string Password;
    [DataMember]
    public string PhoneNumber;
    [DataMember]
    public string Email;
    [DataMember]
    public string Dept_ID;
    [DataMember]
    public string Status;

    public WCF_Staff()
    {

    }


    public WCF_Staff(string Staff_ID, string Staff_Name, String Role_ID, string Password, string PhoneNumber, string Email, string Dept_ID, string Status)
    {
        this.Staff_ID = Staff_ID;
        this.Staff_Name = Staff_Name;
        this.Role_ID = Role_ID;
        this.Password = Password;
        this.PhoneNumber = PhoneNumber;
        this.Email = Email;
        this.Dept_ID = Dept_ID;
        this.Status = Status;
    }

}