using FileStorage.Enums;
using FileStorage.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace FileStorage.Controllers
{
    public class UploadController : AbpController
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly FileManager _fileManager;

        string[] pictureFormatArray = { ".png", ".jpg", ".jpeg", ".gif", ".PNG", ".JPG", ".JPEG", ".GIF" };

        public UploadController(
            IWebHostEnvironment hostEnvironment,
            FileManager fileManager)
        {
            _hostEnvironment = hostEnvironment;
            _fileManager = fileManager;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<ActionResult> Upload([Required]string name, IFormFile file)
        {
            string uniqueFileName = null;

            if (file != null)
            {
                //限制100M
                if (file.Length > 104857600)
                {
                    return new BadRequestObjectResult("上传文件过大");
                }
                //文件格式
                var fileExtension = Path.GetExtension(file.FileName);
                if (!pictureFormatArray.Contains(fileExtension))
                {
                    return new BadRequestObjectResult("上传文件格式错误");
                }

                var size = "";
                if (file.Length < 1024)
                    size = file.Length.ToString() + "B";
                else if (file.Length >= 1024 && file.Length < 1048576)
                    size = ((float)file.Length / 1024).ToString("F2") + "KB";
                else if (file.Length >= 1048576 && file.Length < 104857600)
                    size = ((float)file.Length / 1024 / 1024).ToString("F2") + "MB";
                else size = file.Length.ToString() + "B";

                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                }

                //TODO：文件md5哈希校验
                await _fileManager.Create(name, uniqueFileName, fileExtension, "", size, filePath, "/Images/" + uniqueFileName, FileType.Image);
            }
            return Ok(uniqueFileName);
        }
    }
}
