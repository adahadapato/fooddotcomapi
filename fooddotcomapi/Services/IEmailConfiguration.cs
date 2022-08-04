namespace fooddotcomapi.Services
{
    public interface IEmailConfiguration
    {
        public string SmtpServer { get; }
        public int SmtpPort { get; }
        public string SmtpUsername { get; set; }
        public string From { get; set; }
        public string SmtpPassword { get; }
    }
}
