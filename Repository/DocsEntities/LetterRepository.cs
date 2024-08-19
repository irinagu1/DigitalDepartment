﻿using Contracts.DocsEntities;
using Contracts.RepositoryCore;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DocsEntities
{
    public class LetterRepository : RepositoryBase<LetterRepository>, ILetterRepository
    {
        public LetterRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}
