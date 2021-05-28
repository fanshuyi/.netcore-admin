using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using Web.Areas.Api.Models;
using Web.Models.AccountViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Areas.Api.Controllers
{
    /// <summary>
    /// </summary>
    [SwaggerTag("用户登录")]
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IResInfo _IResInfo;

        /// <summary>
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="iResInfo"></param>
        public LoginController(UserManager<IdentityUser> userManager, IResInfo iResInfo)
        {
            _userManager = userManager;
            _IResInfo = iResInfo;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="item"></param>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginInputModel item)
        {
            var user = await _userManager.FindByNameAsync(item.UserName);

            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, item.Password))
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[] {
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.NameIdentifier, user.Id)
                        }),

                        SigningCredentials = new SigningCredentials(
                            new SymmetricSecurityKey(Encoding.ASCII.GetBytes("AAAAAA0000000000000"))
                            ,
                        SecurityAlgorithms.HmacSha256Signature),
                        Audience = "evan",
                        Issuer = "evan",
                        Expires = DateTime.Now.AddYears(1)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    _IResInfo.Data = new { Token = "Bearer " + tokenHandler.WriteToken(token) };
                }
                else
                {
                    _IResInfo.Msg = "密码错误！";
                }
            }
            else
            {
                _IResInfo.Msg = "用户不存在！";
            }
            return Ok(_IResInfo);
        }
    }
}