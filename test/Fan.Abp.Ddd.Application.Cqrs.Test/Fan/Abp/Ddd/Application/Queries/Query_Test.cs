using System.Linq;
using System.Threading.Tasks;
using Fan.Abp.Cqrs.Queries;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Volo.Abp;
using Volo.Abp.Testing;
using Volo.Abp.Validation;
using Xunit;

namespace Fan.Abp.Ddd.Application.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public class Query_Test : AbpIntegratedTest<FanDddApplicationCqrsTestModule>
    {
        private readonly IQueryExecutor _queryExecutor;

        public Query_Test()
        {
            _queryExecutor = ServiceProvider.GetRequiredService<IQueryExecutor>();
        }

        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task QueryHandle_CommandContextValidation()
        {
            var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _queryExecutor.ExecuteAsync(new ReturnIntQuery());
            });

            exception.ValidationErrors.ShouldContain(e => e.MemberNames.Contains(nameof(ReturnIntQuery.Content)));
        }

        /// <summary>
        /// 执行返回结果为int的 Query
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task QueryHandle_ReturnIntQuery()
        {
            var query = new ReturnIntQuery("Context");
            var result = await _queryExecutor.ExecuteAsync(query);

            // 执行次数
            result.ShouldBe(1);
        }

        [Fact]
        public async Task QueryHandle_ReturnListQuery()
        {
            var query = new ReturnListQuery();
            query.Title = "Post-title-1";
            var result = await _queryExecutor.ExecuteAsync(query);
            result.Items.Count.ShouldBe(11);
        }

        [Fact]
        public async Task QueryHandle_ReturnPagedListQuery()
        {
            var query = new ReturnPagedListQuery();
            query.Title = "Post-title-1";
            query.MaxResultCount = 5;
            var result = await _queryExecutor.ExecuteAsync(query);
            result.Items.Count.ShouldBe(5);
            result.TotalCount.ShouldBe(11);
        }

    }
}
