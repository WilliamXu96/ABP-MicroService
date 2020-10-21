using AutoMapper;
using FileStorage.FileManagement.Dto;
using FileStorage.Models;

namespace FileStorage
{
    public class FileStorageApplicationAutoMapperProfile : Profile
    {
        public FileStorageApplicationAutoMapperProfile()
        {
            CreateMap<FileInfo, FileInfoDto>();
        }
    }
}
