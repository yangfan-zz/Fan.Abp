using Fan.Abp.Cqrs.Queries;
using Fan.Abp.Ddd.Application.Queries;
using Volo.Abp.Application.Dtos;

namespace Fan.Abp.Ddd.Application.QueryHandlers
{
    public interface IListQueryHandler<in TQuery, TResultDto> : IQueryHandler<TQuery, ListResultDto<TResultDto>>
        where TQuery : IListQuery<TResultDto>
    {

    }
}
