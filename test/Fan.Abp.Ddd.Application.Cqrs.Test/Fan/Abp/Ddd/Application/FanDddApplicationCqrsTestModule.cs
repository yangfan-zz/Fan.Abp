using Fan.Abp.Cqrs;
using Fan.Abp.Cqrs.Commands;
using Fan.Abp.Ddd.Application.Commands;
using Fan.Abp.Ddd.Application.Posts;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Modularity;

namespace Fan.Abp.Ddd.Application
{
    [DependsOn(typeof(AbpAutofacModule))]
    [DependsOn(typeof(AbpTestBaseModule), typeof(FanDddApplicationCqrsModule),typeof(FanCqrsMediatRModule))]
    public class FanDddApplicationCqrsTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddTransient<IRepository<Post, int>, PostRepository>();

            context.Services.AddTransient(typeof(ICommandHandler<CreatePostCommand<PostDto>, PostDto>),
                typeof(CreatePostCommandHandler<PostDto>));
        }
    }
}
