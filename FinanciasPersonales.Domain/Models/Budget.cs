using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FinanzasPersonales.Domain
{
    public class Budget
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdBudget { get; set; }
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalBudget { get; set; }    // monto del presupuesto

        [Column(TypeName = "date")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Date { get; set; }     // fecha de creacion
        public ICollection<BudgetCategory> BudgetLists { get; set; } = new List<BudgetCategory>();


        // mapeo de la tabla User   relacion de uno a muchos
        public int IdUser { get; set; }

        [ForeignKey("IdUser")]
        public virtual User User { get; set; }





    }


}
