using AutoMapper;
using Azure.Identity;
using FinanciasPersonalesApiRes;
using FinanciasPersonalesApiRest.DTOs.BudgetDTO;
using FinanciasPersonalesApiRest.Models;
using FinanciasPersonalesApiRest.Repository.Interfaces;
using FinanzasPersonales.Aplication.FP.Aplication.Interfaces.Services;
using FluentValidation;
using Microsoft.Data.SqlClient;

namespace FinanciasPersonalesApiRest.Services
{
    public class BudgetService : IBudgetService
    {

        private readonly IRepository<Budget> _repository;
        private readonly IBudgetRepository _budgetRepository; // implementacion independiente de budget
        private readonly IUserService _userService;
        private readonly IIncomesServices _incomesServices;
        private readonly IMapper _mapper;


        public BudgetService
            (IRepository<Budget> repository,
            IUserService userService,
            IIncomesServices incomesServices,
            IMapper mapper,
            IBudgetRepository budgetRepository
     )
        {
            _repository = repository;
            _incomesServices = incomesServices;
            _userService = userService;
            _mapper = mapper;
            _budgetRepository = budgetRepository;

        }

        // metodo para verificar si ya
        // existe un presupuesto con el mismo nombre


        public async Task<ServiceResult<BudgetDTO>> CreateBudget(CreateBudgetDTO budgetDTO)
        {

            int userId = _userService.GetIdUserAuthenticated();

            var budget = new Budget
            {

                //IdBudget = budgetDTO.IdBudget,
                Name = budgetDTO.Name.ToLower().Trim(),
                Description = budgetDTO.Description.ToLower().Trim(),
                TotalBudget = 0,
                IdUser = userId
            };

            // verifica si ya existe un presupuesto con el mismo nombre
            if (await getBudgetByName(budget.Name) == true)
                return ServiceResult<BudgetDTO>
                    .Fail("Ya existe un prespuesto con ese nombre");

            try
            {
                await _repository.Create(budget);
            }
            catch (SqlException e)
            {
                throw new Exception($"Error en la insercion {e.Message}", e);
            }
            catch (Exception e)
            {
                throw new Exception($"Error desconocido {e.Message}", e);

            }
            // DONE: Mapea de entidad a DTO
            return ServiceResult<BudgetDTO>.Ok(_mapper.Map<BudgetDTO>(budget));


        }

        public async Task<bool> getBudgetByName(string name)
        {
            var budget = await _budgetRepository.getByName(name);

            if (budget is null) return false;

            return true;
        }

        public async Task<ServiceResult<BudgetDTO>> DeleteBudget(int id)
        {
            var budget = await _repository.GetById(id);

            if (budget == null)
                return ServiceResult<BudgetDTO>
                    .Fail("No existe el presupuesto");

            try
            {
                await _repository.Delete(budget);
            }
            catch (SqlException e)
            {
                throw new Exception($"Error en la eliminacion {e.Message}");
            }

            return ServiceResult<BudgetDTO>.Ok(null);
        }

        public async Task<ServiceResult<BudgetDTO>> GetBudget(int id)
        {
            var budget = await _repository.GetById(id);

            if (budget == null)
                return ServiceResult<BudgetDTO>
                    .Fail("No existe el presupuesto");

            return ServiceResult<BudgetDTO>.Ok(_mapper.Map<BudgetDTO>(budget));

        }

        //public async Task<ServiceResult<BudgetDTO>> GetBudgetByUserId(int userId)
        //{
        //    var user = await _userService.GetUser(userId);
        //    if (user == null) return null;

        //    var budgetById = await _budgetRepository.GetBudgetByUser(userId);



        //    return new BudgetDTO
        //    {
        //        IdBudget = budgetById.IdBudget,
        //        Name = budgetById.Name,
        //        Description = budgetById.Description,
        //        TotalBudget = budgetById.TotalBudget,
        //        Date = budgetById.Date,
        //        BudgetLists = budgetById.BudgetLists,
        //        IdUser = budgetById.IdUser
        //    };


        //}


        public async Task<ServiceResult<BudgetDTO>> UpdateBudget(int id, UpdateBudgetDTO budgetDTO)
        {
            var budget = await _repository.GetById(id);

            if (budget is null)
                return ServiceResult<BudgetDTO>
                    .Fail("No existe el presupuesto");

            // si esta vacio se mantiene el valor anterior
            budget.Name = budgetDTO.Name ?? budget.Name;
            budget.Description = budgetDTO.Description ?? budget.Description;

            try
            {
                await _repository.Update(budget);
            }
            catch (SqlException e)
            {

                throw new Exception($"Error en la actualizacion {e.Message}");
            }
            return ServiceResult<BudgetDTO>.Ok(
                _mapper.Map<BudgetDTO>(budget));

        }


    }
}
