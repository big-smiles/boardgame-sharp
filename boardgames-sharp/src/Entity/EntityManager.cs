namespace boardgames_sharp.Entity;

public interface IEntityManager
{
    Entity get_by_id(ulong entityId);
    Entity create_empty_entity();
    void remove_entity(ulong entityId);
    StateEntities get_state();
}
public class EntityManager: IEntityManager, IInitializeWithEngineRoot
{
    public void Initialize(EngineRoot engineRoot)
    {
        
    }
    public Entity get_by_id(ulong entityId)
    {
        if (!_entities.TryGetValue(entityId, out var entity))
        {
          throw new KeyNotFoundException($"Entity with id {entityId} does not exist");
        }

        return entity;
    }

    public Entity create_empty_entity()
    {
        var entityId = NextId;
        var entity = new Entity(entityId);
        _entities.Add(entityId, entity);
        return entity;
        
    }

    public void remove_entity(ulong entityId)
    {
        _entities.Remove(entityId);
    }


    private readonly Dictionary<ulong, Entity> _entities = new Dictionary<ulong, Entity>();

    private ulong NextId
    {
        get => field++;
    } = 1;

    public StateEntities get_state()
    {
        List<Tuple<ulong, StateEntity>> states = new();
        foreach (var kvp in _entities)
        {
            var state = kvp.Value.GetState();
            states.Add(new Tuple<ulong, StateEntity>(kvp.Key, state));
        }
        return new StateEntities(states);
    }
}


public class StateEntities(List<Tuple<ulong, StateEntity>> entities)
{
    public List<Tuple<ulong,StateEntity>> Entities { get; } = entities;
}