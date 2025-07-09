//using LoanCalculator.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace LoanCalculator.Controllers
//{
//    using Microsoft.AspNetCore.Mvc;
//    using LoanCalculator.Models;

//    public class LoanController : Controller
//    {
//        [HttpGet]
//        public IActionResult Loan()
//        {
//            return View(new LoanViewModel());
//        }

//        [HttpPost]
//        public IActionResult Loan(LoanViewModel model)
//        {
//            model.RepaymentSchedule = CalculateSchedule(model);
//            return View(model);
//        }

//        private List<RepaymentDetail> CalculateSchedule(LoanViewModel model)
//        {
//            var schedule = new List<RepaymentDetail>();
//            decimal balance = model.LoanAmount;
//            double monthlyRate = model.AnnualInterestRate / 12 / 100;
//            int term = model.LoanTermMonths;

//            if (model.RepaymentType == "EqualPrincipal")
//            {
//                decimal monthlyPrincipal = balance / term;
//                for (int i = 1; i <= term; i++)
//                {
//                    decimal interest = balance * (decimal)monthlyRate;
//                    schedule.Add(new RepaymentDetail
//                    {
//                        Month = i,
//                        Principal = monthlyPrincipal,
//                        Interest = interest,
//                        RemainingBalance = balance - monthlyPrincipal
//                    });
//                    balance -= monthlyPrincipal;
//                }
//            }
//            else if (model.RepaymentType == "Annuity")
//            {
//                double r = monthlyRate;
//                double annuity = (double)balance * r * Math.Pow(1 + r, term) / (Math.Pow(1 + r, term) - 1);
//                for (int i = 1; i <= term; i++)
//                {
//                    decimal interest = balance * (decimal)r;
//                    decimal principal = (decimal)annuity - interest;
//                    schedule.Add(new RepaymentDetail
//                    {
//                        Month = i,
//                        Principal = principal,
//                        Interest = interest,
//                        RemainingBalance = balance - principal
//                    });
//                    balance -= principal;
//                }
//            }

//            return schedule;
//        }
//    }

//}

using Microsoft.AspNetCore.Mvc;
using LoanCalculator.Models;
using System;
using System.Collections.Generic;

namespace LoanCalculator.Controllers
{
    public class LoanController : Controller
    {
        [HttpGet]
        public IActionResult Loan()
        {
            return View(new LoanViewModel
            {
                StartDate = DateTime.Today
            });
        }

        [HttpPost]
        public IActionResult Loan(LoanViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.RepaymentSchedule = CalculateSchedule(model);
            }

            return View(model);
        }

        private List<RepaymentDetail> CalculateSchedule(LoanViewModel model)
        {
            var schedule = new List<RepaymentDetail>();
            decimal balance = model.LoanAmount;
            double monthlyRate = model.AnnualInterestRate / 12 / 100;
            int term = model.LoanTermMonths;
            DateTime startDate = model.StartDate ?? DateTime.Today;

            if (model.RepaymentType == "EqualPrincipal")
            {
                decimal monthlyPrincipal = Math.Round(balance / term, 0);
                for (int i = 0; i < term; i++)
                {
                    decimal interest = Math.Round(balance * (decimal)monthlyRate, 0);
                    decimal total = monthlyPrincipal + interest;

                    schedule.Add(new RepaymentDetail
                    {
                        Date = startDate.AddMonths(i),
                        Principal = monthlyPrincipal,
                        Interest = interest,
                        TotalPayment = total,
                        RemainingBalance = balance - monthlyPrincipal
                    });

                    balance -= monthlyPrincipal;
                }
            }
            else if (model.RepaymentType == "Annuity")
            {
                double r = monthlyRate;
                double annuity = (double)balance * r * Math.Pow(1 + r, term) / (Math.Pow(1 + r, term) - 1);
                for (int i = 0; i < term; i++)
                {
                    decimal interest = Math.Round(balance * (decimal)r, 0);
                    decimal principal = Math.Round((decimal)annuity - interest, 0);
                    decimal total = principal + interest;

                    schedule.Add(new RepaymentDetail
                    {
                        Date = startDate.AddMonths(i),
                        Principal = principal,
                        Interest = interest,
                        TotalPayment = total,
                        RemainingBalance = balance - principal
                    });

                    balance -= principal;
                }
            }

            return schedule;
        }
    }
}

