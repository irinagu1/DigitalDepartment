using Entities.Models;
using Repository.Extensions.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Globalization;
using Repository.DocsEntities;
using Shared.DataTransferObjects.DocumentVersion;
using Shared.DataTransferObjects.Documents;


namespace Repository.Extensions
{
    public static class RepositoryDocumentExtension

    {

        public static IQueryable<DocumentShowDto> Filter
         (this IQueryable<DocumentShowDto> documentDtos, string date)
        {
            if (string.IsNullOrWhiteSpace(date))
                return documentDtos;

            if (date is not null)
            {
                DateTime dateDt = DateTime.Parse(date);
                var dateValue = dateDt.Date;
                documentDtos = documentDtos.Where(
                    e => e.LetterCreationDate.Date.Year == dateDt.Date.Year &&
                         e.LetterCreationDate.Date.Month == dateDt.Date.Month &&
                         e.LetterCreationDate.Date.Day == dateDt.Date.Day
                );
            }
            return documentDtos;
        }

        public static IQueryable<DocumentShowDto> Search
            (this IQueryable<DocumentShowDto> documentDtos,
            string searchName,
            string searchAuthor)
        {
            if (string.IsNullOrWhiteSpace(searchName) &&
                string.IsNullOrWhiteSpace(searchAuthor))
                return documentDtos;

            string lowerCaseTerm;

            if (!string.IsNullOrWhiteSpace(searchName))
            {
                lowerCaseTerm = searchName.Trim().ToLower();
                documentDtos = documentDtos.Where(e =>
                    e.Name.ToLower().Contains(lowerCaseTerm));
            }

            if (!string.IsNullOrWhiteSpace(searchAuthor))
            {
                lowerCaseTerm = searchAuthor.Trim().ToLower();
                documentDtos = documentDtos.Where(e =>
                    e.LetterAuthorFullName.ToLower().Contains(lowerCaseTerm)
                );
            }

            return documentDtos;
        }

        public static IQueryable<DocumentShowDto> Sort
            (this IQueryable<DocumentShowDto> documentDtos)
        {
            documentDtos = documentDtos.OrderByDescending(e => e.LetterCreationDate);
            return documentDtos;
        }


        //CHECKED

        public static IQueryable<Document> SortDocuments(this IQueryable<Document> documents, 
            string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return documents.OrderBy(e => e.Name);
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Document>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return documents.OrderBy(e => e.Name);

            return documents.OrderBy(orderQuery);
        }

        public static IQueryable<Document> SearchDocuments(this IQueryable<Document> documents, string searchName, string searchAuthor)
        {
            if (string.IsNullOrWhiteSpace(searchName) && string.IsNullOrWhiteSpace(searchAuthor))
                return documents;
            string lowerCaseTerm;
            if (!string.IsNullOrWhiteSpace(searchName))
            {
                lowerCaseTerm = searchName.Trim().ToLower();
                documents = documents.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
            }

            if (!string.IsNullOrWhiteSpace(searchAuthor))
            {
                lowerCaseTerm = searchAuthor.Trim().ToLower();
                return documents.Where(d => d.Letter.Author.FirstName.ToLower().Contains(lowerCaseTerm) ||
                                            d.Letter.Author.SecondName.ToLower().Contains(lowerCaseTerm) ||
                                             d.Letter.Author.LastName.ToLower().Contains(lowerCaseTerm) 
                );
            }

            return documents;
        }
     
        public static IQueryable<Document> FilterDocuments(this IQueryable<Document> documents, string status, string category, string date)
        {
            if (string.IsNullOrWhiteSpace(status) && string.IsNullOrWhiteSpace(category) && string.IsNullOrWhiteSpace(date))
                return documents;
            if (date is not null)
            {
                DateTime dateDt = DateTime.Parse(date);
                var a = dateDt.Date;
               
               /* documents = documents.Where(e => e.CreationDate.Value.Date.Year == dateDt.Date.Year 
                                                && e.CreationDate.Value.Date.Month == dateDt.Date.Month
                                                && e.CreationDate.Value.Date.Day == dateDt.Date.Day
                                                );*/
            }

            if(status is not null)
                documents = documents.Where(e=>e.DocumentStatus.Name == status);
            if (category is not null)
                documents = documents.Where(e => e.DocumentCategory.Name == category);
            return documents;
        }


    }
}
