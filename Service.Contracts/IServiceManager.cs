using Service.Contracts.DocsEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IServiceManager
    {
        IDocumentCategoryService DocumentCategoryService { get; }
        IDocumentStatusService DocumentStatusService { get; }
        IDocumentService DocumentService { get; }
        ILetterService LetterService { get; }
    }
}
