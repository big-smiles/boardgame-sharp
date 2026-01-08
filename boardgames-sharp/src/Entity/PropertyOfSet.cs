namespace boardgames_sharp.Entity;

public interface IPropertyOfSetReadOnly<T>
{
    IReadOnlySet<T> CurrentValue();
}
public class PropertyOfSet<T>():IPropertyOfSetReadOnly<T>
{
    private readonly ISet<T> _set = new HashSet<T>();

    public IReadOnlySet<T> CurrentValue()
    {
        return _set.AsReadOnly();
    }

    public void AddValue(T value)
    {
        _set.Add(value);
    }

    public void RemoveValue(T value)
    {
        _set.Remove(value);
    } 
}
