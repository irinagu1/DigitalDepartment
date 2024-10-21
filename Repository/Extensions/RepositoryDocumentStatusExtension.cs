using Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class RepositoryDocumentStatusExtension
    {
        public static IQueryable<DocumentStatus> FilterDocumentStatuses(this IQueryable<DocumentStatus> statuses, DocumentStatusParameters documentStatusParameters)
        {
            if (documentStatusParameters.isEnable is null)
                return statuses;
            return statuses.Where(s=> s.isEnable == documentStatusParameters.isEnable);
        }
    }
}
