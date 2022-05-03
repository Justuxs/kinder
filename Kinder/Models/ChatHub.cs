using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kinder_app.Models
{
    public class ChatHub
    {
        public string SenderID { get; set; }

        public string ReceiverID { get; set; }
        [Key]
        public string Name { get; set; }

        public DateTime Date;

        public int Status;
        public ChatHub() { }
        public ChatHub(string IownerN, string writerN, string chatN)
        {
            SenderID = IownerN;
            ReceiverID = writerN;
            Name = chatN;
            Status = 0;
            Date = DateTime.Now;
        }
    }
}