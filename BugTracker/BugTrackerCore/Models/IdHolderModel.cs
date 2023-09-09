﻿using BugTrackerCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerCore.Models
{
    public class IdHolderModel : IIdHolderModel
    {
        public string UserId { get; set; }
        public Guid ApplicationId { get; set; }
    }
}
