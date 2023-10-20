using BugTrackerCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerCore.Interfaces
{
    public interface IBugTrackerClient
    {
        Task SendError(string message, Error data);
        Task SendSignal(string message);
    }
}
