using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace kinder_app.Models
{
    public class LikedItems
    {
        [Key]
        public int Key { get; set; }

        //Item that is liked
        [ForeignKey("dbo.Item")]
        public int ItemID { get; set; }

        //User that likes item
        [ForeignKey("dbo.AspNetUsers")]
        public string UserID { get; set; }
    }
}
