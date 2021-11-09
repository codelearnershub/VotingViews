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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public User CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User FindByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            return user;
        }

        public User FindById(int id)
        {
            var user = _context.Users.Find(id);
            return user;
        }

        public bool Exists(int id)
        {
            return _context.Users.Any(u => u.Id == id);
           
        }

        public bool ExistsAsync(Expression<Func<User, bool>> model)
        {
            return _context.Users.Any(model);
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public void DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public User UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return user;
        }
    }
}
