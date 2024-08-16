namespace JokerService.Settings
{
    public class TimersSettings
    {
        public int StartUpDelay { get; set; }
        public int GetRandomJokeDelay { get; set; }
        public int EmailServiceDelay { get; set; }
        public int? MsTeamsServiceDelay { get; set; }
        public int? DebugConstantDelay { get; set; }
    }
}
