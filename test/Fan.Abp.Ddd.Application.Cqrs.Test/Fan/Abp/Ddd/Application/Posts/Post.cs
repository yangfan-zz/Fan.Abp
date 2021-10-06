using Volo.Abp.Domain.Entities;

namespace Fan.Abp.Ddd.Application.Posts
{
    public class Post : Entity<int>
    {
        public Post(int id, string title) : base(id)
        {
            Title = title;
        }

        public string Title { get; set; }

    }
}
