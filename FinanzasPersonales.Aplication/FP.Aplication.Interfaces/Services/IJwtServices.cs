using FinanzasPersonales.Domain;

namespace FinanzasPersonales.Aplication
{
    public interface IJwtServices
    {
        public string GenerateTokenLogin(User user);
        public string GenerateTokenRetrievePassword(User user);
        public string GenerateRefreshToken(); // Genera un nuevo refresh token simple
        public string GenerateRefreshToken(User user); // Genera un refresh token JWT con duración
        public RefreshTokenResponse RefreshAccessToken(RefreshTokenRequest request); // Genera un nuevo access token a partir de un refresh token válido
        public bool ValidateToken(string token); // Valida un token JWT
    }
}
