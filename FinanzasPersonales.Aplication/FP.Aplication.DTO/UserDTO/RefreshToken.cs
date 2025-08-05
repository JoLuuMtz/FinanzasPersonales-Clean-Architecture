namespace FinanzasPersonales.Aplication
{
    public class RefreshToken
    {
        public required string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiryDate;
        public int UserId { get; set; }
    }
}
