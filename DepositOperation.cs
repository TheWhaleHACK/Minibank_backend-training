public class DepositOperation : BankOperation
{
    public DepositOperation(decimal amount):base(amount)
    {
    }
    public override string GetDescription()
    {
        return $"Пополнение: +{Amount} руб.";
    }
}