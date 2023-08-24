using Volo.Abp.Domain.Entities;

namespace FreeSqlDemo.Domain.Users
{
    public class User : Entity<string>
    {
        public User()
        {

        }

        public User(string id, string userName) : base(id)
        {
            UserName = userName;
        }

        public string? UserName { get; set; }
    }
}
