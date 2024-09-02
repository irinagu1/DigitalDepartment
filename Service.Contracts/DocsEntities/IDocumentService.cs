using Shared.DataTransferObjects.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.DocsEntities
{
    public interface IDocumentService
    {
        Task<DocumentDto> CreateDocumentAsync(DocumentForCreationDto documentForCreationDto);
    }
}
