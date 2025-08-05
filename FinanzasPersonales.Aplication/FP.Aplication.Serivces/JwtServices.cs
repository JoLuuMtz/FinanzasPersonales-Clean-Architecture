
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using FinanzasPersonales.Domain;
using FinanzasPersonales.Aplication;

namespace FinanzasPersonales.Aplication
{
    public class JwtServices : IJwtServices
    {
        private readonly string SecretKey;
        private readonly IRepository<User> _userRepository;
        // private readonly IUserService _userService;

        public JwtServices(IConfiguration configuration, IRepository<User> userRepository)
        {
            SecretKey = configuration["SecretJWTKey"] ?? throw new ArgumentNullException("SecretJWTKey not configured");
            _userRepository = userRepository;
            // _userService = userService;
        }

        public string GenerateTokenLogin(User user)
        {
            // Crea un manejador para generar y procesar tokens JWT.
            var tokenHandler = new JwtSecurityTokenHandler();

            // Convierte la clave secreta en un arreglo de bytes.
            var key = Encoding.UTF8.GetBytes(SecretKey);

            // Configura los parametros del token.
            // como el ID del usuario, la fecha de expiración y la firma.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // define el claim que identifica al usuario 
                // ene ste caso obtiene el id del usuario y el email
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()), // Cambiar a NameIdentifier
                    new Claim(ClaimTypes.Email, user.Email),

                }),

                // Establece una fecha de expiración para el token (1 hora desde el momento actual).
                Expires = DateTime.UtcNow.AddMinutes(20),
                // Firma el token usando la clave secreta y el algoritmo HMAC SHA256.
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), // Clave secreta usada para firmar el token.
                    SecurityAlgorithms.HmacSha256Signature // Algoritmo de encriptación utilizado.
                )
            };

            // Crea el token JWT con la configuración proporcionada.
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Convierte el token en una cadena legible (formato JWT) y lo retorna.
            return tokenHandler.WriteToken(token);
        }

        public string GenerateTokenRetrievePassword(User user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Genera un refresh token único con duración extendida
        /// </summary>
        public string GenerateRefreshToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
                    new Claim("token_type", "refresh") // Identificar como refresh token
                }),
                Expires = DateTime.UtcNow.AddDays(7), // ⭐ 7 DÍAS de duración
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Genera un refresh token único (versión simple - mantener por compatibilidad)
        /// </summary>
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        /// <summary>
        /// Valida un token JWT
        /// </summary>
        public bool ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler(); // Crea un manejador para procesar tokens JWT.
                var key = Encoding.UTF8.GetBytes(SecretKey); // Convierte la clave secreta en un arreglo de bytes.

                tokenHandler.ValidateToken(token, new TokenValidationParameters // Configura los parámetros de validación del token
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key), // Verifica la firma con la clave secreta
                    ValidateIssuer = false, // No validamos el emisor para refresh tokens
                    ValidateAudience = false,// No validamos audiencia para refresh tokens
                    ValidateLifetime = false, // No validamos tiempo para refresh
                    ClockSkew = TimeSpan.Zero // Reduce el tiempo de tolerancia a 0 para evitar problemas de expiración
                }, out SecurityToken validatedToken); // Valida el token y obtiene el token validado

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Extrae el ID del usuario desde un token JWT
        /// </summary>
        // private int GetUserIdFromToken(string token)
        // {
        //     // si esta vacio retorna 0 
        //     if (string.IsNullOrWhiteSpace(token))
        //     {
        //         Console.WriteLine("Token is null or empty");
        //         return 0;

        //     }

        //     var handler = new JwtSecurityTokenHandler();

        //     JwtSecurityToken jwtToken;

        //     try
        //     {
        //         jwtToken = handler.ReadJwtToken(token);
        //         Console.WriteLine("Token read successfully");
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine($"Error reading token: {ex.Message}");
        //         return 0;
        //     }


        //     var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

        //     if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
        //     {
        //         Console.WriteLine($"User ID extracted from token: {userId}");
        //         return userId;
        //     }
        //     Console.WriteLine("User ID claim not found or invalid");
        //     return 0;
        // }


        private int GetUserIdFromToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                Console.WriteLine("Token is null or empty");
                return 0;
            }

            var handler = new JwtSecurityTokenHandler();

            JwtSecurityToken jwtToken;
            try
            {
                jwtToken = handler.ReadJwtToken(token);
                Console.WriteLine("Token read successfully");

                // Debug: Mostrar todos los claims del token
                Console.WriteLine("=== ALL CLAIMS IN TOKEN ===");
                foreach (var claim in jwtToken.Claims)
                {
                    Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
                }
                Console.WriteLine("=== END CLAIMS ===");
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error reading token: {ex.Message}");
                return 0;
            }

            // Busca el claim con ClaimTypes.NameIdentifier, que es donde guardas el Id del usuario
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "nameid");

            // var prueva = jwtToken.Claims.FirstOrDefault(c => c.Type == "nameid");
            Console.WriteLine($"ID QUE BUSCO :>  {userIdClaim}");


            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                Console.WriteLine($"User ID extracted from token: {userId}");
                return userId;
            }
            Console.WriteLine("User ID claim not found or invalid");
            return 0;
        }

        /// <summary>
        /// Genera un nuevo access token a partir de un refresh token válido
        /// </summary>
        public RefreshTokenResponse RefreshAccessToken(RefreshTokenRequest request)
        {
            try
            {
                // Validar el access token (sin validar expiración)
                if (!ValidateToken(request.AccessToken))
                {
                    return new RefreshTokenResponse
                    {
                        Success = false,
                        Message = "Token de acceso inválido"
                    };
                }


                var userId = GetUserIdFromToken(request.AccessToken);
                // 
                // var userId = 2017;

                if (userId == 0)
                {
                    return new RefreshTokenResponse
                    {
                        Success = false,
                        Message = "No se pudo obtener el ID del usuario desde el token"
                    };
                }

                // Obtener el usuario desde la base de datos
                var user = _userRepository.GetById(userId).GetAwaiter().GetResult();// es await pero para un metodo síncrono
                if (user is null)
                {
                    return new RefreshTokenResponse
                    {
                        Success = false,
                        Message = "Usuario no encontrado"
                    };
                }

                // Validar el refresh token como JWT
                if (!ValidateToken(request.RefreshToken))
                {
                    return new RefreshTokenResponse
                    {
                        Success = false,
                        Message = "Refresh token inválido o expirado"
                    };
                }

                // Verificar que es realmente un refresh token
                var refreshTokenHandler = new JwtSecurityTokenHandler();
                var refreshJwtToken = refreshTokenHandler.ReadJwtToken(request.RefreshToken);
                var tokenTypeClaim = refreshJwtToken.Claims.FirstOrDefault(c => c.Type == "token_type");

                if (tokenTypeClaim?.Value != "refresh")
                {
                    return new RefreshTokenResponse
                    {
                        Success = false,
                        Message = "El token proporcionado no es un refresh token válido"
                    };
                }

                // Generar nuevo access token y refresh token
                var newAccessToken = GenerateTokenLogin(user);
                var newRefreshToken = GenerateRefreshToken(user); // Usar la versión con duración

                return new RefreshTokenResponse
                {
                    Success = true,
                    Message = "Token renovado exitosamente",
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                };
            }
            catch (Exception ex)
            {
                return new RefreshTokenResponse
                {
                    Success = false,
                    Message = $"Error al renovar el token: {ex.Message}"
                };
            }
        }
    }
}
