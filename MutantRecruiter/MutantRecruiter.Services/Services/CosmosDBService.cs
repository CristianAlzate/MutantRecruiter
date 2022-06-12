using Microsoft.Azure.Cosmos;
using MutantRecruiter.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutantRecruiter.Services.Services
{
    public class CosmosDBService<T> : ICosmosDBService<T> where T : CosmosContainer, new()
    {
        private Database _db;
        private Container _container;
        private CosmosClient _client;

        public CosmosDBService(string endPoint, string key, string database)
        {
            _client = new CosmosClient(endPoint, key);
            _db = _client.GetDatabase(database);
            _container = _db.GetContainer(new T().GetContainer());
        }

        public async Task<List<T>> GetAll()
        {
            FeedIterator<T> queryResultSetIterator = _container.GetItemQueryIterator<T>();
            List<T> objs = new List<T>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<T> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (T item in currentResultSet)
                {
                    objs.Add(item);
                }
            }
            return objs;
        }

        public async Task<T> GetByQuery(string query)
        {
            try
            {
                QueryDefinition queryDefinition = new QueryDefinition(string.Format(new T().GetByQuery(), query));
                FeedIterator<T> queryResultSetIterator = _container.GetItemQueryIterator<T>(queryDefinition);

                if (queryResultSetIterator.HasMoreResults)
                {
                    FeedResponse<T> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    return currentResultSet.Resource.FirstOrDefault();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task Insert(T entity, PartitionKey key)
        {
            await _container.CreateItemAsync(entity, key);
        }
    }
}
