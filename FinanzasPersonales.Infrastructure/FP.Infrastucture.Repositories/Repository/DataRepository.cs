
using Microsoft.EntityFrameworkCore;

using FinanzasPersonales.Aplication;



namespace FinanzasPersonales.Infrastructure
{
    public class DataRepository : IDataRepository<FullUserDataDTO>
    {

        private readonly FinaciasPersonales _context;

        public DataRepository(FinaciasPersonales context)
        {
            _context = context;
        }

        public async Task<FullUserDataDTO> DataById(int id)
        {
            // TODO: Obtener el total de la data de un usuario 

            // query para obtener la data de un usuario

            var query = from user in _context.User
                        where user.IdUser == id
                        select new FullUserDataDTO
                        {
                            IdUser = user.IdUser,
                            DNI = user.DNI,
                            Name = user.Name,
                            LastName = user.LastName,
                            Phone = user.Phone,
                            TotalMoney = user.TotalMoney,
                            Email = user.Email,
                            ProfileImagen = user.ProfileImagen,
                            DateRegister = user.DateRegister.Date,
                            UserSpends =  // lista de gastos
                                (from spend in _context.Spends
                                 where spend.IdUser == user.IdUser
                                 select new FullSpendDataDTO
                                 {
                                     IdSpend = spend.IdSpend,
                                     Name = spend.Name,
                                     Description = spend.Description,
                                     Amount = spend.Amount,
                                     Date = spend.DateSpend.Date,
                                     TypeSpend = spend.TypeSpend
                                 }).ToList(),
                            UserIncomes =  // lista de ingresos
                                (from income in _context.Incomes
                                 where income.IdUser == user.IdUser
                                 select new FullIncomeDataDTO
                                 {
                                     IdIncome = income.IdIncome,
                                     Name = income.Name,
                                     Description = income.Description,
                                     Amount = income.Amount,
                                     Date = income.DateIncome.Date,
                                     TypeIncome = income.TypeIncome
                                 }).ToList(),

                            UserBudgets = // lista de presupuestos
                                (from budget in _context.Budget
                                 where budget.IdUser == user.IdUser
                                 select new FullBudgetDataDTO
                                 {
                                     IdBudget = budget.IdBudget,
                                     Name = budget.Name,
                                     Description = budget.Description,
                                     TotalAmount = budget.TotalBudget,
                                     Date = budget.Date.Date,
                                     BudgetLists = 
                                        // listad e categorias de presupuesto
                                        (from budgetList in _context.BudgetCategory
                                         where budgetList.IdBudget == budget.IdBudget
                                         select new FullBudgetCategoryDTO
                                         {
                                             IdBudgetCategory = budgetList.IdBudgetCategory,
                                             Name = budgetList.Name,
                                             Description = budgetList.Description,
                                             Amount = budgetList.Amount,
                                             Date = budgetList.Date.Date

                                         }).ToList()
                                 }).ToList()
                        };

            // Ejecuta la consulta  

            var result = await query.FirstOrDefaultAsync();

            return result;

        }

        public async Task<IEnumerable<FullUserDataDTO>> UserFullData()
        {
            // retorna toda la data de los usaurios 
           var query = from user in _context.User

                       select new FullUserDataDTO
                       {
                           IdUser = user.IdUser,
                           DNI = user.DNI,
                           Name = user.Name,
                           LastName = user.LastName,
                           Phone = user.Phone,
                           TotalMoney = user.TotalMoney,
                           Email = user.Email,
                           ProfileImagen = user.ProfileImagen,
                           DateRegister = user.DateRegister.Date,
                           UserSpends =  // lista de gastos
                               (from spend in _context.Spends
                                where spend.IdUser == user.IdUser
                                select new FullSpendDataDTO
                                {
                                    IdSpend = spend.IdSpend,
                                    Name = spend.Name,
                                    Description = spend.Description,
                                    Amount = spend.Amount,
                                    Date = spend.DateSpend,
                                    TypeSpend = spend.TypeSpend
                                }).ToList(),
                           UserIncomes =  // lista de ingresos
                               (from income in _context.Incomes
                                where income.IdUser == user.IdUser
                                select new FullIncomeDataDTO
                                {
                                    IdIncome = income.IdIncome,
                                    Name = income.Name,
                                    Description = income.Description,
                                    Amount = income.Amount,
                                    Date = income.DateIncome,
                                    TypeIncome = income.TypeIncome
                                }).ToList(),

                           UserBudgets = // lista de presupuestos
                               (from budget in _context.Budget
                                where budget.IdUser == user.IdUser
                                select new FullBudgetDataDTO
                                {
                                    IdBudget = budget.IdBudget,
                                    Name = budget.Name,
                                    Description = budget.Description,
                                    Date = budget.Date,
                                    BudgetLists =
                                       // listad e categorias de presupuesto
                                       (from budgetList in _context.BudgetCategory
                                        where budgetList.IdBudget == budget.IdBudget
                                        select new FullBudgetCategoryDTO
                                        {
                                            IdBudgetCategory = budgetList.IdBudgetCategory,
                                            Name = budgetList.Name,
                                            Description = budgetList.Description,
                                            Amount = budgetList.Amount,
                                            Date = budgetList.Date

                                        }).ToList()
                                }).ToList()
                       };

           // ejecuta la query en un listado

            var result = await query.ToListAsync();

            return result;
        }
    }
}
