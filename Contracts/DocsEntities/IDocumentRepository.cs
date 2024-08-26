using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DocsEntities
{
    public interface IDocumentRepository
    {
        //get all with filtering
        //get one
        //create one
        void CreateDocument(Document document);

        //create collection
        //delete document
    }
}
