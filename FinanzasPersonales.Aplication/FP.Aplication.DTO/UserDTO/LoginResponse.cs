namespace FinanzasPersonales.Aplication
{
    public class LoginResponse
    {

        public bool Success { get; set; }
        public string Message { get; set; }
        public string AccessToken { get; set; }
        public FullUserDataDTO User { get; set; } // Datos del usuario autenticado


    }
}
