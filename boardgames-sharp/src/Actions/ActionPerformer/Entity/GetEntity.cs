using boardgames_sharp.Entity;

namespace boardgames_sharp.Actions.ActionPerformer.Entity;

internal sealed partial class EntityActionPerformer: IEntityActionPerformer, IInitializeWithEngineRoot
{
    public IEntityReadOnly get_entity(EntityId id)
    {
        if (_entityManager == null)
        {
            throw new BadRootInitializationException("_entityManager was not initialized");
        }

        return _entityManager.get_by_id(id);
    }
}