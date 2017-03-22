using System.Collections.Generic;

namespace PairMe.Api.Models
{
    public class PairCommandFollowUpResponse : PairCommandResponse
    {
        public IList<Attachment> Attachments { get; } = new List<Attachment>();
    }
}