using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kinder_app.Models
{
    public class ClothingItem : Item
    {
        public ClothingItem()
        {
            Points = 50;
        }
        public ClothingTags Tag;
        public Sex Sex;
        public ClothingSize Size;

        public override string ToString()
        {
            string str = "";

            str += ID.ToString() + ';';
            str += DateOfPurchase.ToString("yyyy-MM-dd") + ';';
            str += Condition.ToString() + ';';
            str += Category.ToString() + ';';
            str += UserID.ToString() + ';';
            str += Points.ToString() + ';';
            str += Name.ToString() + ';';
            str += Description;
            str += Tag.ToString();

            str += Sex.ToString();
            str += Size.ToString();

            return str;
        }
    }

    public enum ClothingTags
    {
        [Display(Name = "Tops")]
        Tops,
        [Display(Name = "Bottoms")]
        Bottoms,
        [Display(Name = "Shoes")]
        Shoes,
        [Display(Name = "Hats")]
        Hats,
        [Display(Name = "Accessories")]
        Accessories,
        [Display(Name = "Other")]
        Other
    }

    public enum Sex
    {
        [Display(Name = "Male")]
        Male,
        [Display(Name = "Female")]
        Female,
        [Display(Name = "Unisex")]
        Unisex,
    }

    public enum ClothingSize
    {
        [Display(Name = "XS")]
        XS,
        [Display(Name = "S")]
        S,
        [Display(Name = "M")]
        M,
        [Display(Name = "L")]
        L,
        [Display(Name = "XL")]
        XL,
        [Display(Name = "XXL")]
        XXL,
        [Display(Name = "XXXL")]
        XXXL
    }
}