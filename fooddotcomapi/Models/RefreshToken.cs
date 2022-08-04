using System;

namespace fooddotcomapi.Models
{
    public class RefreshToken
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Expires { get; set; }
        public string CreatedByIp { get; set; }
    }
}
