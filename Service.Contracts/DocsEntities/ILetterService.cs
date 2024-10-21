using Shared.DataTransferObjects.Documents;
using Shared.DataTransferObjects.DocumentStatuses;
using Shared.DataTransferObjects.Letters;
using Shared.DataTransferObjects.Recipients;
using Shared.DataTransferObjects.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.DocsEntities
{
    public interface ILetterService
    {
        Task<LetterDto> CreateLetterAsync(LetterForCreationDto letterForCreationDto);
        Task<string> StoreRecipients(RecipientsForCreationDto recipientsForCreationDto);
        Task<IEnumerable<RecipientsForReportDto>> GetRecipientsForReportByLetterId(int LetterId, int documentId);

        Task<AllRecipientsByCategoryDto> GetRecipientsByLetterId(int letterId);
    }
}
