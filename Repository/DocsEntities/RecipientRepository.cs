using Contracts.DocsEntities;
using Entities.Models;
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
        public RecipientRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public void CreateRecipient(Recipient recipient) => Create(recipient);
    }
}
