using System.Threading.Tasks;
using Resafe.Configuration.Dto;

namespace Resafe.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
