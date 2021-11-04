using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingViews.Model.Entity;

namespace VotingViews.Domain.IRepository
{
    public interface IVoteRepository
    {
        public IQueryable<Vote> Query();

        public Vote FindVoteById(int id);

        public Vote GetVoteByPositionId(int id);
    }
}
