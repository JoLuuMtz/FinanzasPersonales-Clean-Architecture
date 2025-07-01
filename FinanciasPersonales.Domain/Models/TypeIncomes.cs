using System.ComponentModel.DataAnnotations;

namespace FinanciasPersonalesApiRest.Models
{
    public class TypeIncomes
    {

        [Key]

        public int IdTypeIncomes { get; set; }
        public string Name { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
    }
}
