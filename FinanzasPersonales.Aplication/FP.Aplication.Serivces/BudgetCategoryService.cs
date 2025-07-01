using AutoMapper;
using FinanciasPersonalesApiRes;
using FinanciasPersonalesApiRest.DTOs.BudgetDTO;
using FinanciasPersonalesApiRest.Models;
using FinanciasPersonalesApiRest.Repository.Interfaces;
using FinanzasPersonales.Aplication.FP.Aplication.Interfaces.Services;


namespace FinanciasPersonalesApiRest.Services
{
    public class BudgetCategoryService : IBudgetCategoryService
    {
        private readonly IRepository<BudgetCategory> _Repository;
        private readonly IRepository<Budget> _BudgetRepository;
        private readonly IBudgetCategoryRepository _budgetCategoryRepository;
        private readonly IIncomesServices _incomesServices;
        private readonly IUserService _userServices;
        private readonly IMapper _mapper;


        public BudgetCategoryService
            (IRepository<BudgetCategory> Repository,
            IBudgetCategoryRepository budgetCategoryRepository,
            IIncomesServices incomesService,
            IUserService userService,
            IRepository<Budget> budgetRepository,
            IMapper mapper
            )

        {

            _Repository = Repository;
            _budgetCategoryRepository = budgetCategoryRepository;
            _incomesServices = incomesService;
            _userServices = userService;
            _BudgetRepository = budgetRepository;
            _mapper = mapper;
        }

        // verifica si ya existe una categoria con el mismo nombre
        private async Task<bool> BudgetByName(string name)
        {
            var budget = await _budgetCategoryRepository.GetBudgetCategoryByName(name);

            if (budget is null)
            {
                ServiceResult<BudgetCategoryDTO>.Fail("La categoria ya existe");
                return false;
            }

            return true;

        }

        //Verifica que si existe el budgetId

        private async Task<int> ValidatorExistBudgetId(int idbudget)
        {
            var budget = await _BudgetRepository.GetById(idbudget);

            // si no existe el BudgetId retorna 0
            if (budget is null) return 0;

            return budget.IdBudget;
        }


        // TODO: verficar si el presupuesto tiene suficiente dinero 
        //  para crear una categoria
        public async Task<ServiceResult<BudgetCategoryDTO>> Create(CreateBudgetCategoryDTO newCategoryBudget)
        {
            var category = new BudgetCategory
            {
                Name = newCategoryBudget.Name.Trim().ToLower(),
                Description = newCategoryBudget.Description.Trim().ToLower(),
                Amount = newCategoryBudget.Amount,
                Date = newCategoryBudget.Date.Date,
                IdBudget = newCategoryBudget.IdBudget,
            };

            int budget = await ValidatorExistBudgetId(category.IdBudget);

            if (budget == 0)
            {
                return ServiceResult<BudgetCategoryDTO>
                    .Fail("El presupuesto no existe");
            }

            if (category.Amount > await _userServices.TotalMoneyByUser())
            {
                return ServiceResult<BudgetCategoryDTO>
                    .Fail("No tienes suficiente dinero para presupuestar");
            }

            // retorna null si la categoria ya existe
            if (await this.BudgetByName(category.Name))
            {
                return ServiceResult<BudgetCategoryDTO>
                    .Fail("La categoria ya existe");
            }

            await _Repository.Create(category);

            // Mapea de entidad a DTO
            return ServiceResult<BudgetCategoryDTO>
                .Ok(_mapper.Map<BudgetCategoryDTO>(category));


            //.Ok(new BudgetCategoryDTO // mapear con automapper
            //{

            //    Name = category.Name,
            //    Description = category.Description,
            //    Amount = category.Amount,
            //    Date = category.Date,
            //    IdBudget = category.IdBudget
            //});


        }

        public async Task<ServiceResult<BudgetCategoryDTO>> Delete(int id)
        {
            try
            {
                var category = await _Repository.GetById(id);
                if (category is null)
                {
                    return ServiceResult<BudgetCategoryDTO>.Fail("La categoria no existe");
                }

                await _Repository.Delete(category);

                return ServiceResult<BudgetCategoryDTO>.Ok(null);

            }
            catch (Exception e)
            {
                throw new Exception($"Error en la eliminacion {e.Message}", e);
            }

        }

        public async Task<IEnumerable<BudgetCategoryDTO>> GetAll()
        {
            try
            {
                var budgetCategory = await _Repository.GetAll();

                //TODO: mapear con automaper
                return budgetCategory.Select(_mapper.Map<BudgetCategoryDTO>);
            }

            catch (Exception e)
            {
                throw new Exception($"Error en la consulta {e.Message}", e);
            }

        }

        public async Task<ServiceResult<BudgetCategoryDTO>> GetById(int id)
        {
            var budgetCategory = await _Repository.GetById(id);

            if (budgetCategory is null)
            {

                return ServiceResult<BudgetCategoryDTO>.Fail("La categoria no existe");
            }

            //TODO: mappear de entidad a DTO
            return ServiceResult<BudgetCategoryDTO>
                .Ok(_mapper.Map<BudgetCategoryDTO>(budgetCategory));


        }

        //TODO Verificar si el presupuesto tiene suficiente dinero
        public Task<bool> IncomesEnoght()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<BudgetCategoryDTO>> Update(int id, UpdateBudgetCategoryDTO updateBudgetCategoryDTO)
        {


            var budgetCategory = await _Repository.GetById(id);

            if (budgetCategory is null)
            {
                return ServiceResult<BudgetCategoryDTO>.Fail("La categoria no existe");
            }

            // si esta vacio se mantiene el valor anterior
            budgetCategory.Name = updateBudgetCategoryDTO.Name
                                       ?? budgetCategory.Name;

            budgetCategory.Description = updateBudgetCategoryDTO.Description
                                       ?? budgetCategory.Description;

            budgetCategory.Amount = updateBudgetCategoryDTO.Amount
                                       ?? budgetCategory.Amount;

            await _budgetCategoryRepository.Update(budgetCategory);

            // TODO: Mapear 

            return ServiceResult<BudgetCategoryDTO>
                .Ok(_mapper.Map<BudgetCategoryDTO>(budgetCategory));



        }


    }
}
