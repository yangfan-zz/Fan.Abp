using Fan.Abp.FreeSql;
using FreeSql;
using FreeSqlDemo.Domain.Users;

namespace FreeSqlDemo.FreeSql
{
    public class DemoSqlDbContext : FreeSqlDbContext
    {
        protected override void OnModelCreating(ICodeFirst codefirst)
        {
            codefirst.Entity<User>(build =>
            {
                build.ToTable("user");
                build.HasKey(a => a.Id);
                build.Property(a => a.UserName).HasMaxLength(32).HasColumnName("user_name");
            });
        }
    }
}
