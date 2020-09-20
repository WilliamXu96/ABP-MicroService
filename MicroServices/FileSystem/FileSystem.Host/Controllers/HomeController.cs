using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using Volo.Abp.AspNetCore.Mvc;

namespace FileSystem.Controllers
{
    public class HomeController : AbpController
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public ActionResult Index()
        {
            return Redirect("/swagger");
        }

        [HttpPost]
        [Route("file/upload")]
        public ActionResult Upload(List<IFormFile> files)
        {
            try
            {
                var webRootPath = _hostEnvironment.WebRootPath;
                var filePath = $"/Images/";
                if (!Directory.Exists(webRootPath + filePath))
                {
                    Directory.CreateDirectory(webRootPath + filePath);
                }

                foreach (var item in files)
                {
                    if (item != null)
                    {
                        //文件后缀
                        var fileExtension = Path.GetExtension(item.FileName);

                        var strDateTime = DateTime.Now.ToString("yyyyMMddHHmmssfff"); //取得时间字符串
                        var strRan = Convert.ToString(new Random().Next(100, 999)); //生成三位随机数
                        var saveName = strDateTime + strRan + fileExtension;

                        //插入图片数据                 
                        using (FileStream fs = System.IO.File.Create(webRootPath + filePath + saveName))
                        {
                            item.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                }
                return Ok("OK");
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
