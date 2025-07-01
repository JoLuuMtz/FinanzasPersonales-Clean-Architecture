using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanciasPersonalesApiRest.Models
{
    public class Income

    {
        [Key]
        public int IdIncome { get; set; }
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
        public DateTime DateIncome { get; set; }
        public int IdTypeIncome { get; set; }

        [ForeignKey("IdTypeIncome")]
        public virtual TypeIncomes TypeIncome { get; set; }

        public int IdUser { get; set; }

        [ForeignKey("IdUser")]
        public virtual User User { get; set; }

    }



}
