using FinanzasPersonales.Domain;

namespace FinanzasPersonales.Aplication
{
    public class FullSpendDataDTO
    {
        public int IdSpend { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
       public TypeSpends TypeSpend { get; set; }
    }
}
