namespace boardgames_sharp.Entity;

public interface IReadOnlyPropertiesOfDictionary<TK,T> where TK: notnull
{
    IPropertyOfDictionaryReadOnly<TK,T> get_read_only(PropertyId<IDictionary<TK,T>> propertyId);
    bool TryAndGet(PropertyId<IDictionary<TK,T>> propertyId, out IReadOnlyDictionary<TK,T>? value);
    bool Contains(PropertyId<IDictionary<TK,T>> propertyId);
    StatePropertiesOfDictionary<TK, T> get_state();
}

public class PropertiesOfDictionary<TK, T>(): IReadOnlyPropertiesOfDictionary<TK, T> where TK : notnull
{
    private readonly Dictionary<PropertyId<IDictionary<TK,T>>, PropertyOfDictionary<TK,T>> _properties = new();

    public PropertyOfDictionary<TK,T> Add(PropertyId<IDictionary<TK,T>> propertyId)
    {
        var property = new PropertyOfDictionary<TK,T>();
        _properties.Add(propertyId, property);
        return property;
    }
    
    public IPropertyOfDictionaryReadOnly<TK, T> get_read_only(PropertyId<IDictionary<TK, T>> propertyId)
    {
        var property = this.Get(propertyId);
        return property as IPropertyOfDictionaryReadOnly<TK,T>;    }

    public bool TryAndGet(PropertyId<IDictionary<TK,T>> propertyId, out IReadOnlyDictionary<TK,T>? value)
    {
        if (_properties.TryGetValue(propertyId, out var property))
        {
             value = property.CurrentValue();
             return true;
        }
        value = default;
        return false;
    }

    public bool Contains(PropertyId<IDictionary<TK, T>> propertyId)
    {
        return _properties.ContainsKey(propertyId);
    }

    public PropertyOfDictionary<TK,T> Get(PropertyId<IDictionary<TK, T>> propertyId)
    {
        if (_properties.TryGetValue(propertyId, out var property))
        {
            return property;
        }
        throw new KeyNotFoundException();
    }
    

    public StatePropertiesOfDictionary<TK,T> get_state()
    {
        var states = new List<Tuple<PropertyId<IDictionary<TK,T>>, IReadOnlyDictionary<TK,T>>>();
        foreach (var kvp in _properties)
        {
            var value = kvp.Value.CurrentValue();
            states.Add(new Tuple<PropertyId<IDictionary<TK, T>>, IReadOnlyDictionary<TK,T>>(kvp.Key, value));
        }
        return new StatePropertiesOfDictionary<TK,T>(states);
    }

  
}


public class StatePropertiesOfDictionary<TK,T>(List<Tuple<PropertyId<IDictionary<TK,T>>, IReadOnlyDictionary<TK,T>>> properties) where TK: notnull
{
    public List<Tuple<PropertyId<IDictionary<TK,T>>, IReadOnlyDictionary<TK,T>>> Properties { get; } = properties;
    
    public IReadOnlyDictionary<TK,T> Get(PropertyId<IDictionary<TK,T>> propertyId)
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
    public bool TryAndGet(PropertyId<IDictionary<TK,T>> propertyId, out IReadOnlyDictionary<TK,T>? value)
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