public class BankAccount
{
    public string Owner { get; set; }
    public decimal Balance { get; private set; }

    public BankAccount(string name, decimal balance)
    {
        Owner = name;
        Balance = balance >= 0 ? balance : 0;
    }

    public bool Deposit(decimal amount)
    {
        //Balance += amount > 0 ? amount : 0;
        if (amount > 0)
        {
            Balance += amount;
            return true;
        }
        else return false;
    }
    public bool Withdraw(decimal amount)
    {
        //Balance += amount > 0 ? amount : 0;
        if (amount > 0 && amount <= Balance)
        {
            Balance -= amount;
            return true;
        }
        else return false;
    }
}