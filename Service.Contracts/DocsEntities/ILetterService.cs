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
        Task<bool> CheckAfterUploadingAndClearIfUncorrect(int letterId);
        Task CreateReport(long versionId);
        Task<LetterDto> CreateLetterAsync(LetterForCreationDto letterForCreationDto);
        Task<string> StoreRecipients(RecipientsForCreationDto recipientsForCreationDto);
        Task<IEnumerable<RecipientsForReportDto>> GetRecipientsForReportByVersionId(long versionId);

        Task<AllRecipientsByCategoryDto> GetRecipientsByLetterId(int letterId);
    }
}
