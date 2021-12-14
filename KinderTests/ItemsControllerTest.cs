using kinder_app.Controllers;
using kinder_app.Data;
using kinder_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KinderTests
{
    public class ItemsControllerTest
    {
        [Fact]
        public void AggregateSumming()
        {
            ItemsController itemsController = new ItemsController(null);
            List<ItemDTO> list = new List<ItemDTO>();

            Assert.Equal(0, itemsController.AggregateSuming(list));
        }
    }
}
