namespace boardgames_sharp.Entity;

public interface IEntityReadOnly
{
    public EntityId Id { get; }
    StateEntity get_state();
    IReadOnlyPropertiesOfType<T> get_readonly_properties_of_type<T>();
    IReadOnlyPropertiesOfSetOfType<T> get_readonly_properties_of_set<T>();
}
public class Entity(EntityId id): IEntityReadOnly
{
    public EntityId Id { get; } =  id;
    private const int ZeroValueInt = 0;
    private readonly PropertiesOfType<int> _intProperties =  new PropertiesOfType<int>(ZeroValueInt);
    private readonly PropertiesOfSetOfType<EntityId> _setOfEntitiesProperties =  new PropertiesOfSetOfType<EntityId>();

    public IReadOnlyPropertiesOfType<T> get_readonly_properties_of_type<T>()
    {
        var properties = GetPropertiesOfType<T>();
        return properties as IReadOnlyPropertiesOfType<T>;
    }

    public IReadOnlyPropertiesOfSetOfType<T> get_readonly_properties_of_set<T>()
    {
        var properties = GetPropertiesOfSetOfType<T>();
        return properties as IReadOnlyPropertiesOfSetOfType<T>;

    }

    public PropertiesOfSetOfType<T> GetPropertiesOfSetOfType<T>()
    {
        if (typeof(T) == typeof(EntityId))
        {
            return this._setOfEntitiesProperties as PropertiesOfSetOfType<T> ?? throw new InvalidOperationException();
        }
        throw new ArgumentException("Type not supported");
        
    }

    public PropertiesOfType<T> GetPropertiesOfType<T>()
    {
        if (typeof(T) == typeof(int))
        {
            return this._intProperties as PropertiesOfType<T> ?? throw new InvalidOperationException();
        }
        throw new ArgumentException("Type not supported");
    }

    public StateEntity get_state()
    {
        var stateInt = _intProperties.get_state();
        var stateEntity = new StateEntity(entityId:Id, intProperties:stateInt);
        return stateEntity;

    }

    public Property<T> add_property<T>(PropertyId<T> propertyId)
    {
        var properties = GetPropertiesOfType<T>();
        return properties.Add(propertyId);
    }
    public PropertyOfSet<T> add_property_of_set<T>(PropertyId<ISet<T>> propertyId)
    {
        var properties = GetPropertiesOfSetOfType<T>();
        return properties.Add(propertyId);
    }
}

public class StateEntity(EntityId entityId, StatePropertiesOfType<int> intProperties)
{
    public EntityId EntityId { get; } = entityId;
    public StatePropertiesOfType<int> IntProperties { get; } = intProperties;
}