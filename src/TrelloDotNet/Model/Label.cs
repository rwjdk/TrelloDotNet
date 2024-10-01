using System;
using System.Diagnostics;
using System.Linq;
using System.Text.Json.Serialization;
using TrelloDotNet.Control;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent a Label defined on a Board and Assigned to Cards
    /// </summary>
    [DebuggerDisplay("{Name} (Id: {Id})")]
    public class Label
    {
        /// <summary>
        /// Id of the Label
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Name of the Label
        /// </summary>
        [JsonPropertyName("name")]
        [QueryParameter]
        public string Name { get; set; }

        /// <summary>
        /// Color of the Label (Valid values: yellow, purple, blue, red, green, orange, black, sky, pink, lime (+ _light and _dark variants))
        /// </summary>
        [JsonPropertyName("color")]
        [QueryParameter]
        public string Color { get; set; }

        /// <summary>
        /// Color as Enum
        /// </summary>
        [JsonIgnore]
        public LabelColor ColorAsEnum => Enum.GetValues(typeof(LabelColor)).Cast<LabelColor>().FirstOrDefault(x => x.GetJsonPropertyName() == Color);

        /// <summary>
        /// Color as Info
        /// </summary>
        [JsonIgnore]
        public LabelColorInfo ColorAsInfo => ColorAsEnum.GetColorInfo();

        /// <summary>
        /// Id of the Board the Label belong to
        /// </summary>
        [JsonPropertyName("idBoard")]
        [JsonInclude]
        [QueryParameter]
        public string BoardId { get; private set; }

        /// <summary>
        /// When the Label was created [stored in UTC]
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset? Created => IdToCreatedHelper.GetCreatedFromId(Id);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="boardId">Id of the Board to add the label to</param>
        /// <param name="name">Optional Name of the Label</param>
        /// <param name="color">Optional Color of the Label</param>
        public Label(string boardId, string name = null, string color = null)
        {
            BoardId = boardId;
            Name = name;
            Color = color;
        }
    }
}