

using Microsoft.Data.SqlClient;

namespace FinanzasPersonales.Aplication
{
    public class DataService : IDataServices
    {
        private readonly IDataRepository<FullUserDataDTO> _dataRepository;
        private readonly IUserService _userService;

        public DataService(
            IDataRepository<FullUserDataDTO> dataRepository
            , IUserService userService)
        {
            _dataRepository = dataRepository;
            _userService = userService;
        }

        public async Task<ServiceResult<FullUserDataDTO>> DataById(int id)
        {
            var user = await _userService.GetUser(id);
            if (!user.Success)
            {
                return ServiceResult<FullUserDataDTO>.Fail(" Usuario no encontrado  ");
            }

            try
            {
                var dataByUser = await _dataRepository.DataById(id);
                return ServiceResult<FullUserDataDTO>.Ok(dataByUser);
            }
            catch (Exception e)
            {
                throw new Exception($"Error al obtener la data del usuario {e}");
            }
        }

        public async Task<IEnumerable<FullUserDataDTO>> GetAll()
        {
            try
            {
                return await _dataRepository.UserFullData();
            }
            catch (SqlException e)
            {
                throw new Exception($"Error al obtener la data de los usuarios: {e.Message}");
            }
            catch (Exception e)
            {
                throw new Exception($"Error desconocido {e}");
            }

        }
    }


}

