using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Fan.Abp.Cqrs.Queries;
using Fan.Abp.Ddd.Application.QueryHandlers;

namespace Fan.Abp.Ddd.Application.Queries
{
    public class ReturnIntQuery : Query<int>
    {
        public ReturnIntQuery(string content)
        {
            Content = content;
        }


        public ReturnIntQuery()
        {
          
        }

        /// <summary>
        /// 命令内容
        /// </summary>
        [Required]
        public string Content { get; }
    }

    public class ReturnIntQueryHandle : QueryHandler<ReturnIntQuery, int>
    {
        public override Task<int> HandleQueryAsync(ReturnIntQuery request, CancellationToken cancellationToken)
        {
            if (request.Content == "Content")
            {
                return Task.FromResult(0);
            }

            return Task.FromResult(1);
        }
    }
}
