using FileStorage.Enums;
using FileStorage.FileManagement.Dto;
using FileStorage.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using FileInfo = FileStorage.Models.FileInfo;

namespace FileStorage.FileManagement
{
    public class FileAppService : ApplicationService, IFileAppService
    {
        private readonly IRepository<FileInfo, Guid> _repository;
        private readonly FileManager _fileManager;
        string[] pictureFormatArray = { ".png", ".jpg", ".jpeg", ".gif", ".PNG", ".JPG", ".JPEG", ".GIF" };

        public FileAppService(IRepository<FileInfo, Guid> repository, FileManager fileManager)
        {
            _repository = repository;
            _fileManager = fileManager;
        }

        public async Task<PagedResultDto<FileInfoDto>> GetAll(GetFileInputDto input)
        {
            var query = (await _repository.GetQueryableAsync()).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), _ => _.Name.Contains(input.Filter));

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting ?? "Name")
                                   .Skip(input.SkipCount)
                                   .Take(input.MaxResultCount)
                                   .ToListAsync();

            var dtos = ObjectMapper.Map<List<FileInfo>, List<FileInfoDto>>(items);
            return new PagedResultDto<FileInfoDto>(totalCount, dtos);
        }

        public async Task<FileInfoDto> Upload([Required] string name, [Required] IFormFile file)
        {
            if (file == null || file.Length == 0) throw new BusinessException("无法上传空文件");

            //限制100M
            if (file.Length > 104857600)
            {
                throw new BusinessException("上传文件过大");
            }
            //文件格式
            var fileExtension = Path.GetExtension(file.FileName);
            if (!pictureFormatArray.Contains(fileExtension))
            {
                throw new BusinessException("上传文件格式错误");
            }

            var size = "";
            if (file.Length < 1024)
                size = file.Length.ToString() + "B";
            else if (file.Length >= 1024 && file.Length < 1048576)
                size = ((float)file.Length / 1024).ToString("F2") + "KB";
            else if (file.Length >= 1048576 && file.Length < 104857600)
                size = ((float)file.Length / 1024 / 1024).ToString("F2") + "MB";
            else size = file.Length.ToString() + "B";

            string uploadsFolder = Path.Combine(Environment.CurrentDirectory, "wwwroot", "files");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            //TODO：文件md5哈希校验
            var result = await _fileManager.Create(name, uniqueFileName, fileExtension, "", size, filePath, "/files/" + uniqueFileName, FileType.Image);
            return ObjectMapper.Map<FileInfo, FileInfoDto>(result);
        }

        public async Task UploadPrivate([Required] string name, [Required] IFormFile file)
        {
            if (file == null || file.Length == 0) throw new BusinessException("无法上传空文件");
            if (file.Length > 104857600) throw new BusinessException("上传文件过大");
            var fileExtension = Path.GetExtension(file.FileName);
            if (!pictureFormatArray.Contains(fileExtension)) throw new BusinessException("上传文件格式错误");

            var size = "";
            if (file.Length < 1024)
                size = file.Length.ToString() + "B";
            else if (file.Length >= 1024 && file.Length < 1048576)
                size = ((float)file.Length / 1024).ToString("F2") + "KB";
            else if (file.Length >= 1048576 && file.Length < 104857600)
                size = ((float)file.Length / 1024 / 1024).ToString("F2") + "MB";
            else size = file.Length.ToString() + "B";

            string uploadsFolder = Path.Combine(Environment.CurrentDirectory, "files");
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            await _fileManager.Create(name, uniqueFileName, fileExtension, "", size, filePath, "/files/" + uniqueFileName, FileType.Image);
        }

        public dynamic Download([Required] string name)
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "files", name);
            if (!File.Exists(filePath)) throw new BusinessException("找不到文件");
            return new FileStreamResult(new FileStream(filePath, FileMode.Open), "application/octet-stream") { FileDownloadName = name };
        }
    }
}
