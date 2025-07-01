using FinanciasPersonalesApiRest.DTOs.BudgetDTO;
using FinanciasPersonalesApiRest.DTOs.FullDataDTO;
using FinanciasPersonalesApiRest.DTOs.IncomesDTO;
using FinanciasPersonalesApiRest.DTOs.SpendsDTO;
using FinanciasPersonalesApiRest.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanciasPersonalesApiRest.Repository.Abstractions
{
    public interface IDataRepository<Entity> where Entity : class
    {

        public Task<IEnumerable<FullUserDataDTO>> UserFullData();
        public Task<FullUserDataDTO?> DataById(int id);


        //var query = from user in _context.Users
        //            where user.IdUser == userId
        //            select new UserFullDataDTO
        //            {
        //                IdUser = user.IdUser,
        //                DNI = user.DNI,
        //                Name = user.Name,
        //                LastName = user.LastName,
        //                Phone = user.Phone,
        //                TotalMoney = user.TotalMoney,
        //                Email = user.Email,
        //                ProfileImagen = user.ProfileImagen,
        //                DateRegister = user.DateRegister,
        //                UserSpends = (from spend in _context.Spends
        //                              where spend.UserId == user.IdUser
        //                              select new SpendDTO
        //                              {
        //                                  IdSpend = spend.IdSpend,
        //                                  Name = spend.Name,
        //                                  Amount = spend.Amount,
        //                                  Date = spend.Date
        //                              }).ToList(),
        //                UserIncomes = (from income in _context.Incomes
        //                               where income.UserId == user.IdUser
        //                               select new IncomeDTO
        //                               {
        //                                   IdIncome = income.IdIncome,
        //                                   Name = income.Name,
        //                                   Amount = income.Amount,
        //                                   Date = income.Date
        //                               }).ToList(),
        //                UserBudgets = (from budget in _context.Budgets
        //                               where budget.UserId == user.IdUser
        //                               select new BudgetDTO
        //                               {
        //                                   IdBudget = budget.IdBudget,
        //                                   Name = budget.Name,
        //                                   PlannedAmount = budget.PlannedAmount,
        //                                   ActualAmount = budget.ActualAmount,
        //                                   Date = budget.Date
        //                               }).ToList()
        //            };

    }
}
