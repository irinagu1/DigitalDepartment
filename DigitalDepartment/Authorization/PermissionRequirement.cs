using Entities.Models.Auth;
using Microsoft.AspNetCore.Authorization;

namespace DigitalDepartment.Authorzation
{
    public class PermissionRequirement(string permission) : IAuthorizationRequirement
    {
        public string Permission { get; set; } = permission;
    }
}
