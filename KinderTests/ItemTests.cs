using kinder_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KinderTests
{
    public class ItemTests
    {
        [Theory]
        [InlineData(50, 25, -1)]
        [InlineData(50, 50, 0)]
        [InlineData(25, 50, 1)]
        public void ItemCompareTo(int id1, int id2, int expected)
        {
            Item item1 = new Item(id1);
            Item item2 = new Item(id2);

            Assert.Equal(expected, item1.CompareTo(item2));
        }

        [Fact]
        public void ItemToString()
        {
            Item item = new Item(50, DateTime.Now, ConditionEnum.Good, CategoryEnum.Education, "USER", 5, 5, 5, 25, "Thing", "Description of thing");

            Assert.Equal("50;" + DateTime.Now.ToString("yyyy-MM-dd") + ";Good;Education;USER;5,5,5;25;Thing;Description of thing", item.ToString());
        }

        [Theory]
        [InlineData(50, null, false)]
        [InlineData(50, 50, true)]
        [InlineData(50, 25, false)]
        public void ItemEquals(int id1, int id2, bool expected)
        {
            Item item1 = new Item(id1);
            Item item2 = new Item(id2);

            Assert.Equal(expected, item1.Equals(item2));
        }
    }
}
