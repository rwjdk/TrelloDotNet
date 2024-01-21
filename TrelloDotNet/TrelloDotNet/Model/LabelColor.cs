using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// The Label Colors
    /// </summary>
    public enum LabelColor
    {
        /// <summary>
        /// None
        /// </summary>
        [LabelColorInfo(-1, -1, -1, "", -1, -1, -1, "")] [JsonPropertyName("")]
        None = 0,

        /// <summary>
        /// green_light
        /// </summary>
        [LabelColorInfo(22, 75, 53, "#164B35", 186, 243, 219, "#BAF3DB")] [JsonPropertyName("green_light")]
        LightGreen,

        /// <summary>
        /// green
        /// </summary>
        [LabelColorInfo(22, 75, 53, "#164B35", 75, 206, 151, "#4BCE97")] [JsonPropertyName("green")]
        Green,

        /// <summary>
        /// green_dark
        /// </summary>
        [LabelColorInfo(255, 255, 255, "#FFFFFF", 31, 132, 90, "#1F845A")] [JsonPropertyName("green_dark")]
        DarkGreen,

        /// <summary>
        /// yellow_light
        /// </summary>
        [LabelColorInfo(83, 63, 4, "#533F04", 248, 230, 160, "#F8E6A0")] [JsonPropertyName("yellow_light")]
        LightYellow,

        /// <summary>
        /// yellow
        /// </summary>
        [LabelColorInfo(83, 63, 4, "#533F04", 245, 205, 71, "#F5CD47")] [JsonPropertyName("yellow")]
        Yellow,

        /// <summary>
        /// yellow_dark
        /// </summary>
        [LabelColorInfo(255, 255, 255, "#FFFFFF", 148, 111, 0, "#946F00")] [JsonPropertyName("yellow_dark")]
        DarkYellow,

        /// <summary>
        /// orange_light
        /// </summary>
        [LabelColorInfo(112, 46, 0, "#702E00", 254, 222, 200, "#FEDEC8")] [JsonPropertyName("orange_light")]
        LightOrange,

        /// <summary>
        /// orange
        /// </summary>
        [LabelColorInfo(112, 46, 0, "#702E00", 254, 163, 98, "#FEA362")] [JsonPropertyName("orange")]
        Orange,

        /// <summary>
        /// orange_dark
        /// </summary>
        [LabelColorInfo(255, 255, 255, "#FFFFFF", 194, 81, 0, "#C25100")] [JsonPropertyName("orange_dark")]
        DarkOrange,

        /// <summary>
        /// red_light
        /// </summary>
        [LabelColorInfo(93, 31, 26, "#5D1F1A", 255, 213, 210, "#FFD5D2")] [JsonPropertyName("red_light")]
        LightRed,

        /// <summary>
        /// red
        /// </summary>
        [LabelColorInfo(93, 31, 26, "#5D1F1A", 248, 113, 104, "#F87168")] [JsonPropertyName("red")]
        Red,

        /// <summary>
        /// red_dark
        /// </summary>
        [LabelColorInfo(255, 255, 255, "#FFFFFF", 201, 55, 44, "#C9372C")] [JsonPropertyName("red_dark")]
        DarkRed,

        /// <summary>
        /// purple_light
        /// </summary>
        [LabelColorInfo(53, 44, 99, "#352C63", 223, 216, 253, "#DFD8FD")] [JsonPropertyName("purple_light")]
        LightPurple,

        /// <summary>
        /// purple
        /// </summary>
        [LabelColorInfo(53, 44, 99, "#352C63", 159, 143, 239, "#9F8FEF")] [JsonPropertyName("purple")]
        Purple,

        /// <summary>
        /// purple_dark
        /// </summary>
        [LabelColorInfo(255, 255, 255, "#FFFFFF", 110, 93, 198, "#6E5DC6")] [JsonPropertyName("purple_dark")]
        DarkPurple,

        /// <summary>
        /// blue_light
        /// </summary>
        [LabelColorInfo(9, 50, 108, "#09326C", 204, 224, 255, "#CCE0FF")] [JsonPropertyName("blue_light")]
        LightBlue,

        /// <summary>
        /// blue
        /// </summary>
        [LabelColorInfo(9, 50, 108, "#09326C", 87, 157, 255, "#579DFF")] [JsonPropertyName("blue")]
        Blue,

        /// <summary>
        /// blue_dark
        /// </summary>
        [LabelColorInfo(255, 255, 255, "#FFFFFF", 12, 102, 228, "#0C66E4")] [JsonPropertyName("blue_dark")]
        DarkBlue,

        /// <summary>
        /// sky_light
        /// </summary>
        [LabelColorInfo(22, 69, 85, "#164555", 198, 237, 251, "#C6EDFB")] [JsonPropertyName("sky_light")]
        LightSky,

        /// <summary>
        /// sky
        /// </summary>
        [LabelColorInfo(22, 69, 85, "#164555", 108, 195, 224, "#6CC3E0")] [JsonPropertyName("sky")]
        Sky,

        /// <summary>
        /// sky_dark
        /// </summary>
        [LabelColorInfo(255, 255, 255, "#FFFFFF", 34, 125, 155, "#227D9B")] [JsonPropertyName("sky_dark")]
        DarkSky,

        /// <summary>
        /// lime_light
        /// </summary>
        [LabelColorInfo(55, 71, 31, "#37471F", 211, 241, 167, "#D3F1A7")] [JsonPropertyName("lime_light")]
        LightLime,

        /// <summary>
        /// lime
        /// </summary>
        [LabelColorInfo(55, 71, 31, "#37471F", 148, 199, 72, "#94C748")] [JsonPropertyName("lime")]
        Lime,

        /// <summary>
        /// lime_dark
        /// </summary>
        [LabelColorInfo(255, 255, 255, "#FFFFFF", 91, 127, 36, "#5B7F24")] [JsonPropertyName("lime_dark")]
        DarkLime,

        /// <summary>
        /// pink_light
        /// </summary>
        [LabelColorInfo(80, 37, 63, "#50253F", 253, 208, 236, "#FDD0EC")] [JsonPropertyName("pink_light")]
        LightPink,

        /// <summary>
        /// pink
        /// </summary>
        [LabelColorInfo(80, 37, 63, "#50253F", 231, 116, 187, "#E774BB")] [JsonPropertyName("pink")]
        Pink,

        /// <summary>
        /// pink_dark
        /// </summary>
        [LabelColorInfo(255, 255, 255, "#FFFFFF", 174, 71, 135, "#AE4787")] [JsonPropertyName("pink_dark")]
        DarkPink,

        /// <summary>
        /// black_light
        /// </summary>
        [LabelColorInfo(9, 30, 66, "#091E42", 220, 223, 228, "#DCDFE4")] [JsonPropertyName("black_light")]
        LightBlack,

        /// <summary>
        /// black
        /// </summary>
        [LabelColorInfo(9, 30, 66, "#091E42", 133, 144, 162, "#8590A2")] [JsonPropertyName("black")]
        Black,

        /// <summary>
        /// black_dark
        /// </summary>
        [LabelColorInfo(255, 255, 255, "#FFFFFF", 98, 111, 134, "#626F86")] [JsonPropertyName("black_dark")]
        DarkBlack,
    }
}