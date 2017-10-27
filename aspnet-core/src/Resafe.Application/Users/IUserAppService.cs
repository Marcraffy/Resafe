using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Resafe.Roles.Dto;
using Resafe.Users.Dto;

namespace Resafe.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();
    }
}
