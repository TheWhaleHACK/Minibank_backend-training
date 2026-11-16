public class MultiNotification : INotification
{
    private readonly INotification[] notifications;
    public MultiNotification(params INotification[] notifications) // чтобы передавать несколько аргументов как эл-ты одного массива
    {
        this.notifications = notifications;
    }
    public void Send(string message)
    {
        foreach (INotification item in notifications)
        {
            // Здесь можно работать с item
            item.Send(message);
        }
    }
}