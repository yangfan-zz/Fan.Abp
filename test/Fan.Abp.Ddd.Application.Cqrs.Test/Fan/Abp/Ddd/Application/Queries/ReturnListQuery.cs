using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fan.Abp.Ddd.Application.Posts;
using Fan.Abp.Ddd.Application.QueryHandlers;
using Volo.Abp.Application.Dtos;

namespace Fan.Abp.Ddd.Application.Queries
{
    public class ReturnListQuery : ListQuery<Post>, IListQuery<Post>
    {
        public string Title { get; set; }
    }


    public class ReturnListQueryHandler : ListQueryHandler<ReturnListQuery, Post>
    {
        private readonly IPostRepository _postRepository;

        public ReturnListQueryHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public override async Task<ListResultDto<Post>> HandleQueryAsync(ReturnListQuery request, CancellationToken cancellationToken)
        {
            var list = (await _postRepository.GetListAsync(cancellationToken: cancellationToken));

            if (!request.Title.IsNullOrWhiteSpace())
            {
                list = list.Where(p => p.Title.Contains(request.Title)).ToList();
            }

            return new ListResultDto<Post>(list);
        }
    }
}
