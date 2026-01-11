using boardgames_sharp.Actions;

namespace boardgames_sharp.Entity;

public interface IEntityReadOnly
{
    public EntityId Id { get; }
    StateEntity get_state();
    IReadOnlyPropertiesOfType<T> get_readonly_properties_of_type<T>();
    IReadOnlyPropertiesOfSetOfType<T> get_readonly_properties_of_set_of_type<T>();
}
public class Entity(EntityId id): IEntityReadOnly
{
    public EntityId Id { get; } =  id;
    private const int ZeroValueInt = 0;
    private const bool ZeroValueBool = false;
    private const float ZeroValueFloat = 0;
    private const string ZeroValueString = "";
    private const IAction? ZeroValueAction = null;
    
    private readonly PropertiesOfType<int> _intProperties =  new PropertiesOfType<int>(ZeroValueInt);
    private readonly PropertiesOfType<bool> _boolProperties =  new PropertiesOfType<bool>(ZeroValueBool);
    private readonly PropertiesOfType<float> _floatProperties =  new PropertiesOfType<float>(ZeroValueFloat);
    private readonly PropertiesOfType<string> _stringProperties =  new PropertiesOfType<string>(ZeroValueString);
    private readonly PropertiesOfType<IAction?> _actionProperties =  new PropertiesOfType<IAction?>(ZeroValueAction);
    private readonly PropertiesOfType<EntityId> _entityId = new PropertiesOfType<EntityId>(EntityManager.ZeroValueEntityId);
    private readonly PropertiesOfSetOfType<EntityId> _setOfEntitiesProperties =  new PropertiesOfSetOfType<EntityId>();

    public IReadOnlyPropertiesOfType<T> get_readonly_properties_of_type<T>()
    {
        var properties = GetPropertiesOfType<T>();
        return properties as IReadOnlyPropertiesOfType<T>;
    }

    public IReadOnlyPropertiesOfSetOfType<T> get_readonly_properties_of_set_of_type<T>()
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
        if (typeof(T) == typeof(bool))
        {
            return this._boolProperties as PropertiesOfType<T> ?? throw new InvalidOperationException();
        }
        if (typeof(T) == typeof(float))
        {
            return this._floatProperties as PropertiesOfType<T> ?? throw new InvalidOperationException();
        }
        if (typeof(T) == typeof(string))
        {
            return this._stringProperties as PropertiesOfType<T> ?? throw new InvalidOperationException();
        }

        if (typeof(T) == typeof(EntityId))
        {
            return this._entityId as PropertiesOfType<T> ?? throw new InvalidOperationException();
        }
        
        throw new ArgumentException("Type not supported");
    }

    public StateEntity get_state()
    {
        var stateInt = _intProperties.get_state();
        var stateString = _stringProperties.get_state();
        var stateEntity = new StateEntity(entityId:Id, intProperties:stateInt, stringProperties:stateString);
        return stateEntity;

    }

    public Property<T> add_property<T>(PropertyId<T> propertyId, T value)
    {
        var properties = GetPropertiesOfType<T>();
        return properties.Add(propertyId, value);
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

public class StateEntity(EntityId entityId, StatePropertiesOfType<int> intProperties, StatePropertiesOfType<string>  stringProperties)
{
    public EntityId EntityId { get; } = entityId;
    public StatePropertiesOfType<int> IntProperties { get; } = intProperties;
    public StatePropertiesOfType<string> StringProperties { get; } = stringProperties;
}