using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Resafe.Controllers
{
    public abstract class ResafeControllerBase: AbpController
    {
        protected ResafeControllerBase()
        {
            LocalizationSourceName = ResafeConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
