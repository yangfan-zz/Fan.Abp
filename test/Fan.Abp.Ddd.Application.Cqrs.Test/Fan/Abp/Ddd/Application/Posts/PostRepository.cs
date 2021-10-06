using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Linq;

namespace Fan.Abp.Ddd.Application.Posts
{
    public class PostRepository : IPostRepository, ITransientDependency
    {
        private readonly List<Post> _post = new List<Post>();

        public PostRepository()
        {
            for (int i = 0; i < 30; i++)
            {
                _post.Add(new Post(i + 1, $"Post-title-{i + 1}"));
            }
        }

        public IEnumerator<Post> GetEnumerator()
        {
            return _post.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Type ElementType { get; }
        public Expression Expression { get; }
        public IQueryProvider Provider { get; }
        public Task<List<Post>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.FromResult(_post);
        }

        public Task<List<Post>> GetListAsync(Expression<Func<Post, bool>> predicate, bool includeDetails = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<long> GetCountAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.FromResult((long)_post.Count);
        }

        public Task<List<Post>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting, bool includeDetails = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IQueryable<Post> WithDetails()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Post> WithDetails(params Expression<Func<Post, object>>[] propertySelectors)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Post>> WithDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Post>> WithDetailsAsync(params Expression<Func<Post, object>>[] propertySelectors)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Post>> GetQueryableAsync()
        {
            return Task.FromResult(_post.AsQueryable());
        }

        public IAsyncQueryableExecuter AsyncExecuter { get; }
        public Task<Post> InsertAsync(Post entity, bool autoSave = false, CancellationToken cancellationToken = new CancellationToken())
        {

            _post.Add(entity);

            return Task.FromResult(entity);
        }

        public async Task InsertManyAsync(IEnumerable<Post> entities, bool autoSave = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entity in entities)
            {
                await InsertAsync(entity, cancellationToken: cancellationToken);
            }
        }

        public Task<Post> UpdateAsync(Post entity, bool autoSave = false, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.FromResult(entity);
        }

        public Task UpdateManyAsync(IEnumerable<Post> entities, bool autoSave = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Post entity, bool autoSave = false, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task DeleteManyAsync(IEnumerable<Post> entities, bool autoSave = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Post> FindAsync(Expression<Func<Post, bool>> predicate, bool includeDetails = true,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Post> GetAsync(Expression<Func<Post, bool>> predicate, bool includeDetails = true,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Expression<Func<Post, bool>> predicate, bool autoSave = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Post> GetAsync(int id, bool includeDetails = true, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<Post> FindAsync(int id, bool includeDetails = true, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id, bool autoSave = false, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task DeleteManyAsync(IEnumerable<int> ids, bool autoSave = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }
    }
}
