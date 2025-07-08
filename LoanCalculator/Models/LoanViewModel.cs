namespace LoanCalculator.Models
{
    public class LoanViewModel
    {
        // User input
        public decimal LoanAmount { get; set; }
        public int LoanTermMonths { get; set; }
        public double AnnualInterestRate { get; set; }
        public string RepaymentType { get; set; } // "EqualPrincipal" or "Annuity"

        // Output
        public List<RepaymentDetail>? RepaymentSchedule { get; set; }
    }

}
