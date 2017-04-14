using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Options;
using PairMe.Api.Configuration;
using PairMe.Api.Models;
using Action = PairMe.Api.Models.Action;

namespace PairMe.Api.Controllers
{
    [Route("api/[controller]")]
    public class PairController : Controller
    {
        private readonly DocumentDbOptions documentDbOptions;

        public PairController(IOptions<DocumentDbOptions> documentDbOptions)
        {
            this.documentDbOptions = documentDbOptions.Value;
        }

        public PairCommandResponse Post(PairCommandRequest commandRequest)
        {
            var availabilityDetails = GetAvailabilityDetails(commandRequest);

            var potentialPartners = GetPotentialPartners(availabilityDetails);

            if (!potentialPartners.Any())
            {
                AddToPotentialPartners(availabilityDetails);

                return new PairCommandResponse { Text = "There are no partners available yet, but we'll notify you if any become available" };
            }

            var response = new PairCommandFollowUpResponse
            {
                Text = "test question",
                Attachments =
                {
                    new Attachment
                    {
                        Actions =
                        {
                            new Action { Name = "A", Text = "a text", Type = "button", Value = "a" },
                            new Action { Name = "B", Text = "b text", Type = "button", Value = "b" }
                        }
                    }
                }
            };

            return response;
        }

        private void AddToPotentialPartners(PairOpportunity availabilityDetails)
        {
            using (var client = new DocumentClient(new Uri(documentDbOptions.AccountEndpoint), documentDbOptions.AccountKey))
            {
                client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(documentDbOptions.Database, documentDbOptions.Collection),
                                           availabilityDetails).Wait();
            }
        }

        private IList<PairOpportunity> GetPotentialPartners(PairOpportunity opportunity)
        {
            using (var client = new DocumentClient(new Uri(documentDbOptions.AccountEndpoint), documentDbOptions.AccountKey))
            {
                return
                    client.CreateDocumentQuery<PairOpportunity>(UriFactory.CreateDocumentCollectionUri(documentDbOptions.Database,
                                                                                                       documentDbOptions.Collection))
                          .Where(p => p.UserId != opportunity.UserId).ToList();
            }
        }

        private PairOpportunity GetAvailabilityDetails(PairCommandRequest commandRequest)
        {
            return new PairOpportunity
            {
                StartTime = DateTime.Now.AddHours(1),
                EndTime = DateTime.Now.AddHours(2),
                UserId = commandRequest.UserId
            };
        }
    }

    public class PairOpportunity
    {
        public string UserId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}