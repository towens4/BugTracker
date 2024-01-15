using BugTrackerAPICall.Interfaces;
using BugTrackerAPICall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerAPICall.Interfaces
{
    public interface IConnectionProcessingService
    {
        void ProcessError(IErrorPostModel error, bool appNameExists, bool userIdIsEmpty);
    }
}
