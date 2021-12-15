using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using kinder_app.Controllers;
using kinder_app.Data;
using kinder_app.Models;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace KinderTests
{
    public class ItemsControllerTest
    {
        [Fact]
        public void ItemsController_AggregateSumming_ValidList()
        {
            ItemsController itemsController = new ItemsController(null);
            List<ItemDTO> list = new List<ItemDTO>();

            Assert.Equal(0, itemsController.AggregateSuming(list));
        }

        [Fact]
        public void ItemsController_Edit_CreateView()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var mockContext = new Mock<ApplicationDbContext>(optionsBuilder.Options);
            var controller = new ItemsController(mockContext.Object);
            Assert.IsType<ViewResult>(controller.Create());
        }

        /*[Fact]
        public void ItemsController_Create_ValidItem() //async issue?
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var mockContext = new Mock<ApplicationDbContext>(optionsBuilder.Options);
            var controller = new ItemsController(mockContext.Object);
            Item item = new Item(50, DateTime.Now, ConditionEnum.Good, CategoryEnum.Education,
                "USER", 5, 5, 5, 25, "Thing", "Description of thing");
            controller.Create(item);
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void ItemsController_Edit_ItemNotFound()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var mockContext = new Mock<ApplicationDbContext>(optionsBuilder.Options);
            var controller = new ItemsController(mockContext.Object);
            Item item = new Item(50, DateTime.Now, ConditionEnum.Good, CategoryEnum.Education,
                "USER", 5, 5, 5, 25, "Thing", "Description of thing");
            controller.Edit(null);
            //Assert.Throws<NotImplementedException>(controller.Edit(null));
        }*/
    }
}
