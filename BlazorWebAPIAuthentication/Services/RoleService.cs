using BlazorWebAPIAuthentication.Model;
using Microsoft.AspNetCore.Identity;

namespace BlazorWebAPIAuthentication.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RoleService(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<List<string>> AddRolesAsync(string[] roles)
        {
            var rolesList = new List<string>();
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                    rolesList.Add(role);
                }
            }
            return rolesList;
        }

        public async Task<bool> AddUserRoleAsync(string userEmail, string[] roles)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            var esistingRoles = await ExistsRolesAsync(roles);
            if (user != null && esistingRoles.Count == roles.Length)
            {
                var assignRoles = await _userManager.AddToRolesAsync(user, esistingRoles);
                return assignRoles.Succeeded;
            }
            return false;
        }

        private async Task<List<string>> ExistsRolesAsync(string[] roles)
        {
            var rolesList = new List<string>();
            foreach (var role in roles)
            {
                var roleExists = await _roleManager.RoleExistsAsync(role);
                if (roleExists)
                {
                    rolesList.Add(role);
                }
            }
            return rolesList;
        }

        public async Task<List<RoleModel>> GetRolesAsync()
        {
            var roleList = _roleManager.Roles.Select(x =>
                new RoleModel { Id = Guid.Parse(x.Id), Name = x.Name }).ToList();
            return roleList;
        }

        public async Task<List<string>> GetUserRolesAsync(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            var userRoles = await _userManager.GetRolesAsync(user);
            return userRoles.ToList();
        }
    }
}
