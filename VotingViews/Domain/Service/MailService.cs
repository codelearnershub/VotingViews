using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using VotingViews.Domain.IService;
using VotingViews.Models;

namespace VotingViews.Domain.Service
{
    public class MailService : IService.IMailService
    {
        private readonly IVoterService _service;
        private readonly IUserService _user;

        public MailService(IVoterService service, IUserService user)
        {
            _service = service;
            _user = user;
        }
         
        public string SendEmail(MailViewModel model, string email)
        {
            string sent = "Email Sent Successfully";
            var user = _service.FindByEmail(email);

            MimeMessage mail = new MimeMessage();
            mail.From.Add(new MailboxAddress("Tester", user.Email));
            mail.To.Add(MailboxAddress.Parse("ghazallutfulmannan@gmail.com"));
            mail.Subject = model.Subject;
            mail.Body = new TextPart("plain")
            {
                Text = model.Body
            };

            

            //Creating of SIMPLE MAIL TRANSFER PROTOCOL(SMTP) Client
            SmtpClient client = new SmtpClient();

            try
            {
                client.Connect("smtp.gmail.com", 465, true);
                 client.Authenticate(user.Email, user.Password);
                client.Send(mail);
                return sent;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
