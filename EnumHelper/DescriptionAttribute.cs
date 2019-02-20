namespace EnumHelper
{
    public class DescriptionAttribute : System.Attribute
    {
        public string Description { get; private set; }

        public DescriptionAttribute(string text)
        {
            Description = text;
        }
    }
}
