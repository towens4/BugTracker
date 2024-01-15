using BugTrackerAPICall.Interfaces;
using BugTrackerAPICall.Interfaces;
using BugTrackerAPICall.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerAPICall.Services
{
    public class BugTrackerHub : Hub
    {

        public async Task SendErrorDetails(IErrorPostModel errorPostModel)
        {
            await Clients.All.SendAsync("SendErrorDetails", errorPostModel.ErrorModel, errorPostModel.ApplicationName);
            //await Clients.All.SendError($"Sent from {errorPostModel.ApplicationName}", errorPostModel.ErrorModel);
        }

        public async Task SendAppAddedSignal()
        {
            await Clients.All.SendAsync("SendAppAddedSignal");
           // await Clients.All.SendSignal("New application added");
        }
    }
}
