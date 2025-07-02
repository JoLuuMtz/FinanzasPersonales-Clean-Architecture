

namespace FinanzasPersonales.Aplication
{
    public interface IDataServices
    {

        public Task<IEnumerable<FullUserDataDTO>> GetAll();
        public Task<ServiceResult<FullUserDataDTO>> DataById(int id);

    }
}
