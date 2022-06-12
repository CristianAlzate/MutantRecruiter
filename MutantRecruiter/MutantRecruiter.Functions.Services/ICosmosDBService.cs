using Microsoft.Azure.Cosmos;
using MutantRecruiter.Functions.Services.Containers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MutantRecruiter.Functions.Services
{
    public interface ICosmosDBService<T> where T : CosmosContainer
    {
        Task<T> GetByQuery(string query);
        Task<List<T>> GetAll();
        Task Insert(T entity, PartitionKey key);
    }
}
