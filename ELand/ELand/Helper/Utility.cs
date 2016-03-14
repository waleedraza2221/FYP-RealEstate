using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace ELand.Helper
{
    public class Utility
    {
        public static Task SendAsync(string FromUser, string ToUser, string subject, string body)
        { 
            var result = Task.FromResult(0);
            var toAddress = new System.Net.Mail.MailAddress(ToUser, ToUser);


            try
            {

                System.Net.Mail.MailMessage emessage = new System.Net.Mail.MailMessage();

                string Username = "admin@wr-itc.com";
                string Password = "Waleed123!";
                string port = "25";
                string SmptServer = "mail.wr-itc.com";


                emessage.To.Add(toAddress);
                emessage.Subject = subject;
                emessage.From = new System.Net.Mail.MailAddress(FromUser);
                emessage.Body = body;
                emessage.IsBodyHtml = true;
                System.Net.Mail.SmtpClient sc = new System.Net.Mail.SmtpClient();
                var netCredential = new System.Net.NetworkCredential(Username, Password);
                sc.Host = SmptServer;
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
         
                sc.Credentials = netCredential;
                sc.Port = Convert.ToInt32(port);
             
                sc.Send(emessage);



                result = Task.FromResult(1);
            }
            catch (Exception e)
            {
                var x = e;
                result = Task.FromResult(0);
            }


            return result;


        }


       

    }
}