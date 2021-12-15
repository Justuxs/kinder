using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using kinder_app.Data;
using kinder_app.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace kinder_app.Controllers
{
    public class TransferModel
    {
        public TransferModel(int itemID, string itemName, string itemDesc, string userID, int uniqID)
        {
            ItemID = itemID;
            ItemName = itemName;
            ItemDesc = itemDesc;
            UserEmail = userID;
            UniqID = uniqID;
        }

        public int UniqID { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemDesc { get; set; }
        public string UserEmail { get; set; }
      
        public override string ToString()
        {
            return UniqID.ToString() + ',' + ItemID.ToString() + ',' + ItemName + ',' + ItemDesc + ',' + UserEmail;
        }
    }

    public class LikedItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LikedItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LikedItems
        public IActionResult Index()
        {
            return View(
                ControllerMethods
                .LikedModelList
                (CurrentUserExtention.GetUserID(this.User),
                _context.Item.ToList(), _context.LikedItems.ToList(), _context.Users.ToList()));
        }

        // GET: LikedItems/Edit/5
        public IActionResult Give(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ControllerMethods.GiveItem(_context, this.User.GetUserID(), id);

            return RedirectToAction("index");
        }
    }
}
