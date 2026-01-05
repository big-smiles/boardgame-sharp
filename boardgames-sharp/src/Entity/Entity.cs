namespace boardgames_sharp.Entity;

public interface IEntityReadOnly
{
    public EntityId Id { get; }
    StateEntity get_state();
    IReadOnlyPropertiesOfType<T> get_readonly_properties_of_type<T>();
}
public class Entity(EntityId id): IEntityReadOnly
{
    public EntityId Id { get; } =  id;
    private const int ZeroValueInt = 0;
    private readonly PropertiesOfType<int> _intProperties =  new PropertiesOfType<int>(ZeroValueInt);

    public IReadOnlyPropertiesOfType<T> get_readonly_properties_of_type<T>()
    {
        var properties = GetPropertiesOfType<T>();
        return properties as IReadOnlyPropertiesOfType<T>;
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
}

public class StateEntity(EntityId entityId, StatePropertiesOfType<int> intProperties)
{
    public EntityId EntityId { get; } = entityId;
    public StatePropertiesOfType<int> IntProperties { get; } = intProperties;
}