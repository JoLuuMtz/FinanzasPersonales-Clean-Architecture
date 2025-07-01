
using FinanciasPersonalesApiRest.Services.Interfaces;

namespace FinanciasPersonalesApiRest.Services
{
    public class EmailServices : IEmailService
    {
        public Task RetrievePassword(string email)
        {
            throw new NotImplementedException();
        }

        public Task UserRegisteredWelcome(string email, string name)
        {
            throw new NotImplementedException();
        }
    }
}
