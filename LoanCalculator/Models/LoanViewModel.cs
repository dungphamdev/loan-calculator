using System.ComponentModel.DataAnnotations;

namespace LoanCalculator.Models
{
    public class LoanViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập số tiền vay")]
        [Range(1000000, 10000000000, ErrorMessage = "Số tiền vay phải từ 1 triệu đến 10 tỷ")]
        [Display(Name = "Số tiền vay")]
        public decimal LoanAmount { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thời gian vay")]
        [Range(1, 360, ErrorMessage = "Thời gian vay phải từ 1 đến 360 tháng")]
        [Display(Name = "Thời gian vay (tháng)")]
        public int LoanTermMonths { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập lãi suất")]
        [Range(0.1, 100, ErrorMessage = "Lãi suất phải từ 0.1% đến 100%")]
        [Display(Name = "Lãi suất (% mỗi năm)")]
        public double AnnualInterestRate { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn hình thức trả nợ")]
        [Display(Name = "Hình thức trả nợ")]
        public string RepaymentType { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày bắt đầu")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày bắt đầu thanh toán")]
        public DateTime? StartDate { get; set; }

        public List<RepaymentDetail>? RepaymentSchedule { get; set; }
    }

}
