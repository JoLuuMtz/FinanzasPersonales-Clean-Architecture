
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;


namespace FinanzasPersonales.Domain
{
    public class BudgetCategory
    {
        [Key]
        public int IdBudgetCategory { get; set; }

        [Required]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }

        [Column(TypeName = "date")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Date { get; set; }     // fecha de creacion

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }    // monto del presupuesto
        // Relacion 1-* con Budget
        public int IdBudget { get; set; }
        [ForeignKey("IdBudget")]
        public virtual Budget Budget { get; set; }


    }
}
