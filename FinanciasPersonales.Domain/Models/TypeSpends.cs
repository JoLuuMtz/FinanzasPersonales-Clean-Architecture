using System.ComponentModel.DataAnnotations;

namespace FinanzasPersonales.Domain
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
