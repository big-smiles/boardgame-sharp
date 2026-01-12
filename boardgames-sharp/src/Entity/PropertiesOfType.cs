namespace boardgames_sharp.Entity;

public record struct PropertyId<T>(ulong Id)
{
    public readonly ulong Id = Id;
}

public interface IReadOnlyPropertiesOfType<T>
{
    IPropertyReadOnly<T> get_read_only(PropertyId<T> propertyId);
    bool Contains(PropertyId<T> propertyId);
    StatePropertiesOfType<T> get_state();
}
public class PropertiesOfType<T>(T zeroValue): IReadOnlyPropertiesOfType<T>
{
    private readonly Dictionary<PropertyId<T>, Property<T>> _properties = new();

    public Property<T> Add(PropertyId<T> propertyId) => Add(propertyId, zeroValue);
   
    public Property<T> Add(PropertyId<T> propertyId, T value)
    {
        var property = new Property<T>(value);
        _properties.Add(propertyId, property);
        return property;
    }
    public IPropertyReadOnly<T> get_read_only(PropertyId<T> propertyId)
    {
        var property = this.Get(propertyId);
        return property as IPropertyReadOnly<T>;
    }

    public bool Contains(PropertyId<T> propertyId)
    {
        return _properties.ContainsKey(propertyId);
    }

    public Property<T> Get(PropertyId<T> propertyId)
    {
        if (_properties.TryGetValue(propertyId, out var property))
        {
            return property;
        }
        throw new KeyNotFoundException();
    }

    public StatePropertiesOfType<T> get_state()
    {
        var states = new List<Tuple<PropertyId<T>, T>>();
        foreach (var kvp in _properties)
        {
            var value = kvp.Value.CurrentValue();
            states.Add(new Tuple<PropertyId<T>, T>(kvp.Key, value));
        }
        return new StatePropertiesOfType<T>(states);
    }
}

public class StatePropertiesOfType<T>(List<Tuple<PropertyId<T>, T>> properties)
{
    public List<Tuple<PropertyId<T>, T>> Properties { get; } = properties;
    
    public T Get(PropertyId<T> propertyId)
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

    public bool TryAndGet(PropertyId<T> propertyId, out T? value)
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
