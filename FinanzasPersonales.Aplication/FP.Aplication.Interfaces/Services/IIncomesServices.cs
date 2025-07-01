using FinanciasPersonalesApiRes;
using FinanciasPersonalesApiRest.DTOs.IncomesDTO;
using FinanciasPersonalesApiRest.Models;

namespace FinanzasPersonales.Aplication.FP.Aplication.Interfaces.Services
{
    public interface IIncomesServices
    {
        public Task<IEnumerable<TypeIncomesDTO>> TypeGetAll();

        public Task<ServiceResult<TypeIncomesDTO>> CreateType(CreateTypeIncomesDTO typeIncomes);

        public Task<IncomesDTO> CreateIncome(CreateIncomesDTO income);
        public Task<IEnumerable<IncomesDTO>> GetIncomesAll();
        public Task<ServiceResult<IncomesDTO>> GetbyId(int id);
        public Task<ServiceResult<IncomesDTO>> Update(int id, UpdateIncomesDTO income);
        public Task<ServiceResult<IncomesDTO>> Delete(int id);
        //public Task<decimal> TotalIncomes();
        //public Task<decimal> TotalIncomesByUser();
    }
}
