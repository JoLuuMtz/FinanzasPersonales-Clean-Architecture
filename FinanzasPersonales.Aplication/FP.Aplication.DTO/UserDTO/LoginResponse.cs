namespace FinanzasPersonales.Aplication
{
    public class LoginResponse
    {

        public bool Success { get; set; }
        public required string Message { get; set; }
        public required string AccessToken { get; set; }
        public string? RefreshToken { get; set; } // Nuevo campo para refresh token
        public required FullUserDataDTO User { get; set; } // Datos del usuario autenticado


    }
}
