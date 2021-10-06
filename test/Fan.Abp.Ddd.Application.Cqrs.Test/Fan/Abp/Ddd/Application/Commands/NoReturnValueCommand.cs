using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Fan.Abp.Cqrs.Commands;
using Fan.Abp.Ddd.Application.CommandHandlers;

namespace Fan.Abp.Ddd.Application.Commands
{
    /// <summary>
    /// 没有返回结果的 Command
    /// </summary>
    public class NoReturnValueCommand : Command
    {
        public NoReturnValueCommand()
        {

        }

        public NoReturnValueCommand(string content)
        {
            Content = content;
        }

        /// <summary>
        /// 命令内容
        /// </summary>
        [Required]
        public string Content { get;  }

        /// <summary>
        /// 执行次数
        /// </summary>
        public int ExecuteCount { get; set; }
    }

    /// <summary>
    /// 没有返回结果的 CommandHandle
    /// </summary>
    public class NoReturnValueCommandHandle : CommandHandler<NoReturnValueCommand>
    {
        public override Task HandleCommandAsync(NoReturnValueCommand request, CancellationToken cancellationToken)
        {
            request.ExecuteCount += 1;

            return Task.CompletedTask;
        }
    }
}
