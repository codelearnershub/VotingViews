using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingViews.Domain.IRepository;
using VotingViews.Domain.IService;
using VotingViews.DTOs;
using VotingViews.Model.Entity;
using VotingViews.Models;

namespace VotingViews.Domain.Service
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _admin;

        public AdminService(IAdminRepository admin)
        {
            _admin = admin;
        }

        public Admin AddAdmin(Admin admin)
        {
            return _admin.AddAdmin(admin);
        }

        public Admin GetAdmin(int id)
        {
            return _admin.FindByID(id);
        }

        public Admin GetAdminByUserId(int userId) => _admin.FindByUserId(userId);
    }
}
