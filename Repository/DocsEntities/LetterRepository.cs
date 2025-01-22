using Contracts.DocsEntities;
using Contracts.RepositoryCore;
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
    public class LetterRepository : RepositoryBase<Letter>, ILetterRepository
    {
        private RepositoryContext _repositoryContext;
        public LetterRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }



        public async Task<Letter> GetLetterByDocumentId(int documentId)
        {
            var letter = await _repositoryContext.Documents
                .Where(d => d.Id == documentId)
                .Select(d => d.Letter)
                .FirstOrDefaultAsync();
            return letter;
        }
        public void DeleteLetter(Letter letter) => Delete(letter);
        public void CreateLetter(Letter letter) => Create(letter);

        public async Task<Letter> GetLetterById(int id) => 
            await FindByCondition(l => l.Id == id, false).SingleOrDefaultAsync();
    }
}
