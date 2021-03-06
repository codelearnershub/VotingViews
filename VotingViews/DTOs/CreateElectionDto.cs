using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingViews.DTOs
{
    public class CreateElectionDto
    {
        public string Name { get; set; }

        public string Status { get; set; }

        public Guid Code { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
