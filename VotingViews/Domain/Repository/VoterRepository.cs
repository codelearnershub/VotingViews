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
    public class VoterRepository : IVoterRepository
    {
        private readonly ApplicationContext _context;

        public VoterRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Voter FindbyId(int id)
        {
            return _context.Voters.SingleOrDefault(a => a.Id == id);
        }

        public Voter FindByEmail(string email)
        {
            return _context.Voters.SingleOrDefault(a => a.Email == email);
        }

        public Voter FindByUserId(int userid)
        {
            return  _context.Voters.SingleOrDefault(i => i.UserId == userid);
        }

        public Voter AddVoter(Voter voter)
        {
            _context.Voters.Add(voter);
            _context.SaveChanges();
            return voter;
        }

        public List<Voter> GetAll()
        {
            return _context.Voters.ToList();
        }

        public void DeleteVoter(int id)
        {
            var voter = _context.Voters.FirstOrDefault(i => i.UserId.Equals(id));
            _context.Voters.Remove(voter);
            _context.SaveChanges();
        }

        public bool Exists(int id)
        {
            return _context.Voters.Any(e => e.Id == id);
        }

        public Voter Update(Voter voter)
        {
            _context.Voters.Update(voter);
            _context.SaveChanges();
            return voter;
        }
    }
}

