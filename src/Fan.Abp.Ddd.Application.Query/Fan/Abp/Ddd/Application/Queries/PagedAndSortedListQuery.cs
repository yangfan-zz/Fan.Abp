using Fan.Abp.Cqrs.Queries;
using Volo.Abp.Application.Dtos;

namespace Fan.Abp.Ddd.Application.Queries
{
    public abstract class PagedAndSortedListQuery<TResultDto> : PagedAndSortedResultRequestDto,
        IQuery<PagedResultDto<TResultDto>>
    {

    }
}
