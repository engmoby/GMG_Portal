using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using GMG_Portal.Data;

namespace GMG_Portal.API.Helpers
{
    public class NotifyEmail
    {
        public void SendMail(string subj, string message, List<SystemParameters_Notify> receipients)
        {
            try
            {



                var msg = new MailMessage { From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["MailAddress"]) };
                if (receipients != null)
                {

                    msg.Subject = Regex.Replace(subj, @"\t|\n|\r", "");


                    msg.IsBodyHtml = true;

                    string headerTmp =
                        "<table width=100% ><tr><td align ='center' style='background: #000000'><img src = 'http://gmgportal.azurewebsites.net/Content/images/logo1.png' /></td></tr><br/><tr><td>";

                    string footertmp = "</tr></td></table>";

                    msg.Body = headerTmp + message + footertmp;

                    msg.To.Add(new MailAddress(receipients[0].DisplayValue));


                    foreach (var x in receipients)
                    {
                        msg.CC.Add(new MailAddress(x.DisplayValue));
                    }




                    var mailusername = System.Configuration.ConfigurationManager.AppSettings["Mailusername"];
                    var mailPassword = System.Configuration.ConfigurationManager.AppSettings["Mailpassword"];

                    SmtpClient client = new SmtpClient
                    {
                        Host = System.Configuration.ConfigurationManager.AppSettings["MailHost"],
                        Port = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["MailPort"]),
                        UseDefaultCredentials = false,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        EnableSsl = true,
                        Timeout = 10000,


                        Credentials = new NetworkCredential(mailusername, mailPassword)
                    };





                    client.Send(msg);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;

                //  throw;
            }
            return;




        }

    }


}
