namespace TrelloDotNet.Model
{
    /// <summary>
    /// Option on a Custom Field of Type List (Dropdown)
    /// </summary>
    public class CustomFieldOption
    {
        /// <summary>
        /// Color of the Option
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// Name of the Option
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Option</param>
        /// <param name="color">Color of the Option</param>
        public CustomFieldOption(string name, string color = "none")
        {
            Color = color;
            Name = name;
        }
    }
}