namespace boardgames_sharp.Entity.Modifiers;

public interface IEntityQuery
{
    public bool select_entity(IEntityReadOnly entity);
}

public delegate bool DEntityQuery(IEntityReadOnly entity);
public class EntityQuery(DEntityQuery query) : IEntityQuery
{
    public bool select_entity(IEntityReadOnly entity)
    {
        return query.Invoke(entity);
    }
}