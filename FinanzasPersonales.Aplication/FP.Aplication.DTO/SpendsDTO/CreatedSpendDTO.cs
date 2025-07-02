namespace FinanzasPersonales.Aplication
{
    public class CreatedSpendDTO
    {
        //public int? IdSpend { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int IdTypeSpend { get; set; }
     

    }
}
