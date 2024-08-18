namespace JokerService.Settings
{
    public class FileSystemSettings
    {
        public required string RootDirectory { get; set; }
        public required string JokesDirectory { get; set; }
        public string? RemoteJokesDirectory { get; set; }
        public bool ProcessRemoteJokes
        {
            get
            {
                return !string.IsNullOrWhiteSpace(RemoteJokesDirectory);
            }
        }
    }
}
