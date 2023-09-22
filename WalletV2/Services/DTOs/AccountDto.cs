namespace WalletV2.Services.DTOs;

public class AccountDto
{
    public int AccountTypeId { get; private set; }
    public string UserName { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public DateTime Dob { get; private set; }
    private AccountDto(string userName, string fullName, string email, DateTime dob, int typeId)
    {
        AccountTypeId = typeId;
        UserName = userName;
        FullName = fullName;
        Email = email;
        Dob = dob;
    }

    public static AccountDto Create(string userName, string fullName, string email, DateTime dob, int accountTypeId)
    {
        return new AccountDto(userName, fullName, email, dob, accountTypeId);
    }
}