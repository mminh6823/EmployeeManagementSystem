/*using FrontendLibrary.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendLibrary.Services.Implementations
{
    public class EmployeeHubService : IEmployeeHubService
    {
        private readonly HubConnection _hubConnection;

        public event Action<string>? OnEmployeeAdded;

        public EmployeeHubService(NavigationManager navigation)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(navigation.ToAbsoluteUri("/employeeHub")) // Đường dẫn SignalR Hub
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.On<string>("EmployeeAdded", (message) =>
            {
                OnEmployeeAdded?.Invoke(message);
            });
        }

        public async Task StartAsync() => await _hubConnection.StartAsync();
    }
}
*/