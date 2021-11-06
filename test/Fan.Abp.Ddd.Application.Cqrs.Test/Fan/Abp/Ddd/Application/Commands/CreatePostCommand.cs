using System.Threading;
using System.Threading.Tasks;
using Fan.Abp.Cqrs.Commands;
using Fan.Abp.Ddd.Application.CommandHandlers;
using Fan.Abp.Ddd.Application.Posts;
using Volo.Abp.Application.Dtos;

namespace Fan.Abp.Ddd.Application.Commands
{
    public class PostDto : EntityDto<int>
    {
        public string Title { get; set; }
    }

    public class CreatePostCommand<TResult> : CreateCommand<int, TResult>
     where TResult : PostDto
    {
        public string Title { get; set; }
    }

    public class CreatePostCommandHandler<TResult> : CreateCommandHandler<Post, int, CreatePostCommand<TResult>, TResult>
    where TResult : PostDto
    {
        public override async Task<TResult> HandleCommandAsync(CreatePostCommand<TResult> request, CancellationToken cancellationToken)
        {
            var entity = await Repository.InsertAsync(new Post(request.Id, request.Title), cancellationToken: cancellationToken);

            return (TResult)new PostDto {Id = entity.Id, Title = entity.Title};
        }
    }
}
