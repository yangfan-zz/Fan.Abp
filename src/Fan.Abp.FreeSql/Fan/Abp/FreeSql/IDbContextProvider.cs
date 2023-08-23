using System.Threading.Tasks;

namespace Fan.Abp.FreeSql
{
    public interface IDbContextProvider<TDbContext>
        where TDbContext : IFreeSqlDbContext
    {
        Task<TDbContext> GetDbContextAsync();

        TDbContext GetDbContext();
    }
}
