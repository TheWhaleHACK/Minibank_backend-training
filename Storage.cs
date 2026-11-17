public class Storage<T>
{
    private T Value;

    public Storage(T value)
    {
        Value = value;
    }
    public T GetValue()
    {
        return Value;
    }
     public void SetValue(T newValue)
    {
        Value = newValue;
    }
}