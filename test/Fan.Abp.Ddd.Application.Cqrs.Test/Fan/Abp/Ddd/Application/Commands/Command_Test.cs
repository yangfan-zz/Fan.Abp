using System.Linq;
using System.Threading.Tasks;
using Fan.Abp.Cqrs.Commands;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Volo.Abp;
using Volo.Abp.Testing;
using Volo.Abp.Validation;
using Xunit;

namespace Fan.Abp.Ddd.Application.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class Command_Test : AbpIntegratedTest<FanDddApplicationCqrsTestModule>
    {
        private readonly ICommandsExecutor _commandSender;

        public Command_Test()
        {
            _commandSender = ServiceProvider.GetRequiredService<ICommandsExecutor>();
        }

        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }

        /// <summary>
        /// 执行没有返回值的  Command
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CommandHandle_CommandContextValidation()
        {
            var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _commandSender.ExecuteAsync(new NoReturnValueCommand());
            });

            exception.ValidationErrors.ShouldContain(e => e.MemberNames.Contains(nameof(NoReturnValueCommand.Content)));
        }


        /// <summary>
        /// 执行没有返回值的  Command
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CommandHandle_NoReturnValue()
        {
            var command = new NoReturnValueCommand("Context");
            await _commandSender.ExecuteAsync(command);

            // 执行次数
            command.ExecuteCount.ShouldBe(1);
        }

        /// <summary>
        /// 执行返回结果为int的 Command
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CommandHandle_ReturnIntCommand()
        {
            var command = new ReturnIntCommand("Context");
            var result = await _commandSender.ExecuteAsync(command);

            // 执行次数
            result.ShouldBe(1);
        }

        /// <summary>
        /// 执行返回结果为自定义对象的 Command
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CommandHandle_ReturnCustomValueCommand()
        {
            var command = new ReturnCustomValueCommand("Context");
            var result = await _commandSender.ExecuteAsync(command);

            // 执行次数
            result.Value.ShouldBe(1);
        }

        /// <summary>
        /// 执行返回结果为自定义对象的 Command
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreatePostCommand()
        {
            var command = new CreatePostCommand<PostDto> {Id = 100, Title = "CreatePostCommand"};

            var result = await _commandSender.ExecuteAsync(command);

            result.Title.ShouldBe("CreatePostCommand");
        }
    }
}
