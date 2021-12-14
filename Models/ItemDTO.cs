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

        public string Name { get; set; }
        public string Description { get; set; }
        public int KarmaPoints { get; set; }

        private List<ItemDTO> myList = new();

        public IEnumerator<ItemDTO> GetEnumerator()
        {
            return myList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
