using kinder_app.Data;
using kinder_app.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kinder_app.Controllers
{
    public class ChatExplorer : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChatExplorer(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(ControllerMethods.GetUsersChatHub(User.GetUserName(), _context));
        }

    }
}
