using DataAccess.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRoleService
    {
        Task<List<AppRole>> GetAllRolesAsync();
        Task<AppRole> GetRoleByIdAsync(int id);
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> UpdateRoleAsync(AppRole role);
        Task<bool> DeleteRoleAsync(int id);
    }
}
