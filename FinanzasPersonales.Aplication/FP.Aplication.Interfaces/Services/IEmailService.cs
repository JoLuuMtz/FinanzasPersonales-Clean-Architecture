namespace FinanzasPersonales.Aplication.FP.Aplication.Interfaces.Services
{
    public interface IEmailService
    {
        public Task UserRegisteredWelcome(string email, string name);
        public Task RetrievePassword(string email);
    }
}
