using Fan.Abp.Cqrs.Queries;
using Volo.Abp.Application.Dtos;

namespace Fan.Abp.Ddd.Application.Queries
{
    public interface IListQuery<TDto> : IQuery<ListResultDto<TDto>>
    {

    }
}
