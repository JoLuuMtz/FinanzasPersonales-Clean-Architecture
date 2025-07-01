using AutoMapper;
using FinanciasPersonalesApiRes;
using FinanciasPersonalesApiRest.DTOs.SpendsDTO;
using FinanciasPersonalesApiRest.Models;
using FinanciasPersonalesApiRest.Repository.Interfaces;
using FinanciasPersonalesApiRest.Services.Interfaces;
using Microsoft.Data.SqlClient;


namespace FinanciasPersonalesApiRest.Services
{
    public class SpendsService : ISpendsService
    {
        private readonly IRepository<Spend> _Repository;
        private readonly ITypeRepository<TypeSpends> _TypeSpendsRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public SpendsService(
            IRepository<Spend> spendRepository,
            ITypeRepository<TypeSpends> typeSpendsRepository,
            IUserService userService,
            IMapper mapper)
        {
            _Repository = spendRepository;
            _TypeSpendsRepository = typeSpendsRepository;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<ServiceResult<SpendsDTO>> Create(CreatedSpendDTO spends)
        {

            int UserIdAuthenticated = _userService.GetIdUserAuthenticated();

            var spend = new Spend
            {
                Name = spends.Name.ToLower().Trim(),
                Description = spends.Description.ToLower().Trim(),
                Amount = spends.Amount,
                DateSpend = spends.Date.Date,
                IdTypeSpend = spends.IdTypeSpend,
                IdUser = UserIdAuthenticated
            };

            // si el usuario no tiene suficiente dinero
            if (spend.Amount > await _userService.TotalMoneyByUser())
            {
                return ServiceResult<SpendsDTO>.Fail("No tienes suficiente dinero");
            }

            try
            {
                await _Repository.Create(spend);
                return ServiceResult<SpendsDTO>.Ok(_mapper.Map<SpendsDTO>(spend));
            }
            catch (SqlException e)
            {
                throw new Exception($"Error al crear el gasto {e.Message}");
            }
        }

        public async Task<IEnumerable<SpendsDTO>> GetAll()
        {
            try
            {
                var spend = await _Repository.GetAll();
                return spend.Select(x => _mapper.Map<SpendsDTO>(spend));

            }
            catch (SqlException E)
            {
                throw new Exception($"Error en la opcion de los datos {E} ");
            }

        }

        public async Task<ServiceResult<SpendsDTO>> Delete(int id)
        {
            try
            {
                var spend = await _Repository.GetById(id);

                if (spend is null)
                    return ServiceResult<SpendsDTO>.Fail("El gasto no existe");

                await _Repository.Delete(spend);

                return ServiceResult<SpendsDTO>.Ok(null);
            }
            catch (Exception e)
            {
                throw new Exception($"Error al eliminar el gasto {e.Message}");
            }
        }
        public async Task<ServiceResult<SpendsDTO>> GetById(int id)
        {
            try
            {
                var spend = await _Repository.GetById(id);

                if (spend is null)
                    return ServiceResult<SpendsDTO>.Fail("El gasto no existe");

                return ServiceResult<SpendsDTO>.Ok(_mapper.Map<SpendsDTO>(spend));
            }
            catch (Exception e)
            {
                throw new Exception($"Error al buscar el gasto {e.Message}");
            }

        }

        public async Task<IEnumerable<TypeSpendsDTO>> TypeGetAll()
        {
            try
            {
                var TypeIncome = await _TypeSpendsRepository.GetAll();

                // TODO : Implementar Mapeo automapper
                return TypeIncome.Select(x => new TypeSpendsDTO
                {
                    IdTypeSpends = x.IdTypeSpends,
                    Name = x.Name,
                    Description = x.Description
                });
            }
            catch (SqlException e)
            {
                throw new Exception($"Error la lectura {e.Message}");
            }

        }

        public async Task<ServiceResult<TypeSpendsDTO>> CreateType(CreateTypeSpendDTO typeSpend)
        {
            try
            {
                var type = await _TypeSpendsRepository
                                      .getByName(typeSpend.Name.ToLower().Trim());
                if (!(type is null))
                    return ServiceResult<TypeSpendsDTO>
                        .Fail("El tipo de gasto ya existe");
            }
            catch (SqlException e)
            {
                throw new Exception($"Error al buscar el tipo de gasto {e.Message}");
            }


            //TODO : AUTOMAPPER

            var newType = new TypeSpends
            {
                Name = typeSpend.Name.ToLower().Trim(),
                Description = typeSpend.Description.ToLower().Trim()
            };

            try
            {
                await _TypeSpendsRepository.Create(newType);

                //TODO : Implementar Mapeo automapper

                return ServiceResult<TypeSpendsDTO>
                    .Ok(new TypeSpendsDTO
                    {
                        IdTypeSpends = newType.IdTypeSpends,
                        Name = newType.Name,
                        Description = newType.Description
                    });
            }
            catch (SqlException e)
            {
                throw new Exception($"Error al crear el tipo de gasto {e.Message}");
            }

        }

    }
}
