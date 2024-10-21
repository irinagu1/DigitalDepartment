using Contracts.DocsEntities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DocsEntities
{
    public class RecipientRepository  : RepositoryBase<Recipient>, IRecipientRepository
    {
        private RepositoryContext _repositoryContext;
        public RecipientRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public void CreateRecipient(Recipient recipient) => Create(recipient);

        public async Task<IEnumerable<Recipient>> GetRecipientsByTypeAndLetterId(string type, int letterId, bool isToCheck)
        {
            var recipients = await _repositoryContext.Recipients.Where(r => r.LetterId == letterId && r.Type == type && r.ToCheck == isToCheck).ToListAsync();
            return recipients;
        }

    }
}
