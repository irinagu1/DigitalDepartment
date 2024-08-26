using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects.DocumentCategories;
using Shared.DataTransferObjects.DocumentStatuses;

namespace DigitalDepartment
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<DocumentStatus, DocumentStatusDto>();
            CreateMap<DocumentStatusForCreationDto, DocumentStatus>();
            CreateMap<DocumentStatusForUpdateDto, DocumentStatus>();

            CreateMap<DocumentCategory, DocumentCategoryDto>();
            CreateMap<DocumentCategoryForCreationDto, DocumentCategory>();
            CreateMap<DocumentCategoryForUpdateDto, DocumentCategory>();

        }
    }
}
