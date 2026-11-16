public class BankService
{
    private readonly BankAccount account;
    private readonly INotification notification;

    private readonly List<BankOperation> history = new List<BankOperation>();

    public BankService(BankAccount account, INotification notification)
    {
        this.account = account;
        this.notification = notification;
    }
    public bool Deposit(decimal amount)
    {
        bool result = account.Deposit(amount);

        if (result)
        {
            history.Add(new DepositOperation(amount));

            notification.Send(
                $"Счёт пополнен на {amount} руб. " +
                $"Текущий баланс: {account.Balance} руб."
            );
        }
        else
        {
            notification.Send(
                $"Ошибка пополнения! Некорректная сумма: {amount} руб."
            );
        }

        return result;
    }

    public bool Withdraw(
        decimal amount,
        ref decimal remainingLimit,
        out string error)
    {
        if (amount <= 0)
        {
            error = "Некорректная сумма";
            notification.Send($"Ошибка снятия: {error}");
            return false;
        }

        if (amount > remainingLimit)
        {
            error = "Превышен лимит снятия";
            notification.Send($"Ошибка снятия: {error}");
            return false;
        }

        bool result = account.Withdraw(amount);

        if (!result)
        {
            error = "Недостаточно денег на счёте";
            notification.Send($"Ошибка снятия: {error}");
            return false;
        }

        remainingLimit -= amount;
        history.Add(new WithdrawOperation(amount));

        error = "";

        notification.Send(
            $"Со счёта снято {amount} руб. " +
            $"Текущий баланс: {account.Balance} руб."
        );

        return true;
    }

    public void PrintHistory()
    {
        foreach (BankOperation operation in history)
        {
            Console.WriteLine(operation.GetDescription());
        }
    }
}

