namespace VPLab11Task1.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string AccountHolder { get; set; } = string.Empty;
        public decimal Balance { get; set; }
    }
}
