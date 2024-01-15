using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerAPICall.Models
{
    public class CompletedModel
    {
        public Guid ErrorId { get; set; }
        public bool IsCompleted { get; set; }
    }
}
