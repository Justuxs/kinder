using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace Chat.Hubs
{
    public class ChatHub : Hub
    {
        private static readonly HttpClient client = new HttpClient();

        private static readonly string Url = "https://localhost:5001/Messages/Create";

        public async Task SendMessage(string username, string message)
        {

            Console.WriteLine("gAVAU ZINUTE "+ username+" "+ message+" "+Clients.Caller.SendAsync("ReceiveMessage", username, message));
            await Clients.All.SendAsync("ReceiveMessage", username, message);
            
        }
    }
}
