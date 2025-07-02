using System.ComponentModel.DataAnnotations;

namespace FinanzasPersonales.Domain
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
