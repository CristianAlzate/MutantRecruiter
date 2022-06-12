using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MutantRecruiter.Functions.Services.Containers
{
    public class Human : CosmosContainer
    {
        [JsonProperty("id")]
        public string ID { get; set; }
        public string[] DNA { get; set; }
        public bool IsMutant { get; set; }
        public override string GetByQuery()
        {
            return "SELECT * FROM c WHERE c.DNA = '{0}'";
        }

        public override string GetContainer()
        {
            return nameof(Human);
        }
    }
}
