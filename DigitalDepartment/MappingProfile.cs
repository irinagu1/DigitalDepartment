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
using Shared.DataTransferObjects.Position;
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

            CreateMap<Position, PositionDto>();
            CreateMap<PositionForCreationDto, Position>();
            CreateMap<PositionForUpdateDto, Position>();

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
            CreateMap<User, UserDto>().ForMember("FullName", opt => opt.MapFrom(
                src => src.FirstName + ' ' + src.SecondName + ' ' + src.LastName));
            CreateMap<UserDto, User>();
            CreateMap<User, UserForUpdateDto>();
            CreateMap<UserForUpdateDto, User>();

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

            CreateMap<UserRole, UserRoleDto>();
            CreateMap<UserRoleDto, UserRole>();
        }
    }
}
