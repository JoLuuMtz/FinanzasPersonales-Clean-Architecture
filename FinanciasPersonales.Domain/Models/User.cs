
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanzasPersonales.Domain

{
    public class User
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUser { get; set; }

        [Required]
        public string DNI { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

  
        [StringLength(50)]
        public string Phone { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        // suma o resta automaticamente dependiendo de la transaccion
        public decimal TotalMoney { get; set; } 

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [StringLength(1000)]
        public string? ProfileImagen { get; set; }     // url imagen

        [Column(TypeName = "date")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)] // valor registrado automaticamente por la DB
        public DateTime DateRegister { get; set; }

        // Gatos, Prespuestos, Ingresos de un usuario
        public ICollection<Spend> UserSpends { get; set; } = new List<Spend>();
        public ICollection<Income> UserIncomes { get; set; } = new List<Income>();
        public ICollection<Budget> UserBudgets { get; set; } = new List<Budget>();




    }

}
