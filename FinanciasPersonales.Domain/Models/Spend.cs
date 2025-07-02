

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanzasPersonales.Domain
{
    public class Spend
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSpend { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        [Column(TypeName = "date")]

        public DateTime DateSpend { get; set; }
        [Required]
        public int IdTypeSpend { get; set; }


        // mapeo de la tabla TypeSpend
        [ForeignKey("IdTypeSpend")]
        public virtual TypeSpends TypeSpend { get; set; }

        // mapeo de la tabla User
        public int IdUser { get; set; }

        [ForeignKey("IdUser")]
        public virtual User User { get; set; }
    }



}
