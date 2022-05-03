﻿using System;
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
    }
}
