namespace LoanCalculator.Models
{
    public class RepaymentDetail
    {
        public int Month { get; set; }
        public decimal Principal { get; set; }
        public decimal Interest { get; set; }
        public decimal TotalPayment => Principal + Interest;
        public decimal RemainingBalance { get; set; }
    }
}
