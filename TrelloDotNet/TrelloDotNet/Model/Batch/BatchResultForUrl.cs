using System.Text.Json;
using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Batch
{
    /// <summary>
    /// Represent the Result of a batch-request of the specific URL
    /// </summary>
    public class BatchResultForUrl
    {
        /// <summary>
        /// The URL used for the request
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The generic data back as a JSON Element (Use GetData&lt;T&gt; to deserialize the data)
        /// </summary>
        [JsonPropertyName("200")]
        [JsonInclude]
        public JsonElement Data { get; private set; }

        /// <summary>
        /// Get the data of the result deserialized
        /// </summary>
        /// <typeparam name="T">The type to deserialize to</typeparam>
        /// <returns></returns>
        public T GetData<T>()
        {
            return Data.Deserialize<T>();
        }

        /// <summary>
        /// Name of any Error message if request was a failure
        /// </summary>
        [JsonPropertyName("name")]
        [JsonInclude]
        public string Name { get; private set; }

        /// <summary>
        /// Message explaining Error if request was a failure
        /// </summary>
        [JsonPropertyName("message")]
        [JsonInclude]
        public string Message { get; private set; }

        /// <summary>
        /// The StatusCode of the URL request (200 = Success, else Failure with more info in the Name and Message properties)
        /// </summary>
        [JsonPropertyName("statusCode")]
        [JsonInclude]
        public int StatusCode { get; private set; } = 200;
    }
}