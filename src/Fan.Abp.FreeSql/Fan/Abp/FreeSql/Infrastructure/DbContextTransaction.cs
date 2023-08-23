using System.Data.Common;
using JetBrains.Annotations;

namespace Fan.Abp.FreeSql.Infrastructure
{
    internal class DbContextTransaction : IDbContextTransaction
    {
        [CanBeNull] private readonly DbTransaction _dbTransaction;

        public DbContextTransaction([CanBeNull] DbTransaction dbTransaction)
        {
            _dbTransaction = dbTransaction;
        }

        public DbTransaction GetDbTransaction()
        {
            return _dbTransaction;
        }
    }
}
