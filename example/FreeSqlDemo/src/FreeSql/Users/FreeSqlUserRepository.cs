using Fan.Abp.Domain.Repositories.FreeSql;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fan.Abp.FreeSql;
using FreeSqlDemo.Domain.Users;
using Volo.Abp.DependencyInjection;

namespace FreeSqlDemo.FreeSql.Users
{
    public class FreeSqlUserRepository: FreeSqlRepository<DemoSqlDbContext,User>, IUserRepository,ITransientDependency
    {
        public FreeSqlUserRepository(IDbContextProvider<DemoSqlDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public Task<User> GetAsync(string id, bool includeDetails = true, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<User> FindAsync(string id, bool includeDetails = true, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id, bool autoSave = false, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task DeleteManyAsync(IEnumerable<string> ids, bool autoSave = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }
    }
}
