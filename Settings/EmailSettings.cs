namespace JokerService.Settings
{
    public class EmailSettings
    {
        public required string Host { get; set; }
        public required int Port { get; set; }
        public required bool EnableSsl { get; set; }
        public required string FromAddress { get; set; }
        public string? Password { get; set; }
        public required string ToAddress { get; set; }
        public string? Subject { get; set; }
    }
}
