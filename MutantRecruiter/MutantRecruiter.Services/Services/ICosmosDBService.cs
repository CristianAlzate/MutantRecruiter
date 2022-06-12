using Microsoft.Azure.Cosmos;
using MutantRecruiter.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutantRecruiter.Services.Services
{
    public interface ICosmosDBService<T> where T : CosmosContainer
    {
        Task<T> GetByQuery(string query);
        Task<List<T>> GetAll();
        Task Insert(T entity, PartitionKey key);
    }
}
