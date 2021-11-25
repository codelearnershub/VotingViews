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
    public class VoterService : IVoterService
    {
        private readonly IVoterRepository _voter;

        public VoterService(IVoterRepository voter)
        {
            _voter = voter;
        }

        public Voter Update(UpdateVoterDto model, int id)
        {
            var update = _voter.FindbyId(id);

            update.FirstName = model.FirstName;
            update.LastName = model.LastName;
            update.MiddleName = model.MiddleName;
            update.Password = model.Password;
            update.Address = model.Address;

            _voter.Update(update);
            return update;
        }

        public List<VoterDto> GetAll()
        {
            return _voter.GetAll().Select(v => new VoterDto
            {
                Id = v.Id,
                User = v.User,
                UserId = v.UserId,
                FirstName = v.FirstName,
                LastName = v.LastName,
                MiddleName = v.MiddleName,
                Email = v.Email,
                Password = v.Password,
                Address = v.Address
            }).ToList();
        }

        public Voter AddVoter(CreateVoterDto model)
        {
            Voter newVoter = new Voter
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                Email = model.Email,
                Password = model.Password,
                Address = model.Address,
                UserId = model.UserId
            };

            return _voter.AddVoter(newVoter);
        }

        public bool  Exists(int id) => _voter.Exists(id);

        public Voter GetVoterByUserId(int userId) => _voter.FindByUserId(userId);

        public VoterDto FindById(int id)
        {
            var voter = _voter.FindbyId(id);

            VoterDto newVoter = new VoterDto
            {
                Id = voter.Id,
                FirstName = voter.FirstName,
                LastName = voter.LastName,
                MiddleName = voter.MiddleName,
                Email = voter.Email,
                Address = voter.Address,
                Password = voter.Password,
                User = voter.User,
            };

            return newVoter;
        }

        public VoterDto FindByEmail(string email)
        {
            var voter = _voter.FindByEmail(email);
            if(voter == null)
            {
                return null;
            }

            return new VoterDto
            {
                Id = voter.Id,
                User = voter.User,
                FirstName = voter.FirstName,
                LastName = voter.LastName,
                MiddleName = voter.MiddleName,
                Email = voter.Email,
                Password = voter.Password,
                Address = voter.Address,
                VotedContestants = voter.VotedContestants.Select(c => new Contestant
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    MiddleName = c.MiddleName,
                    ItemPictureURL = c.ItemPictureURL,
                    Email = c.Email,
                    Gender = c.Gender
                }).ToList()
            };
        }

        public void DeleteVoter(int id)
        {
            _voter.DeleteVoter(id);
        }

    }
}
