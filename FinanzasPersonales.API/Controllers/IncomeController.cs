
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



using FinanzasPersonales.Aplication;

namespace FinanzasPersonales.API;

    [Route("api/Incomes")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IIncomesServices _incomesServices;
        private readonly IValidator<CreateTypeIncomesDTO> _typeValidor;
        private readonly IValidator<CreateIncomesDTO> _incomesValidator;
        private readonly IValidator<UpdateIncomesDTO> _updateIncomesValidator;

        public IncomeController(
            IValidator<CreateTypeIncomesDTO> validator, 
            IUserService userService, 
            IIncomesServices incomesServices,
            IValidator<CreateIncomesDTO> incomeValidator,
            IValidator<UpdateIncomesDTO> updateIncomeValidator
            )
        {
            _userService = userService;
            _incomesServices = incomesServices;
            _typeValidor = validator;
            _incomesValidator = incomeValidator;
            _updateIncomesValidator = updateIncomeValidator;
        }


        /// <summary>
        /// Crea un nuevo Ingreso 
        /// </summary>
        /// <response code="200">Presupuesto creado </response>
        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult> AddIncome([FromBody] CreateIncomesDTO newIncome)
        {

            var validator = _incomesValidator.Validate(newIncome);
            if (!validator.IsValid)
            {
                var errors = validator.Errors.Select(error => error.ErrorMessage).ToList();
                return BadRequest(errors);
            }
            try
            {
                var income = await _incomesServices.CreateIncome(newIncome);
                return CreatedAtAction(nameof(AddIncome), new { id = income.IdIncomes }, new { message = "Ingreso creado exitosamente", name = income.Name });
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error al crear el ingreso: {e.Message}");
            }
        }
        /// <summary>
        /// Elimina un ingreso por medio del id
        /// </summary>
        /// <response code="200">Presupuesto creado </response>
        [Authorize]
        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteIncome([FromQuery] int id)
        {
            try
            {
                var income = await _incomesServices.Delete(id);

                if (!income.Success)
                    return BadRequest(income.Errors);

                return Ok(
                    new { message = $"Se eliminó el ingreso {income.Data.Name} " });
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error al eliminar el ingreso: {e.Message}");
            }
        }
        /// <summary>
        /// Actualiza un nuevo ingreso por medio del id
        /// </summary>
        /// <response code="200">Presupuesto creado </response>
        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromQuery] int id, [FromBody] UpdateIncomesDTO income)
        {

            var validator = _updateIncomesValidator.Validate(income);
            if (!validator.IsValid)
            {
                var errors = validator.Errors.Select(error => error.ErrorMessage).ToList();
                return BadRequest(errors);
            }
            try
            {
                var incomes = await _incomesServices.Update(id, income);

                if (!incomes.Success) return BadRequest(incomes.Errors);

                return Ok(new { Message = "Ingreso actualizado correctamente" });

            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error al actualizar el ingreso: {e.Message}");
            }
        }
        /// <summary>
        /// Crea un nuevo tipo de ingreso 
        /// </summary>
        /// <response code="200">Presupuesto creado </response>
        [Authorize]
        [HttpPost("create/type")]
        public async Task<IActionResult> AddType([FromBody] CreateTypeIncomesDTO newType)
        {

            var validatorType = await _typeValidor.ValidateAsync(newType);
            if (!validatorType.IsValid)
            {
                var errors = validatorType.Errors.Select(error => error.ErrorMessage).ToList();
                return BadRequest(errors);
            }

            try
            {
                var typeIncome = await _incomesServices.CreateType(newType);

                if (!typeIncome.Success)
                    return BadRequest(typeIncome.Errors);

                return CreatedAtAction(nameof(AddType), new { id = typeIncome.Data.IdTypeIncomes }, typeIncome);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error al crear el tipo de ingreso: {e.Message}");
            }
        }
        /// <summary>
        /// Obtiene  todo los tipos de ingresos 
        /// </summary>
        /// <response code="200">Presupuesto creado </response>
        [HttpGet("types")]
        public async Task<IActionResult> GetAllType()
        {
            try
            {
                var types = await _incomesServices.TypeGetAll();
                return Ok(types);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error al obtener los tipos de ingreso: {e.Message}");
            }
        }
    }
