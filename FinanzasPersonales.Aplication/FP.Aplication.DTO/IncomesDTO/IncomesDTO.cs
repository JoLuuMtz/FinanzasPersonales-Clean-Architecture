﻿namespace FinanzasPersonales.Aplication
{
    public class IncomesDTO
    {
        public int? IdIncomes { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int IdTypeIncomes { get; set; }
        public int IdUser { get; set; }
    }
}
