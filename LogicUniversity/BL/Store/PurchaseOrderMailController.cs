using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace BL.Store
{
    public class PurchaseOrderMailController
    {
        InventorySysDBEntities context = new InventorySysDBEntities();
        public void email_send(string to,string fileUrl)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("wangrz1234@gmail.com");
                mail.To.Add(to);
                mail.Subject = "Logic University Purchase Order";
                mail.Body = "Purchase Order from Logic University is in attachment.";

                System.Net.Mail.Attachment attachment;
                //attachment = new System.Net.Mail.Attachment("C:/Users/Wangrz/Downloads/ADpo/Logic University Purchase Order_001.pdf");
                attachment = new System.Net.Mail.Attachment(fileUrl);
                mail.Attachments.Add(attachment);

                SmtpServer.Port = 25;
                SmtpServer.Credentials = new System.Net.NetworkCredential("wangrz1234@gmail.com", "Wangrz12345678");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch(Exception err)
            {
                Console.WriteLine(err.Message);
            }

        }

        public string GetSupplierEmail(string supID)
        {
            var q = from x in context.Suppliers where x.Supplier_ID == supID select x.Email;
            return q.First();
        }
    }
}
