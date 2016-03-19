using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Report;
using BL.Departments;

namespace BL.Departments
{
    class Test_ALbert
    {
        public static void Main(string[] args)
        {
            //Email sendemail = new Email();
            //sendemail.SendNotification();
            //Staff newstaff = new Staff();

            //BL.Public.Register re = new Public.Register();
            //BL.Public.Login lg = new Public.Login();
            //bool result =  re.CreateUser("Jame1@gmail.com", "qwer1234","staff");

            //Console.WriteLine(result);


            //bool roleResult = re.CreateRole("staff");
            //Console.WriteLine(roleResult);

            //bool loginReuslt = lg.checkLogin("Jame1@gmail.com", "qwer1234");
            //Console.WriteLine(loginReuslt);
            //Console.ReadLine();
            //BL.Store.CreatePurchaseOrderControl pr = new Store.CreatePurchaseOrderControl();
            //Console.WriteLine( pr.GetDefaultItem());

            // test checkDisbursmentID
            //string ID = "DDS00003";
            //Staff f = UserManager.getStaffById("STF0007");

            //Console.WriteLine(DeptDisbursmentList.checkDisbursmentID(ID, f));

            // test getDisbursmentItem
            foreach (dynamic q in ConfirmReceivedController.getDisbursmentItems("D020150107"))
            {
                Console.WriteLine(q.ItemCode);
            }


            //Console.WriteLine(StaffManager.getStaffListById("STF0002").First().Staff_Name);
            //get Manager by Dep ID 
            Console.WriteLine (StaffController.getApprovStaffListByDepId("CPSC").Count);


            // //Mobile RequisitionItem_M
            //foreach (RequisitionItem_M reqm in (RequisitionControl.getRequisitionItem_M("ReqS0001")))
            //{

            //    Console.WriteLine(string.Format("Reqitem {0}, {1} {2}", reqm.ItemName, reqm.NeedQty, reqm.UOM));
            //}

            SendNotificationController de = new SendNotificationController();
            Staff std = StaffController.getStaffById("STF0002");
            Staff std1 = StaffController.getStaffById("STF0011");
            Requisition req = RequisitionController.getReqByID("REQ20160314113326");
            //DepEmailManager.SendNotificaition(std1, req);

            //Console.ReadLine();

            //List<RequisitionItem> reqList = new List<RequisitionItem>();
            //RequisitionItem req = new RequisitionItem();
            //req.Item_ID = "c001";
            //req.Required_Qty = 40;
            //RequisitionItem req1 = new RequisitionItem();
            //req.Item_ID = "c001";
            //req.Required_Qty = 40;
            //reqList.Add(req);
            //reqList.Add(req1);
            //reqList = reqList.GroupBy(x => x.Item_ID)
            //            .Select(y => new RequisitionItem { Item_ID = y.Key, Required_Qty = y.Sum(t => t.Required_Qty) }).ToList();
            DateTime DT = Convert.ToDateTime("02/01/2015");
            ReportController.getReqByDep(DT);
            Console.WriteLine(ReportController.getReqByDep(DT).Count);
            foreach (MonthDepReportReq mr in ReportController.getReqByDep(DT))
            {
                Console.WriteLine(mr.Status);
            }
            Console.ReadLine();
        }


    }
}
