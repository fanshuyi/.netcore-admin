using IServices.Infrastructure;
using IServices.ISysServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
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
    [SwaggerTag("数据字典")]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class DataDictionaryController : ControllerBase
    {
        private readonly IResInfo _IResInfo;
        private readonly IJsonDataService jsonDataService;
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// </summary>
        /// <param name="iResInfo"></param>
        /// <param name="jsonDataService"></param>
        /// <param name="unitOfWork"></param>
        public DataDictionaryController(IResInfo iResInfo, IJsonDataService jsonDataService, IUnitOfWork unitOfWork)
        {
            _IResInfo = iResInfo;
            this.jsonDataService = jsonDataService;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <param name="Category">分类</param>
        /// <param name="isEnable"></param>

        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(string keyword, string Category, bool? isEnable)
        {
            var model = jsonDataService.GetAll<DataDictionary>().Select(a => new
            {
                Category = SqlDbFunctions.JsonValue(a.JsonDataStr, "$.Category"),

                Name = SqlDbFunctions.JsonValue(a.JsonDataStr, "$.Name"),

                SystemId = SqlDbFunctions.JsonValue(a.JsonDataStr, "$.SystemId"),

                Enable = Convert.ToBoolean(SqlDbFunctions.JsonValue(a.JsonDataStr, "$.Enable")),

                a.Id
            }).Search(keyword);

            model = model.Where(a => a.Category == Category);

            if (isEnable.HasValue)
            {
                model = model.Where(a => a.Enable == isEnable.Value);
            }

            model = model.OrderBy(a => a.SystemId);

            _IResInfo.Data = model;

            return Ok(_IResInfo);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var item = await jsonDataService.FindAsync(id);
                _IResInfo.Data = JObject.Parse(item.JsonDataStr);
            }
            catch (Exception e)
            {
                _IResInfo.Msg = e.Message;
            }
            return Ok(_IResInfo);
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(string id, [FromBody] DataDictionary value)
        {
            try
            {
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

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await jsonDataService.Delete(id);
                await unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                _IResInfo.Msg = e.Message;
            }

            return Ok(_IResInfo);
        }
    }
}