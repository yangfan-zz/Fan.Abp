using System;
using System.Linq;
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
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    public ILogger<HelloWorldService> Logger { get; set; }

    public HelloWorldService(IUserRepository userRepository, IUnitOfWorkManager unitOfWorkManager)
    {
        _userRepository = userRepository;
        _unitOfWorkManager = unitOfWorkManager;
        Logger = NullLogger<HelloWorldService>.Instance;
    }

    [UnitOfWork]
    public virtual async Task SayHelloAsync()
    {
        var query = await _userRepository.GetQueryableAsync();
        var aa = query.Take(1).ToList();

        // 内部工作单元
        using (var uow = _unitOfWorkManager.Begin(
                   requiresNew: true, isTransactional: true
               ))
        {
            //...
            await CreateAsync();

            // 内部工作单元
            using (var uowSub = _unitOfWorkManager.Begin(
                       requiresNew: true, isTransactional: true
                   ))
            {
                //...
                await CreateAsync();
                await uowSub.CompleteAsync();

            }
           
            await uow.CompleteAsync();
        }


        // 外部工作单元
        var users = await _userRepository.GetListAsync(u => u.UserName == "张三");
        await CreateAsync(true);
        users = await _userRepository.GetListAsync();

        foreach (var user in users)
        {
            Logger.LogInformation(user.UserName);
        }

        await CreateAsync();

       // throw new UserFriendlyException("aaa");
    }

    private Task CreateAsync(bool autoSave = false)
    {
      return  _userRepository.InsertAsync(new User(Guid.NewGuid().ToString("N"), "张三"), autoSave);
    }

}
