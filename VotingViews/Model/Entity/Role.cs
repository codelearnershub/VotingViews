﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingViews.Model.Entity
{
    public class Role :BaseEntity
    {
        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
