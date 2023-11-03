namespace StoreProducts.Infrastructure.User.Builder
{
    public class UserBuilder : IUserBuilder
    {
        private string FullName { get; set; }
        private string UserName { get; set; }
        private string Email { get; set; }

        public IUserBuilder WithFullName(string fullName)
        {
            FullName = fullName;
            return this;
        }

        public IUserBuilder WithUserName(string userName)
        {
            UserName = userName;
            return this;
        }

        public IUserBuilder WithEmail(string email)
        {
            Email = email;
            return this;
        }

        public Core.User.Entity.User Build()
        {
            return new Core.User.Entity.User
            {
                FullName = FullName,
                UserName = Email,
                Email = Email,
            };
        }
    }
}