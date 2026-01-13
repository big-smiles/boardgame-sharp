using boardgames_sharp.Actions.ActionPerformer.Entity.PropertiesOfSet;
using boardgames_sharp.Entity;
using boardgames_sharp.Entity.Modifiers;

namespace boardgames_sharp.Actions.ActionPerformer.Entity;

public interface IEntityActionPerformer
{
    IEntityReadOnly create_entity();
    IPropertyReadOnly<T> add_property<T>(EntityId id, PropertyId<T> propertyId);
    IPropertyReadOnly<T> add_property<T>(EntityId id, PropertyId<T> propertyId, T value);
    IPropertyOfSetReadOnly<T> add_property_of_set<T>(EntityId id, PropertyId<ISet<T>> propertyId);
    IPropertyOfDictionaryReadOnly<KT,T> add_property_of_dictionary<KT,T>(EntityId id, PropertyId<IDictionary<KT,T>> propertyId) where KT : notnull;
    HashSet<EntityId> query_entity_ids(IEntityQuery query);
    IEntityReadOnly get_entity(EntityId id);
    void add_modifier<T>(EntityId entityId, PropertyId<T> propertyId, IPropertyModifier<T> propertyModifier);
    void add_modifier_set_value<T>(EntityId entityId, PropertyId<T> propertyId, T value);
    void save_entity(EntityId id, int key);
    EntityId get_saved_entity(int key);
    
    IEntityPropertiesOfSetActionPerformer PropertiesOfSet { get; }
    IEntityPropertiesOfDicitonaryActionPerformer PropertiesOfDicitonary { get; }
}
internal sealed partial class EntityActionPerformer: IEntityActionPerformer, IInitializeWithEngineRoot
{
    public void initialize(EngineRoot engineRoot)
    {
        _entityManager = engineRoot.EntityManager;
        _propertiesOfSet.initialize(engineRoot);
        _propertiesOfDicitonary.initialize(engineRoot);
    }
    
    private IEntityManager? _entityManager;
    private readonly EntityPropertiesOfSetActionPerformer _propertiesOfSet = new EntityPropertiesOfSetActionPerformer();
    public IEntityPropertiesOfSetActionPerformer PropertiesOfSet => _propertiesOfSet;
    private readonly EntityPropertiesOfDicitonaryActionPerformer _propertiesOfDicitonary = new();
    public IEntityPropertiesOfDicitonaryActionPerformer PropertiesOfDicitonary => _propertiesOfDicitonary;
}