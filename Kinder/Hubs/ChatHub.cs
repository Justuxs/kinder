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
using kinder_app.Data;
using kinder_app.Models;
using kinder_app.Controllers;

namespace Chat.Hubs
{
    public class ChatHub : Hub
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly ApplicationDbContext _db;
        private static readonly string Url = "https://localhost:5001/Messages/Create";
        public ChatHub(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task SendMessage(string username, string username2,string chatname, string text)
        {

            Console.WriteLine("gAVAU ZINUTE "+ username+" m "+ username2 +" "+ chatname+" "+ text);
            Message message = new Message(username, username2, text, chatname);
            ControllerMethods.SaveMessage(message, _db);
            await Clients.All.SendAsync("ReceiveMessage", username, username2, chatname, text, message.Date.ToString());
            
        }
    }
}
