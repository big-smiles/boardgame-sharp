using boardgames_sharp.Entity;

namespace boardgames_sharp.Actions.ActionPerformer.Entity.PropertiesOfSet;
public interface IEntityPropertiesOfSetActionPerformer
{
    public void add_element<T>(EntityId entityId,  PropertyId<ISet<T>> propertyId, T element);
    public void remove_element<T>(EntityId entityId,  PropertyId<ISet<T>> propertyId, T element);
}
public class EntityPropertiesOfSetActionPerformer:IEntityPropertiesOfSetActionPerformer, IInitializeWithEngineRoot
{
    public void add_element<T>(EntityId entityId, PropertyId<ISet<T>> propertyId, T element)
    {
        if (_entityManager == null)
        {
            throw new BadRootInitializationException("_entityManager was not initialized");
        }
        var entity = _entityManager.get_by_id(entityId);
        var properties = entity.GetPropertiesOfSetOfType<T>();
        var property = properties.Get(propertyId);
        property.AddElement(element);
    }

    public void remove_element<T>(EntityId entityId, PropertyId<ISet<T>> propertyId, T element)
    {
        if (_entityManager == null)
        {
            throw new BadRootInitializationException("_entityManager was not initialized");
        }
        var entity = _entityManager.get_by_id(entityId);
        var properties = entity.GetPropertiesOfSetOfType<T>();
        var property = properties.Get(propertyId);
        property.RemoveElement(element);
    }

    public void initialize(EngineRoot engineRoot)
    {
        _entityManager = engineRoot.EntityManager;
    }

    private IEntityManager? _entityManager;
}