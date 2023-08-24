using Volo.Abp.Domain.Repositories;

namespace FreeSqlDemo.Domain.Users
{
    public interface IUserRepository : IRepository<User, string>
    {

    }
}
