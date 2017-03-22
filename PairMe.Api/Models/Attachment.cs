using System.Collections.Generic;
using Newtonsoft.Json;

namespace PairMe.Api.Models
{
    public class Attachment
    {
        [JsonProperty("callback_id")]
        public string CallbackId { get; set; }
        public IList<Action> Actions { get; } = new List<Action>();
    }
}