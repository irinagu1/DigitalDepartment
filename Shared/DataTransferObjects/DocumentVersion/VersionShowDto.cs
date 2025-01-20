using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.DocumentVersion
{
    public class VersionShowDto
    {
        public long versionId { get; set; }
        public int numberVersion { get; set; }
        public DateTime versionCreationDate { get; set; }
        public string versionMessage { get; set; }
        public string versionAuthorId { get; set; }
        public string versionAuthorFullName { get; set; }
        public int documentId { get; set; }
        public string documentName { get; set; }
        public int documentStatusId { get; set; }
        public string documentStatusName { get; set; }
        public int documentCategoryId { get; set; }
        public string documentCategoryName { get; set; }
        public int letterId { get; set; }
        public bool toCheck { get; set; }
        public bool? isChecked { get; set; }
        public bool? canDelete { get; set; }
    }
}
