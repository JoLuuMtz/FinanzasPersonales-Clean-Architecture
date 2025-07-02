using FinanzasPersonales.Domain;

namespace FinanzasPersonales.Aplication
{
    public class FullIncomeDataDTO
    {
        public int IdIncome { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public TypeIncomes TypeIncome { get; set; }
    }
}
