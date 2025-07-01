namespace FinanciasPersonalesApiRest.DTOs.SpendsDTO
{
    public class SpendsDTO
    {
        public int? IdSpends { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int IdTypeSpends { get; set; }
        public int IdUser { get; set; }
    }
}
