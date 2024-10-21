using Shared.DataTransferObjects.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Documents
{
    public class RecipientsForReportDto
    {
        public UserDto User { get; set; }

        public DateTime? DateChecked {  get; set; }
    }
}
