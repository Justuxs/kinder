using kinder_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KinderTests
{
    public class DimensionTests
    {
        [Fact]
        public void DimensionsLength()
        {
            Dimensions dimensions = new Dimensions(14, 15, 16);

            Assert.Equal(14, dimensions.Length);
        }

        [Fact]
        public void DimensionsToString()
        {
            Dimensions dimensions = new Dimensions(14, 15, 16);

            Assert.Equal("14,15,16", dimensions.ToString());
        }
    }
}
