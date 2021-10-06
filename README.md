# Fan.ABP

# Fan.Abp.Cqrs
### Add **DependsOn** on **MyApplicationModule**
````csharp
[DependsOn(typeof(FanDddApplicationCqrsModule))]
public class MyApplicationModule : AbpModule
{
       
}
````
### Add **NoReturnValueCommand** and **NoReturnValueCommandHandle**
````csharp
using Fan.Abp.Ddd.Application.CommandHandlers;

public class NoReturnValueCommand : Command
{
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

````

### Add **ReturnIntCommand** and **ReturnIntCommandHandle**
````csharp
using Fan.Abp.Ddd.Application.CommandHandlers;

public class ReturnIntCommand : Command<int>
{
    public ReturnIntCommand(string content)
    {
        Content = content;
    }

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

````

###  ICommandsExecutor

````csharp
using Fan.Abp.Ddd.Application.CommandHandlers;

[Authorize]
public class MyAppService : ApplicationService
{
    private readonly ICommandsExecutor _commandsExecutor;
    
    public MyAppService(ICommandsExecutor commandsExecutor)
    {
        _commandsExecutor = commandsExecutor;
    }

    public Task NoReturnValueAsync(NoReturnValueCommand command)
    {
        return _commandsExecutor.ExecuteAsync(command);
    }

    public Task<int> ReturnIntAsync(ReturnIntCommand command)
    {
        return _commandsExecutor.ExecuteAsync(command);
    }
}

````