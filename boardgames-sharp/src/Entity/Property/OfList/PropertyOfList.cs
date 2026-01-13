namespace boardgames_sharp.Entity;

public interface IPropertyOfListReadOnly<T>
{
    IReadOnlyList<T> CurrentValue();
}
public class PropertyOfList<T>():IPropertyOfListReadOnly<T>
{
    private readonly List<T> _set = [];

    public IReadOnlyList<T> CurrentValue()
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
