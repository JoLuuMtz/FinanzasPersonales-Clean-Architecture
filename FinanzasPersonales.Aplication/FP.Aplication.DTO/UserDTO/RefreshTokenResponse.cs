namespace FinanzasPersonales.Aplication
{
    public class RefreshTokenResponse
    {
        public bool Success { get; set; }
        public required string Message { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
