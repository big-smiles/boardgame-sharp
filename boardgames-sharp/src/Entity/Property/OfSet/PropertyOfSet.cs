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

    public void AddElement(T element)
    {
        _set.Add(element);
    }

    public void RemoveElement(T element)
    {
        _set.Remove(element);
    } 
}
