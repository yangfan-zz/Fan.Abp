# Fan.ABP

# Fan.Abp.Cqrs
### Add DependsOn Add DependsOn on MyApplicationModule
````csharp
[DependsOn(typeof(FanDddApplicationCqrsModule))]
public class MyApplicationModule : AbpModule
{
       
}
````
### Add NoReturnValueCommand
````csharp
using Fan.Abp.Ddd.Application.CommandHandlers;

public class NoReturnValueCommand : Command
{
    [Required]
    public string Content { get;  }

    public int ExecuteCount { get; set; }
}
````
### Add NoReturnValueCommandHandle
````csharp
using Fan.Abp.Ddd.Application.CommandHandlers;

public class NoReturnValueCommandHandle : CommandHandler<NoReturnValueCommand>
{
    public override Task HandleCommandAsync(NoReturnValueCommand command, CancellationToken cancellationToken)
    {
        command.ExecuteCount += 1;

        return Task.CompletedTask;
    }
}
````

###  ICommandsExecutor

````csharp
using Fan.Abp.Ddd.Application.CommandHandlers;

[Authorize]
public class MyAppService : ApplicationService
{
    private readonly ICommandsExecutor _commandSender;
    
    public MyAppService(ICommandsExecutor commandSender)
    {
        _commandSender = commandSender;
    }

    public Task NoReturnValueAsync(NoReturnValueCommand command)
    {
        return _commandSender.ExecuteAsync(command);
    }
}

````