using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BL;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AuthDgAngUnDgService" in code, svc and config file together.
public class AuthDgAngUnDgService : IAuthDgAngUnDgService
{
    //for List

    public WCF_Staff[] getStaffListbyDeptAndStaffID(string DeptID, string roleID)
    {

        List<WCF_Staff> wf = new List<WCF_Staff>();
        foreach (Staff result in DgAndUnDgManager_M.getStaffListByDepIDandroleID(DeptID, roleID))
        {

            wf.Add(new WCF_Staff(result.Staff_ID, result.Staff_Name, result.Role_ID, result.Password, result.PhoneNumber, result.Email, result.Dept_ID, result.Status));
        }


        return wf.ToArray<WCF_Staff>();
    }

    //for create and update delegate auth staff
    public bool DelegateAuthStaff(WCF_JSDataDgAndUnDg jsonData)
    {
        bool isInsertSuccesful = false;
        string status = jsonData.status.ToString().Trim();

        try
        {

            //createDelegateHistory
            if (string.Compare(status, "Delegated") == 0)
            {
                DgAndUnDgManager_M.createDelegateHistory(jsonData.Staff_ID, jsonData.Manager_ID, Convert.ToDateTime(jsonData.startDate), Convert.ToDateTime(jsonData.endDate));//correct

                // DgAndUnDgManager_M.createDelegateHistory("STF0032", "STF0001", Convert.ToDateTime("2016-8-3"), Convert.ToDateTime("2016-8-3"));//correct

            }
            isInsertSuccesful = true;
        }
        catch (Exception ex)
        {

        }

        return isInsertSuccesful;
    }

    public bool UnDelegateAuthStaff(WCF_JSDataDgAndUnDg jsonData)
    {
        bool isInsertSuccesful = false;
        string status = jsonData.status.ToString().Trim();

        try
        {
            if (string.Compare(status, "Undelegated") == 0)
            {
                DgAndUnDgManager_M.updateDelegateHistory(jsonData.Staff_ID);//error

                //    DgAndUnDgManager_M.updateDelegateHistory("STF0001");//error
            }

            isInsertSuccesful = true;
        }
        catch (Exception ex)
        {

        }

        return isInsertSuccesful;
    }
}
