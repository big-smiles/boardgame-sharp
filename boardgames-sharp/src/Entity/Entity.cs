namespace boardgames_sharp.Entity;

public class Entity(ulong entityId)
{
    public ulong EntityId { get; } =  entityId;
    private const int ZeroValueInt = 0;
    private readonly PropertiesOfType<int> _intProperties =  new PropertiesOfType<int>(ZeroValueInt);

  private PropertiesOfType<T> GetPropertiesOfType<T>()
    {
        if (typeof(T) == typeof(int))
        {
            return this._intProperties as PropertiesOfType<T> ?? throw new InvalidOperationException();
        }
        throw new ArgumentException("Type not supported");
    }

    public StateEntity GetState()
    {
        var stateInt = _intProperties.GetState();
        var stateEntity = new StateEntity(entityId:EntityId, intProperties:stateInt);
        return stateEntity;

    }
}

public class StateEntity(ulong entityId, StatePropertiesOfType<int> intProperties)
{
    public ulong EntityId { get; } = entityId;
    public StatePropertiesOfType<int> IntProperties { get; } = intProperties;
}