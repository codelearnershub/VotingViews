using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VotingViews.Model.Entity;

namespace VotingViews.Domain.IRepository
{
    public interface IAdminRepository
    {
        public Admin FindByID(int id);

        public Admin FindByUserId(int userId);

        public Admin AddAdmin(Admin admin);

        public List<Admin> GetAll();

        public void DeleteAdmin(int id);

        public Admin UpdateAdmin(Admin admin);
    }
}
