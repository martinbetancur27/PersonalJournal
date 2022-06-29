using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public string GetUserId()
        {
            return _userManager.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
