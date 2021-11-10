using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingViews.Model.Entity;

namespace VotingViews.DTOs
{
    public class VoteDto
    {
        public int Id { get; set; }

        public Contestant Contestant { get; set; }

        public int ContestantId { get; set; }

        public Voter Voter { get; set; }

        public int VoterId { get; set; }

        public Position Position { get; set; }

        public int PositionId { get; set; }
    }
}
