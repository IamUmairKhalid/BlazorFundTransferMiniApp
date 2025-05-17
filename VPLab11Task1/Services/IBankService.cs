using VPLab11Task1.Models;

namespace VPLab11Task1.Services
{
    public interface IBankService
    {
        Task<List<Account>> GetAccountsAsync();
        Task<List<Transaction>> GetTransactionsAsync();
        Task TransferFundsAsync(TransferModel model);
    }
}
