using FileSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace FileSystem.Controllers
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
        [Route("file/upload")]
        public async Task<ActionResult> Upload([Required]string fileName, IFormFile file)
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
                await _fileManager.Create(fileName, "", file.Length, filePath);
            }
            return Ok(uniqueFileName);
        }
    }
}
