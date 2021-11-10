using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingViews.Domain.IService
{
    public interface IResultService
    {
        public int GetVoteByContestantId(int id);
    }
}
