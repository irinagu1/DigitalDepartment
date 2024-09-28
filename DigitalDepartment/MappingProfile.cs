using AutoMapper;
using Entities.Models;
using Entities.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.DocumentCategories;
using Shared.DataTransferObjects.Documents;
using Shared.DataTransferObjects.DocumentStatuses;
using Shared.DataTransferObjects.Letters;
using Shared.DataTransferObjects.PermissionRole;
using Shared.DataTransferObjects.Recipients;
using Shared.DataTransferObjects.Roles;
using Shared.DataTransferObjects.ToCheck;
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
            CreateMap<Document, DocumentForUpdateDto>();
            CreateMap<DocumentForUpdateDto, Document>();
            CreateMap<DocumentDto, DocumentForUpdateDto>();
            CreateMap<DocumentForUpdateDto, DocumentDto>();

            CreateMap<DocumentCategoryForUpdateDto, Document>();


            CreateMap<UserForRegistrationDto, User>();

            CreateMap<string, Role>().ForMember(dest => dest.Name, opt => opt.ToString());
            
            CreateMap<UserForLettersDto, User>();
            CreateMap<User, UserForLettersDto>();

            CreateMap<RolesForLettersDto, Role>();
            CreateMap<Role, RolesForLettersDto>();
            CreateMap<RoleForCreationDto, Role>();
            CreateMap<Role, RoleForCreationDto>();
            CreateMap<Role, RolesDto>().ForMember(dest => dest.isActived, opt => opt.MapFrom(src=> src.IsActived.ToString()));
            CreateMap<RolesDto, Role>();
            CreateMap<Role, RoleForUpdateDto>();
            CreateMap<RoleForUpdateDto, Role>();


            CreateMap<LetterForCreationDto, Letter>();
            CreateMap<Letter, LetterDto>();

            CreateMap<Recipient, RecipientDto>();
            CreateMap<RecipientDto, Recipient>();

            CreateMap<ToCheck, ToCheckForCreateDto>();
            CreateMap<ToCheckForCreateDto, ToCheck>();
            CreateMap<ToCheck, ToCheckDto>();
            CreateMap<ToCheckDto, ToCheck>();

            CreateMap<Role, RolesDto>();
            CreateMap<RolesDto, Role>();

            CreateMap<Permission, PermissionDto>();
            CreateMap<PermissionDto, Permission>();

            CreateMap<PermissionRole, PermissionRoleDto>();
            CreateMap<PermissionRoleDto, PermissionRole>();

        }
    }
}
