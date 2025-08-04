//using FinanzasPersonales.Domain;

namespace FinanzasPersonales.Aplication
{
    public class FullUserDataDTO
    {

        public int IdUser { get; set; }
        public string DNI { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public decimal TotalMoney { get; set; }
        public string Email { get; set; }
        public string ProfileImagen { get; set; }
        public DateTime DateRegister { get; set; }
        public ICollection<FullSpendDataDTO> UserSpends { get; set; }
        public ICollection<FullIncomeDataDTO> UserIncomes { get; set; }
        public ICollection<FullBudgetDataDTO> UserBudgets { get; set; }
    }
}
