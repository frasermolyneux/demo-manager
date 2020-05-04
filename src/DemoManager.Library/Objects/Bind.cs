namespace DemoManager.Library.Objects
{
    /// <summary>
    ///     Represents a key binding in a Call of Duty configuration.
    /// </summary>
    public class Bind
    {
        public Bind(string value, string description)
        {
            Key = null;
            Type = "bind";
            Value = value;
            Description = description;
        }

        public Bind(string key, string value, string description)
        {
            Key = key;
            Type = "bind";
            Value = value;
            Description = description;
        }

        public Bind(string type, string key, string value, string description)
        {
            Key = key;
            Type = type;
            Value = value;
            Description = description;
        }

        public string Type { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }
}