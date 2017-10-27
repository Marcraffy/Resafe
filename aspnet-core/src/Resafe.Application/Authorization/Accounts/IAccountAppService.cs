using System.Threading.Tasks;
using Abp.Application.Services;
using Resafe.Authorization.Accounts.Dto;

namespace Resafe.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
