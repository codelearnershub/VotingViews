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

        public Vote Vote(int positionId, string email, int contestantId)
        {
           
            var voter =  _voterRepo.FindByEmail(email);
            var contestant =  _contestantRepo.FindContestantById(contestantId);
            var position =  _positionRepo.FindPositionById(positionId);
            if (HasVotedBefore(voter.Id, position.Id))
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
            var vote = new Vote
            {
                PositionId = positionId,
                ContestantId = contestantId,
                VoterId = voter.Id
            };

            return _voteRepo.CreateVote(vote);


        }

        private bool HasVotedBefore(int voterid, int positionId)
        {
            var vote = _voteRepo.Query(voterid, positionId);

            return (vote != null) ? true : false;
        }
    }
}
