using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingViews.DTOs;
using VotingViews.Model.Entity;

namespace VotingViews.Domain.IService
{
    public interface IContestantService
    {
        public ContestantDto GetContestantById(int? id);

        public Contestant AddContestant(Contestant model);

        Task<List<ContestantDto>> ListOfContestants();

        Task VoteContestant(int id, string email);

        public void DeleteContestant(int id);

        public List<Contestant> GetContestantByPositionName(int id);

        public Contestant UpdateContestant(UpdateContestantDto model, int id);
    }
}
