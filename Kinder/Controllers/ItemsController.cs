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
        public int AggregateSuming(List<ItemDTO> items)
        {
            if (items.Count() > 0)
            {
                //REQUIREMENT: aggregate
                return items.Aggregate<ItemDTO, int>(0, (TotalKarma, item) => TotalKarma + item.Points);
            }
            else
            {
                return 0;
            }
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
                    Cat = x.Cat,
                    UserID = x.UserID,
                    NSFW = x.NSFW,
                    State = x.State,
                    /*
                    Length = x.Length,
                    Height = x.Height,
                    Width = x.Width,
                    */
                    Name = x.Name,
                    Description = x.Description,
                    Points = x.Points
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
            else if (item.Cat == "Furniture")
                return RedirectToAction(nameof(FurnitureEdit));
            else if (item.Cat == "Electronics")
                return RedirectToAction(nameof(ElectronicsEdit));
            else if (item.Cat == "Clothing")
                return RedirectToAction(nameof(ClothingEdit));
            else
                return View(item);
        }

        /*
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
        */

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DateOfPurchase,Condition,Category,Length,Height,Width,Name,Cat,Description,KarmaPoints")] Item item)
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

        [Authorize]
        public IActionResult SelectCategory()
        {
            return View();
        }
        [Authorize]
        public IActionResult UploadPhotos()
        {
            return View();
        }

        //////////// Furniture /////////////
        [Authorize]
        public IActionResult FurnitureDesc()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FurnitureDesc([Bind("ID,DateOfPurchase,Condition,Category,Name,Height,Width,Cat,NSFW,State,LengthDescription,Points")] FurnitureItem item)
        {
            if (ModelState.IsValid)
            {
                item.UserID = CurrentUserExtention.GetUserID(this.User);
                //REQUIREMENT: insert
                item.Cat = "Furniture";
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        [Authorize]
        public async Task<IActionResult> FurnitureEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.FurnitureItem.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FurnitureEdit(int id, [Bind("ID,DateOfPurchase,Condition,Category,Name,Height,Cat,NSFW,State,Width,LengthDescription,Points")] FurnitureItem item)
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

        //////////// Electronics /////////////
        [Authorize]
        public IActionResult ElectronicsDesc()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ElectronicsDesc([Bind("ID,DateOfPurchase,Condition,Cat,NSFW,State,Category,Name,Description,Points,Manufacturer")] ElectronicsItem item)
        {
            if (ModelState.IsValid)
            {
                item.UserID = CurrentUserExtention.GetUserID(this.User);
                //REQUIREMENT: insert
                item.Cat = "Electronics";
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        [Authorize]
        public async Task<IActionResult> ElectronicsEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.ElectronicsItem.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ElectronicsEdit(int id, [Bind("ID,DateOfPurchase,Condition,Cat,NSFW,State,Category,Name,Description,Points,Manufacturer")] ElectronicsItem item)
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

        //////////// Clothing /////////////
        [Authorize]
        public IActionResult ClothingDesc()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClothingDesc([Bind("ID,DateOfPurchase,Condition,Cat,NSFW,State,Category,Name,Description,Points")] ClothingItem item)
        {
            if (ModelState.IsValid)
            {
                item.UserID = CurrentUserExtention.GetUserID(this.User);
                //REQUIREMENT: insert
                item.Cat = "Clothing";
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        [Authorize]
        public async Task<IActionResult> ClothingEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.ClothingItem.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClothingEdit(int id, [Bind("ID,DateOfPurchase,Condition,Cat,NSFW,State,Category,Name,Description,Points")] ClothingItem item)
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
    }
}
