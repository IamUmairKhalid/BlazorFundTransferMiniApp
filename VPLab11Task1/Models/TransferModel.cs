using System.ComponentModel.DataAnnotations;

namespace VPLab11Task1.Models
{
    public class TransferModel
    {
        [Required(ErrorMessage = "Select a sender account.")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid sender account.")]
        public int FromAccountId { get; set; }

        [Required(ErrorMessage = "Select a receiver account.")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid receiver account.")]
        public int ToAccountId { get; set; }

        [Required(ErrorMessage = "Enter an amount.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }
    }
}
