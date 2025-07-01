using FinanciasPersonalesApiRest.Models;
//using Microsoft.AspNetCore.Identity.Data;
using FinanciasPersonalesApiRest.DTOs.UserDTO;
using FinanciasPersonalesApiRes;

namespace FinanzasPersonales.Aplication
{
    public interface IUserService
    {
        public Task<UserDTO> RegisterUser(RegisterUserDTO newUser);
        public Task<LoginResponse> Login(LoginRequest login); // login con jwt token generado
        public Task<ServiceResult<UserDTO>> DeleteUser(int id);
        public Task<ServiceResult<UserDTO>> UpdateUser(int id, UpdateUserDTO updateUser);
        public Task<UserDTO> RetrievePassword(string email);  // recuperar contraseña
        public Task<UserDTO> ChangePassword(int id, ChangePasswordDTO password);
        public int GetIdUserAuthenticated();// obtiene el id en el jwt para las operaciones
        public Task<IEnumerable<UserDTO>> GetUsers();
        public Task<ServiceResult<UserDTO>> GetUser(int id);
        public Task<ServiceResult<UserDTO>> GetByEmail(string email);
        public string GetInfoUser();
        public Task<decimal> TotalMoneyByUser(); 

    }
}
