using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Mail;
using System.Text.RegularExpressions;
using GMG_Portal.API.Models.SystemParameters;
using GMG_Portal.Business.Logic.SystemParameters;
using GMG_Portal.Data;


namespace Front.Helpers
{
    public class NotifyEmail
    {
        public void SendMail(string subj , string message,List<SystemParameters_Notify> receipients)
        {
            try
            {
                var msg = new MailMessage {From = new MailAddress("info@mobarkhotel.com") };
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


                    SmtpClient client = new SmtpClient
                    {
                        Host = "smtp.sendgrid.net",
                        Port = 587,
                        UseDefaultCredentials = false,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        EnableSsl= true,
                        Timeout = 10000,
                     

                        Credentials = new NetworkCredential("apikey", "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")
                    };





                    client.Send(msg);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
              //  throw;
            }




        }
          
        }

       
    }
