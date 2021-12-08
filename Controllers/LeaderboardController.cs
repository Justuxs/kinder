using kinder_app.Data;
using kinder_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kinder_app.Controllers
{
    public class LeaderboardController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public LeaderboardController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: LeaderboardController
        /*
        public IActionResult Index()
        {
            //
            return View(_context.ApplicationUsers.ToList());
        }*/

        public async Task<IActionResult> Index()
        {
            var users = from s in _context.ApplicationUsers
                        select s;
            users = users.OrderByDescending(s => s.Karma_points);
            return View(await users.AsNoTracking().Take(10).ToListAsync());
        }

    }
}
