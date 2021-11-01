using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingViews.DTOs;
using VotingViews.Model.Entity;
using VotingViews.Models;

namespace VotingViews.Domain.IService
{
    public interface IPositionService
    {
        public IEnumerable<PositionDto> GetPositionByElectionId(int electionId);

        public PositionDto GetPositionByName(string name);

        public PositionDto GetPositionById(int id);

        public CreatedPositionDto AddPosition(CreatePositionDto model);
        
        public List<PositionDto> GetPositionByElectionCode(Guid code);

        public List<PositionDto> ListOfPositions();

        public void DeletePosition(int id);

        public Position UpdatePosition(UpdatePositionDto model, int id);
    }
}
