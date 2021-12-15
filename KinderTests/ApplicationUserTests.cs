using System;
using kinder_app.Models;
using Xunit;

namespace KinderTests
{
    public class ApplicationUserTests
    {
        [Fact]
        public void ApplicationUser_Create_Creation()
        {
            ApplicationUser applicationUser = new ApplicationUser();

            Assert.NotNull(applicationUser);
        }
    }
}
