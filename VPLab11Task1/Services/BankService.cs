using Microsoft.Data.SqlClient;
using System.Data;
using VPLab11Task1.Models;

namespace VPLab11Task1.Services
{
    public class BankService : IBankService
    {
        private readonly string _connectionString;

        public BankService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException(nameof(configuration), "Connection string is missing.");
        }

        public async Task<List<Account>> GetAccountsAsync()
        {
            var accounts = new List<Account>();
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new SqlCommand("SELECT AccountId, AccountHolder, Balance FROM Accounts", connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                accounts.Add(new Account
                {
                    AccountId = reader.GetInt32("AccountId"),
                    AccountHolder = reader.GetString("AccountHolder"),
                    Balance = reader.GetDecimal("Balance")
                });
            }

            return accounts;
        }

        public async Task<List<Transaction>> GetTransactionsAsync()
        {
            var transactions = new List<Transaction>();
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new SqlCommand(
                @"SELECT t.TransactionId, t.FromAccountId, t.ToAccountId, t.Amount, t.TransferDate,
                     f.AccountHolder AS FromAccountHolder, t2.AccountHolder AS ToAccountHolder
              FROM Transactions t
              JOIN Accounts f ON t.FromAccountId = f.AccountId
              JOIN Accounts t2 ON t.ToAccountId = t2.AccountId
              ORDER BY t.TransferDate DESC", connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                transactions.Add(new Transaction
                {
                    TransactionId = reader.GetInt32("TransactionId"),
                    FromAccountId = reader.GetInt32("FromAccountId"),
                    ToAccountId = reader.GetInt32("ToAccountId"),
                    FromAccountHolder = reader.GetString("FromAccountHolder"),
                    ToAccountHolder = reader.GetString("ToAccountHolder"),
                    Amount = reader.GetDecimal("Amount"),
                    TransferDate = reader.GetDateTime("TransferDate")
                });
            }

            return transactions;
        }

        public async Task TransferFundsAsync(TransferModel model)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new SqlCommand("TransferFunds", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@FromAccountId", model.FromAccountId);
            command.Parameters.AddWithValue("@ToAccountId", model.ToAccountId);
            command.Parameters.AddWithValue("@Amount", model.Amount);

            await command.ExecuteNonQueryAsync();
        }
    }
}
