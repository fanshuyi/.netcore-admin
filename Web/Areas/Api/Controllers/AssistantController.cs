using IServices.Infrastructure;
using IServices.ISysServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.SysModels;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Threading.Tasks;
using Web.Areas.Api.Models;
using Web.Extensions;

namespace Web.Areas.Api.Controllers
{
    /// <summary>
    /// </summary>
    [SwaggerTag("助理（Cookie）")]
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AssistantController : ControllerBase
    {
        private readonly IResInfo _IResInfo;
        private readonly IJsonDataService jsonDataService;
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// </summary>
        /// <param name="iResInfo"></param>
        /// <param name="jsonDataService"></param>
        /// <param name="unitOfWork"></param>
        public AssistantController(IResInfo iResInfo, IJsonDataService jsonDataService, IUnitOfWork unitOfWork)
        {
            _IResInfo = iResInfo;
            this.jsonDataService = jsonDataService;
            this.unitOfWork = unitOfWork;
        }


        /// <summary>用户输入
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(string id, [FromBody] Assistant value)
        {
            try
            {
                // 百度词条接口 https://www.baidu.com/s?wd=keyword&rn=1&tn=json


                var item = await jsonDataService.SaveAsync(id, value);
                await unitOfWork.CommitAsync();
                _IResInfo.Data = JObject.Parse(item.JsonDataStr);
            }
            catch (Exception e)
            {
                _IResInfo.Msg = e.Message;
            }
            return Ok(_IResInfo);
        }

    }
}