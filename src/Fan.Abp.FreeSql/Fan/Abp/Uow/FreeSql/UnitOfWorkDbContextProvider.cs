using System;
using System.Threading;
using System.Threading.Tasks;
using Fan.Abp.FreeSql;
using Fan.Abp.FreeSql.DependencyInjection;
using FreeSql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Threading;
using Volo.Abp.Uow;
using IUnitOfWork = Volo.Abp.Uow.IUnitOfWork;

namespace Fan.Abp.Uow.FreeSql
{
    public class UnitOfWorkDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
        where TDbContext : IFreeSqlDbContext
    {
        private const string TransactionsNotSupportedErrorMessage = "Current database does not support transactions. Your database may remain in an inconsistent state in an error case.";

        public ILogger<UnitOfWorkDbContextProvider<TDbContext>> Logger { get; set; }


        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IConnectionStringResolver _connectionStringResolver;
        private readonly ICancellationTokenProvider _cancellationTokenProvider;
        private readonly ICurrentTenant _currentTenant;

        public UnitOfWorkDbContextProvider(IUnitOfWorkManager unitOfWorkManager,
            IConnectionStringResolver connectionStringResolver, ICancellationTokenProvider cancellationTokenProvider,
            ICurrentTenant currentTenant)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _connectionStringResolver = connectionStringResolver;
            _cancellationTokenProvider = cancellationTokenProvider;
            _currentTenant = currentTenant;
        }

        public async Task<TDbContext> GetDbContextAsync()
        {
            var unitOfWork = _unitOfWorkManager.Current;
            if (unitOfWork == null)
            {
                throw new AbpException("A FreeSqlDbContext can only be created inside a unit of work!");
            }

            var targetDbContextType = typeof(TDbContext); // TODO
            var connectionStringName = ConnectionStringNameAttribute.GetConnStringName(targetDbContextType);
            var connectionString = await ResolveConnectionStringAsync(connectionStringName);

            var dbContextKey = $"{targetDbContextType.FullName}_{connectionString}";

            var databaseApi = unitOfWork.FindDatabaseApi(dbContextKey);

            if (databaseApi == null)
            {
                databaseApi = new FreeSqlDatabaseApi(
                    await CreateDbContextAsync(unitOfWork, connectionStringName, connectionString)
                );

                unitOfWork.AddDatabaseApi(dbContextKey, databaseApi);
            }

            return (TDbContext)((FreeSqlDatabaseApi)databaseApi).DbContext;
        }

        private async Task<TDbContext> CreateDbContextAsync(IUnitOfWork unitOfWork, string connectionStringName, string connectionString)
        {
            var creationContext = new DbContextCreationContext(connectionStringName, connectionString);
            using (DbContextCreationContext.Use(creationContext))
            {
                var dbContext = await CreateDbContextAsync(unitOfWork);

                if (dbContext is IFreeSqlDbContext abpEfCoreDbContext)
                {
                    abpEfCoreDbContext.Initialize(
                        new FreeSqlDbContextInitializationContext(
                            unitOfWork
                        )
                    );
                }

                return dbContext;
            }
        }
        private async Task<TDbContext> CreateDbContextAsync(IUnitOfWork unitOfWork)
        {
            return unitOfWork.Options.IsTransactional
                ? await CreateDbContextWithTransactionAsync(unitOfWork)
                : unitOfWork.ServiceProvider.GetRequiredService<TDbContext>();
        }

        private async Task<TDbContext> CreateDbContextWithTransactionAsync(IUnitOfWork unitOfWork)
        {
            var transactionApiKey = $"EntityFrameworkCore_{DbContextCreationContext.Current.ConnectionString}";
            var activeTransaction = unitOfWork.FindTransactionApi(transactionApiKey) as FreeSqlTransactionApi;

            if (activeTransaction == null)
            {
                var dbContext = unitOfWork.ServiceProvider.GetRequiredService<TDbContext>();

                try
                {
                    //var dbTransaction = unitOfWork.Options.IsolationLevel.HasValue
                    //    ? await dbContext.Database.BeginTransactionAsync(unitOfWork.Options.IsolationLevel.Value, GetCancellationToken())
                    //    : await dbContext.Database.BeginTransactionAsync(GetCancellationToken());

                    unitOfWork.AddTransactionApi(
                        transactionApiKey,
                        new FreeSqlTransactionApi(
                           // dbTransaction,
                            dbContext,
                            _cancellationTokenProvider
                        )
                    );
                }
                catch (Exception e) when (e is InvalidOperationException || e is NotSupportedException)
                {
                    Logger.LogError(TransactionsNotSupportedErrorMessage);
                    Logger.LogException(e);

                    return dbContext;
                }

                return dbContext;
            }
            else
            {
                DbContextCreationContext.Current.ExistingConnection = activeTransaction.DbContextTransaction.GetDbTransaction().Connection;

                var dbContext = unitOfWork.ServiceProvider.GetRequiredService<TDbContext>();

                if (dbContext.As<DbContext>().HasRelationalTransactionManager())
                {
                    if (dbContext.Database.GetDbConnection() == DbContextCreationContext.Current.ExistingConnection)
                    {
                        await dbContext.Database.UseTransactionAsync(activeTransaction.DbContextTransaction.GetDbTransaction(), GetCancellationToken());
                    }
                    else
                    {
                        try
                        {
                            /* User did not re-use the ExistingConnection and we are starting a new transaction.
                                * EfCoreTransactionApi will check the connection string match and separately
                                * commit/rollback this transaction over the DbContext instance. */
                            if (unitOfWork.Options.IsolationLevel.HasValue)
                            {
                                await dbContext.Database.BeginTransactionAsync(
                                    unitOfWork.Options.IsolationLevel.Value,
                                    GetCancellationToken()
                                );
                            }
                            else
                            {
                                await dbContext.Database.BeginTransactionAsync(
                                    GetCancellationToken()
                                );
                            }
                        }
                        catch (Exception e) when (e is InvalidOperationException || e is NotSupportedException)
                        {
                            Logger.LogError(TransactionsNotSupportedErrorMessage);
                            Logger.LogException(e);

                            return dbContext;
                        }
                    }
                }
                else
                {
                    try
                    {
                        /* No need to store the returning IDbContextTransaction for non-relational databases
                            * since EfCoreTransactionApi will handle the commit/rollback over the DbContext instance.
                              */
                        await dbContext.Database.BeginTransactionAsync(GetCancellationToken());
                    }
                    catch (Exception e) when (e is InvalidOperationException || e is NotSupportedException)
                    {
                        Logger.LogError(TransactionsNotSupportedErrorMessage);
                        Logger.LogException(e);

                        return dbContext;
                    }
                }

                activeTransaction.AttendedDbContexts.Add(dbContext);

                return dbContext;
            }
        }
        private async Task<string> ResolveConnectionStringAsync(string connectionStringName)
        {
            // Multi-tenancy unaware contexts should always use the host connection string
            if (typeof(TDbContext).IsDefined(typeof(IgnoreMultiTenancyAttribute), false))
            {
                using (_currentTenant.Change(null))
                {
                    return await _connectionStringResolver.ResolveAsync(connectionStringName);
                }
            }

            return await _connectionStringResolver.ResolveAsync(connectionStringName);
        }

        protected virtual CancellationToken GetCancellationToken(CancellationToken preferredValue = default)
        {
            return _cancellationTokenProvider.FallbackToProvider(preferredValue);
        }
    }
}
