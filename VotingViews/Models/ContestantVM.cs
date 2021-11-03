using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingViews.Model.Entity;

namespace VotingViews.Models
{
    public class ContestantVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string FullName => $"{FirstName} {MiddleName.Substring(0, 1)}. {LastName}"; 
        public int ContestantVote { get; set; }
    }

    public class ContestantVoteVM
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public int ContestantVote { get; set; }
    }

    public class CreateContestant
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public int PositionId { get; set; }
        public int ElectionId { get; set; }
    }

    public class ContestantVote
    {
        public string Email { get; set; }
        public int Vote { get; set; }
    }

    public class UpdateContestant
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
