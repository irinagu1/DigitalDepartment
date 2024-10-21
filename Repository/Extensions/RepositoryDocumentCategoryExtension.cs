using Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class RepositoryDocumentCategoryExtension
    {
        public static IQueryable<DocumentCategory> FilterDocumentCategories
            (this IQueryable<DocumentCategory> categories, DocumentCategoryParameters documentCategoryParameters)
        {
            if (documentCategoryParameters.isEnable is null)
                return categories;
            return categories.Where(s=> s.isEnable == documentCategoryParameters.isEnable);
        }
    }
}
