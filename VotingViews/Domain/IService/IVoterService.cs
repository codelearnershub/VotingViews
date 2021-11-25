using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingViews.DTOs;
using VotingViews.Model.Entity;
using VotingViews.Models;

namespace VotingViews.Domain.IService
{
    public interface IVoterService
    {
        public Voter Update(UpdateVoterDto model, int id);

        public List<VoterDto> GetAll();

        public Voter AddVoter(CreateVoterDto model);

        public bool Exists(int id);

        public Voter GetVoterByUserId(int userId);

        public VoterDto FindById(int id);

        public VoterDto FindByEmail(string email);

        public void DeleteVoter(int id);
    }
}
