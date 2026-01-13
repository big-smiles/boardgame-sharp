using boardgames_sharp.Entity;

namespace boardgames_sharp.Actions.ActionPerformer.Entity.PropertiesOfSet;
public interface IEntityPropertiesOfDicitonaryActionPerformer
{
    public void add_element<KT,T>(EntityId entityId,  PropertyId<IDictionary<KT,T>> propertyId, KT key,T element) where KT : notnull;
    public void remove_element<KT,T>(EntityId entityId,  PropertyId<IDictionary<KT,T>> propertyId, KT key) where KT : notnull;
}
public class EntityPropertiesOfDicitonaryActionPerformer:IEntityPropertiesOfDicitonaryActionPerformer, IInitializeWithEngineRoot
{
    public void add_element<KT,T>(EntityId entityId, PropertyId<IDictionary<KT,T>> propertyId, KT key, T element)
    {
        if (_entityManager == null)
        {
            throw new BadRootInitializationException("_entityManager was not initialized");
        }
        var entity = _entityManager.get_by_id(entityId);
        var properties = entity.GetPropertiesOfDictionary<KT,T>();
        var property = properties.Get(propertyId);
        property.AddElement(key, element);
    }

    public void remove_element<KT,T>(EntityId entityId, PropertyId<IDictionary<KT,T>> propertyId, KT key)
    {
        if (_entityManager == null)
        {
            throw new BadRootInitializationException("_entityManager was not initialized");
        }
        var entity = _entityManager.get_by_id(entityId);
        var properties = entity.GetPropertiesOfDictionary<KT,T>();
        var property = properties.Get(propertyId);
        property.RemoveElement(key);
    }

    public void initialize(EngineRoot engineRoot)
    {
        _entityManager = engineRoot.EntityManager;
    }

    private IEntityManager? _entityManager;
}