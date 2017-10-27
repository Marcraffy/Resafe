using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Resafe.EntityFrameworkCore
{
    public static class ResafeDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ResafeDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<ResafeDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
