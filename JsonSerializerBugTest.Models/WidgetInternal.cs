namespace JsonSerializerBugTest.Models
{
    public class WidgetInternal
    {
        internal WidgetInternal()
        {
        }

        public string Description { get; set; }

        public static WidgetInternal Create(string description)
        {
            return new WidgetInternal
            {
                Description = description
            };
        }
    }
}