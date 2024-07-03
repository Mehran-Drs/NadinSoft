namespace NadinSoft.Common.DTOs
{
    public class JwtConfigDto
    {
        public string IssuerSigningKey { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpireMinute{ get; set; }
    }
}
