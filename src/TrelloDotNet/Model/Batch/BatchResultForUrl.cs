using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Batch
{
    /// <summary>
    /// Represent the Result of a batch-request of the specific URL
    /// </summary>
    internal class BatchResultForUrl : BatchResult
    {
        /// <summary>
        /// The URL used for the request
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Name of any Error message if request was a failure
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Name)]
        [JsonInclude]
        public string Name { get; private set; }

        /// <summary>
        /// Message explaining Error if request was a failure
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.Message)]
        [JsonInclude]
        public string Message { get; private set; }

        /// <summary>
        /// The StatusCode of the URL request (200 = Success, else Failure with more info in the Name and Message properties)
        /// </summary>
        [JsonPropertyName(Constants.TrelloIds.ActionFields.StatusCode)]
        [JsonInclude]
        public int StatusCode { get; private set; } = 200;
    }
}




