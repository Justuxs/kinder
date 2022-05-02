using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kinder_app.Models
{
    public class ChatRoom
    {
        public string Talker1 { get; set; }
        public string Talker2 { get; set; }
        public string Talker1Name { get; set; }
        public string Talker2Name { get; set; }
        public virtual ICollection<Message> MessagesS { get; set; }
        public virtual ICollection<Message> MessagesR { get; set; }
        public virtual ICollection<Message> AllMessages { get; set; }


        public ChatRoom(string talker1, string talker1Name, string talker2, string talker2Name, List<Message> messages1, List<Message> messages2)
        {
            MessagesS = messages1;
            MessagesR = messages2;
            Talker1 = talker1;
            Talker2 = talker2;
            Talker1Name = talker1Name;
            Talker2Name = talker2Name;
            AllMessages = messages1;
            AllMessages.Concat(MessagesR);
            Sort();
        }

        private void Sort()
        {
            AllMessages.OrderBy(mes =>mes.Date);
        }


    }
}
