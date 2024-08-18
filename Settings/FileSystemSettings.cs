namespace JokerService.Settings
{
    public class FileSystemSettings
    {
        public required string RootDirectory { get; set; }
        public required string JokesDirectory { get; set; }
        public string? RemoteJokesDirectory { get; set; }

        /// <summary>
        /// Gets a value indicating whether to process remote jokes.
        /// </summary>
        /// <value><c>true</c> if remote jokes should be processed; otherwise, <c>false</c>.</value>
        public bool ProcessRemoteJokes
        {
            get
            {
                return !string.IsNullOrWhiteSpace(RemoteJokesDirectory);
            }
        }

        /// <summary>
        /// Creates a directory if it does not exist.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <returns><c>true</c> if the directory was created; otherwise, <c>false</c>.</returns>
        public bool CreateDirectoryIfNotExists(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                return true;
            }
            return false;
        }
    }
}
