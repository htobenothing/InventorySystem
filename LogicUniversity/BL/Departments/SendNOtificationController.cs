using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Drawing;
using System.Configuration;
using BL;

namespace BL.Departments
{
    public class SendNotificationController
    {
      
        
        public static  void SendNotificaition(Staff fromStaff, string id)
        {

           
            
            // get the toStaff
            List<Staff> toStaffList = new List<Staff>();
            
            switch(fromStaff.Role_ID){
                case "AuthStaff":
                case "Manager":
                    Requisition req = RequisitionController.getReqByID(id);
                    toStaffList = StaffController.getStaffListById(req.SubmissionStaff_ID);
                    break;
                case "Staff":
                    
                    toStaffList = StaffController.getApprovStaffListByDepId(fromStaff.Dept_ID);
                    break;
                case "RepStaff":
                    toStaffList = StaffController.getAllClerk();
                    break;
                case "Supervisor":
                    toStaffList = StaffController.getSManagerByid(fromStaff);
                    break;
            }   

            
            //define the message
            foreach (Staff toStaff in toStaffList)
            {
                string emailAddressSend = "logicunisystem@gmail.com";
                MailMessage email = createNotificaition(toStaff, id);



                // define the mail server 
                SmtpClient smtp = new SmtpClient();
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 25;

                // define the credential, already define in the app.config, no need show here
                smtp.UseDefaultCredentials = false;
                NetworkCredential NetWorkCre = new NetworkCredential(emailAddressSend, "System!@");
                smtp.Credentials = NetWorkCre;

                // later will change try catch to the code behind
                try
                {
                    smtp.Send(email);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.GetType());
                    Console.ReadLine();
                }
            }
          
            
        }

        protected  static MailMessage createNotificaition(Staff toStaff, string id)
        {
            string emailAddressSend = "logicunisystem@gmail.com";

            string emailAddressRec = toStaff.Email;
            MailMessage email = new MailMessage();
            email.From = new MailAddress(emailAddressSend);
            email.To.Add(new MailAddress(emailAddressRec));
            email.IsBodyHtml = true;
            
            // need to be change later, for the url path
            //string requisitionLink = "path"+req.Requisition_ID;

            switch (toStaff.Role_ID)
            {
                case "Staff":
                    Requisition requi = RequisitionController.getReqByID(id);
                    email.Subject = "Requisition Approve notification";
                    email.Body = string.Format("<div> Dear {0},<br/> Your Requisition has been {1}.The RequisitionID is <P1>{2}</P1><hr/>This is System Email do not reply</div>", toStaff.Staff_Name, requi.Status, requi.Requisition_ID);
                    break;
                case "AuthStaff":
                case "Manager":
                    Requisition req = RequisitionController.getReqByID(id);
                    email.Subject = "Requisition notification";
                    email.Body = string.Format("<div > Dear {0},<br/> You have Requisition to be approved.The RequisitionID is <P1>{1}</P1><hr/>This is System Email do not reply</div>", toStaff.Staff_Name,  req.Requisition_ID);
                    break;
                case "StoreClerk":
                    
                    email.Subject = "Disbursment Received Notification";
                    email.Body = string.Format("One Disbursment has been Recieved.The Disbursment ID is <P1>{0}</P1><hr/>This is System Email do not reply</div>",  id);
                    break;
                case "SManager":
                    email.Subject = "Adjustment  Notification";
                    email.Body = string.Format("<div > Dear {0},<br/>A Adjustment Voucher has been more than 250 dollars.The Voucher ID is <P1>{0}</P1><hr/>This is System Email do not reply</div>", toStaff.Staff_Name, id);
                    break;
            }
               

            return email;
        }
        
    }
}
