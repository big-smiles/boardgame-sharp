using boardgames_sharp.Entity;

namespace boardgames_sharp.Actions.ActionPerformer.Entity;

internal sealed partial class EntityActionPerformer: IEntityActionPerformer
{
    public IPropertyOfSetReadOnly<T> add_property_of_set<T>(EntityId entityId, PropertyId<ISet<T>> propertyId)
    {
        if (_entityManager == null)
        {
            throw new BadRootInitializationException("_entityManager was not initialized");
        }

        var entity = _entityManager.get_by_id(entityId);
        return entity.add_property_of_set(propertyId);
    }
}