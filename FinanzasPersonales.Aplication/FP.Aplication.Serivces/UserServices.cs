using AutoMapper;




using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System.Security.Claims;
using FinanzasPersonales.Domain;

namespace FinanzasPersonales.Aplication;

public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IJwtServices _jwtServices;
    private readonly IUserRepository _userRepository1;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;



    public UserService

        (IRepository<User> userRespotiroy,
        IPasswordHasher<User> passwordHasher,
        IJwtServices jwtServices,
        IUserRepository userRepository1,
        IHttpContextAccessor httpContextAccessor,
         IMapper mapper


         )

    {
        _userRepository = userRespotiroy;  // inyecta el repository 
        _passwordHasher = passwordHasher; // inyecta el password hasher 
        _jwtServices = jwtServices; // inyecta el jwt services
        _userRepository1 = userRepository1;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    //  Cambia la contraseña del usuario
    public Task<UserDTO> ChangePassword(int id, ChangePasswordDTO password)
    {
        //TODO: Implemetar cambio de contraseña
        throw new NotImplementedException();
    }

    // Elimina un usuario
    public async Task<ServiceResult<UserDTO>> DeleteUser(int id)
    {
        try
        {
            var user = await _userRepository.GetById(id);
            if (user is null)

                return ServiceResult<UserDTO>
                    .Fail("Usuario no encontrado");
            await _userRepository.Delete(user);
        }
        catch (SqlException e)
        {

            throw new Exception($"Error al eliminar el usuario. Detalles del error:" +
                 $" {e.Message}", e);
        }

        // mapea el usuario a UserDTO
        return ServiceResult<UserDTO>.Ok(null);


    }

    // obtiene el usuario por id
    public async Task<ServiceResult<UserDTO>> GetUser(int id)
    {
        var user = await _userRepository.GetById(id);

        if (user is null)
            return ServiceResult<UserDTO>.Fail("Usuario no encontrado");

        //DONE: Implementar el mapeo de User a UserDTO

        return
            ServiceResult<UserDTO>.Ok(_mapper.Map<UserDTO>(user));


    }

    // retorna el id de usuario autenticado

    public int GetIdUserAuthenticated()
    {
        // obtiene el id del usuario autenticado con el htppContext
        var user = _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (user is null) return 0;   // si no esta autenticado retorna 0
        return int.Parse(user);
    }


    public async Task<IEnumerable<UserDTO>> GetUsers()
    {
        var user = await _userRepository.GetAll();

        //DONE: Implementar el mapeo de User a UserDTO
        // Mapea la lista de usuarios a UserDTO
        return _mapper.Map<IEnumerable<UserDTO>>(user);
    }

    // Login de usuario y genera un token
    public async Task<LoginResponse> Login(LoginRequest login)
    {
        // DONE: Implementar el login con JWT token generado

        // obtiene el  usuario registrado por email
        var userRegistered = await _userRepository1.GetByEmailAsync(login.Email);


        //si no esta registrado retorna null 
        if (userRegistered is null)
            return new LoginResponse
            {
                Success = false,
                Message = "Usuario no registrado"
            };
        // Compara si la contraseña y el correo coinciden con la DB
        var result = _passwordHasher.VerifyHashedPassword(null, userRegistered.Password, login.password);

        // Verifica si la contraseña es Sucess o  failed 
        if (result == PasswordVerificationResult.Failed)
            return new LoginResponse
            {
                Success = false,
                Message = "Contraseña incorrecta",
               
            };

        // Genera el token
        string usertoken = _jwtServices.GenerateTokenLogin(userRegistered);

        // retorna una respuesta con el token y los datos del usuario

        return new LoginResponse
        {
            Message = "Loggeado",
            Success = true,
            AccessToken = usertoken,
            User = _mapper.Map<FullUserDataDTO>(userRegistered)

        };

        throw new Exception("Usuario no registrado");

    }

    // Registra un nuevo usuario
    public async Task<UserDTO> RegisterUser(RegisterUserDTO userRegister)
    {
        // hashea la contraseña 
        var hashedPassword = _passwordHasher.HashPassword(null, userRegister.Password);


        //var userId = await _userRepository.GetById(userRegister.IdUser);
        var userEmail = await _userRepository1.GetByEmailAsync(userRegister.Email);
        var userDNI = await _userRepository1.GetByDNIAsync(userRegister.DNI);

        //Valida si el usuario ya esta registrado
        if (/*userId != null ||*/ userEmail != null || userDNI != null) return null;


        //DONE: Implementar el mapeo de RegisterUserDTO a User
        var userEntity = new User
        {
            Name = userRegister.Name,
            LastName = userRegister.LastName,
            Email = userRegister.Email,
            DNI = userRegister.DNI,
            Phone = userRegister.Phone,
            Password = hashedPassword,
            //TotalMoney = await _incomeServices.TotalIncomes(),
            ProfileImagen = userRegister.ProfileImagen,
        };




        //var userEntity = _mapper.Map<User>(userRegister);


        try
        {
            await _userRepository.Create(userEntity);
        }
        catch (SqlException E)
        {

            throw new Exception($"Error al registrar el usuario. Detalles del error:" +
                 $" {E.Message}", E);
        }
        catch (Exception e)
        {
            throw new Exception($"Error desconocido al registrar el usuario. Detalles del error:" +
                 $" {e.Message}", e);

        }

        //DONE: Implementar el mapeo de User a UserDTO
        return _mapper.Map<UserDTO>(userEntity);



    }

    // Recupera la contraseña del usuario
    public Task<UserDTO> RetrievePassword(string email)
    {
        throw new NotImplementedException();
    }

    // Actualiza la informacion del usuario
    public async Task<ServiceResult<UserDTO>> UpdateUser(int id, UpdateUserDTO updateUser)

    {
        // Obtener el usuario existente desde el repositorio

        var user = await _userRepository.GetById(id);

        // Verificar si el usuario no existe
        if (user == null)
            return ServiceResult<UserDTO>.Fail("Usuario no encontrado");


        // Actualizar el usuario con los nuevos valores, manteniendo los valores existentes no modificados
        user.Name = updateUser.Name ?? user.Name; // si es nullo mantiene el valor anterior
        user.LastName = updateUser.LastName ?? user.LastName;
        user.Email = updateUser.Email ?? user.Email;
        user.Phone = updateUser.Phone ?? user.Phone;
        user.ProfileImagen = updateUser.ProfileImagen ?? user.ProfileImagen;


        try
        {
            await _userRepository.Update(user);
        }
        catch (SqlException sqlEx)
        {
            // Capturar errores específicos de SQL, como problemas de conexión o integridad de datos
            throw new Exception($"Error al actualizar el usuario. Detalles del error:" +
                $" {sqlEx.Message}", sqlEx);
        }
        catch (Exception e)
        {
            // Capturar cualquier otro tipo de error
            throw new Exception($"Error desconocido al actualizar el usuario. Detalles del error:" +
                $" {e.Message}", e);
        }

        // return datos actualizados
        //DONE: Implementar el mapeo de User a UserDTO

        return ServiceResult<UserDTO>.Ok(_mapper.Map<UserDTO>(user));



    }

    // obtiene el usuario por email
    public async Task<ServiceResult<UserDTO>> GetByEmail(string email)
    {

        var user = await _userRepository1.GetByEmailAsync(email);

        if (user == null) return
                ServiceResult<UserDTO>.Fail("Usuario no encontrado");


        //DONE: Implementar el mapeo de User a UserDTO

        return ServiceResult<UserDTO>.Ok(_mapper.Map<UserDTO>(user));
    }

    // retorna la informacion del usuario autenticado
    public string GetInfoUser()
    {
        // Obtenemos el ID del usuario autenticado 
        int userId = GetIdUserAuthenticated();

        // Puedes agregar otros claims si es necesario, como el nombre
        var userName = _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        return $"User ID: {userId},  Name: {userName}";
    }

    public async Task<decimal> TotalMoneyByUser()
    {
        var user = await GetUser(GetIdUserAuthenticated());
        return user.Data.TotalMoney;
    }
}




