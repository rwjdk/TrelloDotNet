using System.Text.Json;
using System.Text.Json.Serialization;

namespace TrelloDotNet.Model.Batch
{
    /// <summary>
    /// Represent a batch-result
    /// </summary>
    public class BatchResult
    {
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
    }
}