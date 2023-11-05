using FluentAssertions;
using StoreProducts.Core.Product.Entity;
using StoreProducts.Core.User.Command;
using StoreProducts.Core.User.Query;

namespace StoreProducts.Test
{
    public class UserUnitTestCommandQuery
    {
        [Fact]
        public void ProductShouldNotNull()
        {
            var course = () => { var product = new Core.Product.Entity.Product(); };
            course.Should().NotBeNull();
        }

        [Fact]
        public void UserRegisterShouldValidation()
        {
            var course = new UserRegisterCommand
            {
                Email = "ehsanmrf@email.com",
                FullName = "EhsanMaarefvand",
                Password = "Passw0rd!",
                ConfirmPassword = "Passw0rd!"
            };
            course.Email.Should().MatchRegex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
            course.FullName.Should().NotBeNullOrEmpty();
            course.FullName.Should().MatchRegex(".{6,}");
            course.Password.Should().NotBeNullOrEmpty().And.BeEquivalentTo("Passw0rd!");
        }

        [Fact]
        public void UserLoginShouldValidate()
        {
            var course = new UserLoginQuery
            {
                Email = "ehsanmrf@email.com",
                Password = "Passw0rd!"
            };
            course.Email.Should()
                .NotBeNullOrEmpty().And
                .MatchRegex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");

            course.Password.Should()
                .NotBeNullOrEmpty().And
                .MatchRegex("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,}$");

        }
    }
}