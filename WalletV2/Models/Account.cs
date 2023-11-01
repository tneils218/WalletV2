namespace WalletV2.Models;

public class Account
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime Dob { get; set; }

    public bool Status { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public AccountType? AccountType { get; set; }

    public int AccountTypeId { get; set; }

    public DateTime? ActivatedAt { get; set; }

    public Account()
    {

    }
    public Account(string userName, string fullName, string email, DateTime dob, int typeId)
    {
        UserName = userName;
        FullName = fullName;
        Email = email;
        Dob = dob;
        AccountTypeId = typeId;
        Status = false;
    }

    public void Active()
    {
        Status = true;
        ActivatedAt = DateTime.Now;
    }
}
