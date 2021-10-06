using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Fan.Abp.Cqrs.Commands;
using Fan.Abp.Ddd.Application.CommandHandlers;

namespace Fan.Abp.Ddd.Application.Commands
{
    public class CustomValue
    {
        public int Value { get; set; }
    }


    public class ReturnCustomValueCommand : Command<CustomValue>
    {
        public ReturnCustomValueCommand(string content)
        {
            Content = content;
        }

        /// <summary>
        /// 命令内容
        /// </summary>
        [Required]
        public string Content { get; }
    }


    public class ReturnCustomValueCommandHandle : CommandHandler<ReturnCustomValueCommand, CustomValue>
    {
        public override Task<CustomValue> HandleCommandAsync(ReturnCustomValueCommand request, CancellationToken cancellationToken)
        {
            if (request.Content == "Content")
            {
                return Task.FromResult((CustomValue)null);
            }

            return Task.FromResult(new CustomValue { Value = 1 });
        }
    }
}
