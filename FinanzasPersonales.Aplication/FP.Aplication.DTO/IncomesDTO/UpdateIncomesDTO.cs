namespace FinanciasPersonalesApiRest.DTOs.IncomesDTO
{
    public class UpdateIncomesDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Date { get; set; }


    }
}
