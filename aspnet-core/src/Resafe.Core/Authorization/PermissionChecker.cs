using Abp.Authorization;
using Resafe.Authorization.Roles;
using Resafe.Authorization.Users;

namespace Resafe.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
