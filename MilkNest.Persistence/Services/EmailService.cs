using Castle.Core.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using MimeKit;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MilkNest.Application.Interfaces;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1.Pkcs;
using Azure.Core;

namespace MilkNest.Persistence.Services
{
    
        public class EmailService : IEmailService
        {
        public Task SendEmailAsync(string email, string subject, string message)
            {
                SmtpClient client = new SmtpClient
                {
                    Port = 587,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("milknestt@gmail.com", "rkernyuikxipswnr")
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("milknestt@gmail.com"),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(email);

                return client.SendMailAsync(mailMessage);
            }
        }
    
}

