using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VotingViews.Context;
using VotingViews.Domain.IRepository;
using VotingViews.DTOs;
using VotingViews.Model.Entity;

namespace VotingViews.Domain.Repository
{
    public class VoteRepository : IVoteRepository
    {
        private readonly ApplicationContext _context;
        private readonly IContestantRepository _contestantRepo;

        public VoteRepository(ApplicationContext context, IContestantRepository contestantRepo)
        {
            _context = context;
            _contestantRepo = contestantRepo;
        }

        public List<Vote> GetVoteByContestantId(int id)
        {
            var contestant = _contestantRepo.FindContestantById(id);

            var vote = contestant.Votes.ToList();
            return vote;
        }

        public List<VoteDto> GetVotes()
        {
            var vote = _context.Votes
                .Include(v => v.Contestant)
                .Select(v => new VoteDto
                {
                    Id = v.Id,
                    Contestant = v.Contestant,
                    ContestantId = v.ContestantId,
                    Position = v.Position,
                    PositionId = v.PositionId,
                    Voter = v.Voter,
                    VoterId = v.VoterId
                }).ToList();
            return vote;
        }

        public Vote Query(int voterId, int positionId)
        {
            return _context.Votes
                .Where(v => v.VoterId == voterId && v.PositionId == positionId).FirstOrDefault();
        }

        public bool Exists(int id)
        {
            return _context.Votes.Any(u => u.Id == id);

        }

        public Vote CreateVote(Vote vote)
        {
            _context.Votes.Add(vote);
            _context.SaveChanges();
            return vote;
        }

        public Vote FindVoteById(int id)
        {
            var vote = _context.Votes.Find(id);
            return vote;
        }

        public Vote GetVoteByPositionId(int id)
        {
            var vote = _context.Votes.Include(c => c.Position)
                .FirstOrDefault(v => v.PositionId == id);

            return vote;
        }
    }
}
