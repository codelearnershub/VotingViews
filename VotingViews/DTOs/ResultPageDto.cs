using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingViews.Model.Entity;

namespace VotingViews.DTOs
{
    public class ResultPageDto
    {
        public Election Election { get; set; }
        public PositionDto Position { get; set; }
        public int ElectionId { get; set; }
        public Guid Code { get; set; }
        public IEnumerable<VotingViews.DTOs.ContestantDto> Contestants { get; set; }

    }
}
