using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VotingViews.DTOs;
using VotingViews.Model.Entity;

namespace VotingViews.Models
{
    public class PositionVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PositionListFilterViewModel
    {
        public List<PositionDto> Positions { get; set; }

        public SelectList Elections { get; set; }

        public string ElectionPosition { get; set; }

        public string SearchString { get; set; }


    }

    public class CreatePosition
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Position Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Election")]
        public Election Election { get; set; }
        public int ElectionId { get; set; }
        public IEnumerable<SelectListItem> Elections { get; set; }

    }

    public class UpdatePosition
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
