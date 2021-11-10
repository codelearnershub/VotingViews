using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingViews.Domain.IRepository;
using VotingViews.Domain.IService;
using VotingViews.DTOs;

namespace VotingViews.Domain.Service
{
    public class ResultService : IResultService
    {
        private readonly IVoteRepository _voteRepo;
        private readonly IContestantService _contestantRepo;

        public ResultService(IContestantService contestantRepo, IVoteRepository voteRepo)
        {
            _contestantRepo = contestantRepo;
            _voteRepo = voteRepo;
        }

        public int GetVoteByContestantId(int id)
        {
            var vote = _voteRepo.GetVoteByContestantId(id).Count();

            return vote;
        }
    }
}
