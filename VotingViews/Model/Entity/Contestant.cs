using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingViews.Model.Entity
{
    public class Contestant : BaseEntity
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public string FullName => $"{FirstName} {MiddleName.Substring(0, 1)}. {LastName}";

        public int ConestantVote { get; set; }

        public Position Position { get; set; }

        public int PositionId { get; set; }

        public ICollection<Vote> Votes { get; set; } = new HashSet<Vote>();
    }
}
