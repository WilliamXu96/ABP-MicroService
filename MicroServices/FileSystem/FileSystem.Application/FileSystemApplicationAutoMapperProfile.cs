using AutoMapper;
using FileSystem.FileManagement.Dto;
using FileSystem.Models;

namespace FileSystem
{
    public class FileSystemApplicationAutoMapperProfile : Profile
    {
        public FileSystemApplicationAutoMapperProfile()
        {
            CreateMap<FileInfo, FileInfoDto>();
        }
    }
}
