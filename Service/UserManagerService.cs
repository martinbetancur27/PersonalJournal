using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using IService;

namespace Service
{
    
    public class UserManagerService : IUserService
    {
        private readonly IHttpContextAccessor _userManager;

        public UserManagerService(IHttpContextAccessor userManager)
        {
            _userManager = userManager;
        }
        public string GetId()
        {
            return _userManager.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
