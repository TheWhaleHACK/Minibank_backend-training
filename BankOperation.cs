public abstract class BankOperation
{
    public decimal Amount {get; private set;}
    public BankOperation(decimal amount)
    {
        Amount = amount;
    }

    public abstract string GetDescription();
}