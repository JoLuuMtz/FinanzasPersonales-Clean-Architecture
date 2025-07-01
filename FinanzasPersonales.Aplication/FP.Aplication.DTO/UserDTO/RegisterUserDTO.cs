namespace FinanciasPersonalesApiRest.DTOs.UserDTO
{
    public class RegisterUserDTO
    {
        //public int? IdUser { get; set; }
        public required string DNI { get; set; }
        public required string Name { get; set; }

        public string LastName { get; set; }
        public string? Phone { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? ProfileImagen { get; set; }
    }
}
