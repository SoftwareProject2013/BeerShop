using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace BeerShop.Models
{
    public class MailSender
    {
       SmtpClient client;
       private string From = "beershop12345@gmail.com";
       private string password = "1234qwerty";
       //Password for Paypal "1q2w3e4rTY"
       public MailSender()
        {
            client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(From, password),
                EnableSsl = true
            };
        }
       public void SendMail(string reciverEmail, string body)
       {
           try
           {
               client.Send(From, reciverEmail, "BeerShop - orderStatusChanged", body);
           }
           catch(Exception e)
           {

           }
       }

    }
}