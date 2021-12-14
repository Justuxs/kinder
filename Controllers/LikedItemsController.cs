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

        // GET: LikedItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var likedItems = await _context.LikedItems
                .FirstOrDefaultAsync(m => m.Key == id);
            if (likedItems == null)
            {
                return NotFound();
            }

            return View(likedItems);
        }

        // GET: LikedItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LikedItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Key,ItemID,UserID")] LikedItems likedItems)
        {
            if (ModelState.IsValid)
            {
                _context.Add(likedItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(likedItems);
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

        // POST: LikedItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Key,ItemID,UserID")] LikedItems likedItems)
        {
            if (id != likedItems.Key)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {


                    _context.Update(likedItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LikedItemsExists(likedItems.Key))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(likedItems);
        }

        // GET: LikedItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var likedItems = await _context.LikedItems
                .FirstOrDefaultAsync(m => m.Key == id);
            if (likedItems == null)
            {
                return NotFound();
            }

            return View(likedItems);
        }

        // POST: LikedItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var likedItems = await _context.LikedItems.FindAsync(id);
            _context.LikedItems.Remove(likedItems);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LikedItemsExists(int id)
        {
            return _context.LikedItems.Any(e => e.Key == id);
        }
    }
}
