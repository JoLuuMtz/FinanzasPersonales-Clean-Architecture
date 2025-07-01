using FinanciasPersonalesApiRest.DTOs.BudgetDTO;
using FinanzasPersonales.Aplication;
using FinanzasPersonales.Aplication.FP.Aplication.Interfaces.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace FinanciasPersonales.API;


    [Route("api/budget")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _budgetService;
       
        private readonly IValidator<CreateBudgetDTO> _createValidator;
        private readonly IValidator<UpdateBudgetDTO> _updateValidator;

        public BudgetController(
            IBudgetService budgetService,
            IUserService userService,
            IValidator<CreateBudgetDTO> createValidator,
            IValidator<UpdateBudgetDTO> updateValidator
            )
        {
            _budgetService = budgetService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }


        /// <summary>
        /// Registra un usuario nuevo
        /// </summary>
        /// <response code="200">Presupuesto creado </response>
        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult<BudgetDTO>> CreateBudget([FromBody] CreateBudgetDTO budgetDTO)
        {
            try
            {
                var validator = _createValidator.Validate(budgetDTO);

                if (!validator.IsValid)
                {
                    var errors = validator.Errors.Select(error => error.ErrorMessage).ToList();
                    return BadRequest(errors);
                }

                var budget = await _budgetService.CreateBudget(budgetDTO);

                if (!budget.Success)
                {
                    return BadRequest(budget.Errors);
                }

                return Ok(new { Message = $"Creado: {budget.Data.Name}" });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message,
                               title: "Error en la actualización",
                               statusCode: 500);
            }
        }

        /// <summary>
        ///Obtiene un prespuesto por el id
        /// </summary>
        /// <response code="200">Presupuesto creado </response>
        [HttpGet("get/{id}")]
        public async Task<ActionResult<BudgetDTO>> GetBudget(int id)
        {
            try
            {
                var budget = await _budgetService.GetBudget(id);

                if (!budget.Success)
                {
                    return NotFound(budget.Errors);
                }
                return Ok(budget.Data);
            }
            catch (Exception ex)
            {
                // Loguear error
                return StatusCode(500, new { Message = "Ocurrió un error interno", Error = ex.Message });
            }
        }
        /// <summary>
        /// Actualiza un presupuesto por medio del id
        /// </summary>
        /// <response code="200">Presupuesto creado </response>
        [Authorize]
        [HttpPut("update")]
        public async Task<ActionResult<BudgetDTO>> UpdateBudget([FromQuery] int id, [FromBody] UpdateBudgetDTO budgetDTO)
        {
            try
            {
                var budget = await _budgetService.UpdateBudget(id, budgetDTO);

                if (!budget.Success)
                {
                    return NotFound(budget.Errors);
                }

                var validator = _updateValidator.Validate(budgetDTO);

                if (!validator.IsValid)
                {
                    var errors = validator.Errors.Select(error => error.ErrorMessage).ToList();
                    return BadRequest(errors);
                }

                return Ok(new { Message = "Actualización exitosa!" });
            }
            catch (Exception ex)
            {
                // Loguear error
                return StatusCode(500, new { Message = "Ocurrió un error interno", Error = ex.Message });
            }
        }


        /// <summary>
        /// Elimina un prespuesto por el id
        /// </summary>
        /// <response code="200">Presupuesto creado </response>
        [Authorize]
        [HttpDelete("delete")]
        public async Task<ActionResult<BudgetDTO>> DeleteBudget([FromQuery] int id)
        {
            try
            {
                var budget = await _budgetService.DeleteBudget(id);

                if (!budget.Success)
                {
                    return NotFound(budget.Errors);
                }

                return Ok(new { Message = $"Elemento eliminado {budget.Data.Name}" });
            }
            catch (Exception ex)
            {
           
                return StatusCode(500, new { Message = "Ocurrió un error interno", Error = ex.Message });
            }
        }
    }
