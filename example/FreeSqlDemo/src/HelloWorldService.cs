using System;
using System.Threading.Tasks;
using FreeSqlDemo.Domain.Users;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.DependencyInjection;

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

    public async Task SayHelloAsync()
    {
      //  await CreateAsync();
        var users = await _userRepository.GetListAsync();
        await CreateAsync(true);
        users = await _userRepository.GetListAsync();

        foreach (var user in users)
        {
            Logger.LogInformation(user.UserName);
        }
       
      
    }

    private Task CreateAsync(bool autoSave = false)
    {
      return  _userRepository.InsertAsync(new User(Guid.NewGuid().ToString("N"), "张三"), autoSave);
    }

}
