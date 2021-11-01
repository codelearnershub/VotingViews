using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingViews.Model.Entity
{
    public class Election : BaseEntity
    {
        public string Name { get; set; }
        public Guid Code { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<Position> Positions { get; set; } = new HashSet<Position>();
    }
}
