
BankAccount account = new BankAccount("Alex", 5000);
decimal remainingLimit = 10000m;

// Storage<decimal> money = new Storage<decimal>(5000m);

// Storage<string> name = new Storage<string>("Alex");
// int[] numbers = { 10, 20, 30, 40 };
// string[] names = { "Alex", "David", "Maria" };

// Utility utility = new Utility();

// Console.WriteLine(utility.GetLast(numbers));
// Console.WriteLine(utility.GetLast(names));

// money.SetValue(10000);
// name.SetValue("David");

// Console.WriteLine(money.GetValue());
// Console.WriteLine(name.GetValue());

Dictionary<int, decimal> MyDatabaseBankAccounts = new();
MyDatabaseBankAccounts.Add(101, 5000);
MyDatabaseBankAccounts.Add(102, 12000);
MyDatabaseBankAccounts.Add(103, 7500);

if (MyDatabaseBankAccounts.TryGetValue(102, out decimal count))
{
    Console.WriteLine(count);
}
else
{
    Console.WriteLine("Пользователь не найден");
}

MyDatabaseBankAccounts[102] = 15000;

if (MyDatabaseBankAccounts.TryGetValue(102, out decimal countt))
{
    Console.WriteLine(countt);
}
else
{
    Console.WriteLine("Пользователь не найден");
}

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
