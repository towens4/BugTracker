using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerCore.Models
{
    public class ErrorPostModel
    {
        public Error ErrorModel { get; set; }
        public string ApplicationName { get; set; }
    }
}
