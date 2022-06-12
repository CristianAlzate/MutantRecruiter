using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using MutantRecruiter.Services.Contract;
using MutantRecruiter.Services.Services;
using Newtonsoft.Json;

namespace MutantRecruiter.Functions
{
    public class FunctionHumanDNA
    {
        private readonly ICosmosDBService<Human> _service;
        public FunctionHumanDNA(ICosmosDBService<Human> service)
        {
            _service = service;
        }
        [FunctionName("FunctionHumanDNA")]
        public async Task Run([QueueTrigger("queuedna", Connection = "QueueConnectionString")]string myQueueItem, ILogger log)
        {
            Human human = JsonConvert.DeserializeObject<Human>(myQueueItem);
            if (await _service.GetByQuery(JsonConvert.SerializeObject(human.DNA)) == null)
            {
                human.Id = Guid.NewGuid().ToString();
                _service.Insert(human, new PartitionKey(human.Id));
            }
        }
    }
}
