using BugTrackerAPICall.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerAPICall.Models
{
    public class IdHolderModel : IIdHolderModel
    {
        public string UserId { get; set; }
        public Guid ApplicationId { get; set; }
    }
}
