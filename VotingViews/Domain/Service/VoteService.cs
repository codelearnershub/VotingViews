using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingViews.Domain.IRepository;
using VotingViews.Domain.IService;
using VotingViews.Model.Entity;

namespace VotingViews.Domain.Service
{
    public class VoteService : IVoteService
    {
        private readonly IVoteRepository _voteRepo;
        private readonly IVoterRepository _voterRepo;
        private readonly IPositionRepository _positionRepo;
        private readonly IContestantRepository _contestantRepo;

        public VoteService(IVoteRepository voteRepo, IPositionRepository positionRepo, IContestantRepository contestantRepo, IVoterRepository voterRepo)
        {
            _voteRepo = voteRepo;
            _positionRepo = positionRepo;
            _contestantRepo = contestantRepo;
            _voterRepo = voterRepo;
        }

        public  Vote Vote(int id, string email)
        {
            var voter =  _voterRepo.FindByEmail(email);
            var vote = _voteRepo.FindVoteById(id);
            var contestant =  _contestantRepo.FindContestantById(id);
            var position =  _positionRepo.FindPositionById(id);
            if (position == null || voter == null)
            {
                return null;
            }
            else if (position.Contestants.Contains(contestant) && voter.VotedContestants.Contains(contestant))
            {
                return null;

            }
            else
            {
                contestant.ConestantVote++;
                position.Contestants.Add(contestant);
                voter.VotedContestants.Add(contestant);
            }
            _contestantRepo.UpdateContestant(contestant);
            return new Vote
            {
                TotalCount = vote.TotalCount
            };


        }


    }
}
