using Fan.Abp.Ddd.Application.Queries;
using Volo.Abp.Application.Dtos;

namespace Fan.Abp.Ddd.Application.QueryHandlers
{
    public abstract class PagedListQueryHandler<TQuery, TResultDto> : QueryHandler<TQuery, PagedResultDto<TResultDto>>,
        IPagedListQueryHandler<TQuery, TResultDto>
        where TQuery : PagedListQuery<TResultDto>
    {

    }
}
