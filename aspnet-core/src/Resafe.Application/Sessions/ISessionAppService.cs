using System.Threading.Tasks;
using Abp.Application.Services;
using Resafe.Sessions.Dto;

namespace Resafe.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
