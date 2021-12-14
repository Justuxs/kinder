using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using kinder_app.Data;
using kinder_app.Models;
using Microsoft.AspNetCore.Authorization;
using kinder_app.Aspects;

namespace kinder_app.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [LogAspect]
        private int AggregateSuming(List<ItemDTO> items, int someting=1)
        {
            //REQUIREMENT: aggregate
            return items.Select(x => x.KarmaPoints).Aggregate((a, b) => a + b);
        }

        
        public async Task<IActionResult> Index()
        {
            //REQUIREMENT: select
            List<ItemDTO> list = await _context.Item
                .Where(x => x.UserID == CurrentUserExtention.GetUserID(this.User))
                .Select(x => new ItemDTO
                {
                    ID = x.ID,
                    DateOfPurchase = x.DateOfPurchase,
                    Condition = x.Condition,
                    Category = x.Category,
                    Length = x.Length,
                    Height = x.Height,
                    Width = x.Width,
                    Name = x.Name,
                    Description = x.Description,
                    KarmaPoints = x.KarmaPoints
                })
                .ToListAsync();

            TempData["sum"] = AggregateSuming(list);
            
            return View(list);
        }

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

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DateOfPurchase,Condition,Category,Length,Height,Width,Name,Description,KarmaPoints")] Item item)
        {
            if (ModelState.IsValid)
            {
                item.UserID = CurrentUserExtention.GetUserID(this.User);
                //REQUIREMENT: insert
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DateOfPurchase,Condition,Category,Length,Height,Width,Name,Description,KarmaPoints")] Item item)
        {
            item.UserID = CurrentUserExtention.GetUserID(this.User);

            if (id != item.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //REQUIREMENT: update
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ID))
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
            return View(item);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
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

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Item.FindAsync(id);
            //REQUIREMENT: delete
            _context.Item.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.ID == id);
        }
    }
}
