using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    public class Board
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        //todo - add more properties (see example in Test-project)

        //todo - should classes include the raw JSON as well (in some base class (easy but bigger payload and clunky in list-results))?
    }
}