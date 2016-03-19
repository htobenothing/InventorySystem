using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BL;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LoginService" in code, svc and config file together.
public class LoginService : ILoginService
{
    public WCF_Staff Login(string email, string password)
    {
        List<WCF_Staff> wf = new List<WCF_Staff>();

        Staff result = LoginManager_M.SelectByLogIn(email, password);
        if (result != null)
            return new WCF_Staff(result.Staff_ID, result.Staff_Name, result.Role_ID, result.Password, result.PhoneNumber, result.Email, result.Dept_ID, result.Status);
        else return null;

    }

}

  