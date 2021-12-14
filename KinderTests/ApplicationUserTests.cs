using kinder_app.Models;
using System;
using Xunit;

namespace KinderTests
{
    public class ApplicationUserTests
    {
        [Fact]
        public void CreateApplicationUser()
        {
            ApplicationUser applicationUser = new ApplicationUser();

            Assert.NotNull(applicationUser);
        }
    }
}
