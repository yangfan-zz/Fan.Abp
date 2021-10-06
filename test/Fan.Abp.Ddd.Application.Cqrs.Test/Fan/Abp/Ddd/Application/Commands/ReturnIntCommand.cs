using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Fan.Abp.Cqrs.Commands;
using Fan.Abp.Ddd.Application.CommandHandlers;

namespace Fan.Abp.Ddd.Application.Commands
{
    public class ReturnIntCommand : Command<int>
    {
        public ReturnIntCommand(string content)
        {
            Content = content;
        }

        /// <summary>
        /// 命令内容
        /// </summary>
        [Required]
        public string Content { get; }
    }

    public class ReturnIntCommandHandle : CommandHandler<ReturnIntCommand, int>
    {
        public override Task<int> HandleCommandAsync(ReturnIntCommand request, CancellationToken cancellationToken)
        {
            if (request.Content == "Content")
            {
                return Task.FromResult(0);
            }

            return Task.FromResult(1);
        }
    }
}
