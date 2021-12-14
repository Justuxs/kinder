using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using kinder_app.Data;
using kinder_app.Models;

namespace kinder_app.Controllers
{
    public class TakeItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TakeItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TakeItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.Item.Where(x => x.GivenTo == User.GetUserID()).ToListAsync());
        }

        // GET: TakeItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.ID == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: TakeItems/Edit/5
        public async Task<IActionResult> Take(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item.FindAsync(id);

            var user = await _context.ApplicationUsers
                .FirstOrDefaultAsync(m => m.Id == item.UserID);

            var liked = _context.LikedItems.FirstOrDefault(m => m.ItemID == item.ID);
            _context.LikedItems.Remove(liked);
            await _context.SaveChangesAsync();

            while(_context.LikedItems.FirstOrDefault(m => m.ItemID == item.ID) != null)
            {
                liked = _context.LikedItems.FirstOrDefault(m => m.ItemID == item.ID);
                _context.LikedItems.Remove(liked);
                _context.SaveChanges();
            }             

            user.Karma_points += item.KarmaPoints;

            _context.Item.Remove(item);
            await _context.SaveChangesAsync();

            

            return RedirectToAction("index");
        }
    }
}
