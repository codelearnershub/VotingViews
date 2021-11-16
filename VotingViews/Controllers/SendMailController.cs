using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using VotingViews.Domain.IService;
using VotingViews.DTOs;
using VotingViews.Model.Entity;
using VotingViews.Models;

namespace VotingViews.Controllers
{
    [Authorize(Roles ="voter")]
    public class SendMailController : Controller
    {
        private readonly IVoterService _service;
        private readonly IUserService _user;
        private readonly IMailService _mail;

        public SendMailController(IVoterService service, IUserService user, IMailService mail)
        {
            _service = service;
            _user = user;
            _mail = mail;
        }

        public IActionResult SendEMail()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendEmail(MailViewModel model )
        {
            var userEmail = User.FindFirst(ClaimTypes.Name).Value;

            if (ModelState.IsValid)
            {
                _mail.SendEmail(model, userEmail);
                return View();
            }
            else
            {
                return View();
            }
        }
    }
}
