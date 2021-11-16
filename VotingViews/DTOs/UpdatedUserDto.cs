using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingViews.Model.Entity;

namespace VotingViews.DTOs
{
    public class UpdatedUserDto
    {
        public int Id { get; set; }

        public Role Role { get; set; }
    }
}
