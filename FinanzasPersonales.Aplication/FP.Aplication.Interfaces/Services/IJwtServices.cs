using FinanciasPersonalesApiRest.Models;

namespace FinanzasPersonales.Aplication.FP.Aplication.Interfaces.Services
{
    public interface IJwtServices
    {
        public string GenerateTokenLogin(User user);
        public string GenerateTokenRetrievePassword(User user);

        //public string RefreshToken(User User);



    }
}
