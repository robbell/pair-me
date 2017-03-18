using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PairMe.Api.Controllers
{
    [Route("api/[controller]")]
    public class PairController : Controller
    {
        // POST api/pair
        [HttpPost]
        public CommandResponse Post(CommandRequest commandRequest)
        {
            return new CommandResponse { Text = $"{commandRequest.Text} {commandRequest.UserId} {commandRequest.User_Id}  {commandRequest.Username} {commandRequest.Token}" };
        }
    }

    public class CommandRequest
    {
        public string Token { get; set; }
        [JsonProperty("team_id")]
        public string TeamId { get; set; }
        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }
        [JsonProperty("channel_name")]
        public string ChannelName { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        public string User_Id { get; set; }
        public string Username { get; set; }
        public string Command { get; set; }
        public string Text { get; set; }
    }

    public class CommandResponse
    {
        public string Text { get; set; }
    }
}
