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
}
