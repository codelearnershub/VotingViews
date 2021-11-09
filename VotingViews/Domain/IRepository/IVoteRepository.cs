using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VotingViews.Model.Entity;

namespace VotingViews.Domain.IRepository
{
    public interface IVoteRepository
    {
        public Vote Query(int voterId, int positionId);

        public Vote FindVoteById(int id);

        public Vote GetVoteByPositionId(int id);

        public Vote CreateVote(Vote vote);
    }
}
