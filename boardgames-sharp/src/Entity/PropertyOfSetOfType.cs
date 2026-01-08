namespace boardgames_sharp.Entity;

public interface IReadOnlyPropertiesOfSetOfType<T>
{
    IPropertyOfSetReadOnly<T> get_read_only(PropertyId<ISet<T>> propertyId);
    bool Contains(PropertyId<ISet<T>> propertyId);
    StatePropertiesOfSetOfType<T> get_state();
}
public class PropertiesOfSetOfType<T>(): IReadOnlyPropertiesOfSetOfType<T>
{
    private readonly Dictionary<PropertyId<ISet<T>>, PropertyOfSet<T>> _properties = new();

    public PropertyOfSet<T> Add(PropertyId<ISet<T>> propertyId)
    {
        var property = new PropertyOfSet<T>();
        _properties.Add(propertyId, property);
        return property;
    }

    public IPropertyOfSetReadOnly<T> get_read_only(PropertyId<ISet<T>> propertyId)
    {
        var property = this.Get(propertyId);
        return property as IPropertyOfSetReadOnly<T>;
    }

    public bool Contains(PropertyId<ISet<T>> propertyId)
    {
        return _properties.ContainsKey(propertyId);
    }

    public PropertyOfSet<T> Get(PropertyId<ISet<T>> propertyId)
    {
        if (_properties.TryGetValue(propertyId, out var property))
        {
            return property;
        }
        throw new KeyNotFoundException();
    }

    public StatePropertiesOfSetOfType<T> get_state()
    {
        var states = new List<Tuple<PropertyId<ISet<T>>, IReadOnlySet<T>>>();
        foreach (var kvp in _properties)
        {
            var value = kvp.Value.CurrentValue();
            states.Add(new Tuple<PropertyId<ISet<T>>, IReadOnlySet<T>>(kvp.Key, value));
        }
        return new StatePropertiesOfSetOfType<T>(states);
    }
}


public class StatePropertiesOfSetOfType<T>(List<Tuple<PropertyId<ISet<T>>, IReadOnlySet<T>>> properties)
{
    public List<Tuple<PropertyId<ISet<T>>, IReadOnlySet<T>>> Properties { get; } = properties;
    
    public IReadOnlySet<T> Get(PropertyId<ISet<T>> propertyId)
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
}