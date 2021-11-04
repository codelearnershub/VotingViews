using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingViews.Context;
using VotingViews.Domain.IRepository;
using VotingViews.Model.Entity;

namespace VotingViews.Domain.Repository
{
    public class VoteRepository : IVoteRepository
    {
        private readonly ApplicationContext _context;

        public VoteRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IQueryable<Vote> Query()
        {
            return _context.Votes
                .AsQueryable();
        }

        public Vote FindVoteById(int id)
        {
            var vote = _context.Votes.Find(id);
            return vote;
        }

        public Vote GetVoteByPositionId(int id)
        {
            var vote = _context.Votes.Include(c=>c.Position)
                .FirstOrDefault(v => v.PositionId == id);

            return vote;
        }
    }
}
