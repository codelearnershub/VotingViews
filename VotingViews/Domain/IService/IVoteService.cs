using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingViews.Model.Entity;

namespace VotingViews.Domain.IService
{
    public interface IVoteService
    {
         Vote Vote(int positionId, string email, int contestantId);
    }
}
