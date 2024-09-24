using Shared.DataTransferObjects.DocumentStatuses;
using Shared.DataTransferObjects.Letters;
using Shared.DataTransferObjects.Recipients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.DocsEntities
{
    public interface ILetterService
    {
        Task<LetterDto> CreateLetterAsync(LetterForCreationDto letterForCreationDto);
        Task<string> StoreRecipients(RecipientsForCreationDto recipientsForCreationDto);
    }
}
