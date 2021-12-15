using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kinder_app.Controllers;
using kinder_app.Data;
using kinder_app.Models;
using Xunit;

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
    }
}
