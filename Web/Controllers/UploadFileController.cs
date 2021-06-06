using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using IServices.ISysServices;

namespace Web.Controllers
{
    [Authorize]
    public class UploadFileController : Controller
    {
        private readonly IConfiguration _IConfiguration;
        private readonly IUserInfo _IUserInfo;

        public UploadFileController(IConfiguration iConfiguration, IUserInfo iUserInfo)
        {
            _IConfiguration = iConfiguration;
            _IUserInfo = iUserInfo;
        }

        [HttpPost]
        public async Task<JsonResult> Index(bool SingleFile = true)
        {
            var uploadFiles = new List<UploadFile>(); //上传的文件列表


            foreach (var formFile in Request.Form.Files)
            {
                //大小，格式校验....
                var filepath = "/uploadfile/" + _IUserInfo.UserId + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";

                var filename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(formFile.FileName);

                System.IO.Directory.CreateDirectory("./wwwroot" + filepath);

                using (var stream = System.IO.File.Create("./wwwroot" + filepath + filename))
                {
                    await formFile.CopyToAsync(stream);
                }

                var file = new UploadFile()
                {
                    ContentType = formFile.ContentType,
                    FileName = formFile.FileName,
                    Length = formFile.Length,
                    uploaded = true,
                    url = filepath + filename
                };

                if (SingleFile)
                {
                    return Json(file);
                }
                else
                {
                    uploadFiles.Add(file);
                }

            }

            return Json(uploadFiles);

        }
    }

    internal class UploadFile
    {
        public UploadFile()
        {
            CreateDateTime = DateTime.Now;
        }

        public string ContentType { get; set; }

        public string FileName { get; set; }

        public long Length { get; set; }

        public string url { get; set; }

        public DateTime CreateDateTime { get; set; }


        public bool uploaded { get; set; }

        public string Message { get; set; }
    }
}