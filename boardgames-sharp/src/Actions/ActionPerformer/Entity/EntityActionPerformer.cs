using boardgames_sharp.Entity;
using boardgames_sharp.Entity.Modifiers;

namespace boardgames_sharp.Actions.ActionPerformer.Entity;

public interface IEntityActionPerformer
{
    boardgames_sharp.Entity.Entity create_entity();
    HashSet<EntityId> query_entity_ids(IEntityQuery query);
    IEntityReadOnly get_entity(EntityId id);
    void add_modifier<T>(EntityId entityId, PropertyId<T> propertyId, IPropertyModifier<T> propertyModifier);
}
internal sealed partial class EntityActionPerformer: IEntityActionPerformer, IInitializeWithEngineRoot
{
    public void initialize(EngineRoot engineRoot)
    {
        _entityManager = engineRoot.EntityManager;
    }
    
    private IEntityManager? _entityManager;
}