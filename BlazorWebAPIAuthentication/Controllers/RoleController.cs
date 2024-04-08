using BlazorWebAPIAuthentication.Model;
using BlazorWebAPIAuthentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlazorWebAPIAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleService.GetRolesAsync();
            return Ok(roles);
        }

        [Authorize(Roles = "admin,user")]
        [HttpGet("GetUserRole")]
        public async Task<IActionResult> GetUserRoles(string userEmail)
        {
            var userClaims = await _roleService.GetUserRolesAsync(userEmail);
            return Ok(userClaims);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("AddRoles")]
        public async Task<IActionResult> AddRoles(string[] roles)
        {
            var roleList = await _roleService.AddRolesAsync(roles);
            if (roleList.Count == 0)
            {
                return BadRequest();
            }
            return Ok(roleList);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("AddUserRoles")]
        public async Task<IActionResult> AddUserRoles([FromBody] AddUserModel addUser)
        {
            var result = await _roleService.AddUserRoleAsync(addUser.UserEmail, addUser.Roles);
            if (!result)
            {
                return BadRequest();
            }
            return StatusCode((int)HttpStatusCode.Created, result);
        }

    }
}
