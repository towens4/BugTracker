using BugTrackerCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerCore.Interfaces
{
    public interface IConnectionProcessingService
    {
        void ProcessError(ErrorPostModel error, bool appNameExists, bool userIdIsEmpty);
    }
}
