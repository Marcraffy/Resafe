using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Resafe.Authorization.Roles;
using Resafe.Authorization.Users;
using Resafe.MultiTenancy;

namespace Resafe.EntityFrameworkCore
{
    public class ResafeDbContext : AbpZeroDbContext<Tenant, Role, User, ResafeDbContext>
    {
        /* Define an IDbSet for each entity of the application */
        public DbSet<Child.Child> Children { get; set; }

        public ResafeDbContext(DbContextOptions<ResafeDbContext> options)
            : base(options)
        {
        }
    }
}
