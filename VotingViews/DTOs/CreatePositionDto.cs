using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingViews.Model.Entity;

namespace VotingViews.DTOs
{
    public class CreatePositionDto
    {
        public string Name { get; set; }
        public Election Election { get; set; }
        public int ElectionId { get; set; }
        public IEnumerable<SelectListItem> Elections { get; set; }


    }
}
