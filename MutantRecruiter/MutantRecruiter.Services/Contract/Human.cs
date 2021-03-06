using Newtonsoft.Json;
using System;

namespace MutantRecruiter.Services.Contract
{
    public class Human : CosmosContainer
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public string[] DNA { get; set; }
        public bool IsMutant { get; set; }

        public override string GetByQuery()
        {
            return "SELECT * FROM c WHERE c.DNA = {0}";
        }

        public override string GetContainer()
        {
            return nameof(Human);
        }
    }
}
