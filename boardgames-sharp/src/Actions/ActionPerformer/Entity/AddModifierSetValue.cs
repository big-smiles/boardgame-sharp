using boardgames_sharp.Entity;
using boardgames_sharp.Entity.Modifiers;

namespace boardgames_sharp.Actions.ActionPerformer.Entity;

internal sealed partial class EntityActionPerformer: IEntityActionPerformer
{
    public void add_modifier_set_value<T>(EntityId entityId, PropertyId<T> propertyId, T value)
    {
        if (_entityManager == null)
        {
            throw new BadRootInitializationException("_entityManager was not initialized");
        }
        var entity = _entityManager.get_by_id(entityId);
        var propertiesOfType = entity.GetPropertiesOfType<T>();
        var property =  propertiesOfType.Get(propertyId);
        var modifier = new ModifierSetValue<T>(value);
        property.AddModifier(modifier);    }
}