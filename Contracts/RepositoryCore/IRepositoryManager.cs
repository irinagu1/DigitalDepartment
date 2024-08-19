using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.DocsEntities;

namespace Contracts.RepositoryCore
{
    public interface IRepositoryManager
    {
        ILetterRepository Letter { get; }
        IDocumentRepository Document { get; }
        IDocumentCategoryRepository DocumentCategory { get; }
        IDocumentStatusRepository DocumentStatus { get; }

        void Save();
    }
}
