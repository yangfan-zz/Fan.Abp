using System.Data.Common;

namespace Fan.Abp.FreeSql.Infrastructure
{
    public interface IDbContextTransaction
    {
        DbTransaction GetDbTransaction();
    }
}
