using BugTrackerAPICall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerAPICall.Interfaces
{
    public interface IErrorPostModel
    {
        public IError ErrorModel { get; set; }
        public string ApplicationName { get; set; }
    }
}
