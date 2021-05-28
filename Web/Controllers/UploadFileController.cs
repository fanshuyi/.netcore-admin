using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Web.Controllers
{
    [Authorize]
    public class UploadFileController : Controller
    {
        private readonly IConfiguration _IConfiguration;

        public UploadFileController(IConfiguration iConfiguration)
        {
            _IConfiguration = iConfiguration;
        }

        [HttpPost]
        public async Task<JsonResult> Index(string from)
        {
            var uploadFiles = new List<UploadFile>(); //上传的文件列表

            foreach (var formFile in Request.Form.Files)
            {
                //大小，格式校验....


                using (var stream = System.IO.File.Create(Guid.NewGuid().ToString()))
                {
                    await formFile.CopyToAsync(stream);
                }




                if (from == "ckfinder")
                {
                    //ckfinder 只处理第一个文件
                    return Json(new
                    {
                        formFile.ContentType,
                        formFile.FileName,
                        formFile.Length,
                        uploaded = true,
                    });
                }

                uploadFiles.Add(new UploadFile()
                {
                    ContentType = formFile.ContentType,
                    FileName = formFile.FileName,
                    Length = formFile.Length,
                }
                );
            }

            return Json(uploadFiles);
        }
    }

    internal class UploadFile
    {
        public UploadFile()
        {
            CreateDateTime = DateTime.Now.ToShortDateString();
        }

        public string ContentType { get; set; }

        public string FileName { get; set; }

        public long Length { get; set; }

        public Uri Url { get; set; }

        public string CreateDateTime { get; set; }

        public string MobileNumber { get; set; }
    }
}