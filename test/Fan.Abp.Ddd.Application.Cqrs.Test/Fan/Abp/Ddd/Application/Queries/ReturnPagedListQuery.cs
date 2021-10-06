using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fan.Abp.Ddd.Application.Posts;
using Fan.Abp.Ddd.Application.QueryHandlers;
using Volo.Abp.Application.Dtos;

namespace Fan.Abp.Ddd.Application.Queries
{
    public class ReturnPagedListQuery : PagedListQuery<Post>
    {
        public string Title { get; set; }
    }

    public class ReturnPagedListQueryHandler : PagedListQueryHandler<ReturnPagedListQuery, Post>
    {
        private readonly IPostRepository _postRepository;

        public ReturnPagedListQueryHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }


        public override async Task<PagedResultDto<Post>> HandleQueryAsync(ReturnPagedListQuery request,
            CancellationToken cancellationToken)
        {
            var list = (await _postRepository.GetListAsync(cancellationToken: cancellationToken));


            if (!request.Title.IsNullOrWhiteSpace())
            {
                list = list.Where(p => p.Title.Contains(request.Title)).ToList();
            }

            var count = list.Count();

            list = list.Skip(request.SkipCount).Take(request.MaxResultCount).ToList();
            return new PagedResultDto<Post>(count, list);
        }
    }
}
