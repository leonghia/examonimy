namespace ExamonimyWeb.Models
{
    public class AuthenticatedResponse
    {      
        public required string Token { get; set; }
        public required string RefreshToken { get; set; }
        
    }
}
