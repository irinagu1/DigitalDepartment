using Shared.DataTransferObjects.Documents;
using Shared.DataTransferObjects.DocumentVersion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.DocsEntities
{
    public interface IDocumentVersionService
    {
        DocumentVersionForCreationDto GetVersionForCreationDto(
            int number,
            int documentId,
            string path,
            string message
            );
        Task CreateVersionEntity(int number, string path, DocumentDto documentDto);

    }
}
