using LoanCalculator.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoanCalculator.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using LoanCalculator.Models;

    public class LoanController : Controller
    {
        [HttpGet]
        public IActionResult Loan()
        {
            return View(new LoanViewModel());
        }

        [HttpPost]
        public IActionResult Loan(LoanViewModel model)
        {
            model.RepaymentSchedule = CalculateSchedule(model);
            return View(model);
        }

        private List<RepaymentDetail> CalculateSchedule(LoanViewModel model)
        {
            var schedule = new List<RepaymentDetail>();
            decimal balance = model.LoanAmount;
            double monthlyRate = model.AnnualInterestRate / 12 / 100;
            int term = model.LoanTermMonths;

            if (model.RepaymentType == "EqualPrincipal")
            {
                decimal monthlyPrincipal = balance / term;
                for (int i = 1; i <= term; i++)
                {
                    decimal interest = balance * (decimal)monthlyRate;
                    schedule.Add(new RepaymentDetail
                    {
                        Month = i,
                        Principal = monthlyPrincipal,
                        Interest = interest,
                        RemainingBalance = balance - monthlyPrincipal
                    });
                    balance -= monthlyPrincipal;
                }
            }
            else if (model.RepaymentType == "Annuity")
            {
                double r = monthlyRate;
                double annuity = (double)balance * r * Math.Pow(1 + r, term) / (Math.Pow(1 + r, term) - 1);
                for (int i = 1; i <= term; i++)
                {
                    decimal interest = balance * (decimal)r;
                    decimal principal = (decimal)annuity - interest;
                    schedule.Add(new RepaymentDetail
                    {
                        Month = i,
                        Principal = principal,
                        Interest = interest,
                        RemainingBalance = balance - principal
                    });
                    balance -= principal;
                }
            }

            return schedule;
        }
    }

}
