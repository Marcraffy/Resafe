using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Resafe.Authorization;

namespace Resafe
{
    [DependsOn(
        typeof(ResafeCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class ResafeApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ResafeAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ResafeApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg.AddProfiles(thisAssembly);
            });
        }
    }
}
