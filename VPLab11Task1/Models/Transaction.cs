namespace VPLab11Task1.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public string FromAccountHolder { get; set; } = string.Empty;
        public string ToAccountHolder { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime TransferDate { get; set; }
    }
}
