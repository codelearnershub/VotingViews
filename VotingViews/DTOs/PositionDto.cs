using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingViews.Model.Entity;

namespace VotingViews.DTOs
{
    public class PositionDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Election Election { get; set; }

        public int? ElectionId { get; set; }

        public int? TotalCount { get; set; }

        public ICollection<ContestantDto> Contestants { get; set; } = new HashSet<ContestantDto>();
    }
}
