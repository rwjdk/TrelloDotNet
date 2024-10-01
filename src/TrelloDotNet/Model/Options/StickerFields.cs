using System.Linq;
using TrelloDotNet.Control;

namespace TrelloDotNet.Model.Options
{
    /// <summary>
    /// Represent Sticker-Fields to include
    /// </summary>
    public class StickerFields
    {
        /// <summary>
        /// Fields to include
        /// </summary>
        internal string[] Fields { get; }

        /// <summary>
        /// All Fields
        /// </summary>
        public static StickerFields All => new StickerFields("all");

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fields">The Sticker-fields to include ('all' or a comma-separated list of top, left, zIndex, rotate, image, imageUrl)</param>
        public StickerFields(params string[] fields)
        {
            Fields = fields;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fields">The Sticker-fields to include</param>
        public StickerFields(params StickerFieldsType[] fields)
        {
            Fields = fields.Select(x => x.GetJsonPropertyName()).ToArray();
        }
    }
}