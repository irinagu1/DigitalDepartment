using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Auth;
using Contracts.DocsEntities;

namespace Contracts.RepositoryCore
{
    public interface IRepositoryManager
    {
        ILetterRepository Letter { get; }
        IDocumentRepository Document { get; }
        IRecipientRepository Recipient { get; }
        IDocumentCategoryRepository DocumentCategory { get; }
        IDocumentStatusRepository DocumentStatus { get; }
        IToCheckRepository ToCheck { get; }



        IUserRepository User { get; }
        IRoleRepository Role { get; }
        IPermissionRepository Permission { get; }
        IPermissionRoleRepository PermissionRole { get; }
        IUserRoleRepository UserRole { get; }
        Task SaveAsync();
        void Save();
    }
}
