using System;
using System.Collections.Generic;
using kinder_app.Models;
using System.Collections;

namespace kinder_app.Models
{
    public class ItemDTO : IEnumerable<ItemDTO>
    {
        public int ID { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public ConditionEnum Condition { get; set; }
        public CategoryEnum Category { get; set; }
        public string UserID { get; set; }

        /*
        public int Length { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        
        
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
        public String Description { get; set; }
        public String Name { get; set; }
        //public String Tag { get; set; }
        //public string Description { get; set; }
        //public LinkedList<Byte[]> Photos;
        public int Points { get; set; }
        public String Cat { get; set; }
        public Boolean NSFW { get; set; } // 0 SFW, 1 NSFW
        public Boolean State { get; set; } = false;// 0 private, 1 public
        public CategoryEnum Filter { get; set; }

        private List<ItemDTO> myList = new();

        public IEnumerator<ItemDTO> GetEnumerator()
        {
            return myList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public override string ToString()
        {
            string str = "";

            str += ID.ToString() + ';';
            str += DateOfPurchase.ToString("yyyy-MM-dd") + ';';
            str += Condition.ToString() + ';';
            str += Category.ToString() + ';';
            //str += Size.ToString() + ';';
            //str += KarmaPoints.ToString() + ';';
            //str += Name.ToString() + ';';
            str += Description;
            //str += Tag;

            return str;
        }
    }
}
