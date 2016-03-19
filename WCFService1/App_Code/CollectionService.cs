using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BL;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CollectionService" in code, svc and config file together.
public class CollectionService : ICollectionService
{
   public WCF_CollectionPoint[] CollectionList()
    {
       

        List<WCF_CollectionPoint> wf = new List<WCF_CollectionPoint>();
        foreach (CollectionPoint res in CollectionManager_M.getAllCollection())
        {
            wf.Add(new WCF_CollectionPoint(res.Collection_ID, res.Collection_Desc));
        }


        return wf.ToArray<WCF_CollectionPoint>();

    }

   public WCF_CollectionPoint getCollectionListbyDept(string DeptID)
    {

        CollectionPoint collection_point = CollectionManager_M.getCollectionInfoByDepID(DeptID);
       
        return new WCF_CollectionPoint(collection_point.Collection_ID, collection_point.Collection_Desc);
  
    }


   public WCF_Staff[] getStaffListbyDept(string DeptID)
   {
       List<WCF_Staff> wf = new List<WCF_Staff>();
       foreach (Staff result in CollectionManager_M.getStaffListByDepID(DeptID))
       {

           wf.Add(new WCF_Staff(result.Staff_ID, result.Staff_Name, result.Role_ID, result.Password, result.PhoneNumber, result.Email, result.Dept_ID, result.Status));
       }


       return wf.ToArray<WCF_Staff>();
   }

   //public WCF_Staff[] getStaffListbyDeptAndStaffID(string DeptID, string StaffID)
   //{

   //    List<WCF_Staff> wf = new List<WCF_Staff>();
   //    foreach (Staff result in CollectionManager_M.getStaffListByDepIDandroleID(DeptID, StaffID))
   //    {

   //        wf.Add(new WCF_Staff(result.Staff_ID, result.Staff_Name, result.Role_ID, result.Password, result.PhoneNumber, result.Email, result.Dept_ID, result.Status));
   //    }


   //    return wf.ToArray<WCF_Staff>();
   //}
    public bool updateCollectionPoint(WCF_JSDataUpdateCollection jsonData)
    {
        bool status = false;
        try
        {

            CollectionManager_M.UpdateCollectionPoint(jsonData.Dept_ID, jsonData.Staff_ID, jsonData.Collection_ID);
             status = true;
        }
        catch (Exception ex)
        {
            
        }
        return status;
        
    }
    

   //public bool insertRequisition(WCF_JSDataUpdateCollection jsonData)
   //{

   //    bool isInsertSuccesful = false;


   //    using (IDbTransaction tran = conn.BeginTransaction())
   //    {
   //        try
   //        {
   //            CollectionManager_M.ChangeRepToStaff("COMM");
   //            CollectionManager_M.ChangeStaffToRep("STF0015");
   //            CollectionManager_M.UpdateCollectionPoint("COMM", "SSAB");
   //            // your code
   //            tran.Commit();
   //            isInsertSuccesful = true;
   //        }
   //        catch
   //        {
   //            tran.Rollback();
   //            isInsertSuccesful = false;
   //            throw;
   //        }
   //    }

      
   //    return isInsertSuccesful;
   //} 
}
