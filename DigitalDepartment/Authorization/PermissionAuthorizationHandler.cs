using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Validations;
using Service.Contracts;
using System.Security.Principal;

namespace DigitalDepartment.Authorzation
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
           // WindowsPrincipal wp = new WindowsPrincipal(WindowsIdentity.GetCurrent());
         //   var userId = wp.Claims.FirstOrDefault(c => c.Type == "userId");
            var userId = context.User.Claims.FirstOrDefault(c=> c.Type == "userId");

            if (userId == null || !Guid.TryParse(userId.Value, out var id)) 
                throw new Exception("cannot get userid from claims ");
            
            using var scope = _serviceScopeFactory.CreateScope();
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            var perms = await userService.GetUserPermissions(id.ToString());
  
            if (perms.Contains(requirement.Permission))
                context.Succeed(requirement);
        }
    }
}
