using AutoMapper;
using FinanciasPersonalesApiRes;
using FinanciasPersonalesApiRest.DTOs.BudgetDTO;
using FinanciasPersonalesApiRest.DTOs.IncomesDTO;

using FinanciasPersonalesApiRest.Models;
using FinanciasPersonalesApiRest.Repository.Interfaces;
using FinanzasPersonales.Aplication.FP.Aplication.Interfaces.Services;
using Microsoft.Data.SqlClient;




namespace FinanciasPersonalesApiRest.Services
{
    public class IncomesServices : IIncomesServices
    {

        private readonly IRepository<Income> Repository;
        private readonly ITypeRepository<TypeIncomes> _TypeIncomesRepository;
        private readonly IIncomesRespository _incomesRespository;
        private readonly IUserService _userServices;
        private readonly IMapper _mapper;

        public IncomesServices
            (
            IRepository<Income> incomeRepository,
            ITypeRepository<TypeIncomes> typeIncomesRepository,
            IIncomesRespository incomesRespository2,
            IUserService userService,
            IMapper mapper
            )
        {
            Repository = incomeRepository;
            _TypeIncomesRepository = typeIncomesRepository;
            _incomesRespository = incomesRespository2;
            _userServices = userService;
            _mapper = mapper;
        }

        public async Task<IncomesDTO> CreateIncome(CreateIncomesDTO income)
        {
            int userIdAuthenticated = _userServices.GetIdUserAuthenticated();

            // no puedo mapear directamente de CreateIncomesDTO a
            // Income porque el IdUser no está en CreateIncomesDTO
            var newIncome = new Income
            {
                Name = income.Name.ToLower().Trim(),
                Description = income.Description.ToLower().Trim(),
                Amount = income.Amount,
                DateIncome = income.Date.Date,
                IdTypeIncome = income.IdTypeIncomes,
                IdUser = userIdAuthenticated   // retorna el id del usuario autenticado
            };


            try
            {
                await Repository.Create(newIncome);
            }
            catch (SqlException e)
            {
                throw new Exception($"Error en la creación {e.Message}");
            }
            catch
            (Exception e)
            {
                throw new Exception($"Error desconocido {e.Message}");
            }
            //DONE : Mapea de Income a IncomesDTO

            return _mapper.Map<IncomesDTO>(newIncome);
        }

        public async Task<ServiceResult<IncomesDTO>> Update(int id, UpdateIncomesDTO income)
        {

            var incomes = Repository.GetById(id);

            if (incomes is null)
                return ServiceResult<IncomesDTO>
                    .Fail("No se encontro el ingreso");

            //Si algun valor es nullo se mantiene el valor anterior 
            // Si no  se actualiza con el nuevo valor
            incomes.Result.Name = income.Name ?? incomes.Result.Name;
            incomes.Result.Description = income.Description ?? incomes.Result.Description;
            incomes.Result.Amount = income.Amount ?? incomes.Result.Amount;
            incomes.Result.DateIncome = income.Date ?? incomes.Result.DateIncome;

            try
            {
                await Repository.Update(incomes.Result);
            }
            catch (SqlException e)
            {
                throw new Exception($" Error en la actualziacion {e.Message} ");
            }


            //DONE : Implementar Mapeo automapper

            // mapea de Incomes.Result a IncomesDTO
            return ServiceResult<IncomesDTO>
                .Ok(_mapper.Map<IncomesDTO>(incomes.Result));
        }

        public async Task<IEnumerable<IncomesDTO>> GetIncomesAll()
        {
            var incomes = await Repository.GetAll();

            return _mapper.Map<IEnumerable<IncomesDTO>>(incomes);
        }

        public async Task<ServiceResult<IncomesDTO>> GetbyId(int id)
        {
            var income = await Repository.GetById(id);

            if (income is null)
                return ServiceResult<IncomesDTO>
                    .Fail("No se encontro el ingreso");

            //TODO : Implementar Mapeo automapper

            return ServiceResult<IncomesDTO>.Ok(_mapper.Map<IncomesDTO>(income));

        }

        public async Task<ServiceResult<IncomesDTO>> Delete(int id)
        {
            var income = await Repository.GetById(id);

            if (income is null) return
                    ServiceResult<IncomesDTO>
                    .Fail("No se encontro el ingreso");

            try
            {
                await Repository.Delete(income);

            }
            catch (SqlException e)
            {

                throw new Exception($"Error de elimiancion {e.Message} ");
            }

            return

                ServiceResult<IncomesDTO>.Ok(null);

        }


        //public async Task<decimal> TotalIncomesByUser()
        //{
        //    int userId = _userServices.GetIdUserAuthenticated();

        //    if (userId == 0 || userId == null)
        //    {
        //        throw new UnauthorizedAccessException("El usuario no está autenticado.");
        //    }

        //    // Obtener los ingresos del usuario y devolver la suma de sus montos
        //    var incomes = await _incomesRespository.GetIncomesByUserIdAsync(userId);

        //    // retorna la suma de los ingresos del usuario
        //    return incomes.Sum(x => x.Amount);

        //}

        public async Task<ServiceResult<TypeIncomesDTO>> CreateType(CreateTypeIncomesDTO typeIncomes)
        {
            var type = await _TypeIncomesRepository
                        .getByName(typeIncomes.Name.ToLower().Trim());

            if (!(type is null))
                return ServiceResult<TypeIncomesDTO>
                    .Fail(" Ya existe un tipo con este nombre  ");

            //TODO : AUTOMAPPER
            var newType = new TypeIncomes
            {

                Name = typeIncomes.Name.ToLower().Trim(),
                Description = typeIncomes.Description.ToLower().Trim()
            };

            try
            {
                await _TypeIncomesRepository.Create(newType);


                return ServiceResult<TypeIncomesDTO>
               .Ok(_mapper.Map<TypeIncomesDTO>(newType));

            }
            catch (SqlException e)
            {
                throw new Exception($" Error en la creacion del tipo {e.Message}");
            }





        }

        public async Task<IEnumerable<TypeIncomesDTO>> TypeGetAll()
        {
            var TypeIncome = await _TypeIncomesRepository.GetAll();

            // DONE: Implementar Mapeo automapper

            return _mapper.Map<IEnumerable<TypeIncomesDTO>>(TypeIncome);


        }

        //    public async Task<decimal> TotalIncomes()
        //    {
        //        var income = await Repository.GetAll();
        //        var totalIncomes = income.Sum(x => x.Amount);
        //        return totalIncomes;
        //    }

        //    //TODO: Testear este método

        //    // retorna el total de ingresos del usuario autenticado




        //}
    }
}
