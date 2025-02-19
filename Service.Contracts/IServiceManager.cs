﻿using Service.Contracts.DocsEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IServiceManager
    {
        IPositionService PositionService { get; }
        IDocumentCategoryService DocumentCategoryService { get; }
        IDocumentStatusService DocumentStatusService { get; }
        IDocumentService DocumentService { get; }
        ILetterService LetterService { get; }
        IAuthenticationService AuthenticationService { get; }
        IUserService UserService { get; }
        IRoleService RoleService { get; }
        IPermissionService PermissionService { get; }
        IToCheckService ToCheckService { get; }
        IDocumentVersionService DocumentVersionService { get; }
    }
}
