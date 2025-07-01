namespace FinanciasPersonalesApiRest.Services.Interfaces
{
    public interface IEmailService
    {
        public Task UserRegisteredWelcome(string email, string name);
        public Task RetrievePassword(string email);
    }
}
