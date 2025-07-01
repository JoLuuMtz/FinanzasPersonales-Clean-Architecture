using FinanciasPersonalesApiRes;
using FinanciasPersonalesApiRest.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace FinanciasPersonalesApiRest.Controllers
{
    [Route("/api/data")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataServices _dataServices;

        public DataController
            (IDataServices dataServices)
        {
            _dataServices = dataServices;
        }
        // retorna toda la data de todos los usuarios
       
        /// <summary>
        /// Obtiene toda la data de los usuarios para hacer la UI
        /// </summary>
        /// <response code="200">Presupuesto creado </response>
        [HttpGet("Users")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _dataServices.GetAll();

            return Ok(result);
        }

        /// <summary>
        /// Obriene un  solo usuario por medio del id
        /// </summary>
        /// <response code="200">Presupuesto creado </response>
        [HttpGet("by/id")]
        public async Task<IActionResult> DataUserById([FromQuery] int id)
        {
            var result = await _dataServices.DataById(id);
            if (!result.Success)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }




    }
}
