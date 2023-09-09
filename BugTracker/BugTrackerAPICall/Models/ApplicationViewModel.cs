using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerAPICall.Models
{
    public class ApplicationViewModel
    {
        public Guid ApplicationId { get; set; }
        public string UserId { get; set; }
        public string ApplicationName { get; set; }
    }
}
