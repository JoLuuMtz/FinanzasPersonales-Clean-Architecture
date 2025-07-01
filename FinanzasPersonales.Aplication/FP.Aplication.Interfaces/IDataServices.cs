using FinanciasPersonalesApiRes;
using FinanciasPersonalesApiRest.DTOs.FullDataDTO;
using FinanciasPersonalesApiRest.DTOs.UserDTO;

namespace FinanciasPersonalesApiRest.Services.Abstractions
{
    public interface IDataServices
    {

        public Task<IEnumerable<FullUserDataDTO>> GetAll();
        public Task<ServiceResult<FullUserDataDTO>> DataById(int id);

    }
}
