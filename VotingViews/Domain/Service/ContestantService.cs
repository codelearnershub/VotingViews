using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingViews.Domain.IRepository;
using VotingViews.Domain.IService;
using VotingViews.DTOs;
using VotingViews.Model.Entity;
using VotingViews.Models;

namespace VotingViews.Domain.Service
{
    public class ContestantService : IContestantService
    {
        private readonly IContestantRepository _contestant;
        private readonly IVoterRepository _voter;
        private readonly IVoteRepository _vote;
        private readonly IPositionService _position;

        public ContestantService(IContestantRepository contestant, IVoterRepository voter, IVoteRepository vote, IPositionService position)
        {
            _contestant = contestant;
            _voter = voter;
            _vote = vote;
            _position = position;
        }

        public Contestant AddContestant(CreateContestant model)
        {
            var contestant = new Contestant
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                Gender = model.Gender,
                Email = model.Email,
                InternalImage = model.InternalImage,
                ItemPictureURL = model.ItemPictureURL,
                PositionId = model.PositionId,

            };
             _contestant.AddContestant(contestant);
            return new Contestant
            {
                FirstName = contestant.FirstName,
                LastName = contestant.LastName,
                MiddleName = contestant.MiddleName,
                Gender = contestant.Gender,
                Email = contestant.Email,
                InternalImage = contestant.InternalImage,
                ItemPictureURL = contestant.ItemPictureURL,
                PositionId = contestant.PositionId,

            };
        }

        public  List<ContestantDto> GetContestantByPositionId(int id)
        {
            var contestant =  _contestant.GetContestantByPositionId(id);
            return contestant;
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
               InternalImage = contestant.InternalImage,
               ItemPictureURL = contestant.ItemPictureURL,
               Position = contestant.Position
            };
            return contestantDto;
        }

        public List<ContestantDto> ListOfContestants()
        {
            return  _contestant.GetAll();
        }

        public Contestant UpdateContestant(UpdateContestantDto model, int id)
        {
            var contestant = _contestant.FindContestantById(id);

            contestant.FirstName = model.FirstName;
            contestant.LastName = model.LastName;
            contestant.MiddleName = model.MiddleName;
            contestant.InternalImage = model.InternalImage;
            contestant.ItemPictureURL = model.ItemPictureURL;

            _contestant.UpdateContestant(contestant);
            return contestant;
        }

        public async  Task VoteContestant(int id, string email)
        {
            await _contestant.VoteContestant(id, email);
        }
    }
}
