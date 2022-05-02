﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using kinder_app.Models;
using kinder_app.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Specialized;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace kinder_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private static readonly HttpClient client = new HttpClient();



        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        private static int current, currentID;
        private static string userID;
        private static List<int> alreadyLiked = new();

        [Authorize]
        public IActionResult Swiping()
        {
            var itemList = ControllerMethods.GetItemsSwiping(_db,
                CurrentUserExtention.GetUserID(this.User));
           
            if (current == itemList.Count())
            {
                current = 0;
            }

            if (itemList.Any())
            {
                TempData["name"] = itemList.ToList()[current].Name;
                TempData["cat"] = itemList.ToList()[current].Category;
                TempData["cond"] = itemList.ToList()[current].Condition;
                TempData["desc"] = itemList.ToList()[current].Description;
                TempData["size"] = itemList.ToList()[current].Size.ToString();
                TempData["date"] = itemList.ToList()[current].DateOfPurchase.ToString("yyyy-MM-dd");
                TempData["karma"] = itemList.ToList()[current].KarmaPoints;
                userID = itemList.ToList()[current].UserID as string;
                
            }
            else
            {
                TempData["name"] = "no";
            }

            return View();
        }
        public IActionResult Chat()
        {
            Console.WriteLine("Gavejas yra () "+ userID);

            TempData["Receiver"] = userID;
            return RedirectToAction("ChatRoom", "Messages");


            var data = new NameValueCollection();
            data["sender"] = "username";
            data["text"] = "message";
            data["receiver"] = "Gavejas";
            Console.WriteLine("Gavau zinute");
            var mess = new Message((data["sender"]), (data["receiver"]), data["text"]);
            _db.Add(mess);


            //await Clients.All.SendAsync("ReceiveMessage", username, ats);
            return View();
        }

        public IActionResult LoadNext()
        {
            current++;
            return RedirectToAction("swiping");
        }
        public IActionResult ChatWithGiver()
        {
            current++;
            return RedirectToAction("swiping");
        }

        public IActionResult LikeThis()
        {
            alreadyLiked=
            ControllerMethods.LikeItem(_db, currentID, CurrentUserExtention.GetUserID(this.User), alreadyLiked);
            
            return RedirectToAction("swiping");
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
