
BankAccount account = new BankAccount("Alex", 5000);
decimal remainingLimit = 10000m;

// ===== ВЫБОР УВЕДОМЛЕНИЙ =====

Console.WriteLine("=== MiniBank ===");
Console.WriteLine("Выберите способ уведомления:");
Console.WriteLine("1. SMS");
Console.WriteLine("2. Email");
Console.WriteLine("3. Оба");

INotification notification;

while (true)
{
    Console.Write("Ваш выбор: ");

    if (!int.TryParse(Console.ReadLine(), out int n))
    {
        Console.WriteLine("Введите число!");
        continue;
    }

    switch (n)
    {
        case 1:
            notification = new SmsNotification();
            break;

        case 2:
            notification = new EmailNotification();
            break;

        case 3:
            notification = new MultiNotification(
                new SmsNotification(),
                new EmailNotification()
            );
            break;

        default:
            Console.WriteLine("Неверный пункт. Попробуйте ещё раз.");
            continue;
    }

    break;
}

// Создаём банковский сервис
BankService service = new BankService(account, notification);

// ===== ОСНОВНОЕ МЕНЮ =====

while (true)
{
    Console.WriteLine("\n=== MiniBank ===");
    Console.WriteLine("1. Пополнить счёт");
    Console.WriteLine("2. Снять деньги");
    Console.WriteLine("3. История операций");
    Console.WriteLine("4. Посмотреть баланс");
    Console.WriteLine("0. Выход");

    Console.Write("Ваш выбор: ");

    if (!int.TryParse(Console.ReadLine(), out int n))
    {
        Console.WriteLine("Введите число!");
        continue;
    }

    switch (n)
    {
        case 1:
            while (true)
            {
                Console.Write("Введите сумму: ");

                if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
                {
                    Console.WriteLine("Введите число!");
                    continue;
                }

                service.Deposit(amount);
                break;
            }
            break;

        case 2:
            while (true)
            {
                Console.Write("Введите сумму: ");

                if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
                {
                    Console.WriteLine("Введите число!");
                    continue;
                }

                service.Withdraw(amount, ref remainingLimit, out _);
                break;
            }
            break;

        case 3:
            service.PrintHistory();
            break;

        case 4:
            Console.WriteLine($"\nТекущий баланс: {account.Balance}");
            Console.WriteLine($"Оставшийся лимит снятия: {remainingLimit}");
            break;

        case 0:
            Console.WriteLine("До свидания!");
            return;

        default:
            Console.WriteLine("Неверный пункт. Попробуйте ещё раз.");
            break;
    }

    // Здесь нет break!
    // Цикл повторится и покажет меню снова.
}
