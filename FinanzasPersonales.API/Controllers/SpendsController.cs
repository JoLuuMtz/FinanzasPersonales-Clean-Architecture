using FinanciasPersonalesApiRest.DTOs.SpendsDTO;

using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


using FinanzasPersonales.Aplication.FP.Aplication.Interfaces.Services;


namespace FinanciasPersonales.API
{
    [Route("api/Spends")]
    [ApiController]
    public class SpendsController : ControllerBase
    {
        //private readonly ITypeSpendsService _typeSpendsService;

        private readonly ISpendsService _spendService;
        private readonly IValidator<CreateTypeSpendDTO> _typeValidator;
        private readonly IValidator<CreatedSpendDTO> _validator;

        public SpendsController(
            IValidator<CreateTypeSpendDTO> Typevalidator,
            ISpendsService spendsServices,
            IValidator<CreatedSpendDTO> validator
            )
        {

            _spendService = spendsServices;
            _typeValidator = Typevalidator;
            _validator = validator;
        }


        // CRUD's  Spends


        /// <summary>
        /// Crea un nuevo gasto
        /// </summary>

       
        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] CreatedSpendDTO newSpend)
        {

            var validator = _validator.Validate(newSpend);

            if (!validator.IsValid)
            {
                var error = validator
                    .Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(error);
            }

            var spend = await _spendService.Create(newSpend);
            if (!spend.Success)
            {
                return BadRequest(spend.Errors);
            }
            // manda el id del gasto creado en una cabecera
            return CreatedAtAction(nameof(Create), new { id = spend.Data.IdSpends }, spend.Data.Name);
        }

        // / <summary>
        // / obtiene el gasto por medio del id
        // / </summary>
  
        // / 
        // [HttpGet("byId")]
        // public async Task<IActionResult> GetById([FromQuery] int id)
        // {
        //    var spend = await _spendService.GetById(id);

        //    if (!spend.Success)
        //        return NotFound(spend.Errors);

        //    return Ok(spend.Data);
        // }

        /// <summary>
        /// elimina un gasto por medio del id
        /// </summary>
    
     

        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var spend = await _spendService.Delete(id);

            if (!spend.Success)
                return NotFound(spend.Errors);

            return Ok(new { Message = $"Gasto {spend.Data.Name} eliminado" });
        }


        // TypeSpends
        /// <summary>
        /// Crea un nuevo tipo de gasto
        /// </summary>
  

        [Authorize]
        [HttpPost("create/Type")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddType([FromBody] CreateTypeSpendDTO newType)
        {

            var validatorType = await _typeValidator.ValidateAsync(newType);
            if (!validatorType.IsValid)
            {         // retorna la lista de errores              
                var errors = validatorType
                    .Errors
                    .Select(error => error.ErrorMessage)
                    .ToList();

                return BadRequest(errors); // Devuelve la lista de errores
            }
            var typeSpend = await _spendService.CreateType(newType);

            if (typeSpend is null)
            {
                return BadRequest("El tipo de gasto ya existe");
            }
            return CreatedAtAction(nameof(AddType), new { id = typeSpend.Data.IdTypeSpends }, typeSpend);
        }

        /// <summary>
        /// Obtiene los tipos de gastos creados
        /// </summary>
 
        [HttpGet("types")]
        public async Task<IEnumerable<TypeSpendsDTO>> GetAllType()
            => await _spendService.TypeGetAll();
    }
}

