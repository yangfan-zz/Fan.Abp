using Fan.Abp.Cqrs.Commands;
using Volo.Abp.Application.Services;

namespace Fan.Abp.Ddd.Application.CommandHandlers
{
    public abstract class ApplicationCommandHandlerBase : ApplicationService, ICommandHandler
    {

    }
}
