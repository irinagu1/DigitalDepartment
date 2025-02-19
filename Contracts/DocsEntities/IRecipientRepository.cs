﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DocsEntities
{
    public interface IRecipientRepository
    {
        void CreateRecipient(Recipient recipient);
        Task<IEnumerable<Recipient>> GetRecipientsByTypeAndLetterId(string type, int letterId, bool isToCheck);
    }
}
