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
    public class PositionRepository : IPositionRepository
    {
        private readonly ApplicationContext _context;

        public PositionRepository(ApplicationContext context)
        {
            _context = context;
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
            return _context.Positions.Find(id);
        }

        public List<Position> GetPositionByElectionCode(Guid code)
        {
            var positions = _context.Positions.Include(p=>p.Election)
                .Where(p => p.Election.Code == code).ToList();
            return positions;
        }

        public Position FindPositionByName(string name)
        {
            return _context.Positions.Find(name);
        }

        public List<Position> GetAll()
        {
            var positions =  _context.Positions.ToList();
            return positions;
        }

        public Position UpdatePosition(Position position)
        {
            _context.Set<Position>().Update(position);
            _context.SaveChanges();
            return position;
        }
    }
}
