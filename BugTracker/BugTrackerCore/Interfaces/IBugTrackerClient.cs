using BugTrackerAPICall.Interfaces;
using BugTrackerAPICall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerAPICall.Interfaces
{
    public interface IBugTrackerClient
    {
        Task SendError(string message, IError data);
        Task SendSignal(string message);
    }
}
