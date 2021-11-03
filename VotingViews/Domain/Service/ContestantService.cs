using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingViews.Domain.IRepository;
using VotingViews.Domain.IService;
using VotingViews.DTOs;
using VotingViews.Model.Entity;

namespace VotingViews.Domain.Service
{
    public class ContestantService : IContestantService
    {
        private readonly IContestantRepository _contestant;
        private readonly IVoterRepository _voter;

        public ContestantService(IContestantRepository contestant, IVoterRepository voter)
        {
            _contestant = contestant;
            _voter = voter;
        }

        public Contestant AddContestant(Contestant model)
        {
            var newContestant = _contestant.AddContestant(model);
            return newContestant;
        }

        public List<Contestant> GetContestantByPositionName(int id)
        {
            return _contestant.GetContestantByPositionName(id);
        }

        public void DeleteContestant(int id)
        {
            _contestant.DeleteContestant(id);
        }

        public ContestantDto GetContestantById(int? id)
        {
           var contestant =  _contestant.FindContestantById(id.Value);
           ContestantDto contestantDto =  new ContestantDto
            {
                Id = contestant.Id,
                FirstName = contestant.FirstName,
                LastName = contestant.LastName,
                MiddleName = contestant.MiddleName,
                Email = contestant.Email,
                Gender = contestant.Gender,
                ConestantVote = contestant.ConestantVote,
                Position = contestant.Position
            };
            return contestantDto;
        }

        public async Task<List<ContestantDto>> ListOfContestants()
        {
            return await _contestant.GetAll();
        }

        public Contestant UpdateContestant(UpdateContestantDto model, int id)
        {
            var contestant = _contestant.FindContestantById(id);

            contestant.FirstName = model.FirstName;
            contestant.LastName = model.LastName;
            contestant.MiddleName = model.MiddleName;

            _contestant.UpdateContestant(contestant);
            return contestant;
        }

        public async  Task VoteContestant(int id, string email)
        {
            await _contestant.VoteContestant(id, email);
        }
    }
}
