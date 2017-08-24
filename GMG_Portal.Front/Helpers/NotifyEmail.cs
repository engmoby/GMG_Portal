using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Mail;


namespace Front.Helpers
{
    public class NotifyEmail
    {
        public void SendMail(string message,string receipients)
        {


            //Convert to Csv
            List<string> names = receipients.Split(',').ToList<string>();

       



            var msg = new MailMessage {From = new MailAddress("sender@outlook.com")};

            msg.To.Add(new MailAddress("test@tes.com"));
            msg.CC.Add(new MailAddress(""));
            


            SmtpClient client = new SmtpClient();
            client.Host = "smtp.googlemail.com";
            client.Port = 587;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("myemail@gmail.com", "password");

            client.Send(msg);
        }

        public void SendMail(string message)
        {
            throw new NotImplementedException();
        }
    }
}