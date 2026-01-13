namespace boardgames_sharp.Entity;

public interface IPropertyOfDictionaryReadOnly<TK,T> where TK: notnull
{
    IReadOnlyDictionary<TK,T> CurrentValue();
}
public class PropertyOfDictionary<K,T>():IPropertyOfDictionaryReadOnly<K,T> where K : notnull
{
    private readonly IDictionary<K,T> _dict = new Dictionary<K, T>();

    public IReadOnlyDictionary<K,T> CurrentValue()
    {
        return _dict.AsReadOnly();
    }

    public void AddElement(K key, T element)
    {
        _dict.Add(key, element);
    }

    public void RemoveElement(K key)
    {
        _dict.Remove(key);
    } 
}
