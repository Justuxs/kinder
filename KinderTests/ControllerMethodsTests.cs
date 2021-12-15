using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kinder_app.Controllers;
using kinder_app.Data;
using kinder_app.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace KinderTests
{
    public class ControllerMethodsTests
    {
        /*[Fact]
        public void ControllerMethods_GetItem()
        {
            var data = new List<Item>
            {
                new Item {ID = 50, DateOfPurchase = DateTime.Now, Condition = ConditionEnum.Good, Category = CategoryEnum.Education,
                UserID = "USER", Length = 5, Height = 5, Width = 5, KarmaPoints = 25, Name = "Thing", Description = "Description of thing"}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Item>>();
            mockSet.As<IQueryable<Item>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Item>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Item>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Item>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            
            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Item).Returns(mockSet.Object);
            
            Assert.IsType<Item>(ControllerMethods.GetItem(1, mockContext.Object));
        }*/
    }
}
