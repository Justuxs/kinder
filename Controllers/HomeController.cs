using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using kinder_app.Models;
using kinder_app.Data;
using Microsoft.EntityFrameworkCore;

namespace kinder_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        private static int current, currentID;
        private static List<int> alreadyLiked = new();

        public IActionResult Index()
        {
            IEnumerable<Item> itemList = _db.Item;
            IEnumerable<LikedItems> likedList = _db.LikedItems;

            var filteredLiked = likedList.Where(x => x.UserID == CurrentUserExtention.getUserID(this.User))
                                         .Select(x => x.ItemID);

            itemList = itemList.Where(x => !alreadyLiked.Contains(x.ID)).Where(x => !filteredLiked.Contains(x.ID));
           
            if (current == itemList.Count())
            {
                current = 0;
            }

            TempData["name"] = itemList.ToList()[current].Name;
            TempData["cat"] = itemList.ToList()[current].Category;
            TempData["cond"] = itemList.ToList()[current].Condition;
            TempData["desc"] = itemList.ToList()[current].Description;
            TempData["size"] = itemList.ToList()[current].Size.ToString();
            TempData["date"] = itemList.ToList()[current].DateOfPurchase.ToString("yyyy-mm-dd");
            TempData["karma"] = itemList.ToList()[current].KarmaPoints;

            currentID = itemList.ToList()[current].ID;

            return View(itemList.ToList()[current]);
        }

        public IActionResult loadNext()
        {
            current++;
            return RedirectToAction("index");
        }

        public IActionResult likeThis()
        {
            LikedItems liked = new();
            liked.ItemID = currentID;
            liked.UserID = CurrentUserExtention.getUserID(this.User);

            alreadyLiked.Add(0 + currentID);

            //inserting?????
            _db.Entry(liked).State = EntityState.Added;
            _db.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Privacy()
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
