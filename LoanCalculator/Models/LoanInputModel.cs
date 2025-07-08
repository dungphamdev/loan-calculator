namespace LoanCalculator.Models
{
    public class LoanInputModel
    {
        public decimal LoanAmount { get; set; }
        public int LoanTermMonths { get; set; }
        public double AnnualInterestRate { get; set; }

        public string RepaymentType { get; set; } // "EqualPrincipal", "Annuity"
    }
}
