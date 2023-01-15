namespace TN.HealthPortal.Logic.DTOs.Authentication
{
    public class TokenDto
    {
        public string Token { get; set; }

        public DateTime ExpiresAt { get; set; }
    }
}
