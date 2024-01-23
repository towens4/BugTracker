using BugTrackerAPICall.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerAPICall.Models
{
    public class ErrorPostModel : IErrorPostModel
    {
        public Error ErrorModel { get; set; }
        public string ApplicationName { get; set; }
    }
}
