using boardgames_sharp.Actions;

namespace boardgames_sharp.Entity;

public interface IEntityReadOnly
{
    public EntityId Id { get; }
    bool try_and_get_value_of_type<T>(PropertyId<T> propertyId, out T? value);
    bool try_and_get_value_of_set<T>(PropertyId<ISet<T>> propertyId, out IReadOnlySet<T>? value);
    bool try_and_get_value_of_dictionary<KT,T>(PropertyId<IDictionary<KT,T>> propertyId, out IReadOnlyDictionary<KT,T>? value) where KT: notnull;
    StateEntity get_state();
    IReadOnlyPropertiesOfType<T> get_readonly_properties_of_type<T>();
    IReadOnlyPropertiesOfSet<T> get_readonly_properties_of_set<T>();
    IReadOnlyPropertiesOfDictionary<KT,T> get_readonly_properties_of_dictionary<KT, T>() where KT: notnull;
    
    IReadOnlyPropertiesOfType<int> IntProperties { get; }
    IReadOnlyPropertiesOfType<IAction?> ActionProperties { get; }
    IReadOnlyPropertiesOfType<bool> BoolProperties { get; }
    IReadOnlyPropertiesOfType<float> FloatProperties { get; }
    IReadOnlyPropertiesOfType<string> StringProperties { get; }
    IReadOnlyPropertiesOfType<EntityId> EntityIdProperties { get; }
    IReadOnlyPropertiesOfSet<EntityId> SetOfEntityIdProperties { get; }
    IReadOnlyPropertiesOfDictionary<EntityId,int> DictOfEntityIdToIntProperties { get; }
    IReadOnlyPropertiesOfDictionary<EntityId,float> DictOfEntityIdToFloatProperties { get; }
        
    IReadOnlyPropertiesOfDictionary<Tuple<int, int>, EntityId> GridOfEntityIdProperties { get; }
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
    public IReadOnlyPropertiesOfType<int> IntProperties => _intProperties;
    
    private readonly PropertiesOfType<IAction?> _actionProperties =  new(ZeroValueAction);
    public IReadOnlyPropertiesOfType<IAction?> ActionProperties => _actionProperties;
    
    private readonly PropertiesOfSet<EntityId> _setOfEntitiesProperties =  new();
    public IReadOnlyPropertiesOfSet<EntityId> SetOfEntityIdProperties => _setOfEntitiesProperties;
    
    private readonly PropertiesOfDictionary<EntityId,int>  _dictOfEntityIdToIntProperties =  new();
    public IReadOnlyPropertiesOfDictionary<EntityId, int> DictOfEntityIdToIntProperties =>
        _dictOfEntityIdToIntProperties;
    
    private readonly PropertiesOfDictionary<EntityId,float>  _dictOfEntityIdToFloatProperties =  new();

    public IReadOnlyPropertiesOfDictionary<EntityId, float> DictOfEntityIdToFloatProperties =>
        _dictOfEntityIdToFloatProperties;

    private readonly PropertiesOfDictionary<Tuple<int, int>, EntityId> _gridOfEntityIdProperties =  new();
    public IReadOnlyPropertiesOfDictionary<Tuple<int, int>, EntityId> GridOfEntityIdProperties =>
        _gridOfEntityIdProperties;

    private readonly PropertiesOfType<bool> _boolProperties =  new(ZeroValueBool);
    public IReadOnlyPropertiesOfType<bool> BoolProperties => _boolProperties;
    
    private readonly PropertiesOfType<float> _floatProperties =  new(ZeroValueFloat);
    public IReadOnlyPropertiesOfType<float> FloatProperties => _floatProperties;
    
    private readonly PropertiesOfType<string> _stringProperties =  new(ZeroValueString);
    public IReadOnlyPropertiesOfType<string> StringProperties => _stringProperties;
    
    private readonly PropertiesOfType<EntityId> _entityId = new(EntityManager.ZeroValueEntityId);
    public IReadOnlyPropertiesOfType<EntityId> EntityIdProperties => _entityId;
    

    public IReadOnlyPropertiesOfType<T> get_readonly_properties_of_type<T>()
    {
        var properties = GetPropertiesOfType<T>();
        return properties as IReadOnlyPropertiesOfType<T>;
    }

    public IReadOnlyPropertiesOfSet<T> get_readonly_properties_of_set<T>()
    {
        var properties = GetPropertiesOfSetOfType<T>();
        return properties as IReadOnlyPropertiesOfSet<T>;

    }
    public IReadOnlyPropertiesOfDictionary<KT, T> get_readonly_properties_of_dictionary<KT, T>() where KT : notnull
    {
        var properties = GetPropertiesOfDictionary<KT, T>();
        return properties as IReadOnlyPropertiesOfDictionary<KT, T>;
    }

    public PropertiesOfSet<T> GetPropertiesOfSetOfType<T>()
    {
        if (typeof(T) == typeof(EntityId))
        {
            return this._setOfEntitiesProperties as PropertiesOfSet<T> ?? throw new InvalidOperationException();
        }
        throw new ArgumentException("Type not supported");
        
    }
    public PropertiesOfDictionary<KT,T> GetPropertiesOfDictionary<KT,T>() where KT : notnull
    {
        if (typeof(T) == typeof(EntityId) && typeof(KT) ==  typeof(Tuple<int,int>))
        {
            return _gridOfEntityIdProperties as PropertiesOfDictionary<KT,T> ?? throw new InvalidOperationException();
        }
        if (typeof(T) == typeof(int) && typeof(KT) ==  typeof(EntityId))
        {
            return _dictOfEntityIdToIntProperties as PropertiesOfDictionary<KT,T> ?? throw new InvalidOperationException();
        }
        if (typeof(T) == typeof(float) && typeof(KT) ==  typeof(EntityId))
        {
            return _dictOfEntityIdToFloatProperties as PropertiesOfDictionary<KT,T> ?? throw new InvalidOperationException();
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
        if (typeof(T) == typeof(IAction))
        {
            return this._actionProperties as PropertiesOfType<T> ?? throw new InvalidOperationException();
        }
        
        throw new ArgumentException("Type not supported" + typeof(T).ToString());
    }
    

    public bool try_and_get_value_of_type<T>(PropertyId<T> propertyId, out T? value)
    {
        var properties = get_readonly_properties_of_type<T>();
        var contains =  properties.Contains(propertyId);
        if (!contains)
        {
            value = default;
            return false;
        }
        var property = properties.get_read_only(propertyId);
        value = property.CurrentValue();
        return true;
    }

    public bool try_and_get_value_of_set<T>(PropertyId<ISet<T>> propertyId, out IReadOnlySet<T>? value)
    {
        var properties = get_readonly_properties_of_set<T>();
        var contains =  properties.Contains(propertyId);
        if (!contains)
        {
            value = default;
            return false;
        }
        var property = properties.get_read_only(propertyId);
        value = property.CurrentValue();
        return true;
    }

    public bool try_and_get_value_of_dictionary<KT, T>(PropertyId<IDictionary<KT, T>> propertyId, out IReadOnlyDictionary<KT, T>? value) where KT : notnull
    {
        var properties = get_readonly_properties_of_dictionary<KT,T>();
        var contains =  properties.Contains(propertyId);
        if (!contains)
        {
            value = default;
            return false;
        }
        var property = properties.get_read_only(propertyId);
        value = property.CurrentValue();
        return true;
        
    }

    public StateEntity get_state()
    {
        var stateInt = _intProperties.get_state();
        var stateString = _stringProperties.get_state();
        var stateSetsOfEntityId = _setOfEntitiesProperties.get_state(); 
        var dictOfEntityIdToInt =  _dictOfEntityIdToIntProperties.get_state();
        var dictOfEntityIdToFloat =  _dictOfEntityIdToFloatProperties.get_state();
        var stateEntity = new StateEntity(
            entityId:Id,
            intProperties:stateInt,
            stringProperties:stateString,
            setsOfEntityId:stateSetsOfEntityId,
            dictOfEntityIdToIntProperties: dictOfEntityIdToInt,
            dictOfEntityIdToFloatProperties: dictOfEntityIdToFloat
            );
        return stateEntity;

    }

    public PropertyOfType<T> add_property<T>(PropertyId<T> propertyId, T value)
    {
        var properties = GetPropertiesOfType<T>();
        return properties.Add(propertyId, value);
    }
    public PropertyOfType<T> add_property<T>(PropertyId<T> propertyId)
    {
        var properties = GetPropertiesOfType<T>();
        return properties.Add(propertyId);
    }
    public PropertyOfSet<T> add_property_of_set<T>(PropertyId<ISet<T>> propertyId)
    {
        var properties = GetPropertiesOfSetOfType<T>();
        return properties.Add(propertyId);
    }
    public PropertyOfDictionary<KT,T> add_property_of_set<KT,T>(PropertyId<IDictionary<KT,T>> propertyId) where KT : notnull
    {
        var properties = GetPropertiesOfDictionary<KT,T>();
        return properties.Add(propertyId);
    }
}

public class StateEntity(
    EntityId entityId,
    StatePropertiesOfType<int> intProperties,
    StatePropertiesOfType<string>  stringProperties,
    StatePropertiesOfSet<EntityId> setsOfEntityId,
    StatePropertiesOfDictionary<EntityId, int> dictOfEntityIdToIntProperties,
    StatePropertiesOfDictionary<EntityId, float> dictOfEntityIdToFloatProperties
)
{
    public EntityId EntityId { get; } = entityId;
    public StatePropertiesOfType<int> IntProperties { get; } = intProperties;
    public StatePropertiesOfType<string> StringProperties { get; } = stringProperties;
    public StatePropertiesOfSet<EntityId> SetsOfEntityId { get; } = setsOfEntityId;
    public StatePropertiesOfDictionary<EntityId,int> DictOfEntityIdToIntProperties { get; } = dictOfEntityIdToIntProperties;
    public StatePropertiesOfDictionary<EntityId,float> DictOfEntityIdToFloatProperties { get; } = dictOfEntityIdToFloatProperties;
}