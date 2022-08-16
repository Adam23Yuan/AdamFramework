using System.ComponentModel.DataAnnotations;

namespace Adam.WebApi.Options
{
    public class PositionOptions
    {
        public const string Position = "Position";

        public string Title { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
    }
    public class TopItemSettings
    {
        public const string Month = "Month";
        public const string Year = "Year";

        public string Name { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
    }

    public class MyConfigOptions
    {
        public const string MyConfig = "MyConfig";

        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$")]
        public string Key1 { get; set; }
        [Range(0, 1000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Key2 { get; set; }
        public int Key3 { get; set; }
    }
}
