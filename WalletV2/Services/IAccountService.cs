using WalletV2.Models;
using WalletV2.Services.DTOs;

namespace WalletV2.Services;

public interface IAccountService
{
    Task<List<Account>> GetAccounts();
    Task<Account> CreateAccount(AccountDto account);

    Task<Account> ActiveStatus(int id);
}