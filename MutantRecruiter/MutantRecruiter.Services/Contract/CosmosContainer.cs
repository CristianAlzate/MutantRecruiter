using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutantRecruiter.Services.Contract
{
    public abstract class CosmosContainer
    {
        public abstract string GetContainer();
        public abstract string GetByQuery();
    }
}
