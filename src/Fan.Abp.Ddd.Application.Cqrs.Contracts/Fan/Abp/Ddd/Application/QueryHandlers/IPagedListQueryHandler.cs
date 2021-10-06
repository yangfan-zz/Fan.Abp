using Fan.Abp.Cqrs.Queries;
using Fan.Abp.Ddd.Application.Queries;
using Volo.Abp.Application.Dtos;

namespace Fan.Abp.Ddd.Application.QueryHandlers
{
    public interface IPagedListQueryHandler<in TQuery, TResultDto> : IQueryHandler<TQuery, PagedResultDto<TResultDto>>
        where TQuery : PagedListQuery<TResultDto>
    {

    }
}
