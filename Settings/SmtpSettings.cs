﻿namespace JokerService.Settings
{
    public class SmtpSettings
    {
        public required string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
