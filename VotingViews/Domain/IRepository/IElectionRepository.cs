using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VotingViews.Model.Entity;

namespace VotingViews.Domain.IRepository
{
    public interface IElectionRepository
    {
        public Election FindByCode(Guid code);

        public Election FindbyId(int id);

        public bool Exists(int id);

        public Election AddElection(Election election);

        public List<Election> GetAll();

        public void DeleteElection(int id);

        public Election UpdateElection(Election election);
    }
}
