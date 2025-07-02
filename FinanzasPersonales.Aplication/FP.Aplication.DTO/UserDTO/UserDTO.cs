using FinanzasPersonales.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinanzasPersonales.Aplication
{
    public class UserDTO
    {
        public int IdUser { get; set; }

        public string DNI { get; set; }
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string? ProfileImagen { get; set; }

        public DateTime DateRegister { get; set; }
        public decimal TotalMoney { get; set; }

        public ICollection<Spend> UserSpends { get; set; } = new List<Spend>();
        public ICollection<Income> UserIncomes { get; set; } = new List<Income>();
        public ICollection<Budget> UserBudgets { get; set; } = new List<Budget>();
    }
}
