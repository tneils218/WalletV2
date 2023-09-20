namespace WalletV2.Models;

public class ActionFee
{
    public int Id { get; set; }

    public int AccountTypeId { get; set; }
    public int ActionTypeId { get; set; }

    public AccountType? AccountType { get; set; }

    public ActionType? ActionType { get; set; }
    public decimal Fee { get; set; }
    public ActionFee(int id, int accountTypeId, int actionTypeId, decimal fee)
    {
        Id = id;
        AccountTypeId = accountTypeId;
        ActionTypeId = actionTypeId;
        Fee = fee;
    }
}