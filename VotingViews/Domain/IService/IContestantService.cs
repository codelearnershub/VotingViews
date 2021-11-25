using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingViews.DTOs;
using VotingViews.Model.Entity;
using VotingViews.Models;

namespace VotingViews.Domain.IService
{
    public interface IContestantService
    {
        public ContestantDto GetContestantById(int? id);

        public Contestant AddContestant(CreateContestant model);

        public List<ContestantDto> ListOfContestants();

        Task VoteContestant(int id, string email);

        public void DeleteContestant(int id);

        public List<ContestantDto> GetContestantByPositionId(int id);

        public Contestant UpdateContestant(UpdateContestantDto model, int id);
    }
}
