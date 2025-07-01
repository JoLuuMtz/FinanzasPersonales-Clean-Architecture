using FinanciasPersonalesApiRes;
using FinanciasPersonalesApiRest.DTOs.FullDataDTO;
using FinanciasPersonalesApiRest.DTOs.UserDTO;

namespace FinanzasPersonales.Aplication.FP.Aplication.Interfaces.Services
{
    public interface IDataServices
    {

        public Task<IEnumerable<FullUserDataDTO>> GetAll();
        public Task<ServiceResult<FullUserDataDTO>> DataById(int id);

    }
}
