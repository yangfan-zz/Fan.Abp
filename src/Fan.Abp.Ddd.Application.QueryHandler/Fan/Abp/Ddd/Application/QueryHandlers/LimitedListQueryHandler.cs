using Fan.Abp.Ddd.Application.Queries;

namespace Fan.Abp.Ddd.Application.QueryHandlers
{
    public abstract class LimitedListQueryHandler<TQuery, TResultDto> : ListQueryHandler<TQuery, TResultDto>,
        ILimitedListQueryHandler<TQuery, TResultDto>
        where TQuery : LimitedListQuery<TResultDto>
    {

    }
}
