namespace boardgames_sharp.Entity;

public interface IReadOnlyPropertiesOfList<T>
{
    IPropertyOfListReadOnly<T> get_read_only(PropertyId<IList<T>> propertyId);
    bool Contains(PropertyId<IList<T>> propertyId);
    StatePropertiesOfList<T> get_state();
}
public class PropertiesOfList<T>(): IReadOnlyPropertiesOfList<T>
{
    private readonly Dictionary<PropertyId<IList<T>>, PropertyOfList<T>> _properties = new();

    public PropertyOfList<T> Add(PropertyId<IList<T>> propertyId)
    {
        var property = new PropertyOfList<T>();
        _properties.Add(propertyId, property);
        return property;
    }

    public IPropertyOfListReadOnly<T> get_read_only(PropertyId<IList<T>> propertyId)
    {
        var property = this.Get(propertyId);
        return property as IPropertyOfListReadOnly<T>;
    }

    public bool Contains(PropertyId<IList<T>> propertyId)
    {
        return _properties.ContainsKey(propertyId);
    }

    public PropertyOfList<T> Get(PropertyId<IList<T>> propertyId)
    {
        if (_properties.TryGetValue(propertyId, out var property))
        {
            return property;
        }
        throw new KeyNotFoundException();
    }

    public StatePropertiesOfList<T> get_state()
    {
        var states = new List<Tuple<PropertyId<IList<T>>, IReadOnlyList<T>>>();
        foreach (var kvp in _properties)
        {
            var value = kvp.Value.CurrentValue();
            states.Add(new Tuple<PropertyId<IList<T>>, IReadOnlyList<T>>(kvp.Key, value));
        }
        return new StatePropertiesOfList<T>(states);
    }
}


public class StatePropertiesOfList<T>(List<Tuple<PropertyId<IList<T>>, IReadOnlyList<T>>> properties)
{
    public List<Tuple<PropertyId<IList<T>>, IReadOnlyList<T>>> Properties { get; } = properties;
    
    public IReadOnlyList<T> Get(PropertyId<IList<T>> propertyId)
    {
        foreach (var tuple in Properties)
        {
            if (tuple.Item1 != propertyId)
            {
                continue;
            }
            return tuple.Item2;
        }
        throw new KeyNotFoundException();
    }
    public bool TryAndGet(PropertyId<IList<T>> propertyId, out IReadOnlyList<T>? value)
    {
        {
            foreach (var tuple in Properties)
            {
                if (tuple.Item1 != propertyId)
                {
                    continue;
                }
                value = tuple.Item2;
                return true;
            }
            value = default;
            return false;
        }
    }
}