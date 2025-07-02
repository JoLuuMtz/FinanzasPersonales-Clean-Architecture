
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using FinanzasPersonales.Domain;
using FinanzasPersonales.Aplication;

namespace FinanzasPersonales.Aplication
{
    public class JwtServices : IJwtServices
    {
        private readonly string SecretKey;


        public JwtServices(IConfiguration configuration)
        {
            SecretKey = configuration["SecretJWTKey"]; // secrect key is in Secret File asp.net 

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
                    new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),

                }),



                // Establece una fecha de expiración para el token (1 hora desde el momento actual).
                Expires = DateTime.UtcNow.AddHours(1),
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

        // Refresh Token 
        private string RereshToken( User user)
        {
            return "";
        }
    }
}
