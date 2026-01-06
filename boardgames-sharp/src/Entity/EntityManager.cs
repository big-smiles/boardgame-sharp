using boardgames_sharp.Entity.Modifiers;

namespace boardgames_sharp.Entity;

public interface IEntityManager
{
    Entity get_by_id(EntityId entityId);
    Entity create_empty_entity();
    void remove_entity(EntityId entityId);
    StateEntities get_state();
    bool entities_exist(HashSet<EntityId> entityIds);
    HashSet<EntityId> query_entity_ids(IEntityQuery query);

}
internal sealed class EntityManager: IEntityManager, IInitializeWithEngineRoot
{
    public void initialize(EngineRoot engineRoot)
    {
        
    }
    public Entity get_by_id(EntityId entityId)
    {
        if (!_entities.TryGetValue(entityId, out var entity))
        {
          throw new KeyNotFoundException($"Entity with id {entityId} does not exist");
        }

        return entity;
    }

    public Entity create_empty_entity()
    {
        var entityId = new EntityId(NextId);
        var entity = new Entity(entityId);
        _entities.Add(entityId, entity);
        _entityIds.Add(entityId);
        return entity;
        
    }

    public void remove_entity(EntityId entityId)
    {
        _entities.Remove(entityId);
        _entityIds.Remove(entityId);
    }
    
    public StateEntities get_state()
    {
        List<Tuple<EntityId, StateEntity>> states = new();
        foreach (var kvp in _entities)
        {
            var state = kvp.Value.get_state();
            states.Add(new Tuple<EntityId, StateEntity>(kvp.Key, state));
        }
        return new StateEntities(states);
    }

    public bool entities_exist(HashSet<EntityId> entityIds)
    {
        return _entityIds.IsSupersetOf(entityIds);
    }

    public HashSet<EntityId> query_entity_ids(IEntityQuery query)
    {
      
        var entityIds = new HashSet<EntityId>();
        
        foreach (var kvp in _entities)
        {
            var entityId = kvp.Key;
            var entity = kvp.Value;
            if (query.select_entity(entity))
            {
                entityIds.Add(entityId);
            }
        }
        return entityIds;
    }

    private readonly Dictionary<EntityId, Entity> _entities = new Dictionary<EntityId, Entity>();
    private readonly HashSet<EntityId> _entityIds = new();
    private ulong NextId
    {
        get => field++;
    } = 1;

}


public class StateEntities(List<Tuple<EntityId, StateEntity>> entities)
{
    public List<Tuple<EntityId,StateEntity>> Entities { get; } = entities;
}

public readonly record struct EntityId(ulong Id)
{
    public ulong Id { get; } = Id;
}