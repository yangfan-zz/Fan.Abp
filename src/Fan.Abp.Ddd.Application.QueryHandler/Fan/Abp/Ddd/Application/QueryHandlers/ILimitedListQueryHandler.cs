using Fan.Abp.Ddd.Application.Queries;

namespace Fan.Abp.Ddd.Application.QueryHandlers
{
    public interface ILimitedListQueryHandler<in TQuery, TResultDto> : IListQueryHandler<TQuery, TResultDto>
        where TQuery : LimitedListQuery<TResultDto>
    {

    }
}
