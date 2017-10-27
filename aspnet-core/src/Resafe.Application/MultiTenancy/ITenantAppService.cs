using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Resafe.MultiTenancy.Dto;

namespace Resafe.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
