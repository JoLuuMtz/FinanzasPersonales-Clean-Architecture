using FinanciasPersonalesApiRest.DTOs.BudgetDTO;
using FinanciasPersonalesApiRest.Services.Interfaces;

using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace FinanciasPersonalesApiRest.Controllers
{

    [Route("/api/budget/category")]
    [ApiController]
    public class BudgetCategoryController : ControllerBase
    {

        private readonly IValidator<CreateBudgetCategoryDTO> _validatorCreate;
        private readonly IValidator<UpdateBudgetCategoryDTO> _validatorUpdate;
        private readonly IBudgetCategoryService _budgetCategoryService;

        public BudgetCategoryController(IValidator<CreateBudgetCategoryDTO> validator, IBudgetCategoryService budgetCategoryService)
        {
            _validatorCreate = validator;
            _budgetCategoryService = budgetCategoryService;
        }


        [HttpGet("all")]
        public async Task<ActionResult> GetAll()
        {
            var categories = await _budgetCategoryService.GetAll();

            return Ok(categories);

        }

        /// <summary>
        /// Crea un nuevo tipo de presupuesto
        /// </summary>
        /// <response code="200">Presupuesto creado </response>
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateBudgetCategoryDTO newCategory)
        {
            var validator = _validatorCreate.Validate(newCategory);

            if (!validator.IsValid)
            {
                // retorna los mensajes de error
                var error = validator
                    .Errors
                    .Select(x => x.ErrorMessage)
                    .ToList();

                return BadRequest(error);
            }

            try
            {
                var category = await _budgetCategoryService.Create(newCategory);

                if (!category.Success) return BadRequest(category.Errors);

                return Ok($"Categoria  {category.Data.Name} creada ");

            }
            catch (Exception e)
            {

                return Problem(detail: e.Message,
                                title: "Error en la creación",
                                statusCode: 500);

            }

        }

        /// <summary>
        /// Actualiza la categoria de presupuesto
        /// </summary>
        /// <response code="200">Presupuesto creado </response> 

        [Authorize]
        [HttpPut("update")]
        public async Task<ActionResult> Update([FromQuery] int id,
              [FromBody] UpdateBudgetCategoryDTO update)
        {

            try
            {
                var validator = _validatorUpdate.Validate(update);

                if (!validator.IsValid)
                {
                    var error = validator.Errors.Select(x => x.ErrorMessage);
                    return BadRequest(new { Message = "Errores de validación", Errors = error });
                }

                var category = await _budgetCategoryService.Update(id, update);

                if (!category.Success)
                    return NotFound(category.Errors);

                return Ok($"Categoria {category.Data.Name} actualizada");
            }
            catch (Exception e)
            {

                //throw new Exception($"Error en la actualizacion {e} ");
                return Problem(detail: e.Message,
                               title: "Error en la actualización",
                               statusCode: 500);

            }

        }

        // TODO : Arreglar esta mrd xd 
        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> delete([FromQuery] int id)
        {
            var category = await _budgetCategoryService.GetById(id);

            if (!category.Success)
            {
                return NotFound(category.Errors);

            };
            await _budgetCategoryService.Delete(id);

            return Ok($"Categoria {category.Data.Name} eliminada");

        }

    }
}
