using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols;

namespace kinder_app.Models
{
    public class FurnitureItem : Item
    {
        public FurnitureItem()
        {
            Points = 200;
        }

        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed.")]
        public int Length { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed.")]
        public int Height { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed.")]
        public int Width { get; set; }
        public FurnitureTags Tag;

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

            str += Length.ToString() + ';';
            str += Height.ToString() + ';';
            str += Width.ToString() + ';';

            return str;
        }
    }
    public enum FurnitureTags
    {
        [Display(Name = "Wardrobe")]
        Wardrobe,
        [Display(Name = "Table")]
        Table,
        [Display(Name = "Chair")]
        Chair,
        [Display(Name = "Bed")]
        Bed,
        [Display(Name = "Other")]
        Other
    }
}