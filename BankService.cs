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

    public decimal GetTotalDeposits()
    {
        // определяет что именно депозит операция, прошли на 2 неделе
        return history
            .Where(operation => operation is DepositOperation)
            .Sum(t => t.Amount);
    }

    public decimal GetTotalWithdraws()
    {
        // определяет что именно депозит операция, прошли на 2 неделе
        return history
            .Where(operation => operation is WithdrawOperation)
            .Sum(t => t.Amount);
    }

    public decimal GetBalanceChange()
    {
        // Можно по модулю, если не надо знать: ушли ли мы в минус например за месяц или нет
        return GetTotalDeposits() - GetTotalWithdraws();
    }

    public decimal GetAverageDeposit()
    {
        // Среднее арифметическое число пополнения
        var deposits = history
            .Where(operation => operation is DepositOperation);

        if (!deposits.Any())
            return 0m;

        return deposits.Average(operation => operation.Amount);
    }
}

