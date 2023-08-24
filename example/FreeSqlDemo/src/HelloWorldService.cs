using System;
using System.Threading.Tasks;
using FreeSqlDemo.Domain.Users;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace FreeSqlDemo;

public class HelloWorldService : ITransientDependency
{
    private readonly IUserRepository _userRepository;

    public ILogger<HelloWorldService> Logger { get; set; }

    public HelloWorldService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        Logger = NullLogger<HelloWorldService>.Instance;
    }

    [UnitOfWork]
    public virtual async Task SayHelloAsync()
    {
        await CreateAsync();
        var users = await _userRepository.GetListAsync(u=>u.UserName =="张三");
        await CreateAsync(true);
        users = await _userRepository.GetListAsync();

        foreach (var user in users)
        {
            Logger.LogInformation(user.UserName);
        }

        await CreateAsync();

        throw new UserFriendlyException("aaa");
    }

    private Task CreateAsync(bool autoSave = false)
    {
      return  _userRepository.InsertAsync(new User(Guid.NewGuid().ToString("N"), "张三"), autoSave);
    }

}
