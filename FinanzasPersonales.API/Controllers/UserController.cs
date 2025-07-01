
using FinanciasPersonalesApiRest.DTOs.UserDTO;
using FinanciasPersonalesApiRest.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace FinanciasPersonalesApiRest.Controllers
{

    [Route("/api/User")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<RegisterUserDTO> _validator;
        private readonly IValidator<UpdateUserDTO> _updateValidator;

        public UserController(IUserService userService,
            IValidator<RegisterUserDTO> validator,
            IValidator<UpdateUserDTO> updateValidator)
        {
            _userService = userService;
            _validator = validator;
            _updateValidator = updateValidator;
        }

        //User CRUD's
        //[HttpGet("all")]
        //public async Task<IActionResult> GetUser()
        //{
        //    try
        //    {
        //        var users = await _userService.GetUsers();
        //        return Ok(users);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, $"Error al obtener los usuarios: {e.Message}");
        //    }

        //}
        /// <summary>
        /// Registra un usuario nuevo
        /// </summary>
    

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO register)
        {
            try
            {
                var validator = _validator.Validate(register);
                if (!validator.IsValid)
                {
                    var errors = validator.Errors.Select(error => error.ErrorMessage).ToList();
                    return BadRequest(errors);
                }

                var userDTO = await _userService.RegisterUser(register);

                if (userDTO is null) return BadRequest("Usuario ya registrado");

                //return Created("Usuario creado, Bienvenid@", userDTO.Name);
                return CreatedAtAction(nameof(GetById),
                    new { id = userDTO.IdUser },
                    new { Message = $"Usuario creado, Bienvenido {userDTO.Name}" });
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error en el registro: {e.Message}");
            }
        }

        //[Authorize(Roles = "admin")]

        /// <summary> Admin elimina a un usuario </summary>
        /// <param name="id">Id del usuario a eliminar</param>
        /// <returns>Retorna un mensaje de confirmación</returns>
        ///  <response code="200">Usuario eliminado</response>
        ///  <responde code="404"> Usuario no encotrado</responde>

        [HttpDelete("admin/delete")]
        public async Task<IActionResult> DeleteUser([FromQuery] int id)
        {
            try
            {

                if (id == 0) return BadRequest("Usuario no encontrado o no autenticado ");

                var userDTO = await _userService.DeleteUser(id);
                if (!userDTO.Success) return NotFound(userDTO.Errors);

                return Ok($"Usuario eliminado: {userDTO.Data.Name}");
                //return Ok("Usuario eliminado ");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error al eliminar el usuario: {e.Message}");
            }
        }


        /// <summary>
        /// Se elimina un usuario automaticamente
        /// </summary>
        /// <response code="200">Usuarios eliminado <response>

        [Authorize]
        [HttpDelete("delete/myself")]// elimina el usuario autenticado automaticamente
        public async Task<IActionResult> DeleteUserSelf()
        {
            try
            {
                var id = _userService.GetIdUserAuthenticated();
                if (id == 0) return NotFound("Usuario no encontrado o no autenticado");

                var userDTO = await _userService.DeleteUser(id);
                if (!userDTO.Success) return NotFound(userDTO.Errors);

                return Ok($"Usuario eliminado: {userDTO.Data.Name}");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error al eliminar el usuario: {e.Message}");
            }
        }


        /// <summary>
        /// Actualiza los datos del usuario menos la contraseña 
        /// </summary>

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO updateUser)
        {
            try
            {
                var id = _userService.GetIdUserAuthenticated();
                if (id is 0) return NotFound("Usuario no encontrado o no autenticado");

                var user = await _userService.GetUser(id);
                if (!user.Success) return NotFound(user.Errors);

                var validationResult = _updateValidator.Validate(updateUser);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(errors);
                }

                await _userService.UpdateUser(id, updateUser);
                return Ok("Usuario actualizado correctamente");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error al actualizar el usuario: {e.Message}");
            }
        }

        /// <summary>
        /// Obtiene la informacion de un suario por el id
        /// </summary>

        [HttpGet("by/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var user = await _userService.GetUser(id);
                if (!user.Success) return NotFound(user.Errors);
                return Ok(user.Data);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error al obtener el usuario: {e.Message}");
            }
        }


        /// <summary>
        /// Obtiene el usuario por mediod el Email 
        /// </summary>

        ///  
        [HttpGet("GetByEmail/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            try
            {
                var user = await _userService.GetByEmail(email);
                if (!user.Success) return NotFound(user.Errors);
                return Ok(user.Data);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error al obtener el usuario por email: {e.Message}");
            }
        }


        /// <summary>
        /// Logging User
        /// </summary>
        /// <param name="login"> correo y contraseña (obligatorios) de usuario autenticado</param>

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            try
            {
                var user = await _userService.GetByEmail(login.Email);

                if (!user.Success) return Unauthorized("Usuario no registrado");

                var loginUser = await _userService.Login(login);

                if (!loginUser.Success) return Unauthorized("Usuario o contraseña incorrecta");

                return Ok(loginUser);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error al iniciar sesión: {e.Message}");
            }
        }






    }
}












