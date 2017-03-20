using Microsoft.AspNetCore.Mvc;

namespace PairMe.Api.Controllers
{
    [Route("api/[controller]")]
    public class PairController : Controller
    {
        public PairCommandResponse Post(PairCommandRequest commandRequest)
        {
            return new PairCommandResponse
            {
                Text = $@"{commandRequest.Token} 
{commandRequest.TeamId} 
{commandRequest.TeamDomain} 
{commandRequest.ChannelId} 
{commandRequest.ChannelName} 
{commandRequest.UserId}
{commandRequest.Username}
{commandRequest.Command}
{commandRequest.Text}"
            };
        }
    }

    public class PairCommandRequest
    {
        public string Token { get; set; }
        [FromQuery(Name = "team_id")]
        public string TeamId { get; set; }
        [FromQuery(Name = "team_domain")]
        public string TeamDomain { get; set; }
        [FromQuery(Name = "channel_id")]
        public string ChannelId { get; set; }
        [FromQuery(Name = "channel_name")]
        public string ChannelName { get; set; }
        [FromQuery(Name = "user_id")]
        public string UserId { get; set; }
        [FromQuery(Name = "user_name")]
        public string Username { get; set; }
        public string Command { get; set; }
        public string Text { get; set; }
    }

    public class PairCommandResponse
    {
        public string Text { get; set; }
    }
}
