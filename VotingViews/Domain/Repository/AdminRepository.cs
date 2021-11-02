using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VotingViews.Context;
using VotingViews.Domain.IRepository;
using VotingViews.Model.Entity;

namespace VotingViews.Domain.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationContext _context;

        public AdminRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Admin FindByID(int id)
        {
            return _context.Admins.SingleOrDefault(a => a.Id == id);
        }

        public Admin FindByUserId(int userId)
        {
            return _context.Admins.SingleOrDefault(i => i.UserId == userId);
        }

        public List<Admin> GetAll()
        {
            return _context.Admins.ToList();
        }

        public void DeleteAdmin(int id)
        {
            Admin admin = _context.Admins.Find(id);

            _context.Admins.Remove(admin);
            _context.SaveChanges();
        }

        public Admin AddAdmin(Admin admin)
        {
            _context.Set<Admin>().Add(admin);
            _context.SaveChanges();
            return admin;
        }


        public Admin UpdateAdmin(Admin admin)
        {
            _context.Set<Admin>().Update(admin);
            _context.SaveChanges();
            return admin;
        }

    }
}
