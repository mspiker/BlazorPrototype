using BlazorWebAPIAuthentication.Model;

namespace BlazorWebAPIAuthentication.Services
{
    public interface IRoleService
    {

        Task<List<RoleModel>> GetRolesAsync();
        Task<List<string>> GetUserRolesAsync(string userEmail);
        Task<List<string>> AddRolesAsync(string[] roles);
        Task<bool> AddUserRoleAsync(string userEmail, string[] roles);
    }
}
