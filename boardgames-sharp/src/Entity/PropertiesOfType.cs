namespace boardgames_sharp.Entity;

public record struct PropertyId
{
    public ulong Id;
    public System.Type Type;
}

public class PropertiesOfType<T>(T zeroValue)
{
    private readonly Dictionary<PropertyId, Property<T>> _properties = new();

    public void Add(PropertyId propertyId)
    {
        var property = new Property<T>(zeroValue);
        _properties.Add(propertyId, property);
    }

    public Property<T> Get(PropertyId propertyId)
    {
        if (_properties.TryGetValue(propertyId, out var property))
        {
            return property;
        }
        throw new KeyNotFoundException();
    }

    public StatePropertiesOfType<T> GetState()
    {
        var states = new List<Tuple<PropertyId, T>>();
        foreach (var kvp in _properties)
        {
            var value = kvp.Value.CurrentValue();
            states.Add(new Tuple<PropertyId, T>(kvp.Key, value));
        }
        return new StatePropertiesOfType<T>(states);
    }
}

public class StatePropertiesOfType<T>(List<Tuple<PropertyId, T>> properties)
{
    private List<Tuple<PropertyId, T>> Properties { get; } = properties;
}