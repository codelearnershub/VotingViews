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
    public class PositionRepository : IPositionRepository
    {
        private readonly ApplicationContext _context;
        private readonly IElectionRepository _election;

        public PositionRepository(ApplicationContext context, IElectionRepository election)
        {
            _context = context;
            _election = election;
        }

        public Position AddPosition(Position position)
        {
            _context.Positions.Add(position);
            _context.SaveChanges();
            return position;
        }

        public void DeletePosition(int id)
        {
            Position position = _context.Positions.Find(id);
            _context.Positions.Remove(position);
            _context.SaveChanges();
        }

        public Position FindPositionById(int id)
        {
            var position = _context.Positions
                .Include(c => c.Contestants)
                .FirstOrDefault(c => c.Id == id);
            return position;
        }

        public List<Position> GetPositionByElectionCode(Guid code)
        {
            var election = _election.FindByCode(code);
           
            var positions = election.Positions.ToList();
            return positions;
        }

        public Position FindPositionByName(string name)
        {
            return _context.Positions.Find(name);
        }

        public List<PositionDto> GetAll()
        {
            return _context.Positions.Include(P=>P.Election)
                .Select(p => new PositionDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Election = p.Election,
                    ElectionId = p.ElectionId
                }).ToList();
        }

        public Position UpdatePosition(Position position)
        {
            _context.Set<Position>().Update(position);
            _context.SaveChanges();
            return position;
        }
    }
}
