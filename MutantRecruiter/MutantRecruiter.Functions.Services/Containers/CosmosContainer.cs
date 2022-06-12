using System;
using System.Collections.Generic;
using System.Text;

namespace MutantRecruiter.Functions.Services.Containers
{
    public abstract class CosmosContainer
    {
        public abstract string GetContainer();
        public abstract string GetByQuery();
    }
}
