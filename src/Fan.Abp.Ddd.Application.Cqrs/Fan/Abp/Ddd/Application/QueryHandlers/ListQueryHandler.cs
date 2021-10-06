using Fan.Abp.Ddd.Application.Queries;
using Volo.Abp.Application.Dtos;

namespace Fan.Abp.Ddd.Application.QueryHandlers
{
    public abstract class ListQueryHandler<TQuery, TResultDto> : QueryHandler<TQuery, ListResultDto<TResultDto>>,
        IListQueryHandler<TQuery, TResultDto>
        where TQuery : IListQuery<TResultDto>
    {

    }
}
