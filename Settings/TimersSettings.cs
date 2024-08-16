namespace JokerService.Settings
{
    public class TimersSettings
    {
        public required int StartUpDelay { get; set; }
        public required int GetRandomJokeDelay { get; set; }
        public required int EmailServiceDelay { get; set; }
        public int? MsTeamsServiceDelay { get; set; }
        public int? DebugConstantDelay { get; set; }
    }
}
