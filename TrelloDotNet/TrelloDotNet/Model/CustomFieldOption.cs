namespace TrelloDotNet.Model
{
    public class CustomFieldOption
    {
        public string Color { get; set; }
        public string Name { get; set; }

        public CustomFieldOption(string name, string color = "none")
        {
            Color = color;
            Name = name;
        }
    }
}