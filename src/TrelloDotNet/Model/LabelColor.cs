using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global

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
        [LabelColorInfo(-1, -1, -1, "", -1, -1, -1, "")]
        [JsonPropertyName("")]
        None = 0,

        /// <summary>
        /// green_light
        /// </summary>
        [LabelColorInfo(22, 75, 53, "#164B35", 186, 243, 219, "#BAF3DB")]
        [JsonPropertyName(Constants.TrelloIds.ColorFields.GreenLight)]
        LightGreen,

        /// <summary>
        /// green
        /// </summary>
        [LabelColorInfo(22, 75, 53, "#164B35", 75, 206, 151, "#4BCE97")]
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Green)]
        Green,

        /// <summary>
        /// green_dark
        /// </summary>
        [LabelColorInfo(255, 255, 255, "#FFFFFF", 31, 132, 90, "#1F845A")]
        [JsonPropertyName(Constants.TrelloIds.ColorFields.GreenDark)]
        DarkGreen,

        /// <summary>
        /// yellow_light
        /// </summary>
        [LabelColorInfo(83, 63, 4, "#533F04", 248, 230, 160, "#F8E6A0")]
        [JsonPropertyName(Constants.TrelloIds.ColorFields.YellowLight)]
        LightYellow,

        /// <summary>
        /// yellow
        /// </summary>
        [LabelColorInfo(83, 63, 4, "#533F04", 245, 205, 71, "#F5CD47")]
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Yellow)]
        Yellow,

        /// <summary>
        /// yellow_dark
        /// </summary>
        [LabelColorInfo(255, 255, 255, "#FFFFFF", 148, 111, 0, "#946F00")]
        [JsonPropertyName(Constants.TrelloIds.ColorFields.YellowDark)]
        DarkYellow,

        /// <summary>
        /// orange_light
        /// </summary>
        [LabelColorInfo(112, 46, 0, "#702E00", 254, 222, 200, "#FEDEC8")]
        [JsonPropertyName(Constants.TrelloIds.ColorFields.OrangeLight)]
        LightOrange,

        /// <summary>
        /// orange
        /// </summary>
        [LabelColorInfo(112, 46, 0, "#702E00", 254, 163, 98, "#FEA362")]
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Orange)]
        Orange,

        /// <summary>
        /// orange_dark
        /// </summary>
        [LabelColorInfo(255, 255, 255, "#FFFFFF", 194, 81, 0, "#C25100")]
        [JsonPropertyName(Constants.TrelloIds.ColorFields.OrangeDark)]
        DarkOrange,

        /// <summary>
        /// red_light
        /// </summary>
        [LabelColorInfo(93, 31, 26, "#5D1F1A", 255, 213, 210, "#FFD5D2")]
        [JsonPropertyName(Constants.TrelloIds.ColorFields.RedLight)]
        LightRed,

        /// <summary>
        /// red
        /// </summary>
        [LabelColorInfo(93, 31, 26, "#5D1F1A", 248, 113, 104, "#F87168")]
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Red)]
        Red,

        /// <summary>
        /// red_dark
        /// </summary>
        [LabelColorInfo(255, 255, 255, "#FFFFFF", 201, 55, 44, "#C9372C")]
        [JsonPropertyName(Constants.TrelloIds.ColorFields.RedDark)]
        DarkRed,

        /// <summary>
        /// purple_light
        /// </summary>
        [LabelColorInfo(53, 44, 99, "#352C63", 223, 216, 253, "#DFD8FD")]
        [JsonPropertyName(Constants.TrelloIds.ColorFields.PurpleLight)]
        LightPurple,

        /// <summary>
        /// purple
        /// </summary>
        [LabelColorInfo(53, 44, 99, "#352C63", 159, 143, 239, "#9F8FEF")]
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Purple)]
        Purple,

        /// <summary>
        /// purple_dark
        /// </summary>
        [LabelColorInfo(255, 255, 255, "#FFFFFF", 110, 93, 198, "#6E5DC6")]
        [JsonPropertyName(Constants.TrelloIds.ColorFields.PurpleDark)]
        DarkPurple,

        /// <summary>
        /// blue_light
        /// </summary>
        [LabelColorInfo(9, 50, 108, "#09326C", 204, 224, 255, "#CCE0FF")]
        [JsonPropertyName(Constants.TrelloIds.ColorFields.BlueLight)]
        LightBlue,

        /// <summary>
        /// blue
        /// </summary>
        [LabelColorInfo(9, 50, 108, "#09326C", 87, 157, 255, "#579DFF")]
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Blue)]
        Blue,

        /// <summary>
        /// blue_dark
        /// </summary>
        [LabelColorInfo(255, 255, 255, "#FFFFFF", 12, 102, 228, "#0C66E4")]
        [JsonPropertyName(Constants.TrelloIds.ColorFields.BlueDark)]
        DarkBlue,

        /// <summary>
        /// sky_light
        /// </summary>
        [LabelColorInfo(22, 69, 85, "#164555", 198, 237, 251, "#C6EDFB")]
        [JsonPropertyName(Constants.TrelloIds.ColorFields.SkyLight)]
        LightSky,

        /// <summary>
        /// sky
        /// </summary>
        [LabelColorInfo(22, 69, 85, "#164555", 108, 195, 224, "#6CC3E0")]
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Sky)]
        Sky,

        /// <summary>
        /// sky_dark
        /// </summary>
        [LabelColorInfo(255, 255, 255, "#FFFFFF", 34, 125, 155, "#227D9B")]
        [JsonPropertyName(Constants.TrelloIds.ColorFields.SkyDark)]
        DarkSky,

        /// <summary>
        /// lime_light
        /// </summary>
        [LabelColorInfo(55, 71, 31, "#37471F", 211, 241, 167, "#D3F1A7")]
        [JsonPropertyName(Constants.TrelloIds.ColorFields.LimeLight)]
        LightLime,

        /// <summary>
        /// lime
        /// </summary>
        [LabelColorInfo(55, 71, 31, "#37471F", 148, 199, 72, "#94C748")]
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Lime)]
        Lime,

        /// <summary>
        /// lime_dark
        /// </summary>
        [LabelColorInfo(255, 255, 255, "#FFFFFF", 91, 127, 36, "#5B7F24")]
        [JsonPropertyName(Constants.TrelloIds.ColorFields.LimeDark)]
        DarkLime,

        /// <summary>
        /// pink_light
        /// </summary>
        [LabelColorInfo(80, 37, 63, "#50253F", 253, 208, 236, "#FDD0EC")]
        [JsonPropertyName(Constants.TrelloIds.ColorFields.PinkLight)]
        LightPink,

        /// <summary>
        /// pink
        /// </summary>
        [LabelColorInfo(80, 37, 63, "#50253F", 231, 116, 187, "#E774BB")]
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Pink)]
        Pink,

        /// <summary>
        /// pink_dark
        /// </summary>
        [LabelColorInfo(255, 255, 255, "#FFFFFF", 174, 71, 135, "#AE4787")]
        [JsonPropertyName(Constants.TrelloIds.ColorFields.PinkDark)]
        DarkPink,

        /// <summary>
        /// black_light
        /// </summary>
        [LabelColorInfo(9, 30, 66, "#091E42", 220, 223, 228, "#DCDFE4")]
        [JsonPropertyName(Constants.TrelloIds.ColorFields.BlackLight)]
        LightBlack,

        /// <summary>
        /// black
        /// </summary>
        [LabelColorInfo(9, 30, 66, "#091E42", 133, 144, 162, "#8590A2")]
        [JsonPropertyName(Constants.TrelloIds.ColorFields.Black)]
        Black,

        /// <summary>
        /// black_dark
        /// </summary>
        [LabelColorInfo(255, 255, 255, "#FFFFFF", 98, 111, 134, "#626F86")]
        [JsonPropertyName(Constants.TrelloIds.ColorFields.BlackDark)]
        DarkBlack,
    }
}






