namespace NYCSS.Utils.Identity
{
    public class JwtConfigurationAppSettings
    {
        public const string KEY = "JwtAppSettings";
        public string Secret { get; set; } = string.Empty;
        public int ExpirationHours { get; set; }
        public string Issuer { get; set; } = string.Empty;
        public string ValidAt { get; set; } = string.Empty;
    }
}