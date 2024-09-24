using AutoMapper;
using Entities.Models;
using Entities.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects.DocumentCategories;
using Shared.DataTransferObjects.Documents;
using Shared.DataTransferObjects.DocumentStatuses;
using Shared.DataTransferObjects.Letters;
using Shared.DataTransferObjects.Recipients;
using Shared.DataTransferObjects.Roles;
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
            
            CreateMap<UserForLettersDto, User>();
            CreateMap<User, UserForLettersDto>();

            CreateMap<RolesForLettersDto, Role>();
            CreateMap<Role, RolesForLettersDto>();

            CreateMap<LetterForCreationDto, Letter>();
            CreateMap<Letter, LetterDto>();

            CreateMap<Recipient, RecipientDto>();
            CreateMap<RecipientDto, Recipient>();

        }
    }
}
