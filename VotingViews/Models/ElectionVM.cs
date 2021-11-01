using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingViews.Models
{
    public class ElectionVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid Code { get; set; }
        public string Status { get; set; }
    }

    public class CreateElectionVM
    {
        public string Name { get; set; }
        public string Status { get; set; }
    }

    public class UpdateElectionVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
