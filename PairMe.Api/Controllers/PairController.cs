using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace PairMe.Api.Controllers
{
    [Route("api/[controller]")]
    public class PairController : Controller
    {
        public PairCommandResponse Post(PairCommandRequest commandRequest)
        {
            var response = new PairCommandFollowUpResponse
            {
                Text = "test question",
                Attachments =
                {
                    new Attachment
                    {
                        Actions =
                        {
                            new Action {Name = "A", Text = "a text", Type = "button", Value = "a"},
                            new Action {Name = "B", Text = "b text", Type = "button", Value = "b"}
                        }
                    }
                }
            };

            return response;
        }
    }
}

public class PairCommandRequest
{
    public string Token { get; set; }
    [FromForm(Name = "team_id")]
    public string TeamId { get; set; }
    [FromForm(Name = "team_domain")]
    public string TeamDomain { get; set; }
    [FromForm(Name = "channel_id")]
    public string ChannelId { get; set; }
    [FromForm(Name = "channel_name")]
    public string ChannelName { get; set; }
    [FromForm(Name = "user_id")]
    public string UserId { get; set; }
    [FromForm(Name = "user_name")]
    public string Username { get; set; }
    public string Command { get; set; }
    public string Text { get; set; }
}

public class PairCommandResponse
{
    public string Text { get; set; }
}

public class PairCommandFollowUpResponse : PairCommandResponse
{
    public List<Attachment> Attachments { get; } = new List<Attachment>();
}

public class Attachment
{
    public List<Action> Actions { get; } = new List<Action>();
}

public class Action
{
    public string Name { get; set; }
    public string Text { get; set; }
    public string Type { get; set; }
    public string Value { get; set; }
}

