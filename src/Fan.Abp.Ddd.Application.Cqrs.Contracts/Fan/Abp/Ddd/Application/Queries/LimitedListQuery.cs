using Volo.Abp.Application.Dtos;

namespace Fan.Abp.Ddd.Application.Queries
{
    public abstract class LimitedListQuery<TResultDto> : LimitedResultRequestDto, IListQuery<TResultDto>
    {

    }
}
