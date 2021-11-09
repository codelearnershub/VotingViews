using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingViews.Model.Entity
{
    public class Voter :BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }



        //public List<Vote> Votes = new List<Vote>();
        public ICollection<Contestant> VotedContestants { get; set; } = new HashSet<Contestant>();
    }
}
