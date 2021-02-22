using Newtonsoft.Json;
using Cosmonaut.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Cosmos
{
    [CosmosCollection("posts")]
    public class CosmosPost
    {
        [CosmosPartitionKey]
        [JsonProperty]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
