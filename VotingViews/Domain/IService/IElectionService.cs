using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingViews.DTOs;
using VotingViews.Model.Entity;
using VotingViews.Models;

namespace VotingViews.Domain.IService
{
    public interface IElectionService
    {
        public CreatedElectionDto AddElection(CreateElectionDto election);

        public ElectionDto GetElectionByCode(Guid code);

        public ElectionDto GetElectionById(int id);

        public void DeleteElection(int id);

        public Election UpdateElection(UpdateElectionDto update, int id);

        public List<ElectionDto> GetAllElections();
    }
}
