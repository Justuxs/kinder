using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kinder_app.Models
{
    public class ElectronicsItem : Item
    {
        public ElectronicsItem()
        {
            Points = 300;
        }

        public String Manufacturer;
        public ElectronicsTags Tag;

        public override string ToString()
        {
            string str = "";

            str += ID.ToString() + ';';
            str += DateOfPurchase.ToString("yyyy-MM-dd") + ';';
            str += Condition.ToString() + ';';
            str += Category.ToString() + ';';
            str += UserID.ToString() + ';';
            str += Points.ToString() + ';';
            str += Name + ';';
            str += Description;
            str += Tag.ToString();

            str += Manufacturer + ';';

            return str;
        }
    }

    public enum ElectronicsTags
    {
        [Display(Name = "Phone")]
        Phone,
        [Display(Name = "Computer")]
        Computer,
        [Display(Name = "Console")]
        Console,
        [Display(Name = "Audio")]
        Audio,
        [Display(Name = "Video")]
        Video,
        [Display(Name = "Household")]
        Household,
        [Display(Name = "Other")]
        Other
    }
}