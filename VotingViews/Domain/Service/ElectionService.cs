using System;
using System.Collections.Generic;
using System.Linq;
using VotingViews.Domain.IRepository;
using VotingViews.Domain.IService;
using VotingViews.DTOs;
using VotingViews.Model.Entity;

namespace VotingViews.Domain.Service
{
    public class ElectionService : IElectionService
    {
        private readonly IElectionRepository _election;

        public ElectionService(IElectionRepository election)
        {
            _election = election;
        }

        public Election AddElection(CreateElectionDto election)
        {
            Election newElection = new Election
            {
                Name = election.Name,
                Code = Guid.NewGuid(),
                StartDate  = election.StartDate,
                EndDate = election.EndDate
            };
            return  _election.AddElection(newElection);

            
        }

        public bool Exists(int id) => _election.Exists(id);

        public ElectionDto GetElectionByCode(Guid code)
        {
            var election = _election.FindByCode(code);
            if (election == null)
            {
                return null;
            }

            return new ElectionDto
            {
                Id = election.Id,
                Name = election.Name,
                Code = election.Code,
                StartDate = election.StartDate,
                EndDate = election.EndDate,
                Status = GetStatus(election.Id),
                Positions = election.Positions.Select(c => new PositionDto()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Contestants =  c.Contestants.Select(c => new ContestantDto()
                    {
                        Id = c.Id,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        MiddleName = c.MiddleName,
                        Gender = c.Gender,
                        Email = c.Email,
                        InternalImage = c.InternalImage,
                        ItemPictureURL = c.ItemPictureURL,
                    }).ToList()
                }).ToList(),

            };
        }

        public ElectionDto GetElectionById(int? id)
        {
            var election =  _election.FindbyId(id.Value);

            ElectionDto electionDto =  new ElectionDto
            {
                Id = election.Id,
                Name = election.Name,
                Code = election.Code,
                StartDate = election.StartDate,
                EndDate = election.EndDate,
                Status = GetStatus(election.Id)
            };

            return electionDto;
        }

        public void DeleteElection(int id)
        {
            _election.DeleteElection(id);
        }

        public Election UpdateElection(UpdateElectionDto update, int id)
        {
            var election = _election.FindbyId(id);

            election.Name = update.Name;
            election.StartDate = update.StartDate;
            election.EndDate = update.EndDate;

            _election.UpdateElection(election);
            return election;
        }

        public List<ElectionDto> GetAllElections()
        {
            return _election.GetAll().Select(e => new ElectionDto
            {
                Id = e.Id,
                Name = e.Name,
                Code = e.Code,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Status = GetStatus(e.Id)
            }).ToList();
        }

       private string GetStatus(int id)
        {
            var election = _election.FindbyId(id);
            string status = "";

            if(election.StartDate > DateTime.Now)
            {
                status = "InActive";
            }else if(election.StartDate<= DateTime.Now && election.EndDate> DateTime.Now)
            {
                status = "Active";
            }else if(election.EndDate<= DateTime.Now)
            {
                status = "Completed";
            }
            return status;
        }
    }
}
