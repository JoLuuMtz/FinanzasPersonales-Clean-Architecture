using System.ComponentModel.DataAnnotations;

namespace FinanciasPersonalesApiRest.Models
{
    public class TypeSpends
    {
        [Key]
      
        public int IdTypeSpends { get; set; }
        public string Name { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
    }
}
