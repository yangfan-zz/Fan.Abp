using Volo.Abp.Uow;

namespace Fan.Abp.FreeSql
{
    public class FreeSqlDbContextInitializationContext
    {
        public IUnitOfWork UnitOfWork { get; }

        public FreeSqlDbContextInitializationContext(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}
