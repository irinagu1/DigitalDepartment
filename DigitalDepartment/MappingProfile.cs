using AutoMapper;
using Entities.Models;
using Entities.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects.DocumentCategories;
using Shared.DataTransferObjects.Documents;
using Shared.DataTransferObjects.DocumentStatuses;
using Shared.DataTransferObjects.Users;

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

            CreateMap<Document, DocumentDto>();
            CreateMap<DocumentForCreationDto, Document>();
            CreateMap<DocumentCategoryForUpdateDto, Document>();

            CreateMap<UserForRegistrationDto, User>();

            CreateMap<string, Role>().ForMember(dest => dest.Name, opt => opt.ToString());

        }
    }
}
