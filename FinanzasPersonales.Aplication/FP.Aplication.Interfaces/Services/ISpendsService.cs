
namespace FinanzasPersonales.Aplication
{
    public interface ISpendsService
    {

        public Task<IEnumerable<SpendsDTO>> GetAll();
        public Task<ServiceResult<SpendsDTO>> GetById(int id);
        public Task<ServiceResult<SpendsDTO>> Create(CreatedSpendDTO spends);
        public Task<ServiceResult<SpendsDTO>> Delete(int id); // en caso de equivoque en insercion
        //public bool BudgetInsufficient();

        public Task<ServiceResult<TypeSpendsDTO>> CreateType(CreateTypeSpendDTO typeSpend);
        public Task<IEnumerable<TypeSpendsDTO>> TypeGetAll();


    }
}
