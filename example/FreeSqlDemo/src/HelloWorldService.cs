using System;
using System.Linq;
using System.Threading.Tasks;
using FreeSqlDemo.Domain.Users;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
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

    [UnitOfWork(isTransactional: false)]
    public virtual async Task SayHelloAsync()
    {
        var query = await _userRepository.GetQueryableAsync();
        var aa = query.Take(1).ToList();

        //// 内部工作单元
        //using (var uow = _unitOfWorkManager.Begin(
        //           requiresNew: true, isTransactional: false
        //       ))
        //{
        //    //...
        //    await CreateAsync();

        //    // 内部工作单元
        //    using (var uowSub = _unitOfWorkManager.Begin(isTransactional: true))
        //    {
        //        //...
        //        await CreateAsync();
        //        await uowSub.CompleteAsync();

        //    }

        //    await uow.CompleteAsync();
        //}


        // 外部工作单元
        var count = await _userRepository.GetCountAsync();
      var u =  await CreateAsync();
      var singleUser = await _userRepository.SingleAsync(s =>  s.Id == u.Id);
        var ff = await _userRepository.FindAsync(s => s.UserName!.Contains("a"));

        var varu = await _userRepository.GetPagedListAsync(0,10,"Id");

        var users = await _userRepository.GetListAsync(u => u.UserName == "张三");
        await CreateAsync(false);
        users = await _userRepository.GetListAsync();

        foreach (var user in users)
        {
            user.UserName = "aa++";
            await _userRepository.UpdateAsync(user);
          
            Logger.LogInformation(user.UserName);
        }

        await CreateAsync();

// throw new UserFriendlyException("aaa");
    }

    private Task<User> CreateAsync(bool autoSave = false)
    {
        return _userRepository.InsertAsync(new User(Guid.NewGuid().ToString("N"), "张三"), autoSave);
    }

}
