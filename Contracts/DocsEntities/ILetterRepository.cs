using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DocsEntities
{
    public interface ILetterRepository
    {
        void DeleteLetter(Letter letter);
        Task<Letter> GetLetterById(int id);
        Task<Letter> GetLetterByDocumentId(int documentId); 
        void CreateLetter(Letter letter);

    }
}
