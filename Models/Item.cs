using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace kinder_app.Models
{
    public class Item : IComparable<Item>, IEquatable<Item>
    {
        public Item()
        {

        }

        public Item(int id)
        {
            this.ID = id;
        }

        public int CompareTo(Item other)
        {
            return other.ID.CompareTo(this.ID); // default: high to low
        }

        public override string ToString()
        {
            string str = "";

            str += ID.ToString() + ';';
            str += DateOfPurchase.ToString("yyyy-MM-dd") + ';';
            str += Condition.ToString() + ';';
            str += Category.ToString() + ';';
            str += UserID.ToString() + ';';
            str += Size.ToString() + ';';
            str += KarmaPoints.ToString() + ';';
            str += Name.ToString() + ';';
            str += Description.ToString();

            return str;
        }

        public bool Equals(Item other)
        {
            if (other == null) return false;
            return (this.ID.Equals(other.ID));
        }

        public int ID { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public ConditionEnum Condition { get; set; }
        public CategoryEnum Category { get; set; }
        public int UserID { get; set; }
        public int Length { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        [NotMapped]
        public Dimensions Size 
        { 
            get 
            { 
                return new Dimensions(this.Length, this.Width, this.Height); 
            }

            set
            {
                this.Length = value.Length;
                this.Width = value.Width;
                this.Height = value.Height;
            }
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int KarmaPoints { get; set; }
    }

}
