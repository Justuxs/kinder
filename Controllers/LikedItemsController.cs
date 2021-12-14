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
    }

    public class LikedItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LikedItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<TransferModel> ModelList()
        {
            //TODO: separate methods
            List<Item> items = _context.Item.ToList();
            List<LikedItems> likedItems = _context.LikedItems.ToList();
            List<IdentityUser> users = _context.Users.ToList();

            //REQUIREMENT: join
            var join = likedItems.Where(x => x.UserID != User.GetUserID())
                            .Join(
                            items,
                            liked => liked.ItemID,
                            item => item.ID,
                            (liked, item) => new
                            {
                                ItemID = item.ID,
                                ItemName = item.Name,
                                ItemDesc = item.Description,
                                UserID = liked.UserID
                            });

            var join2 = join.Join(
                users,
                item => item.UserID,
                user => user.Id,
                (item, user) => new
                {
                    ItemID = item.ItemID,
                    ItemName = item.ItemName,
                    ItemDesc = item.ItemDesc,
                    UserID = user.Id,
                    UserEmail = user.Email
                });

            List<TransferModel> result = new();
            int i = 0;

            foreach (var elem in join2)
            {
                TransferModel temp = new
                    (elem.ItemID, elem.ItemName, elem.ItemDesc, elem.UserEmail, i);

                i++;
                result.Add(temp);
            }

            return result;
        }

        // GET: LikedItems
        public async Task<IActionResult> Index()
        {

            return View(ModelList());
        }

        // GET: LikedItems/Edit/5
        public async Task<IActionResult> Give(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uniqueItem = ModelList()
                .FirstOrDefault(m => m.UniqID == id);

            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.ID == uniqueItem.ItemID);

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Email == uniqueItem.UserEmail);

            item.GivenTo = user.Id;
            _context.Update(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }
    }
}
