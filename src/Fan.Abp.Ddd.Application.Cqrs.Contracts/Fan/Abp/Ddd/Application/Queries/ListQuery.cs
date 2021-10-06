using Fan.Abp.Cqrs.Queries;
using Volo.Abp.Application.Dtos;

namespace Fan.Abp.Ddd.Application.Queries
{
    public abstract class ListQuery<TResult> : IQuery<ListResultDto<TResult>>
    {

    }
}
