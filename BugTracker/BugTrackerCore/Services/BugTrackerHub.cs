using BugTrackerCore.Interfaces;
using BugTrackerCore.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerCore.Services
{
    public class BugTrackerHub : Hub
    {

        public async Task SendErrorDetails(ErrorPostModel errorPostModel)
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
