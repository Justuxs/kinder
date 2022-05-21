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
        public virtual List<Message> MessagesS { get; set; }
        public virtual List<Message> MessagesR { get; set; }
        public virtual List<Message> AllMessages { get; set; }
        public string ChatRoomName;
        public string ChatRoomStatus;
        public bool Approved1;
        public bool Approved2;



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

        public ChatRoom( string talker1Name, string talker2Name, List<Message>  allMessages,string status, bool bool1,bool bool2)
        {

            Talker1Name = talker1Name;
            Talker2Name = talker2Name;
            AllMessages = allMessages;
            ChatRoomStatus = status;
            Approved1 = bool1;
            Approved2 = bool2;
            Sort();
        }

        private void Sort()
        {
            AllMessages.OrderBy(mes =>mes.Date);
        }


    }
}
