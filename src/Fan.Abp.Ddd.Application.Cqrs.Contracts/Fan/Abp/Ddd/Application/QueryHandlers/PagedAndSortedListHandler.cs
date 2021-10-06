using Fan.Abp.Cqrs.Queries;
using Fan.Abp.Ddd.Application.Queries;
using Volo.Abp.Application.Dtos;

namespace Fan.Abp.Ddd.Application.QueryHandlers
{
    public  interface IPagedAndSortedListHandler<in TQuery, TResultDto> : IQueryHandler<TQuery, PagedResultDto<TResultDto>>
        where TQuery : PagedAndSortedListQuery<TResultDto>
    {

    }
}
