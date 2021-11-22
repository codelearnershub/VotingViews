using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VotingViews.DTOs;
using VotingViews.Model.Entity;

namespace VotingViews.Domain.IRepository
{
    public interface IPositionRepository
    {
        public Position AddPosition(Position position);


        public Position FindPositionById(int id);

        public List<PositionDto> GetAll();

        public List<Position> GetPositionByElectionCode(Guid code);

        public List<Position> GetPositionByElectionId(int id);

        public Position FindPositionByName(string name);

        public Position UpdatePosition(Position model);

        public void DeletePosition(int id);
    }
}
