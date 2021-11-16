using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VotingViews.Context;
using VotingViews.Domain.IRepository;
using VotingViews.DTOs;
using VotingViews.Model.Entity;

namespace VotingViews.Domain.Repository
{
    public class ContestantRepository : IContestantRepository
    {
        private readonly ApplicationContext _context;
        private readonly IPositionRepository _position;

        public ContestantRepository(ApplicationContext context, IPositionRepository position)
        {
            _context = context;
            _position = position;
        }
        public Contestant AddContestant(Contestant contestant)
        {
            _context.Contestants.Add(contestant);
            _context.SaveChanges();
            return contestant;
        }

        public void DeleteContestant(int id)
        {
            Contestant contestant = _context.Contestants.Find(id);
            if (contestant == null) return;
            _context.Contestants.Remove(contestant);
            _context.SaveChanges();
        }

        public List<ContestantDto> GetContestantByPositionId(int id)
        {
            var position = _position.FindPositionById(id);

            var contestant = position.Contestants.Select(c => new ContestantDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                MiddleName = c.MiddleName,
                Email = c.Email,
                Gender = c.Gender,
                ConestantVote = c.ConestantVote,
                InternalImage =c.InternalImage,
                ItemPictureURL = c.ItemPictureURL,
                Position = c.Position,
                PositionId = c.PositionId
            }).ToList();

            return contestant;
        }

        public async Task VoteContestant(int id, string email)
        {
            var voter = await _context.Voters.FirstOrDefaultAsync(c => c.Email == email);
            var contestant = await _context.Contestants.FirstOrDefaultAsync(c => c.Id == id);

            if (contestant == null || voter == null)
            {
                return;
            }
            else if (voter.VotedContestants.Contains(contestant))
            {
                return;
            }
            else
            {
                contestant.ConestantVote++;
                voter.VotedContestants.Add(contestant);
            }

            _context.Contestants.Update(contestant);
            await _context.SaveChangesAsync();
        }

        public Contestant FindContestantById(int id)
        {
            return _context.Contestants.Include(c => c.Position).ThenInclude(c => c.Election).FirstOrDefault(a => a.Id == id);
        }

        public async Task<List<ContestantDto>> GetAll()
        {
            return await _context.Contestants
                .Include(c => c.Position)
                .ThenInclude(c => c.Election)
                .Select(c => new ContestantDto
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    MiddleName = c.MiddleName,
                    Email = c.Email,
                    Gender = c.Gender,
                    ConestantVote = c.ConestantVote,
                    InternalImage = c.InternalImage,
                    ItemPictureURL = c.ItemPictureURL,
                    Position = c.Position,
                    PositionId = c.PositionId
                }).ToListAsync();
        }

        public Contestant UpdateContestant(Contestant model)
        {
            _context.Contestants.Update(model);
            _context.SaveChanges();
            return model;
        }
    }
}
