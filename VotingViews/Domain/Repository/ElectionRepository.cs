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
    public class ElectionRepository : IElectionRepository
    {
        private readonly ApplicationContext _context;

        public ElectionRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Election AddElection(Election election)
        {
            _context.Set<Election>().Add(election);
            _context.SaveChanges();
            return election;
        }

        public void DeleteElection(int id)
        {
            Election election = _context.Elections.FirstOrDefault(e=>e.Id ==id);
            _context.Elections.Remove(election);
            _context.SaveChanges();
        }

        public Election FindByCode(Guid code)
        {
            return _context.Elections
                .Include(c=>c.Positions)
                .SingleOrDefault(c => c.Code == code);
           
        }

        public Election FindbyId(int id)
        {
            return _context.Elections.FirstOrDefault(a=>a.Id == id);
        }

        public bool Exists(int id)
        {
            return _context.Elections.Any(e => e.Id == id);
        }

        public List<Election> GetAll()
        {
            var elections = _context.Elections.ToList();
            return elections;
        }

        public Election UpdateElection(Election election)
        {
            _context.Set<Election>().Update(election);
            _context.SaveChanges();
            return election;
        }
    }
}
