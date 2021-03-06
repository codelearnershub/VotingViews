using System;
using System.Collections.Generic;
using System.Linq;
using VotingViews.Domain.IRepository;
using VotingViews.Domain.IService;
using VotingViews.DTOs;
using VotingViews.Model.Entity;

namespace VotingViews.Domain.Service
{
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _position;
        private readonly IElectionRepository _election;

        public PositionService(IPositionRepository position, IElectionRepository election)
        {
            _position = position;
            _election = election;
        }

        public CreatedPositionDto AddPosition(PositionDto model)
        {
            Position newPosition = new Position
            {
                Name = model.Name,
                ElectionId = model.ElectionId,
            };
            var position = _position.AddPosition(newPosition);
            return new CreatedPositionDto
            {
                Id = position.Id
            };
        }

        public List<PositionDto> GetPositionByElectionCode(Guid code)
        {
            return _position.GetPositionByElectionCode(code)
                .Select(p => new PositionDto
                {
                    Id = p.Id,
                    Name = p.Name,
                }).ToList();
        }

        //public List<PositionDto> GetPositionByElectionId(int id)
        //{
        //    return _position.GetPositionByElectionId(id)
        //       .Select(p => new PositionDto
        //       {
        //           Id = p.Id,
        //           Name = p.Name,
        //       }).ToList();
        //}

        public IEnumerable<PositionDto> GetPositionByElectionId(int electionId)
        {
            _election.FindbyId(electionId);
            return _position.GetAll().Where(e => e.ElectionId == electionId).Select(p => new PositionDto
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
        }

        public PositionDto GetPositionByName(string name)
        {
            var position = _position.FindPositionByName(name);
            return new PositionDto
            {
                Name = position.Name,
                TotalCount = position.TotalCount
            };
        }

        public PositionDto GetPositionById(int id)
        {
            var position = _position.FindPositionById(id);

            return new PositionDto
            {
                Id = position.Id,
                Name = position.Name,
                Election = position.Election,
                ElectionId = position.ElectionId,
                TotalCount = position.TotalCount
            };
        }

        public List<PositionDto> ListOfPositions()
        {
            return _position.GetAll();
        }

        public void DeletePosition(int id)
        {
            _position.DeletePosition(id);
        }

        public Position UpdatePosition(UpdatePositionDto model, int id)
        {
            var position = _position.FindPositionById(id);

            position.Name = model.Name;


            _position.UpdatePosition(position);
            return position;
        }

    }
}
