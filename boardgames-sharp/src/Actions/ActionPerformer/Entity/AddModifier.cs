using boardgames_sharp.Entity;

namespace boardgames_sharp.Actions.ActionPerformer.Entity;

internal sealed partial class EntityActionPerformer: IEntityActionPerformer
{
    public void add_modifier<T>(EntityId entityId, PropertyId<T> propertyId, IPropertyModifier<T> propertyModifier)
    {
        if (_entityManager == null)
        {
            throw new BadRootInitializationException("_entityManager was not initialized");
        }
        var entity = _entityManager.get_by_id(entityId);
        var propertiesOfType = entity.GetPropertiesOfType<T>();
        var property =  propertiesOfType.Get(propertyId);
        property.AddModifier(propertyModifier);
    }
}