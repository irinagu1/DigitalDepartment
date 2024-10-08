using AutoMapper;
using Contracts.RepositoryCore;
using Entities.ConfigurationModels;
using Entities.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Service.Contracts;
using Service.Contracts.DocsEntities;
using Service.DocsEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IDocumentStatusService> _documentStatusService;
        private readonly Lazy<IDocumentCategoryService> _documentCategoryService;
        private readonly Lazy<IDocumentService> _documentService;
        private readonly Lazy<ILetterService> _letterService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IRoleService> _roleService;
        private readonly Lazy<IToCheckService> _toCheckService;
        private readonly Lazy<IPermissionService> _permissionService;
        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, ICheckerService checker, 
                              IFilesService filesService, UserManager<User> userManager,
                             IConfiguration configuration)
        {
            _documentStatusService = new Lazy<IDocumentStatusService>
                (() => new DocumentStatusService(repositoryManager, mapper, checker));
            _documentCategoryService = new Lazy<IDocumentCategoryService>
                (() => new DocumentCategoryService(repositoryManager, mapper, checker));
            _documentService = new Lazy<IDocumentService>
                (() => new DocumentService(repositoryManager, mapper, checker, filesService));
            _letterService = new Lazy<ILetterService>
                (() => new LetterService(repositoryManager, mapper));
            _authenticationService = new Lazy<IAuthenticationService>(() =>
                new AuthenticationService(mapper, userManager,
                    configuration));
            _userService = new Lazy<IUserService>(() =>
                 new UserService(repositoryManager, mapper, checker));
            _roleService = new Lazy<IRoleService>(() =>
                new RoleService(repositoryManager, mapper, checker));
            _toCheckService = new Lazy<IToCheckService>(()=> 
                new ToCheckService(repositoryManager, mapper));
            _permissionService = new Lazy<IPermissionService>(()=> 
            new PermissionService(repositoryManager, mapper));
        }

        public IDocumentStatusService DocumentStatusService 
            => _documentStatusService.Value;
        public IDocumentCategoryService DocumentCategoryService
            => _documentCategoryService.Value;
        public IDocumentService DocumentService 
            => _documentService.Value;
        public ILetterService LetterService 
            => _letterService.Value;
        public IAuthenticationService AuthenticationService 
            => _authenticationService.Value;
        public IUserService UserService
            => _userService.Value;
        public IRoleService RoleService
            => _roleService.Value;
        public IToCheckService ToCheckService
            => _toCheckService.Value;
        public IPermissionService PermissionService
            => _permissionService.Value;

    }
}
