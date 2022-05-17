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
using kinder_app.Aspects;

namespace kinder_app.Controllers
{
    public static class ControllerMethods
    {
        [LogAspect]
        public static List<Item> GetGivenItems(string userID, ApplicationDbContext context)
        {
            return context.Item.Where(x => x.GivenTo == userID).ToList();
        }

        [LogAspect]
        public static Item GetItem(int id, ApplicationDbContext context)
        {
            return context.Item.Find(id);
        }

        [LogAspect]//
        public static ApplicationUser GetUser(Item item, ApplicationDbContext context)
        {
            return context.ApplicationUsers
                .FirstOrDefault(m => m.Id == item.UserID);
        }

        [LogAspect]//
        public static List<ApplicationUser> GetUsersForLeaderboard(ApplicationDbContext context)
        {
            var users = from s in context.ApplicationUsers
                        select s;
            users = users.OrderByDescending(s => s.Karma_points);

            return users.AsNoTracking().Take(10).ToList();
        }

        [LogAspect]//
        public static LikedItems GetLiked(Item item, ApplicationDbContext context)
        {
            return context.LikedItems
                .FirstOrDefault(m => m.ItemID == item.ID);
        }

        [LogAspect]//
        public static void RemoveLiked(LikedItems liked, ApplicationDbContext context)
        {
            context.LikedItems.Remove(liked);
            context.SaveChanges();
        }

        [LogAspect]//
        public static void RemoveAllLiked(Item item, ApplicationDbContext context)
        {
            while (GetLiked(item, context) != null)
            {
                RemoveLiked(GetLiked(item, context), context);
            }
        }

        [LogAspect]//
        public static void TakeItem(int id, ApplicationDbContext context)
        {
            var item = GetItem(id, context);
            var user = GetUser(item, context);

            RemoveAllLiked(item, context);

            user.Karma_points += item.KarmaPoints;

            context.Item.Remove(item);
            context.SaveChanges();
        }

        [LogAspect]//
        public static List<TransferModel> LikedModelList
            (string userID,
            List<Item> items, List<LikedItems> likedItems, List<IdentityUser> users)
        {
            //REQUIREMENT: join
            var join = likedItems.Where(x => x.UserID != userID)
                            .Join(
                            items,
                            liked => liked.ItemID,
                            item => item.ID,
                            (liked, item) => new
                            {
                                ItemID = item.ID,
                                ItemName = item.Name,
                                ItemDesc = item.Description,
                                UserID = liked.UserID,
                                ItemBelong = item.UserID
                            });

            var join2 = join.Where(x => x.ItemBelong == userID)
                .Join(
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

        [LogAspect]//
        public static void GiveItem(ApplicationDbContext context, string userID, int? id)
        {
            var uniqueItem = LikedModelList(userID,
                context.Item.ToList(), context.LikedItems.ToList(), context.Users.ToList())
                .FirstOrDefault(m => m.UniqID == id);

            var item = context.Item
                .FirstOrDefault(m => m.ID == uniqueItem.ItemID);

            var user = context.Users
                .FirstOrDefault(m => m.Email == uniqueItem.UserEmail);

            item.GivenTo = user.Id;
            context.Update(item);
            context.SaveChanges();
        }

        [LogAspect]
        public static List<Item> GetItemsSwiping(ApplicationDbContext context, string userID)
        {
            List<Item> itemList = context.Item.ToList();
            List<LikedItems> likedList = context.LikedItems.ToList();

            var filteredLiked = likedList.Where(x => x.UserID == userID)
                                         .Select(x => x.ItemID);

            itemList = itemList.Where(x => !(filteredLiked.Contains(x.ID) ||
                                      x.UserID == userID)).ToList();

            return itemList;
        }

        [LogAspect]
        public static List<int> LikeItem(ApplicationDbContext context, int currentID, string userID, List<int> likeds)
        {
            LikedItems liked = new();
            liked.ItemID = currentID;
            liked.UserID = userID;

            likeds.Add(0 + currentID);

            //REQUIREMENT: insert
            context.Entry(liked).State = EntityState.Added;
            context.SaveChanges();

            return likeds;
        }
        // [LogAspect]
        /* public static List<ChatRoom> GetUserMessages(ApplicationDbContext context, int currentID)
         {
             string userID = currentID + "";

             List<Message> users = context.Message.Where(x => x.UserID.Equals(userID)).ToList();
             List<Message> users = context.Message.Where(x => x.UserID.Equals(userID)).ToList();

             //context.Entry(liked).State = EntityState.Added;
             context.SaveChanges();

            // return likeds;
         }*/
        [LogAspect]
        public static List<ChatHub> GetUsersChatHub(string userN, ApplicationDbContext context)
        {
            RemoveOldChatHubs(userN, context);
            return context.ChatHubs.Where(x => (x.ReceiverID.Equals(userN) || x.SenderID.Equals(userN)) && x.Status!="Blocked" && !(x.Status == "Pending" && x.SenderID.Equals(userN))).ToList();
        }
        public static void RemoveOldChatHubs(string userN, ApplicationDbContext context)
        {
            DateTime date = DateTime.Now;
            List<ChatHub> Oldchats = context.ChatHubs.Where(x => (x.ReceiverID.Equals(userN) || x.SenderID.Equals(userN)) && x.Status != "Pending" && (date - x.Date).TotalDays>ApplicationDbContext.DaysPendingCHat).ToList();
            if (Oldchats.Count == 0) return;
            foreach(var chat in Oldchats)
            {
                context.Remove(chat);
            }
            context.SaveChanges();
        }
        [LogAspect]
        public static void CreateChatHub(string userN,string ownerN, string itemN, ApplicationDbContext context)
        {
            string ChatName = ownerN + "-" + itemN+"-"+ userN;
            Console.WriteLine("Chat hub creating 1");
            ChatHub Exist = context.ChatHubs.Where(x => x.Name.Equals(ChatName)).ToList().FirstOrDefault();
            Console.WriteLine("Chat hub creating 2");

            if (Exist == null)
            {
                context.Add(new ChatHub(ownerN, userN, ChatName));
                Console.WriteLine("Chat hub crated");
                context.SaveChanges();
            }
            Console.WriteLine("Chat hub creating 3");

        }
        [LogAspect]
        public static ChatRoom GetChatRoom(string UserName, string ChatHubName, ApplicationDbContext context)
        {
            List<Message> AllMessages = context.Messages.Where(x => x.ChatHub.Equals(ChatHubName)).ToList();
            List<Message> CurrentUserMes = AllMessages.Where(x => x.UserID.Equals(UserName)).ToList();
            List<Message> NextMes = AllMessages.Where(x => (!x.UserID.Equals(UserName))).ToList();
            ChatHub chat= context.ChatHubs.Where(x => x.Name.Equals(ChatHubName)).FirstOrDefault();
            if (chat == null) return null;
            string nextName;
            if (!chat.ReceiverID.Equals(UserName)) nextName = chat.ReceiverID;
            else nextName = chat.SenderID;
            return new ChatRoom(UserName, nextName, AllMessages,chat.Status);
        }
        [LogAspect]
        public static void SaveMessage(Message message, ApplicationDbContext context)
        {
            if (message.ReceiverID.Length != 0 && message.UserID.Length != 0)
            {
                context.Messages.Add(message);
                context.SaveChanges();
            }
        }
        [LogAspect]
        public static void ChangeChatStatus(string chatName, string status, ApplicationDbContext context)
        {
            ChatHub chathub = context.ChatHubs.Where(x => x.Name.Equals(chatName)).First();
            if (chathub == null) return;
            chathub.Status = status;
            context.Update(chathub);
            context.SaveChanges();
        }

    }
}
