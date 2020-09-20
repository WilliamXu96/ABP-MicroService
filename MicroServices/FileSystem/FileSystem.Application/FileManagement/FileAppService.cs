using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace FileSystem.FileManagement
{
    public class FileAppService : ApplicationService, IFileAppService
    {
        public Task Create()
        {
            return null;
        }
    }
}
