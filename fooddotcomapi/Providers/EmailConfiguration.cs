using fooddotcomapi.Services;
using Microsoft.Extensions.Configuration;
using System;

namespace fooddotcomapi.Providers
{
    public class EmailConfiguration : IEmailConfiguration
    {
        private readonly IConfiguration _configuration;
        public EmailConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string SmtpServer
            => _configuration.GetSection("EmailConfiguration").GetSection("SmtpServer").Value;

        public int SmtpPort
        {
            get
            {
                var result = _configuration.GetSection("EmailConfiguration").GetSection("Port").Value;
                return Convert.ToInt32(result);
            }
        }


        public string SmtpUsername { get; set; }
        public string From { get; set; }
        public string SmtpPassword
            => _configuration.GetSection("EmailConfiguration").GetSection("Password").Value;
    }
}
