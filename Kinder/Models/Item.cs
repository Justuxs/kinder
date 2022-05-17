using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.ComponentModel.DataAnnotations;

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
            //str += Size.ToString() + ';';
            //str += KarmaPoints.ToString() + ';';
            str += Name.ToString() + ';';
            str += Description;

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

        public String Cat { get; set; }
        public Boolean NSFW { get; set; } // 0 SFW, 1 NSFW
        public Boolean State { get; set; } = false;// 0 private, 1 public
        public CategoryEnum Filter { get; set; }

        //public String Tag { get; set; }
        public string UserID { get; set; }


        /*
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
        */

        public String Name { get; set; }

        public String Description { get; set; }
        public int Points { get; set; } //!!!!!!!!!! turetu static but bet problemos kyla, pamirsau kaip sprest ir ntr laiko db aiskintis
                                        // public int [] PhotoIDs{ get; set; }
        public String GivenTo { get; set; }
    }

    public enum State
    {
        [Display(Name = "Private")]
        Tops,
        [Display(Name = "Bottoms")]
        Bottoms,
    }

}
