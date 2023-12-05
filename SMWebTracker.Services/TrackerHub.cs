using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;

namespace SMWebTracker.Services
{
    public class TrackerHub : Hub
    {
        public async Task SendCount(int count)
        {
            await Clients.All.SendAsync("ReceiveCount", count);
        }
    }
}
