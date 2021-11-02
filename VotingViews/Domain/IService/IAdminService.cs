using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingViews.DTOs;
using VotingViews.Model.Entity;
using VotingViews.Models;

namespace VotingViews.Domain.IService
{
    public interface IAdminService
    {
        public Admin AddAdmin(Admin admin);

        public Admin GetAdmin(int id);

        public Admin GetAdminByUserId(int userId);
    }
}
