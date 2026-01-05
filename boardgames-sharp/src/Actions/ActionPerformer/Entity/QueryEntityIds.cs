using boardgames_sharp.Entity;
using boardgames_sharp.Entity.Modifiers;

namespace boardgames_sharp.Actions.ActionPerformer.Entity;

public partial class EntityActionPerformer: IInitializeWithEngineRoot, IEntityActionPerformer
{
    public HashSet<EntityId> query_entity_ids(IEntityQuery query)
    {
        if (_entityManager == null)
        {
            throw new BadRootInitializationException("Entity Manager not initialized");
        }
        return _entityManager.query_entity_ids(query);
    }
}