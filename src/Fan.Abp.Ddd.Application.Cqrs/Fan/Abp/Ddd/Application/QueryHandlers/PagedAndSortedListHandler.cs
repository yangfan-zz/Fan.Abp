using Fan.Abp.Ddd.Application.Queries;
using Volo.Abp.Application.Dtos;

namespace Fan.Abp.Ddd.Application.QueryHandlers
{
    public abstract class PagedAndSortedListHandler<TQuery, TResultDto> : QueryHandler<TQuery, PagedResultDto<TResultDto>>,
        IPagedAndSortedListHandler<TQuery, TResultDto>
        where TQuery : PagedAndSortedListQuery<TResultDto>
    {

    }
}
