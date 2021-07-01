using Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Services
{
    public class UserServiceTest
    {
        [Fact]
        public void is_user_email_confirmed_should_return_true_when_input_is_true()
        {
            //Arrange
            ApplicationUser user = new ApplicationUser();
            user.UserName = "patryk";
            user.EmailConfirmed = true;

            UserService service = new UserService();

            //Act
            bool isEmailConfirmed = service.IsUserEmailConfirmed(user);

            //Assert
            Assert.True(isEmailConfirmed);
        }

        [Fact]
        public void is_user_email_confirmed_should_return_false_when_input_is_false()
        {
            //Arrange
            ApplicationUser user = new ApplicationUser();
            user.UserName = "patryk";
            user.EmailConfirmed = false;

            UserService service = new UserService();

            //Act
            bool isEmailConfirmed = service.IsUserEmailConfirmed(user);

            //Assert
            Assert.False(isEmailConfirmed);
        }

    }
}
