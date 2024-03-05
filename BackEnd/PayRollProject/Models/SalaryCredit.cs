﻿namespace PayRollProject.Models
{
    public class SalaryCredit
    {
        public string UserName { get; set; }
        public DateTime CreditDate { get; set; }
        public Decimal CreditAmount { get; set; }
        public Decimal TaxCut { get; set; }
        public string TransactionId { get; set; }

    }
}
