using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VotingViews.DTOs
{
    public class UpdateContestantDto
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] InternalImage { get; set; }
        [DisplayName("Item Picture URL")]
        [StringLength(1024)]
        public string ItemPictureURL { get; set; }

    }
}
