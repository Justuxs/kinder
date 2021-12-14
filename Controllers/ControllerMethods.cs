using kinder_app.Data;
using kinder_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kinder_app.Controllers
{
    public static class ControllerMethods
    {
        public static List<Item> GetGivenItems(string userID, ApplicationDbContext context)
        {
            return context.Item.Where(x => x.GivenTo == userID).ToList();
        }

        public static Item GetItem(int id, ApplicationDbContext context)
        {
            return context.Item.Find(id);
        }

        public static ApplicationUser GetUser(int id, Item item, ApplicationDbContext context)
        {
            return context.ApplicationUsers
                .FirstOrDefault(m => m.Id == item.UserID);
        }
    }
}
