using System;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Info about a Color
    /// </summary>
    public class LabelColorInfo : Attribute
    {
        /// <summary>
        /// Text Color (Red)
        /// </summary>
        public int TextR { get; }

        /// <summary>
        /// Text Color (Green)
        /// </summary>
        public int TextG { get; }

        /// <summary>
        /// Text Color (Blue)
        /// </summary>
        public int TextB { get; }

        /// <summary>
        /// Text Color (Hex)
        /// </summary>
        public string TextHex { get; }

        /// <summary>
        /// Background Color (Red)
        /// </summary>
        public int BackgroundR { get; }

        /// <summary>
        /// Background Color (Green)
        /// </summary>
        public int BackgroundG { get; }

        /// <summary>
        /// Background Color (Blue)
        /// </summary>
        public int BackgroundB { get; }

        /// <summary>
        /// Background Color (Hex)
        /// </summary>
        public string BackgroundHex { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="textR">Text Color (Red)</param>
        /// <param name="textG">Text Color (Green)</param>
        /// <param name="textB">Text Color (Blue)</param>
        /// <param name="textHex">Text Color (Hex)</param>
        /// <param name="backgroundR">Background Color (Red)</param>
        /// <param name="backgroundG">Background Color (Green)</param>
        /// <param name="backgroundB">Background Color (Blue)</param>
        /// <param name="backgroundHex">Background Color (Hex)</param>
        public LabelColorInfo(int textR, int textG, int textB, string textHex, int backgroundR, int backgroundG, int backgroundB, string backgroundHex)
        {
            TextR = textR;
            TextG = textG;
            TextB = textB;
            TextHex = textHex;
            BackgroundR = backgroundR;
            BackgroundG = backgroundG;
            BackgroundB = backgroundB;
            BackgroundHex = backgroundHex;
        }
    }
}