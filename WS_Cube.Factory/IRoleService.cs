using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WS_Cube.ViewModel;

namespace WS_Cube.Factory
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleViewModel>> GetRoles(int userID);
        Task<bool> AddRole(RoleViewModel role);

        Task<bool> DeleteRole(RoleViewModel role);

        Task<bool> UpdateRole(RoleViewModel role);

        Task<bool> UpdateRoleStatus(RoleViewModel role);

        Task<bool> GetRolePermission(RoleViewModel role);

        Task<bool> GetUserRoleAssignment(RoleViewModel role);

    }
}
