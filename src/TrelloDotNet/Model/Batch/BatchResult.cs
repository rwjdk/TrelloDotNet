using System.Text.Json;
using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Batch
{
    /// <summary>
    /// Represents the result of a batch request to the Trello API, containing the raw response data.
    /// </summary>
    public class BatchResult
    {
        /// <summary>
        /// The raw response data from the batch request as a JSON element. Use <see cref="GetData{T}"/> to deserialize this data into a specific type.
        /// </summary>
        [JsonPropertyName("200")]
        [JsonInclude]
        public JsonElement Data { get; private set; }

        /// <summary>
        /// Deserializes the raw response data into the specified type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the response data to.</typeparam>
        /// <returns>The deserialized response data as the specified type.</returns>
        public T GetData<T>()
        {
            return Data.Deserialize<T>();
        }
    }
}