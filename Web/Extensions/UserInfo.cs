using IServices.ISysServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace Web.Extensions
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo : IUserInfo
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _UserManager;

        /// <summary>
        /// </summary>
        /// <param name="userManager">
        /// </param>
        /// <param name="httpContextAccessor">
        /// </param>
        public UserInfo(IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _UserManager = userManager;
        }

        /// <summary>
        /// </summary>
        //public string UserId => _httpContextAccessor.HttpContext.User.FindFirstValue("UserId");

        public bool IsAuthenticated
        {
            get
            {
                return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
            }
        }

        public string UserName
        {
            get
            {
                return _httpContextAccessor.HttpContext.User.Identity.Name;
            }
        }

        public string DepartmentId
        {
            get
            {
                if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    var user = _UserManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result;

                    var claims = _UserManager.GetClaimsAsync(user).Result;

                    var claim = claims.FirstOrDefault(a => a.Type == "DepartmentId");

                    if (claim == null)
                    {
                        return null;
                    }
                    else
                    {
                        return claim.Value;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public string UserId
        {
            get
            {
                if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    var userid = _UserManager.GetUserId(_httpContextAccessor.HttpContext.User);

                    if (string.IsNullOrEmpty(userid))
                    {
                        throw new Exception("未获取到当前用户Id");
                    }

                    return userid;
                }
                else
                {
                    return null;
                }


            }
        }
    }
}