using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IServices.ISysServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Web.Areas.Api.Models;
using Dapr.Client;
using Dapr;
using System.Net.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Areas.Api.Controllers
{
    /// <summary>
    /// </summary>
    [SwaggerTag("用户信息")]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private IUserInfo userInfo;
        private readonly IResInfo _IResInfo;
        private readonly DaprClient _daprClient;

        /// <summary>
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="iResInfo"></param>
        public UserInfoController(IUserInfo userInfo, IResInfo iResInfo)
        {
            this.userInfo = userInfo;
            _IResInfo = iResInfo;
        }

        // GET: api/values

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _IResInfo.Data = userInfo;

            return Ok(_IResInfo);
        }

        [Topic("pubsub", "PostUeerInfo")]
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            return Ok(_IResInfo);
        }
    }
}