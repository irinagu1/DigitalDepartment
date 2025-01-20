using Repository.Extensions.Utility;
using Shared.DataTransferObjects.DocumentVersion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class RepositoryDocumentVersionExtension
    {
        public static IQueryable<VersionShowDto> FilterVersion
            (this IQueryable<VersionShowDto> versionDtos, 
            string status, 
            string category, 
            string date)
        {
            if (string.IsNullOrWhiteSpace(status) && 
                string.IsNullOrWhiteSpace(category) && 
                string.IsNullOrWhiteSpace(date))
                return versionDtos;
            
            if (date is not null)
            {
                DateTime dateDt = DateTime.Parse(date);
                var dateValue = dateDt.Date;
                versionDtos = versionDtos.Where(
                    e => e.versionCreationDate.Date.Year == dateDt.Date.Year &&
                         e.versionCreationDate.Date.Month == dateDt.Date.Month &&
                         e.versionCreationDate.Date.Day == dateDt.Date.Day
                );
            }

            if (status is not null)
                versionDtos = versionDtos.Where(e => 
                    e.documentStatusName == status);
         
            if (category is not null)
                versionDtos = versionDtos.Where(e => 
                e.documentCategoryName == category);
           
            return versionDtos;
        }

        public static IQueryable<VersionShowDto> SearchVersion
            (this IQueryable<VersionShowDto> versionDtos, 
            string searchName, 
            string searchAuthor)
        {
            if (string.IsNullOrWhiteSpace(searchName) && 
                string.IsNullOrWhiteSpace(searchAuthor))
                return versionDtos;
            
            string lowerCaseTerm;
            
            if (!string.IsNullOrWhiteSpace(searchName))
            {
                lowerCaseTerm = searchName.Trim().ToLower();
                versionDtos= versionDtos.Where(e => 
                    e.documentName.ToLower().Contains(lowerCaseTerm));
            }

            if (!string.IsNullOrWhiteSpace(searchAuthor))
            {
                lowerCaseTerm = searchAuthor.Trim().ToLower();
                versionDtos = versionDtos.Where(e =>
                    e.versionAuthorFullName.ToLower().Contains(lowerCaseTerm)
                );
            }

            return versionDtos;
        }

        public static IQueryable<VersionShowDto> Sort
            (this IQueryable<VersionShowDto> versionDtos)
        {
            versionDtos = versionDtos.OrderByDescending(e => e.versionCreationDate)
                .ThenByDescending(e => e.numberVersion);
            
            return versionDtos;
        }
    }
}
