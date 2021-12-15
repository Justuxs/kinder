using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kinder_app.Models;
using Xunit;

namespace KinderTests
{
    public class DimensionTests
    {
        [Theory]
        [InlineData(14, 15, 16)]
        [InlineData(0, -1, int.MinValue)]
        [InlineData(int.MaxValue, int.MaxValue, int.MaxValue)]
        public void Dimensions_Length_ValidDimensions(int x, int y, int z)
        {
            Dimensions dimensions = new Dimensions(x, y, z);

            Assert.Equal(x, dimensions.Length);
            Assert.Equal(y, dimensions.Height);
            Assert.Equal(z, dimensions.Width);
        }

        [Fact]
        public void Dimensions_ToString_ValidString()
        {
            Dimensions dimensions = new Dimensions(14, 15, 16);

            Assert.Equal("14,15,16", dimensions.ToString());
        }
    }
}
