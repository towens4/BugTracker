using BugTrackerCore.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerCore.Services
{
    public class HubConnectionManager
    {
        private readonly HubConnection _hubConnection;

        public HubConnectionManager()
        {
            _hubConnection = new HubConnectionBuilder().WithUrl("https://localhost:7240/BugTrackerHub").Build();

            _hubConnection.Closed += OnConnectionClosed;
        }

        public async Task StartConnectionAsync()
        {
            try
            {
                if(_hubConnection.State != HubConnectionState.Connected)
                    await _hubConnection.StartAsync();

            }
            catch (Exception ex)
            {

            }
            
        }

        public async Task StopConnectionAsync()
        {
            try
            {
                
                if(_hubConnection.State == HubConnectionState.Connected)
                    await _hubConnection.StopAsync();
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
        }

        private Task OnConnectionClosed(Exception error)
        {
            
            return Task.CompletedTask;
        }


        public async Task SendError(ErrorPostModel errorPostModel)
        {
            try
            {
                await _hubConnection.InvokeAsync("SendErrorDetails", errorPostModel);
            }
            catch (Exception ex)
            {

                
            }
        }

        public async Task SendAppSignal()
        {
            await _hubConnection.InvokeAsync("SendAppAddedSignal");
        }

       

        
    }
}
