using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingViews.Models;

namespace VotingViews.Domain.IService
{
    public interface IMailService
    {
        public string SendEmail(MailViewModel model, string email);
    }
}
