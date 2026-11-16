public class WithdrawOperation : BankOperation
{
    public WithdrawOperation(decimal amount):base(amount)
    {
    }
    public override string GetDescription()
    {
        return $"Снятие: -{Amount} руб.";
    }
}