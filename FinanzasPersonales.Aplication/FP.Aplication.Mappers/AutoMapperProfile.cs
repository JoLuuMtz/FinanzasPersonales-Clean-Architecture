using AutoMapper;
using FinanzasPersonales.Domain;

namespace FinanzasPersonales.Aplication;



public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // USER MAPPING 

        // MAPEA DE User A UserDTO
        CreateMap<User, UserDTO>();

        //MAPEA DE RegisterUserDTO A User
        CreateMap<RegisterUserDTO, User>();


        // Incomes Mapping 

        CreateMap<Income, IncomesDTO>(); // mapea de Income a IncomesDTO
        CreateMap<CreateIncomesDTO, Income>();   // mapea de CreateIncomesDTO a Income

        //Type Mapping 
        CreateMap<TypeIncomes, TypeIncomesDTO>(); // mapea de TypeIncomes a TypeIncomesDTO
        CreateMap<TypeIncomesDTO, TypeIncomes>();
        // Budget Mapping

        CreateMap<Budget, BudgetDTO>(); // mapea de Budget a BudgetDTO
        CreateMap<CreateBudgetDTO, Budget>(); // mapea de CreateBudgetDTO a Budget


        CreateMap<BudgetCategory, BudgetCategoryDTO>();
        CreateMap<CreateBudgetCategoryDTO, BudgetCategory>();
      
        // Spend Type Mapping

        CreateMap<TypeSpends, TypeSpendsDTO>(); // mapea de TypeSpends a TypeSpendsDTO
        CreateMap<CreateTypeSpendDTO, TypeSpends>(); // mapea de CreateTypeSpendDTO a TypeSpends


        CreateMap<Spend, SpendsDTO>(); // mapea de Spend a SpendsDTO
        CreateMap<CreatedSpendDTO, Spend>(); // mapea de CreatedSpendDTO a Spend
    }

}

