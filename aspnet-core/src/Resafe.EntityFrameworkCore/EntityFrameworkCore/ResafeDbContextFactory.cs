using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Resafe.Configuration;
using Resafe.Web;

namespace Resafe.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class ResafeDbContextFactory : IDesignTimeDbContextFactory<ResafeDbContext>
    {
        public ResafeDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ResafeDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            ResafeDbContextConfigurer.Configure(builder, configuration.GetConnectionString(ResafeConsts.ConnectionStringName));

            return new ResafeDbContext(builder.Options);
        }
    }
}
