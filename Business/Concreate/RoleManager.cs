using Business.Abstract;
using DataAccess.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concreate
{
    public class RoleManager : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleManager(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<List<AppRole>> GetAllRolesAsync()
        {
            return _roleManager.Roles.Where(i => i.Name != "Admin").ToList();
        }

        public async Task<AppRole> GetRoleByIdAsync(int id)
        {
            return await _roleManager.FindByIdAsync(id.ToString());
        }

        public async Task<bool> CreateRoleAsync(string roleName)
        {
            var existingRole = await _roleManager.FindByNameAsync(roleName);
            if (existingRole == null)
            {
                var result = await _roleManager.CreateAsync(new AppRole(roleName));
                return result.Succeeded;
            }
            return false;
        }

        public async Task<bool> UpdateRoleAsync(AppRole role)
        {
            var existingRole = await _roleManager.FindByIdAsync(role.Id.ToString());
            if (existingRole != null)
            {
                existingRole.Name = role.Name;
                existingRole.NormalizedName = role.Name.ToUpper();
                var result = await _roleManager.UpdateAsync(existingRole);
                return result.Succeeded;
            }
            return false;
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                return result.Succeeded;
            }
            return false;
        }
    }
}
