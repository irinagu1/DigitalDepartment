using Contracts.Auth;
using Contracts.DocsEntities;
using Contracts.RepositoryCore;
using Repository.DocsEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Core
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;

        private readonly Lazy<IDocumentCategoryRepository> _documentCategoryRepository;
        private readonly Lazy<IDocumentStatusRepository> _documentStatusRepository;
        private readonly Lazy<IDocumentRepository> _documentRepository;
        private readonly Lazy<ILetterRepository> _letterRepository;
        private readonly Lazy<IRecipientRepository> _recipientRepository;
        private readonly Lazy<IToCheckRepository> _toCheckRepository;

        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IRoleRepository> _roleRepository;
        private readonly Lazy<IPermissionRepository> _permissionRepository;
        private readonly Lazy<IPermissionRoleRepository> _permissionRoleRepository;
        private readonly Lazy<IUserRoleRepository> _userRoleRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;

            _documentCategoryRepository = new Lazy<IDocumentCategoryRepository>(() =>
            new DocumentCategoryRepository(repositoryContext));
            
            _documentStatusRepository = new Lazy<IDocumentStatusRepository>(() =>
            new DocumentStatusRepository(repositoryContext));

            _documentRepository = new Lazy<IDocumentRepository>(() =>
            new DocumentRepository(repositoryContext));

            _letterRepository = new Lazy<ILetterRepository>(() 
            => new LetterRepository(repositoryContext));

            _recipientRepository = new Lazy<IRecipientRepository>(() =>
            new RecipientRepository(repositoryContext));

            _toCheckRepository = new Lazy<IToCheckRepository>(() =>
            new ToCheckRepository(repositoryContext));

            _userRepository = new Lazy<IUserRepository>(() 
                => new Auth.UserRepository(repositoryContext));

            _roleRepository = new Lazy<IRoleRepository>(() 
                => new Auth.RoleRepository(repositoryContext));
            _permissionRepository = new Lazy<IPermissionRepository>(()=> 
            new Auth.PermissionRepository(repositoryContext));

            _permissionRoleRepository = new Lazy<IPermissionRoleRepository>(()=>
            new Auth.PermissionRoleRepository(repositoryContext));

            _userRoleRepository = new Lazy<IUserRoleRepository>(() =>
            new Auth.UserRoleRepository(repositoryContext));
        }

        public IDocumentCategoryRepository DocumentCategory =>
            _documentCategoryRepository.Value;
        public IDocumentStatusRepository DocumentStatus => 
            _documentStatusRepository.Value;
        public IDocumentRepository Document => _documentRepository.Value;
        public ILetterRepository Letter => _letterRepository.Value;
        public IRecipientRepository Recipient => _recipientRepository.Value;
        public IUserRepository User => _userRepository.Value;
        public IRoleRepository Role => _roleRepository.Value;
        public IToCheckRepository ToCheck => _toCheckRepository.Value;
        public IPermissionRepository Permission => _permissionRepository.Value;
        public IPermissionRoleRepository PermissionRole => _permissionRoleRepository.Value;

        public IUserRoleRepository UserRole => _userRoleRepository.Value;
        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
        public void Save() => _repositoryContext.SaveChanges();

    }

}
