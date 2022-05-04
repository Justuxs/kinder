using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace kinder_app.Models
{
    public class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string UserID { get; set; }
        [Required]
        public string ReceiverID { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime Date { get; set; }

        public string ChatHub { get; set; }


        //  public virtual AppUser Sender { get; set; }
        //  public virtual AppUser Receiver { get; set; }

        public Message(string user, string receiver, string text)
        {
            UserID = user;
            ReceiverID = receiver;
            Text = text;
            Date = DateTime.Now;
        }
        public Message()
        {

        }

    }
}
