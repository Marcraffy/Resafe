using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Resafe.Configuration.Dto;

namespace Resafe.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : ResafeAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
