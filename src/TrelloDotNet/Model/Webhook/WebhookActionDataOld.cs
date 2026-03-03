using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// Collection of Old values of the Webhook Action Data
    /// </summary>
    public class WebhookActionDataOld
    {
        /// <summary>
        /// due
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Due)]
        [JsonInclude]
        public DateTimeOffset? Due { get; private set; }

        /// <summary>
        /// dueComplete
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.DueComplete)]
        [JsonInclude]
        public bool? DueComplete { get; private set; }

        /// <summary>
        /// start
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Start)]
        [JsonInclude]
        public DateTimeOffset? Start { get; private set; }

        /// <summary>
        /// idLabels
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.IdLabels)]
        [JsonInclude]
        public List<string> Labels { get; private set; }

        /// <summary>
        /// locationName
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.WebhookFields.LocationName)]
        [JsonInclude]
        public string LocationName { get; private set; }

        /// <summary>
        /// address
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.WebhookFields.Address)]
        [JsonInclude]
        public string Address { get; private set; }

        /// <summary>
        /// name
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.WebhookFields.Name)]
        [JsonInclude]
        public string Name { get; private set; }

        /// <summary>
        /// desc
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.WebhookFields.Desc)]
        [JsonInclude]
        public string Description { get; private set; }

        /// <summary>
        /// idList
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ListFields.IdList)]
        [JsonInclude]
        public string ListId { get; private set; }

        /// <summary>
        /// pos
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ListFields.Pos)]
        [JsonInclude]
        public decimal? Position { get; private set; }

        /// <summary>
        /// dueReminder
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.DueReminder)]
        [JsonInclude]
        public int? DueReminder { get; private set; }

        /// <summary>
        /// coordinates
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.WebhookFields.Coordinates)]
        [JsonInclude]
        public Coordinates Coordinates { get; private set; }

        /// <summary>
        /// cover
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.Cover)]
        [JsonInclude]
        public CardCover Cover { get; private set; }

        /// <summary>
        /// Id of the image attachment of this card to use as its cover
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.CardFields.IdAttachmentCover)]
        [JsonInclude]
        public string AttachmentCover { get; private set; }

        /// <summary>
        /// Display Name of the Organization
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.WebhookFields.DisplayName)]
        [JsonInclude]
        public string DisplayName { get; private set; }

        /// <summary>
        /// Website of the Organization
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.OrganizationFields.Website)]
        [JsonInclude]
        public string Website { get; private set; }

        internal static WebhookActionDataOld CreateDummy()
        {
            return new WebhookActionDataOld()
            {
                DueComplete = true,
                Address = "My Street",
                Coordinates = Coordinates.CreateDummy(),
                Name = "MyName",
                Description = "MyDescription",
                Due = new DateTimeOffset(new DateTime(2022, 12, 31)),
                DueReminder = 10,
                Labels = new List<string> { "a", "b", "c" },
                ListId = "63d1239e857afaa8b003c633",
                LocationName = "My Location",
                Position = 42,
                Start = new DateTimeOffset(new DateTime(2023, 1, 1))
            };
        }
    }
}





