using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Fan.Abp.Cqrs.Commands;
using Fan.Abp.Ddd.Application.CommandHandlers;

namespace Fan.Abp.Ddd.Application.Commands
{
    public class NoReturnValueCommand : Command
    {
        public NoReturnValueCommand()
        {

        }

        public NoReturnValueCommand(string content)
        {
            Content = content;
        }

        [Required]
        public string Content { get;  }

        public int ExecuteCount { get; set; }
    }

    public class NoReturnValueCommandHandle : CommandHandler<NoReturnValueCommand>
    {
        public override Task HandleCommandAsync(NoReturnValueCommand command, CancellationToken cancellationToken)
        {
            command.ExecuteCount += 1;

            return Task.CompletedTask;
        }
    }
}
